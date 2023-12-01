using Neuroglia.AsyncApi.v2;
using Neuroglia.AsyncApi.v2.Bindings.Amqp;
using Neuroglia.AsyncApi.v2.Bindings.AmqpV1;
using Neuroglia.AsyncApi.v2.Bindings.Http;
using Neuroglia.AsyncApi.v2.Bindings.Kafka;
using Neuroglia.AsyncApi.v2.Bindings.Mqtt;
using Neuroglia.AsyncApi.v2.Bindings.MqttV5;
using Neuroglia.AsyncApi.v2.Bindings.Nats;
using Neuroglia.AsyncApi.v2.Bindings.Redis;
using Neuroglia.AsyncApi.v2.Bindings.WebSockets;

namespace Neuroglia.AsyncApi.v2.Bindings;

/// <summary>
/// Represents the object used to configure a <see cref="MessageDefinition"/>'s <see cref="IMessageBindingDefinition"/>s
/// </summary>
[DataContract]
public record MessageBindingCollection
    : BindingDefinitionCollection<IMessageBindingDefinition>
{

    /// <summary>
    /// Gets/sets the protocol-specific information for an HTTP message.
    /// </summary>
    [DataMember(Order = 1, Name = "http"), JsonPropertyOrder(1), JsonPropertyName("http"), YamlMember(Order = 1, Alias = "http")]
    public virtual HttpMessageBindingDefinition? Http { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an WebSockets message.
    /// </summary>
    [DataMember(Order = 2, Name = "ws"), JsonPropertyOrder(2), JsonPropertyName("ws"), YamlMember(Order = 2, Alias = "ws")]
    public virtual WsMessageBindingDefinition? Ws { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for a Kafka message.
    /// </summary>
    [DataMember(Order = 3, Name = "kafka"), JsonPropertyOrder(3), JsonPropertyName("kafka"), YamlMember(Order = 3, Alias = "kafka")]
    public virtual KafkaMessageBindingDefinition? Kafka { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an AMQP 0-9-1 message.
    /// </summary>
    [DataMember(Order = 4, Name = "amqp"), JsonPropertyOrder(4), JsonPropertyName("amqp"), YamlMember(Order = 4, Alias = "amqp")]
    public virtual AmqpMessageBindingDefinition? Amqp { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for an AMQP 1.0 message.
    /// </summary>
    [DataMember(Order = 5, Name = "amqp1"), JsonPropertyOrder(5), JsonPropertyName("amqp1"), YamlMember(Order = 5, Alias = "amqp1")]
    public virtual AmqpV1MessageBindingDefinition? Amqp1 { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for an MQTT message.
    /// </summary>
    [DataMember(Order = 6, Name = "mqtt"), JsonPropertyOrder(6), JsonPropertyName("mqtt"), YamlMember(Order = 6, Alias = "mqtt")]
    public virtual MqttMessageBindingDefinition? Mqtt { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for an MQTT 5 message.
    /// </summary>
    [DataMember(Order = 7, Name = "mqtt5"), JsonPropertyOrder(7), JsonPropertyName("mqtt5"), YamlMember(Order = 7, Alias = "mqtt5")]
    public virtual MqttV5MessageBindingDefinition? Mqtt5 { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a NATS message.
    /// </summary>
    [DataMember(Order = 8, Name = "nats"), JsonPropertyOrder(8), JsonPropertyName("nats"), YamlMember(Order = 8, Alias = "nats")]
    public virtual NatsMessageBindingDefinition? Nats { get; set; }

    /// <summary>
    /// Gets/sets the protocol-specific information for an information for a Redis message.
    /// </summary>
    [DataMember(Order = 9, Name = "redis"), JsonPropertyOrder(9), JsonPropertyName("redis"), YamlMember(Order = 9, Alias = "redis")]
    public virtual RedisMessageBindingDefinition? Redis { get; set; }

    /// <inheritdoc/>
    public override IEnumerable<IMessageBindingDefinition> AsEnumerable()
    {
        if (Http != null) yield return Http;
        if (Ws != null) yield return Ws;
        if (Kafka != null) yield return Kafka;
        if (Amqp != null) yield return Amqp;
        if (Amqp1 != null) yield return Amqp1;
        if (Mqtt != null) yield return Mqtt;
        if (Mqtt5 != null) yield return Mqtt5;
        if (Nats != null) yield return Nats;
        if (Redis != null) yield return Redis;
    }

}
