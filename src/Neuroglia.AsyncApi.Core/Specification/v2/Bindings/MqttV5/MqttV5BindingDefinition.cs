using Neuroglia.AsyncApi.Specification.v2;
using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.MqttV5;

/// <summary>
/// Represents the base record for all Mqtt V5 implementations of the <see cref="IBindingDefinition"/> interface
/// </summary>
[DataContract]
public abstract record MqttV5BindingDefinition
    : IBindingDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public IEnumerable<string> Protocols
    {
        get
        {
            yield return AsyncApiProtocol.MqttV5;
        }
    }

}
