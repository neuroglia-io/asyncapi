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

namespace Neuroglia.AsyncApi.v2.Bindings.Solace;

/// <summary>
/// Represents an object used to configure a Solace destination
/// </summary>
[DataContract]
public record SolaceDestinationDefinition
{

    /// <summary>
    /// Gets/sets the version of this binding.
    /// </summary>
    [DataMember(Order = 1, Name = "bindingVersion"), JsonPropertyOrder(1), JsonPropertyName("bindingVersion"), YamlMember(Order = 1, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "0.4.0";

    /// <summary>
    /// Gets/sets the destination's type.
    /// </summary>
    [DataMember(Order = 2, Name = "destinationType"), JsonPropertyOrder(2), JsonPropertyName("destinationType"), YamlMember(Order = 2, Alias = "destinationType")]
    public virtual SolaceDestinationType DestinationType { get; set; }

    /// <summary>
    /// Gets/sets the destination's delivery mode.
    /// </summary>
    [DataMember(Order = 3, Name = "deliveryMode"), JsonPropertyOrder(3), JsonPropertyName("deliveryMode"), YamlMember(Order = 3, Alias = "deliveryMode")]
    public virtual SolaceDeliveryMode DeliveryMode { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the destination's queue, in case the destination type has been set to 'queue'.
    /// </summary>
    [DataMember(Order = 4, Name = "queue"), JsonPropertyOrder(4), JsonPropertyName("queue"), YamlMember(Order = 4, Alias = "queue")]
    public virtual SolaceDestinationQueueDefinition? Queue { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the destination's topic, in case the destination type has been set to 'topic'.
    /// </summary>
    [DataMember(Order = 5, Name = "topic"), JsonPropertyOrder(5), JsonPropertyName("topic"), YamlMember(Order = 5, Alias = "topic")]
    public virtual SolaceDestinationQueueDefinition? Topic { get; set; }

}
