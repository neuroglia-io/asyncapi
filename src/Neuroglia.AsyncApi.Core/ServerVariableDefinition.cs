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
/// Represents an object used to describe a Server Variable for server URL template substitution.
/// </summary>
[DataContract]
public record ServerVariableDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets an <see cref="IEnumerable{T}"/> of values to be used if the substitution options are from a limited set.
    /// </summary>
    [DataMember(Order = 1, Name = "enum"), JsonPropertyOrder(1), JsonPropertyName("enum"), YamlMember(Order = 1, Alias = "enum")]
    public virtual EquatableList<string>? Enum { get; set; }

    /// <summary>
    /// Gets/sets the default value to use for substitution, and to send, if an alternate value is not supplied.
    /// </summary>
    [DataMember(Order = 2, Name = "default"), JsonPropertyOrder(2), JsonPropertyName("default"), YamlMember(Order = 2, Alias = "default")]
    public virtual string? Default { get; set; }

    /// <summary>
    /// Gets/sets an optional string describing the server variable. <see href="https://spec.commonmark.org/">CommonMark</see> syntax MAY be used for rich text representation.
    /// </summary>
    [DataMember(Order = 3, Name = "description"), JsonPropertyOrder(3), JsonPropertyName("description"), YamlMember(Order = 3, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="List{T}"/> of examples of the server variable.
    /// </summary>
    [DataMember(Order = 4, Name = "examples"), JsonPropertyOrder(4), JsonPropertyName("examples"), YamlMember(Order = 4, Alias = "examples")]
    public virtual EquatableList<string>? Examples { get; set; }

}