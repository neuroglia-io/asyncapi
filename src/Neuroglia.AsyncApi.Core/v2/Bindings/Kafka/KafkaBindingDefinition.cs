using Neuroglia.AsyncApi.v2;
using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.Kafka;

/// <summary>
/// Represents the base record for all Kafka implementations of the <see cref="IBindingDefinition"/> interface
/// </summary>
[DataContract]
public abstract record KafkaBindingDefinition
    : IBindingDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public IEnumerable<string> Protocols
    {
        get
        {
            yield return AsyncApiProtocol.Kafka;
        }
    }

}
