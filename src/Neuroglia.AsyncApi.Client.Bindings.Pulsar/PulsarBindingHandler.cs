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

using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reactive.Linq;
using System.Threading;

namespace Neuroglia.AsyncApi.Client.Bindings.Pulsar;

/// <summary>
/// Represents the default Pulsar implementation of the <see cref="IBindingHandler"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="options">The service used to access the current <see cref="PulsarBindingHandlerOptions"/></param>
/// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
public class PulsarBindingHandler(IServiceProvider serviceProvider, ILogger<PulsarBindingHandler> logger, IOptions<PulsarBindingHandlerOptions> options, ISerializerProvider serializerProvider)
    : IBindingHandler<PulsarBindingHandlerOptions>
{

    /// <summary>
    /// Gets the default name for Pulsar subscriptions
    /// </summary>
    public const string DefaultSubscriptionName = "async-api-client";

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; } = logger;

    /// <summary>
    /// Gets the current <see cref="PulsarBindingHandlerOptions"/>
    /// </summary>
    protected PulsarBindingHandlerOptions Options { get; } = options.Value;

    /// <summary>
    /// Gets the service used to provide <see cref="ISerializer"/>s
    /// </summary>
    protected ISerializerProvider SerializerProvider { get; } = serializerProvider;

    /// <inheritdoc/>
    public virtual bool Supports(string protocol, string? protocolVersion) => protocol.Equals(AsyncApiProtocol.Pulsar, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public virtual async Task<IAsyncApiPublishOperationResult> PublishAsync(AsyncApiPublishOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            var serverBinding = context.ServerBinding as PulsarServerBindingDefinition;
            var channelBinding = context.ChannelBinding as PulsarChannelBindingDefinition;
            var clientBuilder = PulsarClient.Builder().ServiceUrl(new($"{context.Host}{context.Path}")).ExceptionHandler(OnClientException);
            var client = clientBuilder.Build();
            var producerBuilder = client.NewProducer().Topic(context.Channel!);
            var producer = producerBuilder.Create();
            var messageBuilder = producer.NewMessage();
            var serializer = SerializerProvider.GetSerializersFor(context.ContentType).FirstOrDefault() ?? throw new NullReferenceException($"Failed to find a serializer for the specified content type '{context.ContentType}'");
            var payload = serializer.SerializeToByteArray(context.Payload)!;
            if (context.Headers != null) foreach (var header in context.Headers.ToDictionary()!) messageBuilder.Property(header.Key, header.Value.ToString()!);
            var messageId = await messageBuilder.Send(payload, cancellationToken).ConfigureAwait(false);
            return new PulsarPublishOperationResult(messageId);
        }
        catch (Exception ex)
        {
            return new PulsarPublishOperationResult() { Exception = ex };
        }
    }

    /// <inheritdoc/>
    public virtual async Task<IAsyncApiSubscribeOperationResult> SubscribeAsync(AsyncApiSubscribeOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            var serverBinding = context.ServerBinding as PulsarServerBindingDefinition;
            var channelBinding = context.ChannelBinding as PulsarChannelBindingDefinition;
            var clientBuilder = PulsarClient.Builder().ServiceUrl(new($"{context.Host}{context.Path}")).ExceptionHandler(OnClientException);
            var client = clientBuilder.Build();
            var consumerBuilder = client.NewConsumer().Topic(context.Channel!).SubscriptionName(DefaultSubscriptionName);
            var consumer = consumerBuilder.Create();
            await consumer.OnStateChangeTo(ConsumerState.Active, TimeSpan.FromSeconds(3), cancellationToken).ConfigureAwait(false);
            var subscription = ActivatorUtilities.CreateInstance<PulsarSubscription>(ServiceProvider, context.Document, context.Messages, consumer, client, context.DefaultContentType);
            return new PulsarSubscribeOperationResult(subscription);
        }
        catch (Exception ex)
        {
            return new PulsarSubscribeOperationResult() { Exception = ex };
        }
    }

    /// <summary>
    /// Handles <see cref="Exception"/>s raised by <see cref="IPulsarClient"/>s
    /// </summary>
    /// <param name="context">The <see cref="ExceptionContext"/> to handle</param>
    protected virtual void OnClientException(ExceptionContext context)
    {
        if (context.Exception is not TaskCanceledException) throw context.Exception;
    }

}
