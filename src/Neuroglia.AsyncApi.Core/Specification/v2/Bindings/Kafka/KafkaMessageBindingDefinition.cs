using Json.Schema;
using Neuroglia.AsyncApi.Specification.v2.Bindings;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Kafka;

/// <summary>
/// Represents the object used to configure a Kafka message binding
/// </summary>
[DataContract]
public record KafkaMessageBindingDefinition
    : KafkaBindingDefinition, IMessageBindingDefinition
{

    /// <summary>
    /// Gets/sets a <see cref="JsonSchema"/> that defines the message key.
    /// </summary>
    [DataMember(Order = 1, Name = "key"), JsonPropertyOrder(1), JsonPropertyName("key"), YamlMember(Order = 1, Alias = "key")]
    public virtual JsonSchema? Key { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding. Defaults to 'latest'.
    /// </summary>
    [DataMember(Order = 2, Name = "bindingVersion"), JsonPropertyOrder(2), JsonPropertyName("bindingVersion"), YamlMember(Order = 2, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
