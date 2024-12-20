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
/// Represents a reference to a component
/// </summary>
[DataContract]
public record V3ReferenceDefinition
{

    /// <summary>
    /// Gets/sets the reference string
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "$ref"), JsonPropertyOrder(1), JsonPropertyName("$ref"), YamlMember(Order = 1, Alias = "$ref", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual string Reference { get; set; } = null!;

}