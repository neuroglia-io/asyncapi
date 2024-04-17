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
/// Represents the object used to configure a SNS channel binding
/// </summary>
[DataContract]
public record SnsChannelBindingDefinition
    : SnsBindingDefinition, IChannelBindingDefinition
{

    /// <summary>
    /// Gets/sets the name of the topic. Can be different from the channel name to allow flexibility around AWS resource naming limitations.
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name"), YamlMember(Order = 1, Alias = "name")]
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the SNS topic ordering
    /// </summary>
    [DataMember(Order = 2, Name = "ordering"), JsonPropertyOrder(2), JsonPropertyName("ordering"), YamlMember(Order = 2, Alias = "ordering")]
    public virtual SnsTopicOrderingDefinition? Ordering { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the SNS topic security
    /// </summary>
    [DataMember(Order = 3, Name = "policy"), JsonPropertyOrder(3), JsonPropertyName("policy"), YamlMember(Order = 3, Alias = "policy")]
    public virtual SnsTopicSecurityPolicyDefinition? Policy { get; set; }

    /// <summary>
    /// Gets key-value pairs that represent AWS tags on the topic.
    /// </summary>
    [DataMember(Order = 4, Name = "tags"), JsonPropertyOrder(4), JsonPropertyName("tags"), YamlMember(Order = 4, Alias = "tags")]
    public virtual EquatableDictionary<string, string>? Tags { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding.
    /// </summary>
    [DataMember(Order = 5, Name = "bindingVersion"), JsonPropertyOrder(5), JsonPropertyName("bindingVersion"), YamlMember(Order = 5, Alias = "bindingVersion")]
    public virtual string? BindingVersion { get; set; } = "latest";

}
