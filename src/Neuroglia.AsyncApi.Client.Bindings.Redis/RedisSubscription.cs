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

using Neuroglia.AsyncApi.v3;
using StackExchange.Redis;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Neuroglia.AsyncApi.Client.Bindings.Redis;

/// <summary>
/// Represents a subscription to a Redis channel, used to stream <see cref="IAsyncApiMessage"/>s
/// </summary>
/// <param name="logger">The service used to perform logging</param>
/// <param name="connection">The <see cref="IConnectionMultiplexer"/> used to interact with the Redis server</param>
/// <param name="channel">The <see cref="RedisChannel"/> to subscribe to</param>
/// <param name="messageContentType">The content type of consumed messages</param>
/// <param name="runtimeExpressionEvaluator">The service used to evaluate runtime expressions</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/>s</param>
/// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
/// <param name="document">The <see cref="V3AsyncApiDocument"/> that defines the operation for which to consume MQTT messages</param>
/// <param name="messageDefinitions">An <see cref="IEnumerable{T}"/> containing the definitions of all messages that can potentially be consumed</param>
public class RedisSubscription(ILogger<RedisSubscription> logger, IConnectionMultiplexer connection, RedisChannel channel, string messageContentType, IRuntimeExpressionEvaluator runtimeExpressionEvaluator, ISchemaHandlerProvider schemaHandlerProvider, ISerializerProvider serializerProvider, V3AsyncApiDocument document, IEnumerable<V3MessageDefinition> messageDefinitions)
        : IObservable<IAsyncApiMessage>, IDisposable, IAsyncDisposable
{

    bool _disposed;

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; } = logger;

    /// <summary>
    /// Gets the <see cref="IConnectionMultiplexer"/> used to interact with the Redis server
    /// </summary>
    protected IConnectionMultiplexer Connection { get; } = connection;

    /// <summary>
    /// Gets the <see cref="RedisChannel"/> to subscribe to
    /// </summary>
    protected RedisChannel Channel { get; } = channel;

    /// <summary>
    /// Gets the content type of consumed messages
    /// </summary>
    protected string MessageContentType { get; } = messageContentType;

    /// <summary>
    /// Gets the service used to evaluate runtime expressions
    /// </summary>
    protected IRuntimeExpressionEvaluator RuntimeExpressionEvaluator { get; } = runtimeExpressionEvaluator;

    /// <summary>
    /// Gets the service used to provide <see cref="ISchemaHandler"/>s
    /// </summary>
    protected ISchemaHandlerProvider SchemaHandlerProvider { get; } = schemaHandlerProvider;

    /// <summary>
    /// Gets the service used to provide <see cref="ISerializer"/>s
    /// </summary>
    protected ISerializerProvider SerializerProvider { get; } = serializerProvider;

    /// <summary>
    /// Gets the <see cref="V3AsyncApiDocument"/> that defines the operation for which to consume MQTT messages
    /// </summary>
    protected V3AsyncApiDocument Document { get; } = document;

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the definitions of all messages that can potentially be consumed
    /// </summary>
    protected IEnumerable<V3MessageDefinition> MessageDefinitions { get; } = messageDefinitions;

    /// <summary>
    /// Gets the <see cref="RedisSubscription"/>'s <see cref="System.Threading.CancellationTokenSource"/>
    /// </summary>
    protected CancellationTokenSource CancellationTokenSource { get; } = new();

    /// <summary>
    /// Gets the <see cref="Subject{T}"/> used to observe consumed <see cref="IAsyncApiMessage"/>
    /// </summary>
    protected Subject<IAsyncApiMessage> Subject { get; } = new();

    /// <inheritdoc/>
    public virtual IDisposable Subscribe(IObserver<IAsyncApiMessage> observer) => Subject.Subscribe(observer);

    /// <summary>
    /// Starts the <see cref="RedisSubscription"/>
    /// </summary>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    public virtual async Task StartAsync()
    {
        var subscriber = Connection.GetSubscriber();
        await subscriber.SubscribeAsync(Channel, OnMessageReceivedAsync).ConfigureAwait(false);
    }

    /// <summary>
    /// Handles the consumption of a Redis message
    /// </summary>
    /// <param name="channel">The channel that has received the message</param>
    /// <param name="value">The value of the message that has been received</param>
    protected virtual async void OnMessageReceivedAsync(RedisChannel channel, RedisValue value)
    {
        try
        {
            var buffer = (byte[]?)value;
            if (buffer == null) return;
            using var stream = new MemoryStream(buffer);
            var serializer = SerializerProvider.GetSerializersFor(MessageContentType).FirstOrDefault() ?? throw new NullReferenceException($"Failed to find a serializer for the specified content type '{MessageContentType}'");
            var payload = serializer.Deserialize<object>(stream);
            var headers = (object?)null;
            var messageDefinition = await MessageDefinitions.ToAsyncEnumerable().SingleOrDefaultAwaitAsync(async m => await MessageMatchesAsync(payload, headers, m, CancellationTokenSource.Token).ConfigureAwait(false), CancellationTokenSource.Token).ConfigureAwait(false) ?? throw new NullReferenceException("Failed to resolve the message definition for the specified operation. Make sure the message matches one and only one of the message definitions configured for the specified operation");
            var correlationId = string.Empty;
            if (messageDefinition.CorrelationId != null)
            {
                var correlationIdDefinition = messageDefinition.CorrelationId.IsReference ? Document.DereferenceCorrelationId(messageDefinition.CorrelationId.Reference!) : messageDefinition.CorrelationId;
                correlationId = await RuntimeExpressionEvaluator.EvaluateAsync(correlationIdDefinition.Location, payload, headers, CancellationTokenSource.Token).ConfigureAwait(false);
            }
            var message = new AsyncApiMessage(MessageContentType, payload, headers, correlationId);
            Subject.OnNext(message);
        }
        catch (Exception ex)
        {
            Logger.LogError("An error occurred while consuming a Redis message: {ex}", ex);
        }
    }

    /// <summary>
    /// Determines whether or not the specified payload matches the specified <see cref="V3MessageDefinition"/>
    /// </summary>
    /// <param name="payload">The message's payload, if any</param>
    /// <param name="headers">The message's headers, if any</param>
    /// <param name="message">The <see cref="V3MessageDefinition"/> to check</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A boolean indicating whether or not the specified <see cref="AsyncApiPublishOperationParameters"/> matches the specified <see cref="V3MessageDefinition"/></returns>
    protected virtual async Task<bool> MessageMatchesAsync(object? payload, object? headers, V3MessageDefinition message, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(message);
        if (message.Payload != null)
        {
            var schemaDefinition = message.Payload.IsReference ? Document.DereferenceSchema(message.Payload.Reference!) : message.Payload;
            var schemaFormat = message.Payload.SchemaFormat ?? SchemaFormat.AsyncApi;
            var schemaHandler = SchemaHandlerProvider.GetHandler(schemaFormat);
            if (schemaHandler == null) this.Logger.LogWarning("Failed to find an handler used to validate the specified schema format '{schemaFormat}", schemaFormat);
            else
            {
                var result = await schemaHandler.ValidateAsync(payload ?? new { }, schemaDefinition.Schema, cancellationToken).ConfigureAwait(false);
                if (!result.IsSuccess()) return false;
            }
        }
        if (message.Headers != null)
        {
            var schemaDefinition = message.Headers.IsReference ? Document.DereferenceSchema(message.Headers.Reference!) : message.Headers;
            var schemaFormat = message.Headers.SchemaFormat ?? SchemaFormat.AsyncApi;
            var schemaHandler = SchemaHandlerProvider.GetHandler(schemaFormat);
            if (schemaHandler == null) this.Logger.LogWarning("Failed to find an handler used to validate the specified schema format '{schemaFormat}", schemaFormat);
            else
            {
                var result = await schemaHandler.ValidateAsync(headers ?? new { }, schemaDefinition.Schema, cancellationToken).ConfigureAwait(false);
                if (!result.IsSuccess()) return false;
            }
        }
        if (message.CorrelationId != null)
        {
            var correlationIdDefinition = message.CorrelationId.IsReference ? Document.DereferenceCorrelationId(message.CorrelationId.Reference!) : message.CorrelationId;
            var correlationId = await RuntimeExpressionEvaluator.EvaluateAsync(correlationIdDefinition.Location, payload, headers, cancellationToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(correlationId)) return false;
        }
        return true;
    }

    /// <summary>
    /// Disposes of the <see cref="RedisSubscription"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="RedisSubscription"/> is being disposed of</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                CancellationTokenSource.Dispose();
                Connection.Dispose();
                Subject.Dispose();
            }
            _disposed = true;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes of the <see cref="RedisSubscription"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="RedisSubscription"/> is being disposed of</param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                CancellationTokenSource.Dispose();
                await Connection.DisposeAsync().ConfigureAwait(false);
                Subject.Dispose();
            }
            _disposed = true;
        }
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(disposing: true).ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

}