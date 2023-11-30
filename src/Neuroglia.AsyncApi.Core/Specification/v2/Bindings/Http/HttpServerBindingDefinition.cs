using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Http;

/// <summary>
/// Represents the object used to configure an http server binding
/// </summary>
[DataContract]
public record HttpServerBindingDefinition
    : HttpBindingDefinition, IServerBindingDefinition
{



}
