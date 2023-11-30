using Json.Schema;
using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Http;

/// <summary>
/// Represents the object used to configure an http operation binding
/// </summary>
[DataContract]
public record HttpOperationBindingDefinition
    : HttpBindingDefinition, IOperationBindingDefinition
{

    /// <summary>
    /// Gets/sets the binding's operation type
    /// </summary>
    [DataMember(Order = 1, Name = "type"), JsonPropertyOrder(1), JsonPropertyName("type"), YamlMember(Order = 1, Alias = "type")]
    public virtual HttpBindingOperationType Type { get; set; }

    /// <summary>
    /// Gets/sets the binding's method
    /// </summary>
    [DataMember(Order = 2, Name = "method"), JsonPropertyOrder(2), JsonPropertyName("method"), YamlMember(Order = 2, Alias = "method")]
    public virtual HttpMethod? Method { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="JsonSchema"/> containing the definitions for each query parameter. This schema MUST be of type object and have a properties key.
    /// </summary>
    [DataMember(Order = 3, Name = "query"), JsonPropertyOrder(3), JsonPropertyName("query"), YamlMember(Order = 3, Alias = "query")]
    public virtual JsonSchema? Query { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding. Defaults to 'latest'.
    /// </summary>
    [DataMember(Order = 4, Name = "bindingVersion"), JsonPropertyOrder(4), JsonPropertyName("bindingVersion"), YamlMember(Order = 4, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
