/*
 * Copyright © 2021 Neuroglia SPRL. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

namespace Neuroglia.AsyncApi.Models.Bindings.Amqp
{
    /// <summary>
    /// Represents the object used to configure an AMQP channel binding
    /// </summary>
    public class AmqpChannelBindingDefinition
        : AmqpBindingDefinition, IChannelBindingDefinition
    {

        /// <summary>
        /// Gets/sets AMQP channel's type
        /// </summary>
        [Newtonsoft.Json.JsonProperty("is")]
        [YamlDotNet.Serialization.YamlMember(Alias = "is")]
        [System.Text.Json.Serialization.JsonPropertyName("is")]
        public virtual AmqpChannelType Type { get; set; }

        /// <summary>
        /// Gets/sets the object used to configure the AMQP exchange, when <see cref="Type"/> is set to <see cref="AmqpChannelType.RoutingKey"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty("exchange")]
        [YamlDotNet.Serialization.YamlMember(Alias = "exchange")]
        [System.Text.Json.Serialization.JsonPropertyName("exchange")]
        public virtual AmqpExchangeDefinition Exchange { get; set; }

        /// <summary>
        /// Gets/sets the object used to configure the AMQP queue, when <see cref="Type"/> is set to <see cref="AmqpChannelType.Queue"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty("queue")]
        [YamlDotNet.Serialization.YamlMember(Alias = "queue")]
        [System.Text.Json.Serialization.JsonPropertyName("queue")]
        public virtual AmqpQueueDefinition Queue { get; set; }

        /// <summary>
        /// Gets/sets the version of this binding. Defaults to 'latest'.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindingVersion")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindingVersion")]
        [System.Text.Json.Serialization.JsonPropertyName("bindingVersion")]
        public virtual string BindingVersion { get; set; } = "latest";

    }

}
