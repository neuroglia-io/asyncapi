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

using YamlDotNet.Core;

namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Represents an <see href="https://www.asyncapi.com">Async API</see> version 2 document
/// </summary>
[DataContract]
public record AsyncApiDocument
{

    /// <summary>
    /// Gets/sets the the AsyncAPI Specification version being used. It can be used by tooling Specifications and clients to interpret the version. 
    /// </summary>
    /// <remarks>
    /// The structure shall be major.minor.patch, where patch versions must be compatible with the existing major.minor tooling. 
    /// Typically patch versions will be introduced to address errors in the documentation, and tooling should typically be compatible with the corresponding major.minor (1.0.*). 
    /// Patch versions will correspond to patches of this document.
    /// </remarks>
    [Required]
    [DataMember(Order = 1, Name = "asyncApi"), JsonPropertyOrder(1), JsonPropertyName("asyncApi"), YamlMember(Order = 1, Alias = "asyncApi", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual string AsyncApi { get; set; } = AsyncApiSpecVersion.V2;

    /// <summary>
    /// Gets/sets the identifier of the application the AsyncAPI document is defining
    /// </summary>
    [DataMember(Order = 2, Name = "id"), JsonPropertyOrder(2), JsonPropertyName("id"), YamlMember(Order = 2, Alias = "id")]
    public virtual string? Id { get; set; }

    /// <summary>
    /// Gets/sets the object that provides metadata about the API. The metadata can be used by the clients if needed. 
    /// </summary>
    [Required]
    [DataMember(Order = 3, Name = "info"), JsonPropertyOrder(3), JsonPropertyName("info"), YamlMember(Order = 3, Alias = "info")]
    public virtual ApiInfo Info { get; set; } = null!;

    /// <summary>
    /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> containing name/configuration mappings for the application's servers
    /// </summary>
    [DataMember(Order = 4, Name = "servers"), JsonPropertyOrder(4), JsonPropertyName("servers"), YamlMember(Order = 4, Alias = "servers")]
    public virtual EquatableDictionary<string, ServerDefinition>? Servers { get; set; }

    /// <summary>
    /// Gets/sets the default content type to use when encoding/decoding a message's payload.
    /// </summary>
    [DataMember(Order = 5, Name = "defaultContentType"), JsonPropertyOrder(5), JsonPropertyName("defaultContentType"), YamlMember(Order = 5, Alias = "defaultContentType")]
    public virtual string? DefaultContentType { get; set; }

    /// <summary>
    /// Gets/sets a collection containing the available channels and messages for the API.
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 6, Name = "channels"), JsonPropertyOrder(6), JsonPropertyName("channels"), YamlMember(Order = 6, Alias = "channels")]
    public virtual EquatableDictionary<string, ChannelDefinition> Channels { get; set; } = null!;

    /// <summary>
    /// Gets/sets an object used to hold various schemas for the specification.
    /// </summary>
    [DataMember(Order = 7, Name = "components"), JsonPropertyOrder(7), JsonPropertyName("components"), YamlMember(Order = 7, Alias = "components")]
    public virtual ComponentDefinitionCollection? Components { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="List{T}"/> of tags used by the specification with additional metadata. Each tag name in the list MUST be unique.
    /// </summary>
    [DataMember(Order = 8, Name = "tags"), JsonPropertyOrder(8), JsonPropertyName("tags"), YamlMember(Order = 8, Alias = "tags")]
    public virtual EquatableList<TagDefinition>? Tags { get; set; }

    /// <summary>
    /// Gets/sets an object containing additional external documentation.
    /// </summary>
    [DataMember(Order = 9, Name = "externalDocs"), JsonPropertyOrder(9), JsonPropertyName("externalDocs"), YamlMember(Order = 9, Alias = "externalDocs")]
    public virtual ExternalDocumentationDefinition? ExternalDocs { get; set; }

    /// <summary>
    /// Attempts to get the <see cref="OperationDefinition"/> with the specified id
    /// </summary>
    /// <param name="operationId">The id of the <see cref="OperationDefinition"/> to get</param>
    /// <param name="operation">The resulting <see cref="OperationDefinition"/>, if any</param>
    /// <param name="channelName">The name of the <see cref="ChannelDefinition"/> the <see cref="OperationDefinition"/> belongs to, if any</param>
    /// <returns>A boolean indicating whether or not the <see cref="OperationDefinition"/> with the specified id could be found</returns>
    public virtual bool TryGetOperation(string operationId, out OperationDefinition? operation, out string? channelName)
    {
        if (string.IsNullOrWhiteSpace(operationId)) throw new ArgumentNullException(nameof(operationId));
        operation = null;
        channelName = null;
        var channel = Channels?.FirstOrDefault(c => c.Value.DefinesOperationWithId(operationId));
        if (!channel.HasValue || channel.Value.Equals(default(KeyValuePair<string, ChannelDefinition>))) return false;
        operation = channel.Value.Value.GetOperationById(operationId);
        channelName = channel.Value.Key;
        return true;
    }

    /// <summary>
    /// Attempts to get the <see cref="OperationDefinition"/> with the specified id
    /// </summary>
    /// <param name="operationId">The id of the <see cref="OperationDefinition"/> to get</param>
    /// <param name="operation">The resulting <see cref="OperationDefinition"/>, if any</param>
    /// <returns>A boolean indicating whether or not the <see cref="OperationDefinition"/> with the specified id could be found</returns>
    public virtual bool TryGetOperation(string operationId, out OperationDefinition? operation)
    {
        if (string.IsNullOrWhiteSpace(operationId))
            throw new ArgumentNullException(nameof(operationId));
        return TryGetOperation(operationId, out operation, out _);
    }

    /// <inheritdoc/>
    public override string? ToString() => Info == null || string.IsNullOrWhiteSpace(Info.Title) ? Id : Info?.Title;

}
