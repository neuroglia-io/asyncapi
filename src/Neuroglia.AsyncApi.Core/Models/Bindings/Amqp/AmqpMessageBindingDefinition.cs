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
    /// Represents the object used to configure an AMQP 0.9+ message binding
    /// </summary>
    public class AmqpMessageBindingDefinition
        : AmqpBindingDefinition, IMessageBindingDefinition
    {

        /// <summary>
        /// Gets/sets a MIME encoding for the message content.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("contentEncoding")]
        [YamlDotNet.Serialization.YamlMember(Alias = "contentEncoding")]
        [System.Text.Json.Serialization.JsonPropertyName("contentEncoding")]
        public virtual string ContentEncoding { get; set; }

        /// <summary>
        /// Gets/sets a string that represents an application-specific message type.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("messageType")]
        [YamlDotNet.Serialization.YamlMember(Alias = "messageType")]
        [System.Text.Json.Serialization.JsonPropertyName("messageType")]
        public virtual string MessageType { get; set; }

    }

}
