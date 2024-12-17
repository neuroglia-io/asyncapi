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

using Neuroglia.AsyncApi.Bindings.Amqp;
using Neuroglia.AsyncApi.Bindings.AmqpV1;
using Neuroglia.AsyncApi.Bindings.AnypointMQ;
using Neuroglia.AsyncApi.Bindings.GooglePubSub;
using Neuroglia.AsyncApi.Bindings.Http;
using Neuroglia.AsyncApi.Bindings.IbmMQ;
using Neuroglia.AsyncApi.Bindings.Jms;
using Neuroglia.AsyncApi.Bindings.Kafka;
using Neuroglia.AsyncApi.Bindings.Mercure;
using Neuroglia.AsyncApi.Bindings.Mqtt;
using Neuroglia.AsyncApi.Bindings.MqttV5;
using Neuroglia.AsyncApi.Bindings.Nats;
using Neuroglia.AsyncApi.Bindings.Pulsar;
using Neuroglia.AsyncApi.Bindings.Redis;
using Neuroglia.AsyncApi.Bindings.Sns;
using Neuroglia.AsyncApi.Bindings.Solace;
using Neuroglia.AsyncApi.Bindings.Sqs;
using Neuroglia.AsyncApi.Bindings.Stomp;
using Neuroglia.AsyncApi.Bindings.WebSockets;
using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.Bindings;

/// <summary>
/// Represents the object used to configure a <see cref="V2ChannelDefinition"/>'s <see cref="IChannelBindingDefinition"/>s
/// </summary>
[DataContract]
public record ChannelBindingDefinitionCollection
    : BindingDefinitionCollection<IChannelBindingDefinition>
{

    /// <summary>
    /// Gets/sets the protocol-specific information for an HTTP channel.
    /// </summary>
    [DataMember(Order = 1, Name = "http"), JsonPropertyOrder(1), JsonPropertyName("http"), YamlMember(Order = 1, Alias = "http")]
    public virtual HttpChannelBindingDefinition? Http { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an WebSockets channel.
    /// </summary>
    [DataMember(Order = 2, Name = "ws"), JsonPropertyOrder(2), JsonPropertyName("ws"), YamlMember(Order = 2, Alias = "ws")]
    public virtual WsChannelBindingDefinition? Ws { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for a Kafka channel.
    /// </summary>
    [DataMember(Order = 3, Name = "kafka"), JsonPropertyOrder(3), JsonPropertyName("kafka"), YamlMember(Order = 3, Alias = "kafka")]
    public virtual KafkaChannelBindingDefinition? Kafka { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for a Anypoint MQ channel.
    /// </summary>
    [DataMember(Order = 4, Name = "anypointmq"), JsonPropertyOrder(4), JsonPropertyName("anypointmq"), YamlMember(Order = 4, Alias = "anypointmq")]
    public virtual AnypointMQChannelBindingDefinition? AnypointMQ { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an AMQP 0-9-1 channel.
    /// </summary>
    [DataMember(Order = 5, Name = "amqp"), JsonPropertyOrder(5), JsonPropertyName("amqp"), YamlMember(Order = 5, Alias = "amqp")]
    public virtual AmqpChannelBindingDefinition? Amqp { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for an AMQP 1.0 channel.
    /// </summary>
    [DataMember(Order = 6, Name = "amqp1"), JsonPropertyOrder(6), JsonPropertyName("amqp1"), YamlMember(Order = 6, Alias = "amqp1")]
    public virtual AmqpV1ChannelBindingDefinition? Amqp1 { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for an MQTT channel.
    /// </summary>
    [DataMember(Order = 7, Name = "mqtt"), JsonPropertyOrder(7), JsonPropertyName("mqtt"), YamlMember(Order = 7, Alias = "mqtt")]
    public virtual MqttChannelBindingDefinition? Mqtt { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for an MQTT 5 channel.
    /// </summary>
    [DataMember(Order = 8, Name = "mqtt5"), JsonPropertyOrder(8), JsonPropertyName("mqtt5"), YamlMember(Order = 8, Alias = "mqtt5")]
    public virtual MqttV5ChannelBindingDefinition? Mqtt5 { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a NATS channel.
    /// </summary>
    [DataMember(Order = 9, Name = "nats"), JsonPropertyOrder(9), JsonPropertyName("nats"), YamlMember(Order = 9, Alias = "nats")]
    public virtual NatsChannelBindingDefinition? Nats { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a JMS channel.
    /// </summary>
    [DataMember(Order = 10, Name = "jms"), JsonPropertyOrder(10), JsonPropertyName("jms"), YamlMember(Order = 10, Alias = "jms")]
    public virtual JmsChannelBindingDefinition? Jms { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a SNS channel.
    /// </summary>
    [DataMember(Order = 11, Name = "sns"), JsonPropertyOrder(11), JsonPropertyName("sns"), YamlMember(Order = 11, Alias = "sns")]
    public virtual SnsChannelBindingDefinition? Sns { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a Solace channel.
    /// </summary>
    [DataMember(Order = 12, Name = "solace"), JsonPropertyOrder(12), JsonPropertyName("solace"), YamlMember(Order = 12, Alias = "solace")]
    public virtual SolaceChannelBindingDefinition? Solace { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a SQS channel.
    /// </summary>
    [DataMember(Order = 13, Name = "sqs"), JsonPropertyOrder(13), JsonPropertyName("sqs"), YamlMember(Order = 13, Alias = "sqs")]
    public virtual SqsChannelBindingDefinition? Sqs { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a STOMP channel.
    /// </summary>
    [DataMember(Order = 14, Name = "stomp"), JsonPropertyOrder(14), JsonPropertyName("stomp"), YamlMember(Order = 14, Alias = "stomp")]
    public virtual StompChannelBindingDefinition? Stomp { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a Redis channel.
    /// </summary>
    [DataMember(Order = 15, Name = "redis"), JsonPropertyOrder(15), JsonPropertyName("redis"), YamlMember(Order = 15, Alias = "redis")]
    public virtual RedisChannelBindingDefinition? Redis { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a Redis channel.
    /// </summary>
    [DataMember(Order = 16, Name = "mercure"), JsonPropertyOrder(16), JsonPropertyName("mercure"), YamlMember(Order = 16, Alias = "mercure")]
    public virtual MercureChannelBindingDefinition? Mercure { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for an IBM MQ channel.
    /// </summary>
    [DataMember(Order = 17, Name = "ibmmq"), JsonPropertyOrder(17), JsonPropertyName("ibmmq"), YamlMember(Order = 17, Alias = "ibmmq")]
    public virtual IbmMQChannelBindingDefinition? IbmMQ { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a Google Cloud Pub/Sub channel.
    /// </summary>
    [DataMember(Order = 18, Name = "googlepubsub"), JsonPropertyOrder(18), JsonPropertyName("googlepubsub"), YamlMember(Order = 18, Alias = "googlepubsub")]
    public virtual GooglePubSubChannelBindingDefinition? GooglePubSub { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a Google Cloud Pub/Sub channel.
    /// </summary>
    [DataMember(Order = 19, Name = "pulsar"), JsonPropertyOrder(19), JsonPropertyName("pulsar"), YamlMember(Order = 19, Alias = "pulsar")]
    public virtual PulsarChannelBindingDefinition? Pulsar { get; set; }

    /// <inheritdoc/>
    public override IEnumerable<IChannelBindingDefinition> AsEnumerable()
    {
        if (Http != null) yield return Http;
        if (Ws != null) yield return Ws;
        if (Kafka != null) yield return Kafka;
        if (AnypointMQ != null) yield return AnypointMQ;
        if (Amqp != null) yield return Amqp;
        if (Amqp1 != null) yield return Amqp1;
        if (Mqtt != null) yield return Mqtt;
        if (Mqtt5 != null) yield return Mqtt5;
        if (Nats != null) yield return Nats;
        if (Jms != null) yield return Jms;
        if (Sns != null) yield return Sns;
        if (Solace != null) yield return Solace;
        if (Sqs != null) yield return Sqs;
        if (Stomp != null) yield return Stomp;
        if (Redis != null) yield return Redis;
        if (Mercure != null) yield return Mercure;
        if (IbmMQ != null) yield return IbmMQ;
        if (GooglePubSub != null) yield return GooglePubSub;
        if (Pulsar != null) yield return Pulsar;
    }

}
