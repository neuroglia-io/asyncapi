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
using Stomp.Net;

namespace Neuroglia.AsyncApi.Client.Bindings.Stomp;

/// <summary>
/// Represents the default Stomp implementation of the <see cref="IBindingHandler"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="options">The service used to access the current <see cref="StompBindingHandlerOptions"/></param>
/// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
public partial class StompBindingHandler(IServiceProvider serviceProvider, ILogger<StompBindingHandler> logger, IOptions<StompBindingHandlerOptions> options, ISerializerProvider serializerProvider)
    : IBindingHandler<StompBindingHandlerOptions>
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
    /// Gets the current <see cref="StompBindingHandlerOptions"/>
    /// </summary>
    protected StompBindingHandlerOptions Options { get; } = options.Value;

    /// <summary>
    /// Gets the service used to provide <see cref="ISerializer"/>s
    /// </summary>
    protected ISerializerProvider SerializerProvider { get; } = serializerProvider;

    /// <inheritdoc/>
    public virtual bool Supports(string protocol, string? protocolVersion) => protocol.Equals(AsyncApiProtocol.Stomp, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public virtual Task<IAsyncApiPublishOperationResult> PublishAsync(AsyncApiPublishOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            var connectionSettings = new StompConnectionSettings()
            {
                UserName = "guest",
                Password = "guest",
                HostHeaderOverride = "/"
            };
            var connectionFactory = new ConnectionFactory($"{context.Host}{context.Path}", connectionSettings);
            using var connection = connectionFactory.CreateConnection();
            connection.Start();
            using var session = connection.CreateSession(AcknowledgementMode.IndividualAcknowledge);
            var queue = session.GetQueue(context.Channel);
            using var producer = session.CreateProducer(queue);
            var serializer = SerializerProvider.GetSerializersFor(context.ContentType).FirstOrDefault() ?? throw new NullReferenceException($"Failed to find a serializer for the specified content type '{context.ContentType}'");
            var payload = serializer.SerializeToByteArray(context.Payload)!;
            var message = session.CreateBytesMessage(payload);
            if (context.Headers != null) foreach (var header in context.Headers.ToDictionary()!) message.Headers[header.Key] = header.Value.ToString();
            producer.Send(message);
            return Task.FromResult<IAsyncApiPublishOperationResult>(new StompPublishOperationResult());
        }
        catch (Exception ex)
        {
            return Task.FromResult<IAsyncApiPublishOperationResult>(new StompPublishOperationResult() { Exception = ex });
        }
    }

    /// <inheritdoc/>
    public virtual Task<IAsyncApiSubscribeOperationResult> SubscribeAsync(AsyncApiSubscribeOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            var connectionSettings = new StompConnectionSettings()
            {
                UserName = "guest",
                Password = "guest",
                HostHeaderOverride = "/"
            };
            var connectionFactory = new ConnectionFactory($"{context.Host}{context.Path}", connectionSettings);
            var connection = connectionFactory.CreateConnection();
            connection.Start();
            var session = connection.CreateSession(AcknowledgementMode.IndividualAcknowledge);
            var queue = session.GetQueue(context.Channel);
            var consumer = session.CreateConsumer(queue);
            var subscription = ActivatorUtilities.CreateInstance<StompSubscription>(ServiceProvider, context.Document, context.Messages, connection, session, consumer, context.DefaultContentType);
            return Task.FromResult<IAsyncApiSubscribeOperationResult>(new StompSubscribeOperationResult(subscription));
        }
        catch(Exception ex)
        {
            return Task.FromResult<IAsyncApiSubscribeOperationResult>(new StompSubscribeOperationResult() { Exception = ex });
        }
    }

}

