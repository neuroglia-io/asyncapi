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
using SolaceSystems.Solclient.Messaging;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Neuroglia.AsyncApi.Client.Bindings.Solace;

/// <summary>
/// Represents a subscription to a Solace channel, used to stream <see cref="IAsyncApiMessage"/>s
/// </summary>
public class SolaceSubscription
    : IObservable<IAsyncApiMessage>, IDisposable, IAsyncDisposable
{

    bool _disposed;

    /// <summary>
    /// Initializes a new <see cref="SolaceSubscription"/>
    /// </summary>
    /// <param name="logger">The service used to perform logging</param>
    /// <param name="context">The current <see cref="IContext"/></param>
    /// <param name="session">The current <see cref="ISession"/></param>
    /// <param name="queue">The current <see cref="IQueue"/></param>
    /// <param name="messageContentType">The content type of consumed messages</param>
    /// <param name="runtimeExpressionEvaluator">The service used to evaluate runtime expressions</param>
    /// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/>s</param>
    /// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
    /// <param name="document">The <see cref="V3AsyncApiDocument"/> that defines the operation for which to consume MQTT messages</param>
    /// <param name="messageDefinitions">An <see cref="IEnumerable{T}"/> containing the definitions of all messages that can potentially be consumed</param>
    public SolaceSubscription(ILogger<SolaceSubscription> logger, IContext context, ISession session, IQueue queue, string messageContentType, IRuntimeExpressionEvaluator runtimeExpressionEvaluator, ISchemaHandlerProvider schemaHandlerProvider, ISerializerProvider serializerProvider, V3AsyncApiDocument document, IEnumerable<V3MessageDefinition> messageDefinitions)
    {
        Logger = logger;
        Context = context;
        Session = session;
        Queue = queue;
        MessageContentType = messageContentType;
        RuntimeExpressionEvaluator = runtimeExpressionEvaluator;
        SchemaHandlerProvider = schemaHandlerProvider;
        SerializerProvider = serializerProvider;
        Document = document;
        MessageDefinitions = messageDefinitions;
        Flow = Session.CreateFlow(new FlowProperties()
        {
            AckMode = MessageAckMode.ClientAck
        }, Queue, null, OnMessageAsync, null);
        Flow.Start();
    }

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; }

    /// <summary>
    /// Gets the current <see cref="IContext"/>
    /// </summary>
    protected IContext Context { get; }

    /// <summary>
    /// Gets the current <see cref="ISession"/>
    /// </summary>
    protected ISession Session { get; }

    /// <summary>
    /// Gets the current <see cref="IQueue"/>
    /// </summary>
    protected IQueue Queue { get; }

    /// <summary>
    /// Gets the current <see cref="IFlow"/>
    /// </summary>
    protected IFlow Flow { get; }

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
    /// Gets the <see cref="SolaceSubscription"/>'s <see cref="System.Threading.CancellationTokenSource"/>
    /// </summary>
    protected CancellationTokenSource CancellationTokenSource { get; } = new();

    /// <summary>
    /// Gets the <see cref="Subject{T}"/> used to observe consumed <see cref="IAsyncApiMessage"/>
    /// </summary>
    protected Subject<IAsyncApiMessage> Subject { get; } = new();

    /// <inheritdoc/>
    public virtual IDisposable Subscribe(IObserver<IAsyncApiMessage> observer) => Subject.Subscribe(observer);

    /// <summary>
    /// Handles the consumption of a Solace message
    /// </summary>
    /// <param name="sender">The event's sender</param>
    /// <param name="e">The object that wraps the message to consume</param>
    protected virtual async void OnMessageAsync(object? sender, MessageEventArgs e)
    {
        try
        {
            var contentType = e.Message.HttpContentType ?? MessageContentType;
            var serializer = SerializerProvider.GetSerializersFor(contentType).FirstOrDefault() ?? throw new NullReferenceException($"Failed to find a serializer for the specified content type '{contentType}'");
            var payload = serializer.Deserialize<object>(e.Message.BinaryAttachment);
            var headers = e.Message.UserData == null ? null : serializer.Deserialize<object>(e.Message.UserData);
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
            Logger.LogError("An error occurred while consuming a Solace message: {ex}", ex);
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
    /// Disposes of the <see cref="SolaceSubscription"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="SolaceSubscription"/> is being disposed of</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                CancellationTokenSource.Dispose();
                Flow.Stop();
                Flow.Dispose();
                Queue.Dispose();
                Session.Dispose();
                Context.Dispose();
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
    /// Disposes of the <see cref="SolaceSubscription"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="SolaceSubscription"/> is being disposed of</param>
    protected virtual ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                CancellationTokenSource.Dispose();
                Flow.Stop();
                Flow.Dispose();
                Queue.Dispose();
                Session.Dispose();
                Context.Dispose();
                Subject.Dispose();
            }
            _disposed = true;
        }
        return ValueTask.CompletedTask;
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(disposing: true).ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

}