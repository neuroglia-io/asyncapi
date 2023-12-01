using Neuroglia.AsyncApi.v2;
using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.Redis;

/// <summary>
/// Represents the base record for all REDIS implementations of the <see cref="IBindingDefinition"/> interface
/// </summary>
[DataContract]
public abstract record RedisBindingDefinition
    : IBindingDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public IEnumerable<string> Protocols
    {
        get
        {
            yield return AsyncApiProtocol.Redis;
        }
    }

}
