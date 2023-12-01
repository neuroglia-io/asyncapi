using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.MqttV5;

/// <summary>
/// Represents the object used to configure an MQTT V5+ server binding
/// </summary>
[DataContract]
public record MqttV5ServerBindingDefinition
    : MqttV5BindingDefinition, IServerBindingDefinition
{


}
