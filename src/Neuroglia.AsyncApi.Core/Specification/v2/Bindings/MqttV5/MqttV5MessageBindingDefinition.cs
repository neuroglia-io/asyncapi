namespace Neuroglia.AsyncApi.Specification.v2.Bindings.MqttV5;

/// <summary>
/// Represents the object used to configure an MQTT V5+ message binding
/// </summary>
[DataContract]
public record MqttV5MessageBindingDefinition
    : MqttV5BindingDefinition, IMessageBindingDefinition
{


}
