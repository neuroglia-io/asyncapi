using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.MqttV5;

/// <summary>
/// Represents the object used to configure an MQTT V5+ message binding
/// </summary>
[DataContract]
public record MqttV5MessageBindingDefinition
    : MqttV5BindingDefinition, IMessageBindingDefinition
{


}
