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

namespace Neuroglia.AsyncApi.Bindings.Mqtt;

/// <summary>
/// Represents the object used to configure an MQTT message binding
/// </summary>
[DataContract]
public record MqttMessageBindingDefinition
    : MqttBindingDefinition, IMessageBindingDefinition
{

    /// <summary>
    /// Gets/sets an integer that indicates that the payload is unspecified bytes, if set to 0, or that the payload is UTF-8 encoded character dat, if set to 1
    /// </summary>
    [DataMember(Order = 1, Name = "payloadFormatIndicator"), JsonPropertyOrder(1), JsonPropertyName("payloadFormatIndicator"), YamlMember(Order = 1, Alias = "payloadFormatIndicator")]
    public virtual int PayloadFormatIndicator { get; set; }

    /// <summary>
    /// Gets/sets the data used by the sender of the request message to identify which request the response message is for when it is received.
    /// </summary>
    [DataMember(Order = 2, Name = "correlationData"), JsonPropertyOrder(2), JsonPropertyName("correlationData"), YamlMember(Order = 2, Alias = "correlationData")]
    public virtual JsonSchema? CorrelationData { get; set; }

    /// <summary>
    /// Gets/sets the content type of the message payload. This should not conflict with the contentType field of the associated AsyncAPI Message object.
    /// </summary>
    [DataMember(Order = 3, Name = "contentType"), JsonPropertyOrder(3), JsonPropertyName("contentType"), YamlMember(Order = 3, Alias = "contentType")]
    public virtual string? ContentType { get; set; }

    /// <summary>
    /// Gets/sets the topic (channel URI) for a response message.
    /// </summary>
    [DataMember(Order = 4, Name = "responseTopic"), JsonPropertyOrder(4), JsonPropertyName("responseTopic"), YamlMember(Order = 4, Alias = "responseTopic")]
    public virtual string? ResponseTopic { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding. Defaults to 'latest'.
    /// </summary>
    [DataMember(Order = 5, Name = "bindingVersion"), JsonPropertyOrder(5), JsonPropertyName("bindingVersion"), YamlMember(Order = 5, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
