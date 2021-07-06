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
using Neuroglia.AsyncApi.Sdk.Models.Bindings;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.Amqp;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.AmqpV1;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.Http;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.Kafka;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.Mqtt;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.Mqtt5;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.Nats;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.Redis;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.WebSockets;
using System.Collections;
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Sdk.Models
{

    /// <summary>
    /// Represents the object used to configure a <see cref="OperationDefinition"/>'s <see cref="IOperationBinding"/>s
    /// </summary>
    public class OperationBindingCollection
        : IEnumerable<IOperationBinding>
    {

        /// <summary>
        /// Gets/sets the protocol-specific information for an HTTP server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("http")]
        [YamlDotNet.Serialization.YamlMember(Alias = "http")]
        [System.Text.Json.Serialization.JsonPropertyName("http")]
        public virtual HttpOperationBinding Http { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an WebSockets server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("ws")]
        [YamlDotNet.Serialization.YamlMember(Alias = "ws")]
        [System.Text.Json.Serialization.JsonPropertyName("ws")]
        public virtual WsOperationBinding Ws { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for a Kafka server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("kafka")]
        [YamlDotNet.Serialization.YamlMember(Alias = "kafka")]
        [System.Text.Json.Serialization.JsonPropertyName("kafka")]
        public virtual KafkaOperationBinding Kafka { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an AMQP 0-9-1 server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("amqp")]
        [YamlDotNet.Serialization.YamlMember(Alias = "amqp")]
        [System.Text.Json.Serialization.JsonPropertyName("amqp")]
        public virtual AmqpOperationBinding Amqp { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for an AMQP 1.0 server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("amqp1")]
        [YamlDotNet.Serialization.YamlMember(Alias = "amqp1")]
        [System.Text.Json.Serialization.JsonPropertyName("amqp1")]
        public virtual AmqpV1OperationBinding Amqp1 { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for an MQTT server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("mqtt")]
        [YamlDotNet.Serialization.YamlMember(Alias = "mqtt")]
        [System.Text.Json.Serialization.JsonPropertyName("mqtt")]
        public virtual MqttOperationBinding Mqtt { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for an MQTT 5 server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("mqtt5")]
        [YamlDotNet.Serialization.YamlMember(Alias = "mqtt5")]
        [System.Text.Json.Serialization.JsonPropertyName("mqtt5")]
        public virtual MqttV5OperationBinding Mqtt5 { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for a NATS server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("nats")]
        [YamlDotNet.Serialization.YamlMember(Alias = "nats")]
        [System.Text.Json.Serialization.JsonPropertyName("nats")]
        public virtual NatsOperationBinding Nats { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for a Redis server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("redis")]
        [YamlDotNet.Serialization.YamlMember(Alias = "redis")]
        [System.Text.Json.Serialization.JsonPropertyName("redis")]
        public virtual RedisOperationBinding Redis { get; set; }

        /// <inheritdoc/>
        public virtual IEnumerator<IOperationBinding> GetEnumerator()
        {
            if (this.Http != null)
                yield return this.Http;
            if (this.Ws != null)
                yield return this.Ws;
            if (this.Amqp != null)
                yield return this.Amqp;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }

}
