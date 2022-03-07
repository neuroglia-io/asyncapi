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
using Neuroglia.AsyncApi.Models.Bindings;
using Neuroglia.AsyncApi.Models.Bindings.Amqp;
using Neuroglia.AsyncApi.Models.Bindings.AmqpV1;
using Neuroglia.AsyncApi.Models.Bindings.Http;
using Neuroglia.AsyncApi.Models.Bindings.Kafka;
using Neuroglia.AsyncApi.Models.Bindings.Mqtt;
using Neuroglia.AsyncApi.Models.Bindings.Mqtt5;
using Neuroglia.AsyncApi.Models.Bindings.Nats;
using Neuroglia.AsyncApi.Models.Bindings.Redis;
using Neuroglia.AsyncApi.Models.Bindings.WebSockets;
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Models
{
    /// <summary>
    /// Represents the object used to configure a <see cref="MessageDefinition"/>'s <see cref="IMessageBindingDefinition"/>s
    /// </summary>
    public class MessageBindingCollection
        : BindingDefinitionCollection<IMessageBindingDefinition>, IEnumerable<IMessageBindingDefinition>
    {

        /// <summary>
        /// Gets/sets the protocol-specific information for an HTTP server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("http")]
        [YamlDotNet.Serialization.YamlMember(Alias = "http")]
        [System.Text.Json.Serialization.JsonPropertyName("http")]
        public virtual HttpMessageBindingDefinition Http { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an WebSockets server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("ws")]
        [YamlDotNet.Serialization.YamlMember(Alias = "ws")]
        [System.Text.Json.Serialization.JsonPropertyName("ws")]
        public virtual WsMessageBindingDefinition Ws { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for a Kafka server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("kafka")]
        [YamlDotNet.Serialization.YamlMember(Alias = "kafka")]
        [System.Text.Json.Serialization.JsonPropertyName("kafka")]
        public virtual KafkaMessageBindingDefinition Kafka { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an AMQP 0-9-1 server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("amqp")]
        [YamlDotNet.Serialization.YamlMember(Alias = "amqp")]
        [System.Text.Json.Serialization.JsonPropertyName("amqp")]
        public virtual AmqpMessageBindingDefinition Amqp { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for an AMQP 1.0 server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("amqp1")]
        [YamlDotNet.Serialization.YamlMember(Alias = "amqp1")]
        [System.Text.Json.Serialization.JsonPropertyName("amqp1")]
        public virtual AmqpV1MessageBindingDefinition Amqp1 { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for an MQTT server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("mqtt")]
        [YamlDotNet.Serialization.YamlMember(Alias = "mqtt")]
        [System.Text.Json.Serialization.JsonPropertyName("mqtt")]
        public virtual MqttMessageBindingDefinition Mqtt { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for an MQTT 5 server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("mqtt5")]
        [YamlDotNet.Serialization.YamlMember(Alias = "mqtt5")]
        [System.Text.Json.Serialization.JsonPropertyName("mqtt5")]
        public virtual MqttV5MessageBindingDefinition Mqtt5 { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for a NATS server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("nats")]
        [YamlDotNet.Serialization.YamlMember(Alias = "nats")]
        [System.Text.Json.Serialization.JsonPropertyName("nats")]
        public virtual NatsMessageBindingDefinition Nats { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for a Redis server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("redis")]
        [YamlDotNet.Serialization.YamlMember(Alias = "redis")]
        [System.Text.Json.Serialization.JsonPropertyName("redis")]
        public virtual RedisMessageBindingDefinition Redis { get; set; }

    }

}
