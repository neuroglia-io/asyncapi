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

namespace Neuroglia.AsyncApi.Bindings.Amqp;

/// <summary>
/// Represents the object used to configure an AMQP channel binding
/// </summary>
[DataContract]
public record AmqpChannelBindingDefinition
    : AmqpBindingDefinition, IChannelBindingDefinition
{

    /// <summary>
    /// Gets/sets AMQP channel's type
    /// </summary>
    [DataMember(Order = 1, Name = "is"), JsonPropertyOrder(1), JsonPropertyName("is"), YamlMember(Order = 1, Alias = "is")]
    public virtual AmqpChannelType Type { get; set; }

    /// <summary>
    /// Gets/sets the object used to configure the AMQP exchange, when <see cref="Type"/> is set to <see cref="AmqpChannelType.RoutingKey"/>
    /// </summary>
    [DataMember(Order = 2, Name = "exchange"), JsonPropertyOrder(2), JsonPropertyName("exchange"), YamlMember(Order = 2, Alias = "exchange")]
    public virtual AmqpExchangeDefinition? Exchange { get; set; }

    /// <summary>
    /// Gets/sets the object used to configure the AMQP queue, when <see cref="Type"/> is set to <see cref="AmqpChannelType.Queue"/>
    /// </summary>
    [DataMember(Order = 3, Name = "queue"), JsonPropertyOrder(3), JsonPropertyName("queue"), YamlMember(Order = 3, Alias = "queue")]
    public virtual AmqpQueueDefinition? Queue { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding. Defaults to 'latest'.
    /// </summary>
    [DataMember(Order = 4, Name = "bindingVersion"), JsonPropertyOrder(4), JsonPropertyName("bindingVersion"), YamlMember(Order = 4, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
