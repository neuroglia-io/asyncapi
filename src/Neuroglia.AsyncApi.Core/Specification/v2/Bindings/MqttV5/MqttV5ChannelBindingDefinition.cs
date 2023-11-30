using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.MqttV5;

/// <summary>
/// Represents the object used to configure an MQTT V5+ channel binding
/// </summary>
[DataContract]
public record MqttV5ChannelBindingDefinition
    : MqttV5BindingDefinition, IChannelBindingDefinition
{


}
