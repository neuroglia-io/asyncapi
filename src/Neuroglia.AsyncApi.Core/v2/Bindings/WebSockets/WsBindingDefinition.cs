using Neuroglia.AsyncApi.v2;
using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.WebSockets;

/// <summary>
/// Represents the base record for all Websocket implementations of the <see cref="IBindingDefinition"/> interface
/// </summary>
[DataContract]
public abstract record WsBindingDefinition
    : IBindingDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public IEnumerable<string> Protocols
    {
        get
        {
            yield return AsyncApiProtocol.Ws;
        }
    }

}
