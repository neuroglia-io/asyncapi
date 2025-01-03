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

namespace Neuroglia.AsyncApi.v3;

/// <summary>
/// Defines extensions for <see cref="V3AsyncApiDocument"/>s
/// </summary>
public static class V3AsyncApiDocumentExtensions
{

    /// <summary>
    /// Gets all the <see cref="V3ChannelDefinition"/>s defined by the specified <see cref="V3ServerDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="serverReference">A reference to the <see cref="V3ServerDefinition"/> to get the <see cref="V3ChannelDefinition"/>s for</param>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all the <see cref="V3ChannelDefinition"/>s defined by the specified <see cref="V3ServerDefinition"/></returns>
    public static IEnumerable<KeyValuePair<string, V3ChannelDefinition>> GetChannelsFor(this V3AsyncApiDocument document, string serverReference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(serverReference);
        foreach(var channelKvp in document.Channels.Where(c => c.Value.Servers.Any(s => s.Reference == serverReference))) yield return channelKvp;
        if (document.Components?.Channels != null) foreach (var channelKvp in document.Components.Channels.Where(c => c.Value.Servers.Any(s => s.Reference == serverReference))) yield return channelKvp;
        
    }

    /// <summary>
    /// Gets all the <see cref="V3OperationDefinition"/>s defined by the specified <see cref="V3OperationDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="channelReference">A reference to the <see cref="V3OperationDefinition "/> to get the <see cref="V3OperationDefinition"/>s for</param>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all the <see cref="V3OperationDefinition"/>s defined by the specified <see cref="V3ChannelDefinition"/></returns>
    public static IEnumerable<KeyValuePair<string, V3OperationDefinition>> GetOperationsFor(this V3AsyncApiDocument document, string channelReference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(channelReference);
        foreach (var operationKvp in document.Operations.Where(o => o.Value.Channel.Reference == channelReference)) yield return operationKvp;
        if (document.Components?.Operations != null) foreach (var operationKvp in document.Components.Operations.Where(o => o.Value.Channel.Reference == channelReference)) yield return operationKvp;
    }

    /// <summary>
    /// Dereferences the specified component
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the component to get</param>
    /// <returns>The specified component</returns>
    public static ReferenceableComponentDefinition Dereference(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        var componentType = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[^2];
        return componentType switch
        {
            "servers" => document.DereferenceServer(reference),
            "channels" => document.DereferenceChannel(reference),
            "operations" => document.DereferenceOperation(reference),
            "schemas" => document.DereferenceSchema(reference),
            "messages" => document.DereferenceMessage(reference),
            "securitySchemes" => document.DereferenceSecurityScheme(reference),
            "serverVariables" => document.DereferenceServerVariable(reference),
            "parameters" => document.DereferenceParameter(reference),
            "correlationIds" => document.DereferenceCorrelationId(reference),
            "replies" => document.DereferenceReply(reference),
            "replyAddresses" => document.DereferenceReplyAddress(reference),
            "externalDocs" => document.DereferenceExternalDocumentation(reference),
            "tags" => document.DereferenceTag(reference),
            "operationTraits" => document.DereferenceOperationTrait(reference),
            "messageTraits" => document.DereferenceMessageTrait(reference),
            "serverBindings" => document.DereferenceServerBinding(reference),
            "channelBindings" => document.DereferenceChannelBinding(reference),
            "operationBindings" => document.DereferenceOperationBinding(reference),
            "messageBindings" => document.DereferenceMessageBinding(reference),
            _ => throw new NotSupportedException($"The specified component type '{componentType}' is not supported")
        };
    }

    /// <summary>
    /// Dereferences the specified component
    /// </summary>
    /// <typeparam name="TComponent">The type of component to dereference</typeparam>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the component to get</param>
    /// <returns>The specified component</returns>
    public static TComponent Dereference<TComponent>(this V3AsyncApiDocument document, string reference)
        where TComponent : ReferenceableComponentDefinition
    {
        return (TComponent)document.Dereference(reference);
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3ServerDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3ServerDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3ServerDefinition"/></returns>
    public static V3ServerDefinition DereferenceServer(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        var referenceComponents = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1..];
        var componentContainer = referenceComponents[0];
        var componentName = referenceComponents.Last();
        var server = componentContainer switch
        {
            "servers" => document.Servers?[componentName],
            "components" => document.Components?.Servers?[componentName],
            _ => throw new NullReferenceException($"Failed to dereference the specified server '{reference}'. The reference is invalid and/or the server is undefined")
        } ?? throw new NullReferenceException($"Failed to dereference the specified server '{reference}': it does not exist or cannot be found");
        return server;
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3ChannelDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3ChannelDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3ChannelDefinition"/></returns>
    public static V3ChannelDefinition DereferenceChannel(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        var referenceComponents = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1..];
        var componentContainer = referenceComponents[0];
        var componentName = referenceComponents.Last();
        var channel = componentContainer switch
        {
            "channels" => document.Channels?[componentName],
            "components" => document.Components?.Channels?[componentName],
            _ => throw new NullReferenceException($"Failed to dereference the specified channel '{reference}'. The reference is invalid and/or the channel is undefined")
        } ?? throw new NullReferenceException($"Failed to dereference the specified channel '{reference}': it does not exist or cannot be found");
        return channel;
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3OperationDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3OperationDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3OperationDefinition"/></returns>
    public static V3OperationDefinition DereferenceOperation(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        var referenceComponents = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1..];
        var componentContainer = referenceComponents[0];
        var componentName = referenceComponents.Last();
        var channel = componentContainer switch
        {
            "operations" => document.Operations?[componentName],
            "components" => document.Components?.Operations?[componentName],
            _ => throw new NullReferenceException($"Failed to dereference the specified operation '{reference}'. The reference is invalid and/or the operation is undefined")
        } ?? throw new NullReferenceException($"Failed to dereference the specified operation '{reference}': it does not exist or cannot be found");
        return channel;
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3SchemaDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3SchemaDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3SchemaDefinition"/></returns>
    public static V3SchemaDefinition DereferenceSchema(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/schemas/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid schema reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.Schemas?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified schema '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3MessageDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3MessageDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3MessageDefinition"/></returns>
    public static V3MessageDefinition DereferenceMessage(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/messages/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid message reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.Messages?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified message '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3MessageDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="channelName">The name of the <see cref="V3ChannelDefinition"/> that defines the <see cref="V3MessageDefinition"/> to dereference</param>
    /// <param name="channelDefinition">The <see cref="V3ChannelDefinition"/> that defines the <see cref="V3MessageDefinition"/> to dereference</param>
    /// <param name="reference">The reference to the <see cref="V3MessageDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3MessageDefinition"/></returns>
    public static V3MessageDefinition DereferenceChannelMessage(this V3AsyncApiDocument document, string channelName, V3ChannelDefinition channelDefinition, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(channelName);
        ArgumentNullException.ThrowIfNull(channelDefinition);
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith($"#/channels/{channelName}/messages/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid channel message reference");
        var messageName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        if (!channelDefinition.Messages.TryGetValue(messageName, out var message) || message == null) throw new NullReferenceException($"Failed to dereference the specified channel message '{reference}': it does not exist or cannot be found");
        return message.IsReference ? document.DereferenceMessage(message.Reference!) : message;
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3SecuritySchemeDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3SecuritySchemeDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3SecuritySchemeDefinition"/></returns>
    public static V3SecuritySchemeDefinition DereferenceSecurityScheme(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/securitySchemes/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid security scheme reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.SecuritySchemes?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified security scheme '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3ServerVariableDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3ServerVariableDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3ServerVariableDefinition"/></returns>
    public static V3ServerVariableDefinition DereferenceServerVariable(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/serverVariables/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid server variable reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.ServerVariables?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified server variable '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3ParameterDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3ParameterDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3ParameterDefinition"/></returns>
    public static V3ParameterDefinition DereferenceParameter(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/parameters/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid parameter reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.Parameters?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified parameter '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3CorrelationIdDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3CorrelationIdDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3CorrelationIdDefinition"/></returns>
    public static V3CorrelationIdDefinition DereferenceCorrelationId(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/correlationIds/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid correlation id reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.CorrelationIds?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified correlation id '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3ReplyDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3ReplyDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3ReplyDefinition"/></returns>
    public static V3ReplyDefinition DereferenceReply(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/replies/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid reply reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.Replies?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified reply '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3ReplyAddressDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3ReplyAddressDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3ReplyAddressDefinition"/></returns>
    public static V3ReplyAddressDefinition DereferenceReplyAddress(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/replyAddresses/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid reply address reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.ReplyAddresses?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified reply address '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3ExternalDocumentationDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3ExternalDocumentationDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3ExternalDocumentationDefinition"/></returns>
    public static V3ExternalDocumentationDefinition DereferenceExternalDocumentation(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/externalDocs/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid external docs reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.ExternalDocs?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified external docs '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3TagDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3TagDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3TagDefinition"/></returns>
    public static V3TagDefinition DereferenceTag(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/tags/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid tag reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.Tags?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified tag '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3OperationTraitDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3OperationTraitDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3OperationTraitDefinition"/></returns>
    public static V3OperationTraitDefinition DereferenceOperationTrait(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/operationTraits/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid operation trait reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.OperationTraits?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified operation trait '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="V3MessageTraitDefinition"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="V3MessageTraitDefinition"/> to get</param>
    /// <returns>The specified <see cref="V3MessageTraitDefinition"/></returns>
    public static V3MessageTraitDefinition DereferenceMessageTrait(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/messageTraits/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid message trait reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.MessageTraits?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified message trait '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="ServerBindingDefinitionCollection"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="ServerBindingDefinitionCollection"/> to get</param>
    /// <returns>The specified <see cref="ServerBindingDefinitionCollection"/></returns>
    public static ServerBindingDefinitionCollection DereferenceServerBinding(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/serverBindings/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid server binding reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.ServerBindings?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified server binding '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="ChannelBindingDefinitionCollection"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="ChannelBindingDefinitionCollection"/> to get</param>
    /// <returns>The specified <see cref="ChannelBindingDefinitionCollection"/></returns>
    public static ChannelBindingDefinitionCollection DereferenceChannelBinding(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/channelBindings/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid channel binding reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.ChannelBindings?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified channel binding '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="OperationBindingDefinitionCollection"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="OperationBindingDefinitionCollection"/> to get</param>
    /// <returns>The specified <see cref="OperationBindingDefinitionCollection"/></returns>
    public static OperationBindingDefinitionCollection DereferenceOperationBinding(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/operationBindings/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid operation binding reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.OperationBindings?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified operation binding '{reference}': it does not exist or cannot be found");
    }

    /// <summary>
    /// Dereferences the specified <see cref="MessageBindingDefinitionCollection"/>
    /// </summary>
    /// <param name="document">The extended <see cref="V3AsyncApiDocument"/></param>
    /// <param name="reference">The reference to the <see cref="MessageBindingDefinitionCollection"/> to get</param>
    /// <returns>The specified <see cref="MessageBindingDefinitionCollection"/></returns>
    public static MessageBindingDefinitionCollection DereferenceMessageBinding(this V3AsyncApiDocument document, string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        if (!reference.StartsWith("#/components/messageBindings/")) throw new InvalidDataException($"The specified value '{reference}' is not a valid message binding reference");
        var componentName = reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
        return document.Components?.MessageBindings?[componentName] ?? throw new NullReferenceException($"Failed to dereference the specified message binding '{reference}': it does not exist or cannot be found");
    }

}
