using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.AmqpV1;

/// <summary>
/// Represents the object used to configure an AMQP 1.0 channel binding
/// </summary>
[DataContract]
public record AmqpV1ChannelBindingDefinition
    : AmqpV1BindingDefinition, IChannelBindingDefinition
{



}
