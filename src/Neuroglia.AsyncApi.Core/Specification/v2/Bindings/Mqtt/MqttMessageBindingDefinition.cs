using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Mqtt;

/// <summary>
/// Represents the object used to configure an MQTT message binding
/// </summary>
[DataContract]
public record MqttMessageBindingDefinition
    : MqttBindingDefinition, IMessageBindingDefinition
{

    /// <summary>
    /// Gets/sets the version of this binding. Defaults to 'latest'.
    /// </summary>
    [DataMember(Order = 1, Name = "bindingVersion"), JsonPropertyOrder(1), JsonPropertyName("bindingVersion"), YamlMember(Order = 1, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
