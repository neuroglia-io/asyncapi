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
/// Represents an <see href="https://www.asyncapi.com">Async API</see> version 3 document
/// </summary>
[DataContract]
public record V3AsyncApiDocument
    : IAsyncApiDocument
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
    [DataMember(Order = 1, Name = "asyncapi"), JsonPropertyOrder(1), JsonPropertyName("asyncapi"), YamlMember(Order = 1, Alias = "asyncapi", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual string AsyncApi { get; set; } = AsyncApiSpecVersion.V3;

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
    public virtual V3ApiInfo Info { get; set; } = null!;

    /// <summary>
    /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> containing name/configuration mappings for the application's servers
    /// </summary>
    [DataMember(Order = 4, Name = "servers"), JsonPropertyOrder(4), JsonPropertyName("servers"), YamlMember(Order = 4, Alias = "servers")]
    public virtual EquatableDictionary<string, V3ServerDefinition>? Servers { get; set; }

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
    public virtual EquatableDictionary<string, V3ChannelDefinition> Channels { get; set; } = null!;

    /// <summary>
    /// Gets/sets a name/value map of the operations this application MUST implement.
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 7, Name = "operations"), JsonPropertyOrder(7), JsonPropertyName("operations"), YamlMember(Order = 7, Alias = "operations")]
    public virtual EquatableDictionary<string, V3OperationDefinition> Operations { get; set; } = null!;

    /// <summary>
    /// Gets/sets an element to hold various reusable objects for the specification. Everything that is defined inside this object represents a resource that MAY or MAY NOT be used in the rest of the document and MAY or MAY NOT be used by the implemented Application.
    /// </summary>
    [DataMember(Order = 8, Name = "components"), JsonPropertyOrder(8), JsonPropertyName("components"), YamlMember(Order = 8, Alias = "components")]
    public virtual V3ComponentDefinitionCollection? Components { get; set; }

    /// <inheritdoc/>
    public override string? ToString() => this.Info == null || string.IsNullOrWhiteSpace(this.Info.Title) ? Id : this.Info?.Title;

}
