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

using Neuroglia.AsyncApi.v2.Bindings.Amqp;
using Neuroglia.AsyncApi.v2.Bindings.AmqpV1;
using Neuroglia.AsyncApi.v2.Bindings.AnypointMQ;
using Neuroglia.AsyncApi.v2.Bindings.GooglePubSub;
using Neuroglia.AsyncApi.v2.Bindings.Http;
using Neuroglia.AsyncApi.v2.Bindings.IbmMQ;
using Neuroglia.AsyncApi.v2.Bindings.Jms;
using Neuroglia.AsyncApi.v2.Bindings.Kafka;
using Neuroglia.AsyncApi.v2.Bindings.Mercure;
using Neuroglia.AsyncApi.v2.Bindings.Mqtt;
using Neuroglia.AsyncApi.v2.Bindings.MqttV5;
using Neuroglia.AsyncApi.v2.Bindings.Nats;
using Neuroglia.AsyncApi.v2.Bindings.Pulsar;
using Neuroglia.AsyncApi.v2.Bindings.Redis;
using Neuroglia.AsyncApi.v2.Bindings.Sns;
using Neuroglia.AsyncApi.v2.Bindings.Solace;
using Neuroglia.AsyncApi.v2.Bindings.Sqs;
using Neuroglia.AsyncApi.v2.Bindings.Stomp;
using Neuroglia.AsyncApi.v2.Bindings.WebSockets;

namespace Neuroglia.AsyncApi.v2.Bindings;

/// <summary>
/// Represents the object used to configure a <see cref="ServerDefinition"/>'s <see cref="IServerBindingDefinition"/>s
/// </summary>
[DataContract]
public record ServerBindingDefinitionCollection
    : BindingDefinitionCollection<IServerBindingDefinition>
{

    /// <summary>
    /// Gets/sets the protocol-specific information for an HTTP server.
    /// </summary>
    [DataMember(Order = 1, Name = "http"), JsonPropertyOrder(1), JsonPropertyName("http"), YamlMember(Order = 1, Alias = "http")]
    public virtual HttpServerBindingDefinition? Http { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an WebSockets server.
    /// </summary>
    [DataMember(Order = 2, Name = "ws"), JsonPropertyOrder(2), JsonPropertyName("ws"), YamlMember(Order = 2, Alias = "ws")]
    public virtual WsServerBindingDefinition? Ws { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for a Kafka server.
    /// </summary>
    [DataMember(Order = 3, Name = "kafka"), JsonPropertyOrder(3), JsonPropertyName("kafka"), YamlMember(Order = 3, Alias = "kafka")]
    public virtual KafkaServerBindingDefinition? Kafka { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for a Anypoint MQ server.
    /// </summary>
    [DataMember(Order = 4, Name = "anypointmq"), JsonPropertyOrder(4), JsonPropertyName("anypointmq"), YamlMember(Order = 4, Alias = "anypointmq")]
    public virtual AnypointMQServerBindingDefinition? AnypointMQ { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an AMQP 0-9-1 server.
    /// </summary>
    [DataMember(Order = 5, Name = "amqp"), JsonPropertyOrder(5), JsonPropertyName("amqp"), YamlMember(Order = 5, Alias = "amqp")]
    public virtual AmqpServerBindingDefinition? Amqp { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for an AMQP 1.0 server.
    /// </summary>
    [DataMember(Order = 6, Name = "amqp1"), JsonPropertyOrder(6), JsonPropertyName("amqp1"), YamlMember(Order = 6, Alias = "amqp1")]
    public virtual AmqpV1ServerBindingDefinition? Amqp1 { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for an MQTT server.
    /// </summary>
    [DataMember(Order = 7, Name = "mqtt"), JsonPropertyOrder(7), JsonPropertyName("mqtt"), YamlMember(Order = 7, Alias = "mqtt")]
    public virtual MqttServerBindingDefinition? Mqtt { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for an MQTT 5 server.
    /// </summary>
    [DataMember(Order = 8, Name = "mqtt5"), JsonPropertyOrder(8), JsonPropertyName("mqtt5"), YamlMember(Order = 8, Alias = "mqtt5")]
    public virtual MqttV5ServerBindingDefinition? Mqtt5 { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a NATS server.
    /// </summary>
    [DataMember(Order = 9, Name = "nats"), JsonPropertyOrder(9), JsonPropertyName("nats"), YamlMember(Order = 9, Alias = "nats")]
    public virtual NatsServerBindingDefinition? Nats { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a JMS server.
    /// </summary>
    [DataMember(Order = 10, Name = "jms"), JsonPropertyOrder(10), JsonPropertyName("jms"), YamlMember(Order = 10, Alias = "jms")]
    public virtual JmsServerBindingDefinition? Jms { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a SNS server.
    /// </summary>
    [DataMember(Order = 11, Name = "sns"), JsonPropertyOrder(11), JsonPropertyName("sns"), YamlMember(Order = 11, Alias = "sns")]
    public virtual SnsServerBindingDefinition? Sns { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a Solace server.
    /// </summary>
    [DataMember(Order = 12, Name = "solace"), JsonPropertyOrder(12), JsonPropertyName("solace"), YamlMember(Order = 12, Alias = "solace")]
    public virtual SolaceServerBindingDefinition? Solace { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a SQS server.
    /// </summary>
    [DataMember(Order = 13, Name = "sqs"), JsonPropertyOrder(13), JsonPropertyName("sqs"), YamlMember(Order = 13, Alias = "sqs")]
    public virtual SqsServerBindingDefinition? Sqs { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a STOMP server.
    /// </summary>
    [DataMember(Order = 14, Name = "stomp"), JsonPropertyOrder(14), JsonPropertyName("stomp"), YamlMember(Order = 14, Alias = "stomp")]
    public virtual StompServerBindingDefinition? Stomp { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a Redis server.
    /// </summary>
    [DataMember(Order = 15, Name = "redis"), JsonPropertyOrder(15), JsonPropertyName("redis"), YamlMember(Order = 15, Alias = "redis")]
    public virtual RedisServerBindingDefinition? Redis { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a Redis server.
    /// </summary>
    [DataMember(Order = 16, Name = "mercure"), JsonPropertyOrder(16), JsonPropertyName("mercure"), YamlMember(Order = 16, Alias = "mercure")]
    public virtual MercureServerBindingDefinition? Mercure { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for an IBM MQ server.
    /// </summary>
    [DataMember(Order = 17, Name = "ibmmq"), JsonPropertyOrder(17), JsonPropertyName("ibmmq"), YamlMember(Order = 17, Alias = "ibmmq")]
    public virtual IbmMQServerBindingDefinition? IbmMQ { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a Google Cloud Pub/Sub server.
    /// </summary>
    [DataMember(Order = 18, Name = "googlepubsub"), JsonPropertyOrder(18), JsonPropertyName("googlepubsub"), YamlMember(Order = 18, Alias = "googlepubsub")]
    public virtual GooglePubSubServerBindingDefinition? GooglePubSub { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a Google Cloud Pub/Sub server.
    /// </summary>
    [DataMember(Order = 19, Name = "pulsar"), JsonPropertyOrder(19), JsonPropertyName("pulsar"), YamlMember(Order = 19, Alias = "pulsar")]
    public virtual PulsarServerBindingDefinition? Pulsar { get; set; }

    /// <inheritdoc/>
    public override IEnumerable<IServerBindingDefinition> AsEnumerable()
    {
        if (this.Http != null) yield return this.Http;
        if (this.Ws != null) yield return this.Ws;
        if (this.Kafka != null) yield return this.Kafka;
        if (this.AnypointMQ != null) yield return this.AnypointMQ;
        if (this.Amqp != null) yield return this.Amqp;
        if (this.Amqp1 != null) yield return this.Amqp1;
        if (this.Mqtt != null) yield return this.Mqtt;
        if (this.Mqtt5 != null) yield return this.Mqtt5;
        if (this.Nats != null) yield return this.Nats;
        if (this.Jms != null) yield return this.Jms;
        if (this.Sns != null) yield return this.Sns;
        if (this.Solace != null) yield return this.Solace;
        if (this.Sqs != null) yield return this.Sqs;
        if (this.Stomp != null) yield return this.Stomp;
        if (this.Redis != null) yield return this.Redis;
        if (this.Mercure != null) yield return this.Mercure;
        if (this.IbmMQ != null) yield return this.IbmMQ;
        if (this.GooglePubSub != null) yield return this.GooglePubSub;
        if (this.Pulsar != null) yield return this.Pulsar;
    }

}
