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

namespace Neuroglia.AsyncApi.v2.Bindings.Sns;

/// <summary>
/// Represents an object used to configure an SNS identifier
/// </summary>
[DataContract]
public record SnsIdentifier
{

    /// <summary>
    /// Gets/sets the endpoint's URL
    /// </summary>
    [DataMember(Order = 1, Name = "url"), JsonPropertyOrder(1), JsonPropertyName("url"), YamlMember(Order = 1, Alias = "url")]
    public virtual Uri? Url { get; set; }

    /// <summary>
    /// Gets/sets the endpoint's email
    /// </summary>
    [DataMember(Order = 2, Name = "email"), JsonPropertyOrder(2), JsonPropertyName("email"), YamlMember(Order = 2, Alias = "email")]
    public virtual string? Email { get; set; }

    /// <summary>
    /// Gets/sets the endpoint's phone number
    /// </summary>
    [DataMember(Order = 3, Name = "phone"), JsonPropertyOrder(3), JsonPropertyName("phone"), YamlMember(Order = 3, Alias = "phone")]
    public virtual string? Phone { get; set; }

    /// <summary>
    /// Gets/sets the endpoint's arn
    /// </summary>
    [DataMember(Order = 4, Name = "arn"), JsonPropertyOrder(4), JsonPropertyName("arn"), YamlMember(Order = 4, Alias = "arn")]
    public virtual string? Arn { get; set; }

    /// <summary>
    /// Gets/sets the endpoint's name
    /// </summary>
    [DataMember(Order = 5, Name = "name"), JsonPropertyOrder(5), JsonPropertyName("name"), YamlMember(Order = 5, Alias = "name")]
    public virtual string? Name { get; set; }

}
