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
using Neuroglia;
using Neuroglia.AsyncApi.v3;

namespace Neuroglia.AsyncApi.Client.Services;

/// <summary>
/// Represents the default AsyncAPI v3 implementation of the <see cref="IAsyncApiClient"/> interface
/// </summary>
/// <param name="logger">The service used to perform logging</param>
/// <param name="runtimeExpressionEvaluator">The service used to evaluate runtime expressions</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/>s</param>
/// <param name="protocolHandlerProvider">The service used to provide <see cref="IProtocolHandler"/>s</param>
/// <param name="document">The <see cref="IAsyncApiDocument"/> used to describe the application to interact with</param>
public class V3AsyncApiClient(ILogger<V3AsyncApiClient> logger, IRuntimeExpressionEvaluator runtimeExpressionEvaluator, ISchemaHandlerProvider schemaHandlerProvider, IProtocolHandlerProvider protocolHandlerProvider, V3AsyncApiDocument document)
    : IAsyncApiClient
{

    bool _disposed;

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; } = logger;

    /// <summary>
    /// Gets the service used to evaluate runtime expressions
    /// </summary>
    protected IRuntimeExpressionEvaluator RuntimeExpressionEvaluator { get; } = runtimeExpressionEvaluator;

    /// <summary>
    /// Gets the service used to provide <see cref="ISchemaHandler"/>s
    /// </summary>
    protected ISchemaHandlerProvider SchemaHandlerProvider { get; } = schemaHandlerProvider;

    /// <summary>
    /// Gets the service used to provide <see cref="IProtocolHandler"/>s
    /// </summary>
    protected IProtocolHandlerProvider ProtocolHandlerProvider { get; } = protocolHandlerProvider;

    /// <summary>
    /// Gets the <see cref="V3AsyncApiDocument"/> used to describe the application to interact with
    /// </summary>
    protected V3AsyncApiDocument Document { get; } = document;

    /// <inheritdoc/>
    public virtual async Task<AsyncApiOperationResult> SendAsync(AsyncApiOutboundMessage message, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(message);
        if (!this.Document.Operations.TryGetValue(message.Operation, out var operationDefinition) || operationDefinition == null)
        {
            if (this.Document.Components?.Operations?.TryGetValue(message.Operation, out operationDefinition) != true || operationDefinition == null) throw new NullReferenceException($"Failed to find the specified operation '{message.Operation}' in the AsyncAPI definition. Verify that the operation is defined or that its reference is spelled correctly");
        }
        if (operationDefinition.Action != V3OperationAction.Receive) throw new NotSupportedException($"Cannot send an outbound message to the specified operation '{message.Operation}' because its action type has not been set to 'receive'. Make sure you are targeting the proper operation or change its action type to 'receive'");
        var channelName = operationDefinition.Channel.Reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        var channelDefinition = this.Document.DereferenceChannel(operationDefinition.Channel.Reference);
        var channelParameters = (await this.ResolveChannelParametersAsync(channelDefinition, message, cancellationToken).ConfigureAwait(false)).ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value);
        var channelAddress = StringFormatter.NamedFormat(channelDefinition.Address, channelParameters);
        var serverDefinition = this.ResolveServerDefinition(channelDefinition, message.Server, message.Protocol);
        var serverVariables = this.ResolveServerVariables(serverDefinition, message.ServerVariables).ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value);
        var host = StringFormatter.NamedFormat(serverDefinition.Host, serverVariables);
        var path = string.IsNullOrWhiteSpace(serverDefinition.PathName) ? null : StringFormatter.NamedFormat(serverDefinition.PathName, serverVariables);
        var messageDefinition = await this.ResolveMessageDefinitionAsync(channelName, channelDefinition, operationDefinition, message, cancellationToken).ConfigureAwait(false);
        var correlationId = string.Empty;
        if (messageDefinition.CorrelationId != null)
        {
            var correlationIdDefinition = messageDefinition.CorrelationId.IsReference ? Document.DereferenceCorrelationId(messageDefinition.CorrelationId.Reference!) : messageDefinition.CorrelationId;
            correlationId = await RuntimeExpressionEvaluator.EvaluateAsync(correlationIdDefinition.Location, message.Payload, message.Headers, cancellationToken).ConfigureAwait(false);
        }
        var serverBindings = serverDefinition.Bindings == null ? null : serverDefinition.Bindings.IsReference ? Document.DereferenceServerBinding(serverDefinition.Bindings.Reference!) : serverDefinition.Bindings;
        var serverBinding = serverBindings?.AsEnumerable().FirstOrDefault(b => b.Protocols.Contains(serverDefinition.Protocol));
        var channelBindings = channelDefinition.Bindings == null ? null : channelDefinition.Bindings.IsReference ? Document.DereferenceChannelBinding(channelDefinition.Bindings.Reference!) : channelDefinition.Bindings;
        var channelBinding = channelBindings?.AsEnumerable().FirstOrDefault(b => b.Protocols.Contains(serverDefinition.Protocol));
        var operationBindings = operationDefinition.Bindings == null ? null : operationDefinition.Bindings.IsReference ? Document.DereferenceOperationBinding(operationDefinition.Bindings.Reference!) : operationDefinition.Bindings;
        var operationBinding = operationBindings?.AsEnumerable().FirstOrDefault(b => b.Protocols.Contains(serverDefinition.Protocol));
        var messageBindings = messageDefinition.Bindings == null ? null : messageDefinition.Bindings.IsReference ? Document.DereferenceMessageBinding(messageDefinition.Bindings.Reference!) : messageDefinition.Bindings;
        var messageBinding = messageBindings?.AsEnumerable().FirstOrDefault(b => b.Protocols.Contains(serverDefinition.Protocol));
        var protocolHandler = this.ProtocolHandlerProvider.GetHandlerFor(serverDefinition.Protocol, serverDefinition.ProtocolVersion);
        return await protocolHandler.HandleAsync(new(operationDefinition.Action, host, path, channelAddress, message.Payload, message.Headers, correlationId, serverBinding, channelBinding, operationBinding, messageBinding), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Resolves the <see cref="V3MessageDefinition"/> for the specified <see cref="V3ChannelDefinition"/>, <see cref="V3OperationDefinition"/> and <see cref="AsyncApiOutboundMessage"/>
    /// </summary>
    /// <param name="channelName">The name of the <see cref="V3ChannelDefinition"/> to resolve the <see cref="V3MessageDefinition"/> for</param>
    /// <param name="channelDefinition">The <see cref="V3ChannelDefinition"/> to resolve the <see cref="V3MessageDefinition"/> for</param>
    /// <param name="operation">The <see cref="V3OperationDefinition"/> to resolve the <see cref="V3MessageDefinition"/> for</param>
    /// <param name="message">The <see cref="AsyncApiOutboundMessage"/> to resolve the <see cref="V3MessageDefinition"/> for</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The resolved <see cref="V3MessageDefinition"/></returns>
    protected virtual async Task<V3MessageDefinition> ResolveMessageDefinitionAsync(string channelName, V3ChannelDefinition channelDefinition, V3OperationDefinition operation, AsyncApiOutboundMessage message, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(channelName);
        ArgumentNullException.ThrowIfNull(channelDefinition);
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(message);
        var messageDefinitions = operation.Messages.Select(m => Document.DereferenceChannelMessage(channelName, channelDefinition, m.Reference));
        return await messageDefinitions.ToAsyncEnumerable().SingleOrDefaultAwaitAsync(async m => await MessageMatchesAsync(message, m, cancellationToken).ConfigureAwait(false)).ConfigureAwait(false) ?? throw new NullReferenceException("Failed to resolve the message definition for the specified operation. Make sure the message matches one and only one of the message definitions configured for the specified operation");
    }

    /// <summary>
    /// Determines whether or not the specified <see cref="AsyncApiOutboundMessage"/> matches the specified <see cref="V3MessageDefinition"/>
    /// </summary>
    /// <param name="message">The <see cref="AsyncApiOutboundMessage"/> to check</param>
    /// <param name="messageDefinition">The <see cref="V3MessageDefinition"/> to check</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A boolean indicating whether or not the specified <see cref="AsyncApiOutboundMessage"/> matches the specified <see cref="V3MessageDefinition"/></returns>
    protected virtual async Task<bool> MessageMatchesAsync(AsyncApiOutboundMessage message, V3MessageDefinition messageDefinition, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(messageDefinition);
        if (messageDefinition.Payload != null)
        {
            var schemaDefinition = messageDefinition.Payload.IsReference ? Document.DereferenceSchema(messageDefinition.Payload.Reference!) : messageDefinition.Payload;
            var schemaFormat = messageDefinition.Payload.SchemaFormat ?? SchemaFormat.AsyncApi;
            var schemaHandler = SchemaHandlerProvider.GetHandler(schemaFormat);
            if (schemaHandler == null) this.Logger.LogWarning("Failed to find an handler used to validate the specified schema format '{schemaFormat}", schemaFormat);
            else
            {
                var result = await schemaHandler.ValidateAsync(message.Payload ?? new { }, schemaDefinition.Schema, cancellationToken).ConfigureAwait(false);
                if (!result.IsSuccess()) return false;
            }
        }
        if (messageDefinition.Headers != null)
        {
            var schemaDefinition = messageDefinition.Headers.IsReference ? Document.DereferenceSchema(messageDefinition.Headers.Reference!) : messageDefinition.Headers;
            var schemaFormat = messageDefinition.Headers.SchemaFormat ?? SchemaFormat.AsyncApi;
            var schemaHandler = SchemaHandlerProvider.GetHandler(schemaFormat);
            if (schemaHandler == null) this.Logger.LogWarning("Failed to find an handler used to validate the specified schema format '{schemaFormat}", schemaFormat);
            else
            {
                var result = await schemaHandler.ValidateAsync(message.Headers ?? new { }, schemaDefinition.Schema, cancellationToken).ConfigureAwait(false);
                if (!result.IsSuccess()) return false;
            }
        }
        if (messageDefinition.CorrelationId != null)
        {
            var correlationIdDefinition = messageDefinition.CorrelationId.IsReference ? Document.DereferenceCorrelationId(messageDefinition.CorrelationId.Reference!) : messageDefinition.CorrelationId;
            var correlationId = await RuntimeExpressionEvaluator.EvaluateAsync(correlationIdDefinition.Location, message.Payload, message.Headers, cancellationToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(correlationId)) return false;
        }
        return true;
    }

    /// <summary>
    /// Resolves the specified <see cref="V3ChannelDefinition"/>'s parameters
    /// </summary>
    /// <param name="channel">The <see cref="V3ChannelDefinition"/> to resolve parameters for</param>
    /// <param name="message">The <see cref="AsyncApiOutboundMessage"/> to resolve the <see cref="V3ChannelDefinition"/>'s parameters for</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new key/value mapping of the <see cref="V3ChannelDefinition"/>'s resolved parameters</returns>
    protected virtual async Task<IDictionary<string, string>> ResolveChannelParametersAsync(V3ChannelDefinition channel, AsyncApiOutboundMessage message, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(channel);
        ArgumentNullException.ThrowIfNull(message);
        var parameters = new Dictionary<string, string>();
        if (channel.Parameters != null)
        {
            foreach(var parameter in channel.Parameters)
            {
                var parameterValue = string.IsNullOrWhiteSpace(parameter.Value.Location)
                    ? parameter.Value.Default
                    : await this.RuntimeExpressionEvaluator.EvaluateAsync(parameter.Value.Location, message.Payload, message.Headers, cancellationToken).ConfigureAwait(false) ?? parameter.Value.Default;
                if (string.IsNullOrWhiteSpace(parameterValue))
                {
                    this.Logger.LogWarning("Ignoring parameter '{parameter}' because its resolved value is null or empty", parameter.Key);
                    continue;
                }
                if (parameter.Value.Enum != null && !parameter.Value.Enum.Contains(parameterValue))
                {
                    this.Logger.LogWarning("Ignoring parameter '{parameter}' because it does not support the specified value '{value}'. Supported values are: {supportedValues}", parameter.Key, parameterValue, string.Join(',', parameter.Value.Enum));
                    continue;
                }
                parameters[parameter.Key] = parameterValue!;
            }
        }
        return parameters;
    }

    /// <summary>
    /// Resolves the <see cref="V3ServerDefinition"/> to use
    /// </summary>
    /// <param name="channel">The <see cref="V3ChannelDefinition"/> to resolve the <see cref="V3ServerDefinition"/> for</param>
    /// <param name="serverId">The id, if any, of the <see cref="V3ServerDefinition"/> to use</param>
    /// <param name="protocol">The protocol, if any, used to select the <see cref="V3ServerDefinition"/> to use</param>
    /// <returns>The resolved <see cref="V3ServerDefinition"/></returns>
    protected virtual V3ServerDefinition ResolveServerDefinition(V3ChannelDefinition channel, string? serverId = null, string? protocol = null)
    {
        ArgumentNullException.ThrowIfNull(channel);
        if (!string.IsNullOrWhiteSpace(serverId))
        {
            if (this.Document.Servers?.TryGetValue(serverId, out var server) != true || server == null)
            {
                if (this.Document.Components?.Servers?.TryGetValue(serverId, out server) != true || server == null) throw new NullReferenceException($"Failed to find the specified server '{serverId}' in the AsyncAPI definition. Verify that the server is defined or that its reference is spelled correctly");
            }
            return server;
        }
        var servers = channel.Servers.Count > 0 ? channel.Servers.Select(s => this.Document.DereferenceServer(s.Reference)) : this.Document.Servers?.Values;
        if (!string.IsNullOrWhiteSpace(protocol)) return servers?.FirstOrDefault(s => s.Protocol.Equals(protocol, StringComparison.OrdinalIgnoreCase)) ?? throw new NullReferenceException($"Failed to resolve a server supporting protocol '{protocol}' for the specified operation. Verify that at least one server in the AsyncAPI definition is configured with this protocol");
        return servers?.FirstOrDefault() ?? throw new NullReferenceException($"Failed to resolve the server to use for the specified operation. Verify that the AsyncAPI definition is valid and that the channel the specified operation applies to defines at least one server, or that at least one server has been defined at top level");
    }

    /// <summary>
    /// Resolves the specified <see cref="V3ServerDefinition"/>'s variables
    /// </summary>
    /// <param name="server">The <see cref="V3ServerDefinition"/> to resolve the variables for</param>
    /// <param name="variableAssignments">A key/value mapping of the values, if any, of the <see cref="V3ServerDefinition"/>'s variables</param>
    /// <returns>A new key/value mapping of the specified <see cref="V3ServerDefinition"/>'s variables</returns>
    protected virtual IDictionary<string, string> ResolveServerVariables(V3ServerDefinition server, IDictionary<string, string>? variableAssignments = null)
    {
        var variables = server.Variables?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Default!) ?? [];
        if (server.Variables != null && variableAssignments != null)
        {
            foreach (var variableAssignment in variableAssignments)
            {
                if (!server.Variables.TryGetValue(variableAssignment.Key, out var variableDefinition) || variableDefinition == null)
                {
                    this.Logger.LogWarning("Ignoring variable '{variable}' because the server does not define a variable with the same name", variableAssignment.Key);
                    continue;
                }
                if (variableDefinition.Enum != null && !variableDefinition.Enum.Contains(variableAssignment.Value))
                {
                    this.Logger.LogWarning("Ignoring variable '{variable}' because it does not support the specified value '{value}'. Supported values are: {supportedValues}", variableAssignment.Key, variableAssignment.Value, string.Join(',', variableDefinition.Enum));
                    continue;
                }
                variables[variableAssignment.Key] = variableAssignment.Value;
            }
        }
        return variables;
    }

    /// <summary>
    /// Disposes of the <see cref="V3AsyncApiClient"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="V3AsyncApiClient"/> is being disposed of</param>
    protected virtual ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {

            }
            _disposed = true;
        }
        return ValueTask.CompletedTask;
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes of the <see cref="V3AsyncApiClient"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="V3AsyncApiClient"/> is being disposed of</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {

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
