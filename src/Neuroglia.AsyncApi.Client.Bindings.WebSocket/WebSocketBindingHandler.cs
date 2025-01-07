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
using Neuroglia.AsyncApi.Bindings.WebSockets;
using System.Net.WebSockets;

namespace Neuroglia.AsyncApi.Client.Bindings.WebSocket;

/// <summary>
/// Represents the default WebSocket implementation of the <see cref="IBindingHandler"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="options">The service used to access the current <see cref="WebSocketBindingHandlerOptions"/></param>
/// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
public class WebSocketBindingHandler(IServiceProvider serviceProvider, ILogger<WebSocketBindingHandler> logger, IOptions<WebSocketBindingHandlerOptions> options, ISerializerProvider serializerProvider)
    : IBindingHandler<WebSocketBindingHandlerOptions>
{

    const int SendBufferSize = 1024;

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; } = logger;

    /// <summary>
    /// Gets the current <see cref="WebSocketBindingHandlerOptions"/>
    /// </summary>
    protected WebSocketBindingHandlerOptions Options { get; } = options.Value;

    /// <summary>
    /// Gets the service used to provide <see cref="ISerializer"/>s
    /// </summary>
    protected ISerializerProvider SerializerProvider { get; } = serializerProvider;

    /// <inheritdoc/>
    public virtual bool Supports(string protocol, string? protocolVersion) => protocol.Equals(AsyncApiProtocol.Ws, StringComparison.OrdinalIgnoreCase) || protocol.Equals(AsyncApiProtocol.Wss, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public virtual async Task<IAsyncApiPublishOperationResult> PublishAsync(AsyncApiPublishOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            var channelBinding = context.ChannelBinding as WsChannelBindingDefinition;
            using var webSocket = new ClientWebSocket();
            await webSocket.ConnectAsync(new($"{context.Host}{context.Path}"), cancellationToken).ConfigureAwait(false);
            using var stream = new MemoryStream();
            var serializer = SerializerProvider.GetSerializersFor(context.ContentType).FirstOrDefault() ?? throw new NullReferenceException($"Failed to find a serializer for the specified content type '{context.ContentType}'");
            serializer.Serialize(context.Payload ?? new { }, stream);
            await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
            stream.Position = 0;
            var buffer = stream.ToArray();
            var offset = 0;
            while (offset < buffer.Length)
            {
                var remaining = buffer.Length - offset;
                var currentSegmentSize = Math.Min(SendBufferSize, remaining);
                var segment = new ArraySegment<byte>(buffer, offset, currentSegmentSize);
                var endOfMessage = (offset + currentSegmentSize) >= buffer.Length;
                await webSocket.SendAsync(segment, WebSocketMessageType.Binary, endOfMessage, cancellationToken).ConfigureAwait(false);
                offset += currentSegmentSize;
            }
            await webSocket.SendAsync(buffer, WebSocketMessageType.Binary, true, cancellationToken).ConfigureAwait(false);
            return new WebSocketPublishOperationResult();
        }
        catch (Exception ex)
        {
            return new WebSocketPublishOperationResult()
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
            var channelBinding = context.ChannelBinding as WsChannelBindingDefinition;
            var webSocket = new ClientWebSocket();
            await webSocket.ConnectAsync(new($"{context.Host}{context.Path}"), cancellationToken).ConfigureAwait(false);
            var subscription = ActivatorUtilities.CreateInstance<WebSocketSubscription>(ServiceProvider, context.Document, context.Messages, context.DefaultContentType, webSocket);
            return new WebSocketSubscribeOperationResult(subscription);
        }
        catch(Exception ex)
        {
            return new WebSocketSubscribeOperationResult()
            {
                Exception = ex
            }; 
        }
    }

}
