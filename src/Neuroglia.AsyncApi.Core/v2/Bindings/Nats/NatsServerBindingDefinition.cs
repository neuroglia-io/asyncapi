using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.Nats;

/// <summary>
/// Represents the object used to configure a <see href="https://nats.io/">NATS</see> server binding
/// </summary>
[DataContract]
public record NatsServerBindingDefinition
    : NatsBindingDefinition, IServerBindingDefinition
{



}
