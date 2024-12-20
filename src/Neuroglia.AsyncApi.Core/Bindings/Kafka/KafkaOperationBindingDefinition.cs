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

using Json.Schema;
using Neuroglia.AsyncApi.Bindings;

namespace Neuroglia.AsyncApi.Bindings.Kafka;

/// <summary>
/// Represents the object used to configure a Kafka operation binding
/// </summary>
[DataContract]
public record KafkaOperationBindingDefinition
    : KafkaBindingDefinition, IOperationBindingDefinition
{

    /// <summary>
    /// Gets/sets the <see cref="JsonSchema"/> of the consumer group.
    /// </summary>
    [DataMember(Order = 1, Name = "groupId"), JsonPropertyOrder(1), JsonPropertyName("groupId"), YamlMember(Order = 1, Alias = "groupId")]
    public virtual JsonSchema? GroupId { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="JsonSchema"/> of the consumer inside a consumer group.
    /// </summary>
    [DataMember(Order = 2, Name = "clientId"), JsonPropertyOrder(2), JsonPropertyName("clientId"), YamlMember(Order = 2, Alias = "clientId")]
    public virtual JsonSchema? ClientId { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding. Defaults to 'latest'.
    /// </summary>
    [DataMember(Order = 3, Name = "bindingVersion"), JsonPropertyOrder(3), JsonPropertyName("groubindingVersionpId"), YamlMember(Order = 3, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
