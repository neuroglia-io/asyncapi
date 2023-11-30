using Neuroglia.AsyncApi.Specification.v2;
using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.AmqpV1;

/// <summary>
/// Represents the base record for all AMQP V1 implementations of the <see cref="IBindingDefinition"/> interface
/// </summary>
[DataContract]
public abstract record AmqpV1BindingDefinition
    : IBindingDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public IEnumerable<string> Protocols
    {
        get
        {
            yield return AsyncApiProtocol.AmqpV1;
        }
    }

}
