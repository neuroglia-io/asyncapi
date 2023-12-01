using Json.Schema;
using Neuroglia.AsyncApi.v2.Bindings;
using Neuroglia.AsyncApi.v2.Bindings.Http;

namespace Neuroglia.AsyncApi.v2.Bindings.WebSockets;

/// <summary>
/// Represents the object used to configure an WebSocket channel binding
/// </summary>
[DataContract]
public record WsChannelBindingDefinition
    : WsBindingDefinition, IChannelBindingDefinition
{

    /// <summary>
    /// Gets/sets the <see cref="WsChannelBindingDefinition"/>'s operation type
    /// </summary>
    [DataMember(Order = 1, Name = "type"), JsonPropertyOrder(1), JsonPropertyName("type"), YamlMember(Order = 1, Alias = "type")]
    public virtual HttpBindingOperationType Type { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="JsonSchema"/> containing the definitions for each query parameter. This schema MUST be of type object and have a properties key.
    /// </summary>
    [DataMember(Order = 2, Name = "query"), JsonPropertyOrder(2), JsonPropertyName("query"), YamlMember(Order = 2, Alias = "query")]
    public virtual JsonSchema? Query { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="JsonSchema"/> containing the definitions for HTTP-specific headers. This schema MUST be of type object and have a properties key.
    /// </summary>
    [DataMember(Order = 3, Name = "headers"), JsonPropertyOrder(3), JsonPropertyName("headers"), YamlMember(Order = 3, Alias = "headers")]
    public virtual JsonSchema? Headers { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding. Defaults to 'latest'.
    /// </summary>
    [DataMember(Order = 4, Name = "bindingVersion"), JsonPropertyOrder(4), JsonPropertyName("bindingVersion"), YamlMember(Order = 4, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
