using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.WebSockets;

/// <summary>
/// Represents the object used to configure a WebSocket operation binding
/// </summary>
[DataContract]
public record WsOperationBindingDefinition
    : WsBindingDefinition, IOperationBindingDefinition
{



}
