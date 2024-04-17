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

namespace Neuroglia.AsyncApi.v2.Bindings.AnypointMQ;

/// <summary>
/// Represents the object used to configure an Anypoint MQ channel binding
/// </summary>
[DataContract]
public record AnypointMQChannelBindingDefinition
    : AnypointMQBindingDefinition, IChannelBindingDefinition
{

    /// <summary>
    /// Gets/sets the destination (queue or exchange) name for this channel. SHOULD only be specified if the channel name differs from the actual destination name, such as when the channel name is not a valid destination name in Anypoint MQ.
    /// </summary>
    [DataMember(Order = 1, Name = "destination"), JsonPropertyOrder(1), JsonPropertyName("destination"), YamlMember(Order = 1, Alias = "destination")]
    public virtual string? Destination { get; set; }

    /// <summary>
    /// Gets/sets the type of destination, which MUST be either exchange or queue or fifo-queue. SHOULD be specified to document the messaging model (publish/subscribe, point-to-point, strict message ordering) supported by this channel.
    /// </summary>
    [DataMember(Order = 2, Name = "destinationType"), JsonPropertyOrder(2), JsonPropertyName("destinationType"), YamlMember(Order = 2, Alias = "destinationType")]
    public virtual AnypointMQDestinationType DestinationType { get; set; } = AnypointMQDestinationType.Queue;

    /// <summary>
    /// Gets/sets the version of this binding.
    /// </summary>
    [DataMember(Order = 3, Name = "bindingVersion"), JsonPropertyOrder(3), JsonPropertyName("bindingVersion"), YamlMember(Order = 3, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
