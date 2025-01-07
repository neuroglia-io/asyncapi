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
using SolaceSystems.Solclient.Messaging;
using System.Text;
using System.Threading.Channels;
using static SolaceSystems.Solclient.Messaging.Helper;

namespace Neuroglia.AsyncApi.Client.Bindings.Solace;

/// <summary>
/// Represents the default Solace implementation of the <see cref="IBindingHandler"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="options">The service used to access the current <see cref="SolaceBindingHandlerOptions"/></param>
/// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
public partial class SolaceBindingHandler(IServiceProvider serviceProvider, ILogger<SolaceBindingHandler> logger, IOptions<SolaceBindingHandlerOptions> options, ISerializerProvider serializerProvider)
    : IBindingHandler<SolaceBindingHandlerOptions>
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
    /// Gets the current <see cref="SolaceBindingHandlerOptions"/>
    /// </summary>
    protected SolaceBindingHandlerOptions Options { get; } = options.Value;

    /// <summary>
    /// Gets the service used to provide <see cref="ISerializer"/>s
    /// </summary>
    protected ISerializerProvider SerializerProvider { get; } = serializerProvider;

    /// <inheritdoc/>
    public virtual bool Supports(string protocol, string? protocolVersion) => protocol.Equals(AsyncApiProtocol.Solace, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public virtual Task<IAsyncApiPublishOperationResult> PublishAsync(AsyncApiPublishOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            var serverBinding = context.ServerBinding as SolaceServerBindingDefinition;
            var operationBinding = context.OperationBinding as SolaceOperationBindingDefinition;
            var solaceContextFactoryProperties = new ContextFactoryProperties();
            ContextFactory.Instance.Init(solaceContextFactoryProperties);
            var solaceContextProperties = new ContextProperties();
            using var solaceContext = ContextFactory.Instance.CreateContext(solaceContextProperties, null);
            var sessionProperties = new SessionProperties()
            {
                Host = $"{context.Host}{context.Path}",
                VPNName = serverBinding?.MessageVpn ?? string.Empty,
                ClientName = serverBinding?.ClientName ?? string.Empty,
            };
            using var session = solaceContext.CreateSession(sessionProperties, null, null);
            var returnCode = session.Connect();
            if (returnCode != ReturnCode.SOLCLIENT_OK) throw new SolaceSessionConnectionException(returnCode);
            var destinations = new List<SolaceDestinationDescriptor>();
            if (operationBinding?.Destinations != null && operationBinding.Destinations.Count > 0)
            {
                foreach (var destinationDefinition in operationBinding.Destinations)
                {
                    switch (destinationDefinition.DestinationType)
                    {
                        case SolaceDestinationType.Queue:
                            if (destinationDefinition.Queue == null) throw new NullReferenceException($"The queue property must be set for a destination of type '{SolaceDestinationType.Queue}'");
                            var queue = ContextFactory.Instance.CreateQueue(destinationDefinition.Queue.Name);
                            var endpointProperties = new EndpointProperties()
                            {
                                Permission = EndpointProperties.EndpointPermission.Consume,
                                AccessType = destinationDefinition.Queue.AccessType switch
                                {
                                    SolaceQueueAccessType.Exclusive => EndpointProperties.EndpointAccessType.Exclusive,
                                    SolaceQueueAccessType.NonExclusive => EndpointProperties.EndpointAccessType.NonExclusive,
                                    _ => throw new NotSupportedException($"The specified access type '{destinationDefinition.DestinationType}' is not supported")
                                }
                            };
                            session.Provision(queue, endpointProperties, ProvisionFlag.IgnoreErrorIfEndpointAlreadyExists | ProvisionFlag.WaitForConfirm, null);
                            destinations.Add(new(destinationDefinition, queue));
                            break;
                        case SolaceDestinationType.Topic:
                            var topic = ContextFactory.Instance.CreateTopic(context.Channel);
                            destinations.Add(new(destinationDefinition, topic));
                            break;
                        default:
                            throw new NotSupportedException($"The specified destination type '{destinationDefinition.DestinationType}' is not supported");
                    }
                }
            }
            else
            {
                var queue = ContextFactory.Instance.CreateQueue(context.Channel);
                var endpointProperties = new EndpointProperties()
                {
                    Permission = EndpointProperties.EndpointPermission.Consume,
                    AccessType = EndpointProperties.EndpointAccessType.Exclusive
                };
                session.Provision(queue, endpointProperties, ProvisionFlag.IgnoreErrorIfEndpointAlreadyExists | ProvisionFlag.WaitForConfirm, null);
                destinations.Add(new(new(), queue));
            }
            var serializer = SerializerProvider.GetSerializersFor(context.ContentType).FirstOrDefault() ?? throw new NullReferenceException($"Failed to find a serializer for the specified content type '{context.ContentType}'");
            var payload = serializer.SerializeToByteArray(context.Payload)!;
            var headers = context.Headers == null ? null : serializer.SerializeToByteArray(context.Headers)!;
            foreach (var destination in destinations)
            {
                using var message = ContextFactory.Instance.CreateMessage();
                message.Destination = destination.Instance;
                if (destination.Definition.DeliveryMode.HasValue) message.DeliveryMode = destination.Definition.DeliveryMode switch
                {
                    SolaceDeliveryMode.Direct => MessageDeliveryMode.Direct,
                    SolaceDeliveryMode.Persistent => MessageDeliveryMode.Persistent,
                    _ => throw new NotSupportedException($"The specified message delivery mode '{destination.Definition.DeliveryMode}' is not supported")
                };
                message.HttpContentType = context.ContentType;
                message.BinaryAttachment = payload;
                message.UserData = headers;
                returnCode = session.Send(message);
                if (returnCode != ReturnCode.SOLCLIENT_OK) throw new SolaceMessagePublishingException(returnCode);
                destination.Instance.Dispose();
            }
            return Task.FromResult<IAsyncApiPublishOperationResult>(new SolacePublishOperationResult());
        }
        catch (Exception ex)
        {
            return Task.FromResult<IAsyncApiPublishOperationResult>(new SolacePublishOperationResult() { Exception = ex });
        }
    }

    /// <inheritdoc/>
    public virtual Task<IAsyncApiSubscribeOperationResult> SubscribeAsync(AsyncApiSubscribeOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            var serverBinding = context.ServerBinding as SolaceServerBindingDefinition;
            var operationBinding = context.OperationBinding as SolaceOperationBindingDefinition;
            var solaceContextFactoryProperties = new ContextFactoryProperties();
            ContextFactory.Instance.Init(solaceContextFactoryProperties);
            var solaceContextProperties = new ContextProperties();
            var solaceContext = ContextFactory.Instance.CreateContext(solaceContextProperties, null);
            var sessionProperties = new SessionProperties()
            {
                Host = $"{context.Host}{context.Path}",
                VPNName = serverBinding?.MessageVpn ?? string.Empty,
                ClientName = serverBinding?.ClientName ?? string.Empty,
            };
            var session = solaceContext.CreateSession(sessionProperties, null, null);
            var returnCode = session.Connect();
            if (returnCode != ReturnCode.SOLCLIENT_OK) throw new SolaceSessionConnectionException(returnCode);
            var queue = ContextFactory.Instance.CreateQueue(context.Channel);
            var endpointProperties = new EndpointProperties()
            {
                Permission = EndpointProperties.EndpointPermission.Consume,
                AccessType = EndpointProperties.EndpointAccessType.Exclusive
            };
            session.Provision(queue, endpointProperties, ProvisionFlag.IgnoreErrorIfEndpointAlreadyExists | ProvisionFlag.WaitForConfirm, null);
            var subscription = ActivatorUtilities.CreateInstance<SolaceSubscription>(ServiceProvider, context.Document, context.Messages, solaceContext, session, queue, context.DefaultContentType);
            return Task.FromResult< IAsyncApiSubscribeOperationResult>(new SolaceSubscribeOperationResult(subscription));
        }
        catch(Exception ex)
        {
            return Task.FromResult<IAsyncApiSubscribeOperationResult>(new SolaceSubscribeOperationResult() { Exception = ex });
        }
    }

}

