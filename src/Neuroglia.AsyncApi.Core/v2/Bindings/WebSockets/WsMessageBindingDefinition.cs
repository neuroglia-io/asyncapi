using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.WebSockets;

/// <summary>
/// Represents the object used to configure a WebSocket message binding
/// </summary>
[DataContract]
public record WsMessageBindingDefinition
    : WsBindingDefinition, IMessageBindingDefinition
{



}
