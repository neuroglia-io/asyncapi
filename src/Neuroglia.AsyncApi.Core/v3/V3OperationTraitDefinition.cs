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
/// Represents an object that describes a trait that MAY be applied to an Operation Object.
/// </summary>
[DataContract]
public record V3OperationTraitDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets a human-friendly title for the operation.
    /// </summary>
    [DataMember(Order = 1, Name = "title"), JsonPropertyOrder(1), JsonPropertyName("title"), YamlMember(Order = 1, Alias = "title", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual string? Title { get; set; }

    /// <summary>
    /// Gets/sets a short summary of what the operation is about.
    /// </summary>
    [DataMember(Order = 2, Name = "summary"), JsonPropertyOrder(2), JsonPropertyName("summary"), YamlMember(Order = 2, Alias = "summary", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual string? Summary { get; set; }

    /// <summary>
    /// Gets/sets a verbose explanation of the operation. CommonMark syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 3, Name = "description"), JsonPropertyOrder(3), JsonPropertyName("description"), YamlMember(Order = 3, Alias = "description", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets a declaration of which security schemes are associated with this operation. Only one of the security scheme objects MUST be satisfied to authorize an operation. In cases where Server Security also applies, it MUST also be satisfied.
    /// </summary>
    [DataMember(Order = 4, Name = "security"), JsonPropertyOrder(4), JsonPropertyName("security"), YamlMember(Order = 4, Alias = "security", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual EquatableList<V3SecuritySchemeDefinition>? Security { get; set; }

    /// <summary>
    /// Gets/sets a list of tags for logical grouping and categorization of operations.
    /// </summary>
    [DataMember(Order = 5, Name = "tags"), JsonPropertyOrder(5), JsonPropertyName("tags"), YamlMember(Order = 5, Alias = "tags", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual EquatableList<TagDefinition>? Tags { get; set; }

    /// <summary>
    /// Gets/sets additional external documentation for this operation.
    /// </summary>
    [DataMember(Order = 6, Name = "externalDocs"), JsonPropertyOrder(6), JsonPropertyName("externalDocs"), YamlMember(Order = 6, Alias = "externalDocs", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual ExternalDocumentationDefinition? ExternalDocs { get; set; }

    /// <summary>
    /// Gets/sets a map where the keys describe the name of the protocol and the values describe protocol-specific definitions for the operation.
    /// </summary>
    [DataMember(Order = 7, Name = "bindings"), JsonPropertyOrder(7), JsonPropertyName("bindings"), YamlMember(Order = 7, Alias = "bindings", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual OperationBindingDefinitionCollection? Bindings { get; set; }

}
