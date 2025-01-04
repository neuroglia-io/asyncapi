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

using Microsoft.Extensions.Logging;
using Neuroglia.AsyncApi.Bindings.Http;
using Neuroglia.Serialization;

namespace Neuroglia.AsyncApi.Client.Services;

/// <summary>
/// Represents the default HTTP implementation of the <see cref="IProtocolHandler"/> interface
/// </summary>
/// <param name="logger">The service used to perform logging</param>
/// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
/// <param name="httpClient">The service used to perform HTTP requests</param>
public class HttpProtocolHandler(ILogger<HttpProtocolHandler> logger, ISerializerProvider serializerProvider, HttpClient httpClient)
    : IProtocolHandler
{

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; } = logger;

    /// <summary>
    /// Gets the service used to provide <see cref="ISerializer"/>s
    /// </summary>
    protected ISerializerProvider SerializerProvider { get; } = serializerProvider;

    /// <summary>
    /// Gets the service used to perform HTTP requests
    /// </summary>
    protected HttpClient HttpClient { get; } = httpClient;

    /// <inheritdoc/>
    public virtual bool Supports(string protocol, string? protocolVersion) => protocol.Equals(AsyncApiProtocol.Http, StringComparison.OrdinalIgnoreCase) || protocol.Equals(AsyncApiProtocol.Https, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public virtual async Task<IAsyncApiOperationResult> HandleAsync(AsyncApiOperation operation, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(operation);
        var operationBinding = operation.OperationBinding as HttpOperationBindingDefinition;
        var messageBinding = operation.MessageBinding as HttpMessageBindingDefinition;
        var method = operationBinding?.Method.HasValue == true ? new System.Net.Http.HttpMethod(EnumHelper.Stringify(operationBinding.Method.Value)) : System.Net.Http.HttpMethod.Get;
        var uri = $"{operation.Host}{operation.Path}{operation.Channel}";
        if (operationBinding?.Query != null)
        {
            var query = string.Empty; //todo: implement
            uri += "?";
        }
        HttpContent? requestContent = null;
        if (operation.Payload != null)
        {
            var serializer = this.SerializerProvider.GetSerializersFor(operation.ContentType).FirstOrDefault() ?? throw new NullReferenceException($"Failed to find a serializer for the specified content type '{operation.ContentType}'");
            var stream = new MemoryStream();
            serializer.Serialize(operation.Payload, stream);
            await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
            stream.Position = 0;
            requestContent = new StreamContent(stream);
        }
        using var request = new HttpRequestMessage(method, uri) { Content = requestContent };
        if (messageBinding?.Headers != null)
        {
            //todo: implement
        }
        using var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        var memoryStream = new MemoryStream();
        await responseStream.CopyToAsync(memoryStream, cancellationToken).ConfigureAwait(false);
        await memoryStream.FlushAsync(cancellationToken).ConfigureAwait(false);
        memoryStream.Position = 0;
        return new HttpOperationResult(response.StatusCode, response.Headers, memoryStream);
    }

}