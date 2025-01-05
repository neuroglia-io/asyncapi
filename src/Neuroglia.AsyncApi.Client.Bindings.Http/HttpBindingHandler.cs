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
using System.Net.Mime;

namespace Neuroglia.AsyncApi.Client.Bindings.Http;

/// <summary>
/// Represents the default HTTP implementation of the <see cref="IBindingHandler"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="options">The service used to access the current <see cref="HttpBindingHandlerOptions"/></param>
/// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
/// <param name="httpClient">The service used to perform HTTP requests</param>
public class HttpBindingHandler(IServiceProvider serviceProvider, ILogger<HttpBindingHandler> logger, IOptions<HttpBindingHandlerOptions> options, ISerializerProvider serializerProvider, HttpClient httpClient)
    : IBindingHandler<HttpBindingHandlerOptions>
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
    /// Gets the current <see cref="HttpBindingHandlerOptions"/>
    /// </summary>
    protected HttpBindingHandlerOptions Options { get; } = options.Value;

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
    public virtual async Task<IAsyncApiPublishOperationResult> PublishAsync(AsyncApiPublishOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        var operationBinding = context.OperationBinding as HttpOperationBindingDefinition;
        var messageBinding = context.MessageBinding as HttpMessageBindingDefinition;
        var method = operationBinding?.Method.HasValue == true ? new System.Net.Http.HttpMethod(EnumHelper.Stringify(operationBinding.Method.Value)) : System.Net.Http.HttpMethod.Get;
        var uri = $"{context.Host}{context.Path}{context.Channel}";
        if (operationBinding?.Query != null)
        {
            var query = string.Empty; //todo: implement
            uri += $"?{query}";
        }
        HttpContent? requestContent = null;
        if (context.Payload != null)
        {
            var serializer = this.SerializerProvider.GetSerializersFor(context.ContentType).FirstOrDefault() ?? throw new NullReferenceException($"Failed to find a serializer for the specified content type '{context.ContentType}'");
            var stream = new MemoryStream();
            serializer.Serialize(context.Payload, stream);
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
        var contentStream = new MemoryStream();
        await responseStream.CopyToAsync(contentStream, cancellationToken).ConfigureAwait(false);
        await contentStream.FlushAsync(cancellationToken).ConfigureAwait(false);
        contentStream.Position = 0;
        return new HttpPublishOperationResult(response.StatusCode, response.Headers, contentStream);
    }

    /// <inheritdoc/>
    public virtual async Task<IAsyncApiSubscribeOperationResult> SubscribeAsync(AsyncApiSubscribeOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        var operationBinding = context.OperationBinding as HttpOperationBindingDefinition;
        var method = operationBinding?.Method.HasValue == true ? new System.Net.Http.HttpMethod(EnumHelper.Stringify(operationBinding.Method.Value)) : System.Net.Http.HttpMethod.Get;
        var uri = $"{context.Host}{context.Path}{context.Channel}";
        if (operationBinding?.Query != null)
        {
            var query = string.Empty; //todo: implement
            uri += $"?{query}";
        }
        using var request = new HttpRequestMessage(method, uri);
        var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        IObservable<IAsyncApiMessage>? messages = null;
        if (response.IsSuccessStatusCode)
        {
            var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
            messages = (response.Content.Headers.ContentType?.MediaType) switch
            {
                MediaTypeNames.Application.Json => ActivatorUtilities.CreateInstance<ChunkedJsonMessageStream>(ServiceProvider, context.Document, context.Messages, responseStream),
                "application/x-ndjson" => ActivatorUtilities.CreateInstance<NewlineDelimitedJsonMessageStream>(ServiceProvider, context.Document, context.Messages, responseStream),
                "text/event-stream" => ActivatorUtilities.CreateInstance<ServerSentEventMessageStream>(ServiceProvider, context.Document, context.Messages, responseStream),
                _ => throw new NotSupportedException($"The specified content type '{response.Content.Headers.ContentType?.MediaType}' is not supported in this context"),
            };
        }
        return new HttpSubscribeOperationResult(response.StatusCode, response.Headers, messages); 
    }

}
