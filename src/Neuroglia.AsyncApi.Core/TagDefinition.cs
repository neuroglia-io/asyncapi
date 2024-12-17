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

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents an object used to define a tag assigned to an Async API component
/// </summary>
[DataContract]
public record TagDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets the name of the tag.
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name"), YamlMember(Order = 1, Alias = "name")]
    public virtual string Name { get; set; } = null!;

    /// <summary>
    /// Gets/sets an optional description of this channel item. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 2, Name = "description"), JsonPropertyOrder(2), JsonPropertyName("description"), YamlMember(Order = 2, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets an object containing additional external documentation for this tag.
    /// </summary>
    [DataMember(Order = 3, Name = "externalDocs"), JsonPropertyOrder(3), JsonPropertyName("externalDocs"), YamlMember(Order = 3, Alias = "externalDocs")]
    public virtual ExternalDocumentationDefinition? ExternalDocs { get; set; }

    /// <inheritdoc/>
    public override string ToString() => Name;

}
