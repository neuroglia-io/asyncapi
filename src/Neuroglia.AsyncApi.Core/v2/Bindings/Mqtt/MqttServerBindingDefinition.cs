using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.Mqtt;

/// <summary>
/// Represents the object used to configure an MQTT server binding
/// </summary>
[DataContract]
public record MqttServerBindingDefinition
    : MqttBindingDefinition, IServerBindingDefinition
{

    /// <summary>
    /// Gets/sets the client identifier.
    /// </summary>
    [DataMember(Order = 1, Name = "qos"), JsonPropertyOrder(1), JsonPropertyName("qos"), YamlMember(Order = 1, Alias = "qos")]
    public virtual string? ClientId { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether to create a persisten connection or not. When false, the connection will is persistent.
    /// </summary>
    [DataMember(Order = 2, Name = "cleanSession"), JsonPropertyOrder(2), JsonPropertyName("cleanSession"), YamlMember(Order = 2, Alias = "cleanSession")]
    public virtual bool CleanSession { get; set; }

    /// <summary>
    /// Gets/sets the last Will and Testament configuration.
    /// </summary>
    [DataMember(Order = 3, Name = "lastWill"), JsonPropertyOrder(3), JsonPropertyName("lastWill"), YamlMember(Order = 3, Alias = "lastWill")]
    public virtual MqttLastWillDefinition? LastWill { get; set; }

    /// <summary>
    /// Gets/sets an interval in seconds of the longest period of time the broker and the client can endure without sending a message.
    /// </summary>
    [DataMember(Order = 4, Name = "keepAlive"), JsonPropertyOrder(4), JsonPropertyName("keepAlive"), YamlMember(Order = 4, Alias = "keepAlive")]
    public virtual bool KeepAlive { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding. Defaults to 'latest'.
    /// </summary>
    [DataMember(Order = 5, Name = "bindingVersion"), JsonPropertyOrder(5), JsonPropertyName("bindingVersion"), YamlMember(Order = 5, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
