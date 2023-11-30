using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.AmqpV1;

/// <summary>
/// Represents the object used to configure an AMQP 1.0 server binding
/// </summary>
[DataContract]
public record AmqpV1ServerBindingDefinition
    : AmqpV1BindingDefinition, IServerBindingDefinition
{



}
