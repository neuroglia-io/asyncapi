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

namespace Neuroglia.AsyncApi.v2.Bindings.Jms;

/// <summary>
/// Represents the object used to configure a JMS server binding
/// </summary>
[DataContract]
public record JmsServerBindingDefinition
    : JmsBindingDefinition, IServerBindingDefinition
{

    /// <summary>
    /// Gets/sets the classname of the ConnectionFactory implementation for the JMS Provider
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "jmsConnectionFactory"), JsonPropertyOrder(1), JsonPropertyName("jmsConnectionFactory"), YamlMember(Order = 1, Alias = "jmsConnectionFactory")]
    public virtual string JmsConnectionFactory { get; set; } = null!;

    /// <summary>
    /// Gets/sets additional properties to set on the JMS ConnectionFactory implementation for the JMS Provider.
    /// </summary>
    [DataMember(Order = 2, Name = "properties"), JsonPropertyOrder(2), JsonPropertyName("properties"), YamlMember(Order = 2, Alias = "properties")]
    public virtual EquatableList<JsonSchema>? Properties { get; set; }

    /// <summary>
    /// Gets/sets a client identifier for applications that use this JMS connection factory. If the Client ID Policy is set to 'Restricted' (the default), then configuring a Client ID on the ConnectionFactory prevents more than one JMS client from using a connection from this factory.
    /// </summary>
    [DataMember(Order = 3, Name = "clientID"), JsonPropertyOrder(3), JsonPropertyName("clientID"), YamlMember(Order = 3, Alias = "clientID")]
    public virtual string? ClientId { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding.
    /// </summary>
    [DataMember(Order = 4, Name = "bindingVersion"), JsonPropertyOrder(4), JsonPropertyName("bindingVersion"), YamlMember(Order = 4, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
