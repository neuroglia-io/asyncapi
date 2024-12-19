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

namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Represents an object used to define a reference to an external documentation
/// </summary>
[DataContract]
public record V2ExternalDocumentationDefinition
{

    /// <summary>
    /// Gets/sets the <see cref="Uri"/> for the target documentation.
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "url"), JsonPropertyOrder(1), JsonPropertyName("url"), YamlMember(Order = 1, Alias = "url")]
    public virtual Uri Url { get; set; } = null!;

    /// <summary>
    /// Gets/sets an optional description of this documentation. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 2, Name = "description"), JsonPropertyOrder(2), JsonPropertyName("description"), YamlMember(Order = 2, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <inheritdoc/>
    public override string ToString() => Url.ToString();

}
