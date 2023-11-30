using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Http;

/// <summary>
/// Represents the object used to configure an http channel binding
/// </summary>
[DataContract]
public record HttpChannelBindingDefinition
    : HttpBindingDefinition, IChannelBindingDefinition
{



}
