using Neuroglia.AsyncApi.v2;
using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.Nats;

/// <summary>
/// Represents the base record for all NATS implementations of the <see cref="IBindingDefinition"/> interface
/// </summary>
[DataContract]
public abstract record NatsBindingDefinition
    : IBindingDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public IEnumerable<string> Protocols
    {
        get
        {
            yield return AsyncApiProtocol.Nats;
        }
    }

}
