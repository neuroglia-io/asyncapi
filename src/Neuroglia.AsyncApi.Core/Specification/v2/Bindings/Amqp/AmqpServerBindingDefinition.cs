using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Amqp;

/// <summary>
/// Represents the object used to configure an AMQP 0.9+ server binding
/// </summary>
[DataContract]
public record AmqpServerBindingDefinition
    : AmqpBindingDefinition, IServerBindingDefinition
{



}
