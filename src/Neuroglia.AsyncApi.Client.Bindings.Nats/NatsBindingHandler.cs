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
using Microsoft.Extensions.Primitives;
using NATS.Client.Core;
using NATS.Net;

namespace Neuroglia.AsyncApi.Client.Bindings.Nats;

/// <summary>
/// Represents the default NATS implementation of the <see cref="IBindingHandler"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="options">The service used to access the current <see cref="NatsBindingHandlerOptions"/></param>
/// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
public class NatsBindingHandler(IServiceProvider serviceProvider, ILogger<NatsBindingHandler> logger, IOptions<NatsBindingHandlerOptions> options, ISerializerProvider serializerProvider)
    : IBindingHandler<NatsBindingHandlerOptions>
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
    /// Gets the current <see cref="NatsBindingHandlerOptions"/>
    /// </summary>
    protected NatsBindingHandlerOptions Options { get; } = options.Value;

    /// <summary>
    /// Gets the service used to provide <see cref="ISerializer"/>s
    /// </summary>
    protected ISerializerProvider SerializerProvider { get; } = serializerProvider;

    /// <inheritdoc/>
    public virtual bool Supports(string protocol, string? protocolVersion) => protocol.Equals(AsyncApiProtocol.Nats, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public virtual async Task<IAsyncApiPublishOperationResult> PublishAsync(AsyncApiPublishOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            var operationBinding = context.OperationBinding as NatsOperationBindingDefinition;
            var url = $"{context.Host}{context.Path}";
            await using var client = new NatsClient(url);
            var publishOptions = new NatsPubOpts();
            using var stream = new MemoryStream();
            var serializer = SerializerProvider.GetSerializersFor(context.ContentType).FirstOrDefault() ?? throw new NullReferenceException($"Failed to find a serializer for the specified content type '{context.ContentType}'");
            serializer.Serialize(context.Payload ?? new { }, stream);
            await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
            stream.Position = 0;
            var payload = stream.ToArray();
            var headers = context.Headers == null ? null : new NatsHeaders();
            if (headers != null && context.Headers != null) foreach (var header in context.Headers.ToDictionary()!) headers.Add(header.Key, new StringValues(header.Value.ToString()));
            await client.PublishAsync(context.Channel!, payload, headers, null, null, publishOptions, cancellationToken: cancellationToken).ConfigureAwait(false);
            return new NatsPublishOperationResult();
        }
        catch (Exception ex)
        {
            return new NatsPublishOperationResult() { Exception = ex };
        }
    }

    /// <inheritdoc/>
    public virtual Task<IAsyncApiSubscribeOperationResult> SubscribeAsync(AsyncApiSubscribeOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            var operationBinding = context.OperationBinding as NatsOperationBindingDefinition;
            var url = $"{context.Host}{context.Path}";
            var client = new NatsClient(url);
            var subscribeOptions = new NatsSubOpts();
            var messages = client.SubscribeAsync<byte[]>(context.Channel!, operationBinding?.Queue, null, subscribeOptions, cancellationToken);
            var subscription = ActivatorUtilities.CreateInstance<NatsSubscription>(ServiceProvider, context.Document, context.Messages, context.DefaultContentType, client, messages);
            return Task.FromResult<IAsyncApiSubscribeOperationResult>(new NatsSubscribeOperationResult(subscription));
        }
        catch(Exception ex)
        {
            return Task.FromResult<IAsyncApiSubscribeOperationResult>(new NatsSubscribeOperationResult() { Exception = ex });
        }
    }

}