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
/// Represents an object used to define an Async API parameter
/// </summary>
[DataContract]
public record V2ParameterDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets a runtime expression that specifies the location of the parameter value. 
    /// Even when a definition for the target field exists, it MUST NOT be used to validate this parameter but, instead, the <see cref="Schema"/> property MUST be used.
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "location"), JsonPropertyOrder(1), JsonPropertyName("location"), YamlMember(Order = 1, Alias = "location")]
    public virtual string Location { get; set; } = null!;

    /// <summary>
    /// Gets/sets a short description of the parameter. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 2, Name = "description"), JsonPropertyOrder(2), JsonPropertyName("description"), YamlMember(Order = 2, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="V2ParameterDefinition"/>'s <see cref="JsonSchema"/>
    /// </summary>
    [DataMember(Order = 3, Name = "schema"), JsonPropertyOrder(3), JsonPropertyName("schema"), YamlMember(Order = 3, Alias = "schema")]
    public virtual JsonSchema? Schema { get; set; }

    private RuntimeExpression _LocationExpression = null!;
    /// <summary>
    /// Gets the location's <see cref="RuntimeExpression"/>
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual RuntimeExpression LocationExpression
    {
        get
        {
            if (_LocationExpression == null && !string.IsNullOrWhiteSpace(Location)) _LocationExpression = RuntimeExpression.Parse(Location);
            return _LocationExpression!;
        }
    }

    /// <inheritdoc/>
    public override string ToString() => Location;

}
