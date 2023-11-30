using Neuroglia.AsyncApi.Specification.v2;
using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Mqtt;

/// <summary>
/// Represents the base record for all Mqtt implementations of the <see cref="IBindingDefinition"/> interface
/// </summary>
[DataContract]
public abstract record MqttBindingDefinition
    : IBindingDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public IEnumerable<string> Protocols
    {
        get
        {
            yield return AsyncApiProtocol.Mqtt;
        }
    }

}
