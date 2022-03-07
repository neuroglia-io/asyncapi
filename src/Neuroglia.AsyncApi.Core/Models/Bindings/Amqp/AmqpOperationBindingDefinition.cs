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
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Models.Bindings.Amqp
{

    /// <summary>
    /// Represents the object used to configure an AMQP 0.9+ operation binding
    /// </summary>
    public class AmqpOperationBindingDefinition
        : AmqpBindingDefinition, IOperationBindingDefinition
    {

        /// <summary>
        /// Gets/sets the TTL (Time-To-Live) for the message. It MUST be greater than or equal to zero.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("expiration")]
        [YamlDotNet.Serialization.YamlMember(Alias = "expiration")]
        [System.Text.Json.Serialization.JsonPropertyName("expiration")]
        public virtual int Expiration { get; set; }

        /// <summary>
        /// Gets/sets a string that identifies the user who has sent the message.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("userId")]
        [YamlDotNet.Serialization.YamlMember(Alias = "userId")]
        [System.Text.Json.Serialization.JsonPropertyName("userId")]
        public virtual string UserId { get; set; }

        /// <summary>
        /// Gets/sets the routing keys the message should be routed to at the time of publishing.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("cc")]
        [YamlDotNet.Serialization.YamlMember(Alias = "cc")]
        [System.Text.Json.Serialization.JsonPropertyName("cc")]
        public virtual List<string> Cc { get; set; }

        /// <summary>
        /// Gets/sets a priority for the message.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("priority")]
        [YamlDotNet.Serialization.YamlMember(Alias = "priority")]
        [System.Text.Json.Serialization.JsonPropertyName("priority")]
        public virtual byte Priority { get; set; }

        /// <summary>
        /// Gets/sets the delivery mode of the message
        /// </summary>
        [Newtonsoft.Json.JsonProperty("deliveryMode")]
        [YamlDotNet.Serialization.YamlMember(Alias = "deliveryMode")]
        [System.Text.Json.Serialization.JsonPropertyName("deliveryMode")]
        public virtual AmqpDeliveryMode DeliveryMode { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether the message is mandatory or not.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("mandatory")]
        [YamlDotNet.Serialization.YamlMember(Alias = "mandatory")]
        [System.Text.Json.Serialization.JsonPropertyName("mandatory")]
        public virtual bool Mandatory { get; set; }

        /// <summary>
        /// Gets/sets the routing keys the message should be routed to at the time of publishing, undisclosed to consumers
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bcc")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bcc")]
        [System.Text.Json.Serialization.JsonPropertyName("bcc")]
        public virtual List<string> Bcc { get; set; }

        /// <summary>
        /// Gets/sets the name of the queue where the consumer should send the response.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("replyTo")]
        [YamlDotNet.Serialization.YamlMember(Alias = "replyTo")]
        [System.Text.Json.Serialization.JsonPropertyName("replyTo")]
        public virtual string ReplyTo { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether the message should include a timestamp or not.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("timestamp")]
        [YamlDotNet.Serialization.YamlMember(Alias = "timestamp")]
        [System.Text.Json.Serialization.JsonPropertyName("timestamp")]
        public virtual bool Timestamp { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether the consumer should ack the message or not.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("ack")]
        [YamlDotNet.Serialization.YamlMember(Alias = "ack")]
        [System.Text.Json.Serialization.JsonPropertyName("ack")]
        public virtual bool Ack { get; set; }

    }

}
