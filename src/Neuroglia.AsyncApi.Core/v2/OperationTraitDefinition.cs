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

using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Represents an object used to define a Async API operation trait
/// </summary>
[DataContract]
public record OperationTraitDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets a unique string used to identify the operation. The id MUST be unique among all operations described in the API. The operationId value is case-sensitive. Tools and libraries MAY use the operationId to uniquely identify an operation, therefore, it is RECOMMENDED to follow common programming naming conventions.
    /// </summary>
    [DataMember(Order = 1, Name = "operationId"), JsonPropertyOrder(1), JsonPropertyName("operationId"), YamlMember(Order = 1, Alias = "operationId")]
    public virtual string? OperationId { get; set; }

    /// <summary>
    /// Gets/sets a short summary of what the operation is about.
    /// </summary>
    [DataMember(Order = 2, Name = "summary"), JsonPropertyOrder(2), JsonPropertyName("summary"), YamlMember(Order = 2, Alias = "summary")]
    public virtual string? Summary { get; set; }

    /// <summary>
    /// Gets/sets an optional description of the operation trait. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 3, Name = "description"), JsonPropertyOrder(3), JsonPropertyName("description"), YamlMember(Order = 3, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="List{T}"/> of tags for API documentation control. Tags can be used for logical grouping of operations.
    /// </summary>
    [DataMember(Order = 4, Name = "tags"), JsonPropertyOrder(4), JsonPropertyName("tags"), YamlMember(Order = 4, Alias = "tags")]
    public virtual EquatableList<TagDefinition>? Tags { get; set; }

    /// <summary>
    /// Gets/sets an object containing additional external documentation for this operation.
    /// </summary>
    [DataMember(Order = 5, Name = "externalDocs"), JsonPropertyOrder(5), JsonPropertyName("externalDocs"), YamlMember(Order = 5, Alias = "externalDocs")]
    public virtual ExternalDocumentationDefinition? ExternalDocs { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the <see cref="OperationTraitDefinition"/>'s <see cref="IOperationBindingDefinition"/>s
    /// </summary>
    [DataMember(Order = 6, Name = "bindings"), JsonPropertyOrder(6), JsonPropertyName("bindings"), YamlMember(Order = 6, Alias = "bindings")]
    public virtual OperationBindingDefinitionCollection? Bindings { get; set; }

    /// <inheritdoc/>
    public override string ToString() => OperationId ?? base.ToString();

}
