namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Represents an object used to define a Async API operation message
/// </summary>
[DataContract]
public record MessageDefinition
    : MessageTraitDefinition
{

    /// <summary>
    /// Gets/sets the definition of the message payload. It can be of any type but defaults to Schema object. It must match the <see cref="MessageTraitDefinition.SchemaFormat"/>, including encoding type - e.g Avro should be inlined as either a YAML or JSON object NOT a string to be parsed as YAML or JSON.
    /// </summary>
    [DataMember(Order = 1, Name = "payload"), JsonPropertyOrder(1), JsonPropertyName("payload"), YamlMember(Order = 1, Alias = "payload")]
    public virtual object? Payload { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="List{T}"/> of traits to apply to the message object. Traits MUST be merged into the message object using the JSON Merge Patch algorithm in the same order they are defined here.
    /// </summary>
    [DataMember(Order = 2, Name = "traits"), JsonPropertyOrder(2), JsonPropertyName("traits"), YamlMember(Order = 2, Alias = "traits")]
    public virtual EquatableList<MessageTraitDefinition>? Traits { get; set; }

}
