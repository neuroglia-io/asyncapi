using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.Kafka;

/// <summary>
/// Represents the object used to configure a Kafka channel binding
/// </summary>
[DataContract]
public record KafkaChannelBindingDefinition
    : KafkaBindingDefinition, IChannelBindingDefinition
{



}
