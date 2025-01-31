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
using System.Net.Mime;

namespace Neuroglia.AsyncApi.Client.Bindings.Http;

/// <summary>
/// Represents a service used to read <see cref="IAsyncApiMessage"/>s from a newline delimited JSON (NDJSON) stream
/// </summary>
/// <param name="logger">The service used to perform logging</param>
/// <param name="runtimeExpressionEvaluator">The service used to evaluate runtime expressions</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/>s</param>
/// <param name="jsonSerializer">The service used to serialize/deserialize data to/from JSON</param>
/// <param name="document">The <see cref="V3AsyncApiDocument"/> that defines the operation for which to consume SSEs</param>
/// <param name="stream">The <see cref="System.IO.Stream"/> to read SSE from</param>
/// <param name="messageDefinitions">An <see cref="IEnumerable{T}"/> containing the definitions of all messages that can potentially be consumed</param>
public class NewlineDelimitedJsonMessageStream(ILogger<ServerSentEventMessageStream> logger, IRuntimeExpressionEvaluator runtimeExpressionEvaluator, ISchemaHandlerProvider schemaHandlerProvider, IJsonSerializer jsonSerializer, V3AsyncApiDocument document, Stream stream, IEnumerable<V3MessageDefinition> messageDefinitions)
    : AsyncApiMessageStream(logger, runtimeExpressionEvaluator, schemaHandlerProvider, jsonSerializer, document, stream, messageDefinitions)
{

    /// <inheritdoc/>
    protected override async Task ReadAsync()
    {
        try
        {
            while (!CancellationTokenSource.IsCancellationRequested)
            {
                using var streamReader = new StreamReader(Stream);
                while (!streamReader.EndOfStream)
                {
                    var json = (await streamReader.ReadLineAsync(CancellationTokenSource.Token).ConfigureAwait(false))?.Trim();
                    if (string.IsNullOrWhiteSpace(json)) continue;
                    object? payload;
                    object? headers = null;
                    payload = JsonSerializer.Deserialize<object>(json);
                    var messageDefinition = await MessageDefinitions.ToAsyncEnumerable().SingleOrDefaultAwaitAsync(async m => await MessageMatchesAsync(payload, headers, m, CancellationTokenSource.Token).ConfigureAwait(false), CancellationTokenSource.Token).ConfigureAwait(false) ?? throw new NullReferenceException("Failed to resolve the message definition for the specified operation. Make sure the message matches one and only one of the message definitions configured for the specified operation");
                    var correlationId = string.Empty;
                    if (messageDefinition.CorrelationId != null)
                    {
                        var correlationIdDefinition = messageDefinition.CorrelationId.IsReference ? Document.DereferenceCorrelationId(messageDefinition.CorrelationId.Reference!) : messageDefinition.CorrelationId;
                        correlationId = await RuntimeExpressionEvaluator.EvaluateAsync(correlationIdDefinition.Location, payload, headers, CancellationTokenSource.Token).ConfigureAwait(false);
                    }
                    var message = new AsyncApiMessage(MediaTypeNames.Application.Json, payload, headers, correlationId);
                    Subject.OnNext(message);
                }
                Subject.OnCompleted();
            }
        }
        catch (Exception ex) when (ex is TaskCanceledException or OperationCanceledException) { }
        catch (Exception ex)
        {
            Subject.OnError(ex);
        }
    }

}