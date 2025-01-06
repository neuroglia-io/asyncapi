// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Neuroglia.AsyncApi.Client.Bindings.Amqp;

/// <summary>
/// Represents the default Amqp implementation of the <see cref="IBindingHandler"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="options">The service used to access the current <see cref="AmqpBindingHandlerOptions"/></param>
/// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
public class AmqpBindingHandler(IServiceProvider serviceProvider, ILogger<AmqpBindingHandler> logger, IOptions<AmqpBindingHandlerOptions> options, ISerializerProvider serializerProvider)
    : IBindingHandler<AmqpBindingHandlerOptions>
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; } = logger;

    /// <summary>
    /// Gets the current <see cref="AmqpBindingHandlerOptions"/>
    /// </summary>
    protected AmqpBindingHandlerOptions Options { get; } = options.Value;

    /// <summary>
    /// Gets the service used to provide <see cref="ISerializer"/>s
    /// </summary>
    protected ISerializerProvider SerializerProvider { get; } = serializerProvider;

    /// <inheritdoc/>
    public virtual bool Supports(string protocol, string? protocolVersion) => protocol.Equals(AsyncApiProtocol.Amqp, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public virtual async Task<IAsyncApiPublishOperationResult> PublishAsync(AsyncApiPublishOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            var channelBinding = context.ChannelBinding as AmqpChannelBindingDefinition;
            var operationBinding = context.OperationBinding as AmqpOperationBindingDefinition;
            var messageBinding = context.MessageBinding as AmqpMessageBindingDefinition;
            var channelType = channelBinding?.Type ?? AmqpChannelType.Queue;
            var virtualHost = channelBinding?.Type switch
            {
                AmqpChannelType.Queue => channelBinding?.Queue?.VirtualHost,
                AmqpChannelType.RoutingKey => channelBinding?.Exchange?.VirtualHost,
                 _ => null
            };
            var hostComponents = context.Host.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var host = hostComponents[0];
            var port = 5672;
            if (hostComponents.Length > 1) port = int.Parse(hostComponents[1]);
            var connectionFactory = new ConnectionFactory
            {
                HostName = host,
                Port = port
            };
            if(!string.IsNullOrWhiteSpace(virtualHost)) connectionFactory.VirtualHost = virtualHost;
            using var connection = await connectionFactory.CreateConnectionAsync(cancellationToken).ConfigureAwait(false);
            using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            try
            {
                switch (channelType)
                {
                    case AmqpChannelType.Queue:
                        var queueDeclareResult = await channel.QueueDeclareAsync(channelBinding?.Queue?.Name ?? string.Empty, channelBinding?.Queue?.Durable ?? false, channelBinding?.Queue?.Exclusive ?? false, channelBinding?.Queue?.AutoDelete ?? false, cancellationToken: cancellationToken).ConfigureAwait(false);
                        break;
                    case AmqpChannelType.RoutingKey:
                        await channel.ExchangeDeclareAsync(channelBinding?.Exchange?.Name ?? string.Empty, EnumHelper.Stringify(channelBinding?.Exchange?.Type ?? AmqpExchangeType.Default), channelBinding?.Exchange?.Durable ?? false, channelBinding?.Exchange?.AutoDelete ?? false, cancellationToken: cancellationToken).ConfigureAwait(false);
                        break;
                    default:
                        throw new NotSupportedException($"The specified AMQP channel type '{channelType}' is not supported");
                }
            }
            catch { }
            var exchangeName = channelBinding?.Exchange?.Name ?? string.Empty;
            var serializer = SerializerProvider.GetSerializersFor(context.ContentType).FirstOrDefault() ?? throw new NullReferenceException($"Failed to find a serializer for the specified content type '{context.ContentType}'");
            using var stream = new MemoryStream();
            serializer.Serialize(context.Payload ?? new { }, stream);
            await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
            stream.Position = 0;
            var deliveryMode = operationBinding?.DeliveryMode switch
            {
                AmqpDeliveryMode.Transient => (DeliveryModes?)DeliveryModes.Transient,
                AmqpDeliveryMode.Persistent => DeliveryModes.Persistent,
                _ => null
            };
            var properties = new BasicProperties()
            {
                ContentType = context.ContentType,
                UserId = operationBinding?.UserId,
                Priority = operationBinding?.Priority ?? 0,
                ReplyTo = operationBinding?.ReplyTo,
                Headers = context.Headers.ToDictionary()!
            };
            if(deliveryMode.HasValue) properties.DeliveryMode = deliveryMode.Value;
            var payload = stream.ToArray();
            await channel.BasicPublishAsync(exchangeName, context.Channel!, operationBinding?.Mandatory ?? false, properties, payload, cancellationToken).ConfigureAwait(false);
            return new AmqpPublishOperationResult();
        }
        catch (Exception ex)
        {
            return new AmqpPublishOperationResult()
            {
                Exception = ex
            };
        }
    }

    /// <inheritdoc/>
    public virtual async Task<IAsyncApiSubscribeOperationResult> SubscribeAsync(AsyncApiSubscribeOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            var channelBinding = context.ChannelBinding as AmqpChannelBindingDefinition;
            var operationBinding = context.OperationBinding as AmqpOperationBindingDefinition;
            var channelType = channelBinding?.Type ?? AmqpChannelType.Queue;
            var virtualHost = channelBinding?.Type switch
            {
                AmqpChannelType.Queue => channelBinding?.Queue?.VirtualHost,
                AmqpChannelType.RoutingKey => channelBinding?.Exchange?.VirtualHost,
                _ => null
            };
            var hostComponents = context.Host.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var host = hostComponents[0];
            var port = 5672;
            if (hostComponents.Length > 1) port = int.Parse(hostComponents[1]);
            var connectionFactory = new ConnectionFactory
            {
                HostName = host,
                Port = port
            };
            if (!string.IsNullOrWhiteSpace(virtualHost)) connectionFactory.VirtualHost = virtualHost;
            var connection = await connectionFactory.CreateConnectionAsync(cancellationToken).ConfigureAwait(false);
            var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            var queueName = channelBinding?.Queue?.Name ?? context.Channel ?? string.Empty;
            var exchangeName = channelBinding?.Exchange?.Name ?? string.Empty;
            try
            {
                switch (channelType)
                {
                    case AmqpChannelType.Queue:
                        var queueDeclareResult = await channel.QueueDeclareAsync(queueName, channelBinding?.Queue?.Durable ?? false, channelBinding?.Queue?.Exclusive ?? false, channelBinding?.Queue?.AutoDelete ?? false, cancellationToken: cancellationToken).ConfigureAwait(false);
                        break;
                    case AmqpChannelType.RoutingKey:
                        await channel.ExchangeDeclareAsync(exchangeName, EnumHelper.Stringify(channelBinding?.Exchange?.Type ?? AmqpExchangeType.Default), channelBinding?.Exchange?.Durable ?? false, channelBinding?.Exchange?.AutoDelete ?? false, cancellationToken: cancellationToken).ConfigureAwait(false);
                        break;
                    default:
                        throw new NotSupportedException($"The specified AMQP channel type '{channelType}' is not supported");
                }
            }
            catch { }
            var consumer = new AsyncEventingBasicConsumer(channel);
            var subscription = ActivatorUtilities.CreateInstance<AmqpSubscription>(ServiceProvider, context.Document, context.Messages, context.DefaultContentType, connection, channel, consumer);
            await channel.BasicConsumeAsync(queueName, !(operationBinding?.Ack ?? false), consumer, cancellationToken).ConfigureAwait(false);
            return new AmqpSubscribeOperationResult(subscription);
        }
        catch (Exception ex)
        {
            return new AmqpSubscribeOperationResult()
            {
                Exception = ex
            };
        }
    }

}
