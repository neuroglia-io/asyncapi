namespace Neuroglia.AsyncApi.v2.Bindings.Mqtt;

/// <summary>
/// Represents an object used to configure an <see cref="MqttServerBindingDefinition"/>'s last Will and Testament
/// </summary>
[DataContract]
public record MqttLastWillDefinition
{

    /// <summary>
    /// Gets/sets the topic where the Last Will and Testament message will be sent.
    /// </summary>
    [DataMember(Order = 1, Name = "topic"), JsonPropertyOrder(1), JsonPropertyName("topic"), YamlMember(Order = 1, Alias = "topic")]
    public virtual string? Topic { get; set; }

    /// <summary>
    /// Gets/sets an integer that defines how hard the broker/client will try to ensure that the Last Will and Testament message is received. Its value MUST be either 0, 1 or 2.
    /// </summary>
    [DataMember(Order = 2, Name = "qos"), JsonPropertyOrder(2), JsonPropertyName("qos"), YamlMember(Order = 2, Alias = "qos")]
    public virtual MqttQoSLevel QoS { get; set; }

    /// <summary>
    /// Gets/sets the Last Will message
    /// </summary>
    [DataMember(Order = 3, Name = "message"), JsonPropertyOrder(3), JsonPropertyName("message"), YamlMember(Order = 3, Alias = "message")]
    public virtual string? Message { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the broker should retain the Last Will and Testament message or not.
    /// </summary>
    [DataMember(Order = 7, Name = "retain"), JsonPropertyOrder(7), JsonPropertyName("retain"), YamlMember(Order = 7, Alias = "retain")]
    public virtual bool Retain { get; set; }

}
