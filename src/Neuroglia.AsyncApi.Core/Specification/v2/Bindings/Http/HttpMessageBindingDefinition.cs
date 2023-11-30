using Json.Schema;
using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Http;

/// <summary>
/// Represents the object used to configure an http message binding
/// </summary>
[DataContract]
public record HttpMessageBindingDefinition
    : HttpBindingDefinition, IMessageBindingDefinition
{

    /// <summary>
    /// Gets/sets a <see cref="JsonSchema"/> containing the definitions for HTTP-specific headers. This schema MUST be of type object and have a properties key.
    /// </summary>
    [DataMember(Order = 1, Name = "headers"), JsonPropertyOrder(1), JsonPropertyName("headers"), YamlMember(Order = 1, Alias = "headers")]
    public virtual JsonSchema? Headers { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding. Defaults to 'latest'.
    /// </summary>
    [DataMember(Order = 2, Name = "bindingVersion"), JsonPropertyOrder(2), JsonPropertyName("bindingVersion"), YamlMember(Order = 2, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
