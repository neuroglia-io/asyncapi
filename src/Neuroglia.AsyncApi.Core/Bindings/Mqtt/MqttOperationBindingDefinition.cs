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

using Neuroglia.AsyncApi.Bindings;

namespace Neuroglia.AsyncApi.Bindings.Mqtt;

/// <summary>
/// Represents the object used to configure an MQTT operation binding
/// </summary>
[DataContract]
public record MqttOperationBindingDefinition
    : MqttBindingDefinition, IOperationBindingDefinition
{

    /// <summary>
    /// Gets/sets an integer that defines the Quality of Service (QoS) levels for the message flow between client and server. Its value MUST be either 0 (At most once delivery), 1 (At least once delivery), or 2 (Exactly once delivery).
    /// </summary>
    [DataMember(Order = 1, Name = "qos"), JsonPropertyOrder(1), JsonPropertyName("qos"), YamlMember(Order = 1, Alias = "qos")]
    public virtual MqttQoSLevel QoS { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the broker the broker should retain the message or not.
    /// </summary>
    [DataMember(Order = 2, Name = "retain"), JsonPropertyOrder(2), JsonPropertyName("retain"), YamlMember(Order = 2, Alias = "retain")]
    public virtual bool Retain { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding. Defaults to 'latest'.
    /// </summary>
    [DataMember(Order = 3, Name = "bindingVersion"), JsonPropertyOrder(3), JsonPropertyName("bindingVersion"), YamlMember(Order = 3, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
