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

namespace Neuroglia.AsyncApi.v2.Bindings.IbmMQ;

/// <summary>
/// Represents the object used to configure an IBMMQ message binding
/// </summary>
[DataContract]
public record IbmMQMessageBindingDefinition
    : IbmMQBindingDefinition, IMessageBindingDefinition
{

    /// <summary>
    /// Gets/sets the type of the message.
    /// </summary>
    [DataMember(Order = 1, Name = "type"), JsonPropertyOrder(1), JsonPropertyName("type"), YamlMember(Order = 1, Alias = "type")]
    public virtual IbmMQMessageType Type { get; set; }

    /// <summary>
    /// Gets/sets a string that defines the IBM MQ message headers to include with this message. More than one header can be specified as a comma separated list. Supporting information on IBM MQ message formats can be found on this page in the IBM MQ Knowledge Center.
    /// </summary>
    [DataMember(Order = 2, Name = "headers"), JsonPropertyOrder(2), JsonPropertyName("headers"), YamlMember(Order = 2, Alias = "headers")]
    public virtual string? Headers { get; set; }

    /// <summary>
    /// Gets/sets additional information for application developers: describes the message type or format.
    /// </summary>
    [DataMember(Order = 3, Name = "description"), JsonPropertyOrder(3), JsonPropertyName("description"), YamlMember(Order = 3, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets the recommended setting the client should use for the TTL (Time-To-Live) of the message. This is a period of time expressed in milliseconds and set by the application that puts the message. expiry values are API dependant e.g., MQI and JMS use different units of time and default values for unlimited. General information on IBM MQ message expiry can be found on this page in the IBM MQ Knowledge Center.
    /// </summary>
    [DataMember(Order = 4, Name = "expiry"), JsonPropertyOrder(4), JsonPropertyName("expiry"), YamlMember(Order = 4, Alias = "expiry")]
    public virtual int? Expiry { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding.
    /// </summary>
    [DataMember(Order = 5, Name = "bindingVersion"), JsonPropertyOrder(5), JsonPropertyName("bindingVersion"), YamlMember(Order = 5, Alias = "bindingVersion")]
    public virtual string? BindingVersion { get; set; } = "latest";

}
