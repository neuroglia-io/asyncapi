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
using System.ComponentModel.DataAnnotations;

namespace Neuroglia.AsyncApi.Models.Bindings.Mqtt
{

    /// <summary>
    /// Represents an object used to configure an <see cref="MqttServerBindingDefinition"/>'s last Will and Testament
    /// </summary>
    public class MqttLastWillDefinition
    {

        /// <summary>
        /// Gets/sets the topic where the Last Will and Testament message will be sent.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("topic")]
        [YamlDotNet.Serialization.YamlMember(Alias = "topic")]
        [System.Text.Json.Serialization.JsonPropertyName("topic")]
        public virtual string Topic { get; set; }

        /// <summary>
        /// Gets/sets an integer that defines how hard the broker/client will try to ensure that the Last Will and Testament message is received. Its value MUST be either 0, 1 or 2.
        /// </summary>
        [Range(0, 2)]
        [Newtonsoft.Json.JsonProperty("qos")]
        [YamlDotNet.Serialization.YamlMember(Alias = "qos")]
        [System.Text.Json.Serialization.JsonPropertyName("qos")]
        public virtual MqttQoSLevel QoS { get; set; }

        /// <summary>
        /// Gets/sets the Last Will message
        /// </summary>
        [Newtonsoft.Json.JsonProperty("message")]
        [YamlDotNet.Serialization.YamlMember(Alias = "message")]
        [System.Text.Json.Serialization.JsonPropertyName("message")]
        public virtual string Message { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether the broker should retain the Last Will and Testament message or not.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("retain")]
        [YamlDotNet.Serialization.YamlMember(Alias = "retain")]
        [System.Text.Json.Serialization.JsonPropertyName("retain")]
        public virtual bool Retain { get; set; }

    }

}
