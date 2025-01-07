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
using System.Reactive.Subjects;

namespace Neuroglia.AsyncApi.Client.Bindings.Http;

/// <summary>
/// Represents the base class for all <see cref="IAsyncApiMessage"/> streams
/// </summary>
public abstract class AsyncApiMessageStream
    : IObservable<IAsyncApiMessage>, IDisposable
{

    bool _disposed;

    /// <summary>
    /// Initializes a new <see cref="AsyncApiMessageStream"/>
    /// </summary>
    /// <param name="logger">The service used to perform logging</param>
    /// <param name="runtimeExpressionEvaluator">The service used to evaluate runtime expressions</param>
    /// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/>s</param>
    /// <param name="jsonSerializer">The service used to serialize/deserialize data to/from JSON</param>
    /// <param name="document">The <see cref="V3AsyncApiDocument"/> that defines the operation for which to consume SSEs</param>
    /// <param name="stream">The <see cref="System.IO.Stream"/> to read SSE from</param>
    /// <param name="messageDefinitions">An <see cref="IEnumerable{T}"/> containing the definitions of all messages that can potentially be consumed</param>
    public AsyncApiMessageStream(ILogger logger, IRuntimeExpressionEvaluator runtimeExpressionEvaluator, ISchemaHandlerProvider schemaHandlerProvider, IJsonSerializer jsonSerializer, V3AsyncApiDocument document, Stream stream, IEnumerable<V3MessageDefinition> messageDefinitions)
    {
        Logger = logger;
        RuntimeExpressionEvaluator = runtimeExpressionEvaluator;
        SchemaHandlerProvider = schemaHandlerProvider;
        JsonSerializer = jsonSerializer;
        Document = document;
        Stream = stream;
        MessageDefinitions = messageDefinitions;
        _ = Task.Run(ReadAsync);
    }

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; }

    /// <summary>
    /// Gets the service used to evaluate runtime expressions
    /// </summary>
    protected IRuntimeExpressionEvaluator RuntimeExpressionEvaluator { get; }

    /// <summary>
    /// Gets the service used to provide <see cref="ISchemaHandler"/>s
    /// </summary>
    protected ISchemaHandlerProvider SchemaHandlerProvider { get; }

    /// <summary>
    /// Gets the service used to serialize/deserialize data to/from JSON
    /// </summary>
    protected IJsonSerializer JsonSerializer { get; }

    /// <summary>
    /// Gets the <see cref="V3AsyncApiDocument"/> that defines the operation for which to consume SSEs
    /// </summary>
    protected V3AsyncApiDocument Document { get; }

    /// <summary>
    /// Gets the <see cref="System.IO.Stream"/> to read SSE from
    /// </summary>
    protected Stream Stream { get; }

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the definitions of all messages that can potentially be consumed
    /// </summary>
    protected IEnumerable<V3MessageDefinition> MessageDefinitions { get; }

    /// <summary>
    /// Gets the <see cref="ServerSentEventMessageStream"/>'s <see cref="System.Threading.CancellationTokenSource"/>
    /// </summary>
    protected CancellationTokenSource CancellationTokenSource { get; } = new();

    /// <summary>
    /// Gets the <see cref="Subject{T}"/> used to observe consumed <see cref="IAsyncApiMessage"/>
    /// </summary>
    protected Subject<IAsyncApiMessage> Subject { get; } = new();

    /// <inheritdoc/>
    public virtual IDisposable Subscribe(IObserver<IAsyncApiMessage> observer) => this.Subject.Subscribe(observer);

    /// <summary>
    /// Reads <see cref="IAsyncApiMessage"/>s from the underlying <see cref="System.IO.Stream"/>
    /// </summary>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    protected abstract Task ReadAsync();

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
    /// Disposes of the <see cref="ServerSentEventMessageStream"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="ServerSentEventMessageStream"/> is being disposed of</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                CancellationTokenSource.Dispose();
                Stream.Dispose();
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

}
