using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Nats;

/// <summary>
/// Represents the object used to configure a <see href="https://nats.io/">NATS</see> channel binding
/// </summary>
[DataContract]
public record NatsChannelBindingDefinition
    : NatsBindingDefinition, IChannelBindingDefinition
{



}
