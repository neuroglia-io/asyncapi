using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.WebSockets;

/// <summary>
/// Represents the object used to configure an WebSocket server binding
/// </summary>
[DataContract]
public record WsServerBindingDefinition
    : WsBindingDefinition, IServerBindingDefinition
{



}
