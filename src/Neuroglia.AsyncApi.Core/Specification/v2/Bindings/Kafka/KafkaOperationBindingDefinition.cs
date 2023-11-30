using Json.Schema;
using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Kafka;

/// <summary>
/// Represents the object used to configure a Kafka operation binding
/// </summary>
[DataContract]
public record KafkaOperationBindingDefinition
    : KafkaBindingDefinition, IOperationBindingDefinition
{

    /// <summary>
    /// Gets/sets the <see cref="JsonSchema"/> of the consumer group.
    /// </summary>
    [DataMember(Order = 1, Name = "groupId"), JsonPropertyOrder(1), JsonPropertyName("groupId"), YamlMember(Order = 1, Alias = "groupId")]
    public virtual JsonSchema? GroupId { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="JsonSchema"/> of the consumer inside a consumer group.
    /// </summary>
    [DataMember(Order = 2, Name = "clientId"), JsonPropertyOrder(2), JsonPropertyName("clientId"), YamlMember(Order = 2, Alias = "clientId")]
    public virtual JsonSchema? ClientId { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding. Defaults to 'latest'.
    /// </summary>
    [DataMember(Order = 3, Name = "bindingVersion"), JsonPropertyOrder(3), JsonPropertyName("groubindingVersionpId"), YamlMember(Order = 3, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
