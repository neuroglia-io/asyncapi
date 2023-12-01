using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.Http;

/// <summary>
/// Represents the object used to configure an http server binding
/// </summary>
[DataContract]
public record HttpServerBindingDefinition
    : HttpBindingDefinition, IServerBindingDefinition
{



}
