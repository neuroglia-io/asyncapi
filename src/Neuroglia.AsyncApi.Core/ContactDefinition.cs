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
/// Represents an object used to provide contact information for the exposed API
/// </summary>
[DataContract]
public record ContactDefinition
{

    /// <summary>
    /// Gets/sets the identifying name of the contact person/organization.
    /// </summary>
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name"), YamlMember(Order = 1, Alias = "name")]
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="Uri"/> pointing to the contact information.
    /// </summary>
    [DataMember(Order = 2, Name = "url"), JsonPropertyOrder(2), JsonPropertyName("url"), YamlMember(Order = 2, Alias = "url")]
    public virtual Uri? Url { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="Uri"/> pointing to the contact information.
    /// </summary>
    [DataType(DataType.EmailAddress)]
    [DataMember(Order = 3, Name = "email"), JsonPropertyOrder(3), JsonPropertyName("email"), YamlMember(Order = 3, Alias = "email")]
    public virtual string? Email { get; set; }

}
