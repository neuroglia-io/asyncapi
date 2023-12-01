using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.Mqtt;

/// <summary>
/// Represents the object used to configure an MQTT channel binding
/// </summary>
[DataContract]
public record MqttChannelBindingDefinition
    : MqttBindingDefinition, IChannelBindingDefinition
{



}
