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
using StackExchange.Redis;
using System.Reactive.Linq;

namespace Neuroglia.AsyncApi.Client.Bindings.Redis;

/// <summary>
/// Represents the default Redis implementation of the <see cref="IBindingHandler"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="options">The service used to access the current <see cref="RedisBindingHandlerOptions"/></param>
/// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
public class RedisBindingHandler(IServiceProvider serviceProvider, ILogger<RedisBindingHandler> logger, IOptions<RedisBindingHandlerOptions> options, ISerializerProvider serializerProvider)
    : IBindingHandler<RedisBindingHandlerOptions>
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
    /// Gets the current <see cref="RedisBindingHandlerOptions"/>
    /// </summary>
    protected RedisBindingHandlerOptions Options { get; } = options.Value;

    /// <summary>
    /// Gets the service used to provide <see cref="ISerializer"/>s
    /// </summary>
    protected ISerializerProvider SerializerProvider { get; } = serializerProvider;

    /// <inheritdoc/>
    public virtual bool Supports(string protocol, string? protocolVersion) => protocol.Equals(AsyncApiProtocol.Redis, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public virtual async Task<IAsyncApiPublishOperationResult> PublishAsync(AsyncApiPublishOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            using var connection = await ConnectionMultiplexer.ConnectAsync(context.Host).ConfigureAwait(false);
            var database = connection.GetDatabase();
            var channel = new RedisChannel(context.Channel!, RedisChannel.PatternMode.Auto);
            using var stream = new MemoryStream();
            var serializer = SerializerProvider.GetSerializersFor(context.ContentType).FirstOrDefault() ?? throw new NullReferenceException($"Failed to find a serializer for the specified content type '{context.ContentType}'");
            serializer.Serialize(context.Payload ?? new { }, stream);
            await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
            stream.Position = 0;
            var buffer = stream.ToArray();
            var clients = await database.PublishAsync(channel, buffer).ConfigureAwait(false);
            return new RedisPublishOperationResult() { Clients = clients };
        }
        catch (Exception ex)
        {
            return new RedisPublishOperationResult() { Exception = ex };
        }
    }

    /// <inheritdoc/>
    public virtual async Task<IAsyncApiSubscribeOperationResult> SubscribeAsync(AsyncApiSubscribeOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        ConnectionMultiplexer? connection = null;
        try
        {
            connection = await ConnectionMultiplexer.ConnectAsync(context.Host).ConfigureAwait(false);
            var channel = new RedisChannel(context.Channel!, RedisChannel.PatternMode.Auto);
            var subscription = ActivatorUtilities.CreateInstance<RedisSubscription>(ServiceProvider, context.Document, context.Messages, connection, channel, context.DefaultContentType);
            await subscription.StartAsync().ConfigureAwait(false);
            return new RedisSubscribeOperationResult(subscription);
        }
        catch(Exception ex)
        {
            if (connection != null) await connection.DisposeAsync().ConfigureAwait(false);
            return new RedisSubscribeOperationResult() { Exception = ex };
        }
    }

}
