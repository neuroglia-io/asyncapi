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
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Neuroglia.AsyncApi.Client.Bindings.Amqp;

/// <summary>
/// Represents a subscription to a Amqp channel, used to stream <see cref="IAsyncApiMessage"/>s
/// </summary>
public class AmqpSubscription
    : IObservable<IAsyncApiMessage>, IDisposable, IAsyncDisposable
{

    bool _disposed;

    /// <summary>
    /// Initializes a new <see cref="AmqpSubscription"/>
    /// </summary>
    /// <param name="logger">The service used to perform logging</param>
    /// <param name="connection">The current AMQP connection</param>
    /// <param name="channel">The current AMQP channel</param>
    /// <param name="consumer">The service used to consume AMQP messages</param>
    /// <param name="messageContentType">The content type of consumed messages</param>
    /// <param name="runtimeExpressionEvaluator">The service used to evaluate runtime expressions</param>
    /// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/>s</param>
    /// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
    /// <param name="document">The <see cref="V3AsyncApiDocument"/> that defines the operation for which to consume MQTT messages</param>
    /// <param name="messageDefinitions">An <see cref="IEnumerable{T}"/> containing the definitions of all messages that can potentially be consumed</param>
    public AmqpSubscription(ILogger<AmqpSubscription> logger, IConnection connection, IChannel channel, IAsyncBasicConsumer consumer, string messageContentType, IRuntimeExpressionEvaluator runtimeExpressionEvaluator, ISchemaHandlerProvider schemaHandlerProvider, ISerializerProvider serializerProvider, V3AsyncApiDocument document, IEnumerable<V3MessageDefinition> messageDefinitions)
    {
        Logger = logger;
        Connection = connection;
        Channel = channel;
        Consumer = consumer;
        if (consumer is AsyncEventingBasicConsumer asyncEventingBasicConsumer) asyncEventingBasicConsumer.ReceivedAsync += OnAmqpMessageAsync;
        MessageContentType = messageContentType;
        RuntimeExpressionEvaluator = runtimeExpressionEvaluator;
        SchemaHandlerProvider = schemaHandlerProvider;
        SerializerProvider = serializerProvider;
        Document = document;
        MessageDefinitions = messageDefinitions;
    }

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; }

    /// <summary>
    /// Gets the current AMQP connection
    /// </summary>
    protected IConnection Connection { get; }

    /// <summary>
    /// Gets the current AMQP channel
    /// </summary>
    protected IChannel Channel { get; }

    /// <summary>
    /// Gets the service used to consume AMQP messages
    /// </summary>
    protected IAsyncBasicConsumer Consumer { get; }

    /// <summary>
    /// Gets the content type of consumed messages
    /// </summary>
    protected string MessageContentType { get; }

    /// <summary>
    /// Gets the service used to evaluate runtime expressions
    /// </summary>
    protected IRuntimeExpressionEvaluator RuntimeExpressionEvaluator { get; }

    /// <summary>
    /// Gets the service used to provide <see cref="ISchemaHandler"/>s
    /// </summary>
    protected ISchemaHandlerProvider SchemaHandlerProvider { get; }

    /// <summary>
    /// Gets the service used to provide <see cref="ISerializer"/>s
    /// </summary>
    protected ISerializerProvider SerializerProvider { get; }

    /// <summary>
    /// Gets the <see cref="V3AsyncApiDocument"/> that defines the operation for which to consume MQTT messages
    /// </summary>
    protected V3AsyncApiDocument Document { get; }

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the definitions of all messages that can potentially be consumed
    /// </summary>
    protected IEnumerable<V3MessageDefinition> MessageDefinitions { get; }

    /// <summary>
    /// Gets the <see cref="AmqpSubscription"/>'s <see cref="System.Threading.CancellationTokenSource"/>
    /// </summary>
    protected CancellationTokenSource CancellationTokenSource { get; } = new();

    /// <summary>
    /// Gets the <see cref="Subject{T}"/> used to observe consumed <see cref="IAsyncApiMessage"/>
    /// </summary>
    protected Subject<IAsyncApiMessage> Subject { get; } = new();

    /// <inheritdoc/>
    public virtual IDisposable Subscribe(IObserver<IAsyncApiMessage> observer) => Subject.Subscribe(observer);

    /// <summary>
    /// Handles the consumption of an AMQP message
    /// </summary>
    /// <param name="sender">The sender of the message</param>
    /// <param name="e">The <see cref="BasicDeliverEventArgs"/> that wraps the consumed AMQP message</param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    protected virtual async Task OnAmqpMessageAsync(object sender, BasicDeliverEventArgs e)
    {
        try
        {
            var buffer = e.Body.ToArray();
            using var stream = new MemoryStream(buffer);
            var contentType = e.BasicProperties.ContentType ?? MessageContentType;
            var serializer = SerializerProvider.GetSerializersFor(contentType).FirstOrDefault() ?? throw new NullReferenceException($"Failed to find a serializer for the specified content type '{contentType}'");
            var payload = serializer.Deserialize<object>(stream);
            var headers = e.BasicProperties.Headers;
            var messageDefinition = await MessageDefinitions.ToAsyncEnumerable().SingleOrDefaultAwaitAsync(async m => await MessageMatchesAsync(payload, headers, m, CancellationTokenSource.Token).ConfigureAwait(false), CancellationTokenSource.Token).ConfigureAwait(false) ?? throw new NullReferenceException("Failed to resolve the message definition for the specified operation. Make sure the message matches one and only one of the message definitions configured for the specified operation");
            var correlationId = string.Empty;
            if (messageDefinition.CorrelationId != null)
            {
                var correlationIdDefinition = messageDefinition.CorrelationId.IsReference ? Document.DereferenceCorrelationId(messageDefinition.CorrelationId.Reference!) : messageDefinition.CorrelationId;
                correlationId = await RuntimeExpressionEvaluator.EvaluateAsync(correlationIdDefinition.Location, payload, headers, CancellationTokenSource.Token).ConfigureAwait(false);
            }
            var message = new AsyncApiMessage(contentType, payload, headers, correlationId);
            Subject.OnNext(message);
        }
        catch (Exception ex)
        {
            Logger.LogError("An error occurred while consuming a NATS message: {ex}", ex);
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
    /// Disposes of the <see cref="AmqpSubscription"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="AmqpSubscription"/> is being disposed of</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                CancellationTokenSource.Dispose();
                Channel.Dispose();
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
    /// Disposes of the <see cref="AmqpSubscription"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="AmqpSubscription"/> is being disposed of</param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                CancellationTokenSource.Dispose();
                await Channel.DisposeAsync().ConfigureAwait(false);
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