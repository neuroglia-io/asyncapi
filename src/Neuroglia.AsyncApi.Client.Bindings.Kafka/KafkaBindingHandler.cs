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

using Confluent.Kafka;
using Json.Schema;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Neuroglia.AsyncApi.Client.Bindings.Kafka;

/// <summary>
/// Represents the default NATS implementation of the <see cref="IBindingHandler"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="options">The service used to access the current <see cref="KafkaBindingHandlerOptions"/></param>
/// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
/// <param name="jsonSerializer">The service used to serialize/deserialize data to/from JSON</param>
public class KafkaBindingHandler(IServiceProvider serviceProvider, ILogger<KafkaBindingHandler> logger, IOptions<KafkaBindingHandlerOptions> options, ISerializerProvider serializerProvider, IJsonSerializer jsonSerializer)
    : IBindingHandler<KafkaBindingHandlerOptions>
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
    /// Gets the current <see cref="KafkaBindingHandlerOptions"/>
    /// </summary>
    protected KafkaBindingHandlerOptions Options { get; } = options.Value;

    /// <summary>
    /// Gets the service used to provide <see cref="ISerializer"/>s
    /// </summary>
    protected ISerializerProvider SerializerProvider { get; } = serializerProvider;

    /// <summary>
    /// Gets the service used to serialize/deserialize data to/from JSON
    /// </summary>
    protected IJsonSerializer JsonSerializer { get; } = jsonSerializer;

    /// <inheritdoc/>
    public virtual bool Supports(string protocol, string? protocolVersion) => protocol.Equals(AsyncApiProtocol.Kafka, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public virtual async Task<IAsyncApiPublishOperationResult> PublishAsync(AsyncApiPublishOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            var serverBinding = context.ServerBinding as KafkaServerBindingDefinition;
            var channelBinding = context.ChannelBinding as KafkaChannelBindingDefinition;
            var operationBinding = context.OperationBinding as KafkaOperationBindingDefinition;
            var messageBinding = context.MessageBinding as KafkaMessageBindingDefinition;
            var producerConfig = new ProducerConfig 
            { 
                BootstrapServers = $"{context.Host}{context.Path}",
                AllowAutoCreateTopics = true
            };
            using var producer = new ProducerBuilder<Null, byte[]>(producerConfig).Build();
            using var stream = new MemoryStream();
            var serializer = SerializerProvider.GetSerializersFor(context.ContentType).FirstOrDefault() ?? throw new NullReferenceException($"Failed to find a serializer for the specified content type '{context.ContentType}'");
            serializer.Serialize(context.Payload ?? new { }, stream);
            await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
            stream.Position = 0;
            var payload = stream.ToArray();
            var headers = context.Headers == null ? null : new Headers();
            if (headers != null && context.Headers != null) foreach (var header in context.Headers.ToDictionary()!) headers.Add(header.Key, serializer.SerializeToByteArray(header.Value));
            var message = new Message<Null, byte[]>()
            {
                Value = payload,
                Headers = headers,
            };
            var topic = channelBinding?.Topic ?? context.Channel!;
            var result = await producer.ProduceAsync(topic, message, cancellationToken).ConfigureAwait(false);
            return new KafkaPublishOperationResult()
            {
                PersistenceStatus = result.Status,
                Partition = result.Partition,
                Offset = result.Offset,
                TopicPartition = result.TopicPartition,
                TopicPartitionOffset = result.TopicPartitionOffset
            };
        }
        catch (Exception ex)
        {
            return new KafkaPublishOperationResult() { Exception = ex };
        }
    }

    /// <inheritdoc/>
    public virtual Task<IAsyncApiSubscribeOperationResult> SubscribeAsync(AsyncApiSubscribeOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            var serverBinding = context.ServerBinding as KafkaServerBindingDefinition;
            var channelBinding = context.ChannelBinding as KafkaChannelBindingDefinition;
            var operationBinding = context.OperationBinding as KafkaOperationBindingDefinition;
            var groupId = operationBinding?.GroupId?.GetKeyword<DefaultKeyword>()?.Value ?? operationBinding?.GroupId?.GetKeyword<EnumKeyword>()?.Values?[0];
            var clientId = operationBinding?.ClientId?.GetKeyword<DefaultKeyword>()?.Value ?? operationBinding?.ClientId?.GetKeyword<EnumKeyword>()?.Values?[0];
            var topic = channelBinding?.Topic ?? context.Channel!;
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = $"{context.Host}{context.Path}",
                GroupId = groupId == null ? null : JsonSerializer.Deserialize<string>(groupId),
                ClientId = clientId == null ? null : JsonSerializer.Deserialize<string>(clientId)
            };
            var consumer = new ConsumerBuilder<Ignore, byte[]>(consumerConfig).Build();
            consumer.Subscribe(topic);
            var subscription = ActivatorUtilities.CreateInstance<KafkaSubscription>(ServiceProvider, context.Document, context.Messages, context.DefaultContentType, consumer);
            return Task.FromResult<IAsyncApiSubscribeOperationResult>(new KafkaSubscribeOperationResult(subscription));
        }
        catch(Exception ex)
        {
            return Task.FromResult<IAsyncApiSubscribeOperationResult>(new KafkaSubscribeOperationResult() { Exception = ex });
        }
    }

}