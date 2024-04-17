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

using Neuroglia.AsyncApi.v2.Bindings.Solace;

namespace Neuroglia.AsyncApi.v2.Bindings.IbmMQ;

/// <summary>
/// Represents the object used to configure an IBMMQ channel binding
/// </summary>
[DataContract]
public record IbmMQChannelBindingDefinition
    : IbmMQBindingDefinition, IChannelBindingDefinition
{

    /// <summary>
    /// Gets/sets a logical group of IBM MQ server objects. This is necessary to specify multi-endpoint configurations used in high availability deployments. If omitted, the server object is not part of a group.
    /// </summary>
    [DataMember(Order = 1, Name = "destinationType"), JsonPropertyOrder(1), JsonPropertyName("destinationType"), YamlMember(Order = 1, Alias = "destinationType")]
    public virtual IbmMQDestinationType DestinationType { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the channel's queue, if destination type has been set to 'queue'.
    /// </summary>
    [DataMember(Order = 2, Name = "queue"), JsonPropertyOrder(2), JsonPropertyName("queue"), YamlMember(Order = 2, Alias = "queue")]
    public virtual IbmMQQueueDefinition? Queue { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the channel's topic, if destination type has been set to 'topic'.
    /// </summary>
    [DataMember(Order = 3, Name = "topic"), JsonPropertyOrder(3), JsonPropertyName("topic"), YamlMember(Order = 3, Alias = "topic")]
    public virtual IbmMQTopicDefinition? Topic { get; set; }

    /// <summary>
    /// Gets/sets the maximum length of the physical message (in bytes) accepted by the Topic or Queue. Messages produced that are greater in size than this value may fail to be delivered. More information on the maximum message length can be found on this page in the IBM MQ Knowledge Center.
    /// </summary>
    [DataMember(Order = 4, Name = "maxMsgLength"), JsonPropertyOrder(4), JsonPropertyName("maxMsgLength"), YamlMember(Order = 4, Alias = "maxMsgLength")]
    public virtual int? MaxMsgLength { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding.
    /// </summary>
    [DataMember(Order = 5, Name = "bindingVersion"), JsonPropertyOrder(5), JsonPropertyName("bindingVersion"), YamlMember(Order = 5, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
