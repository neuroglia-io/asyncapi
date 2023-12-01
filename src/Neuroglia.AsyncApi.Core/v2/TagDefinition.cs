namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Represents an object used to define a tag assigned to an Async API component
/// </summary>
[DataContract]
public record TagDefinition
{

    /// <summary>
    /// Gets/sets the name of the tag.
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name"), YamlMember(Order = 1, Alias = "name")]
    public virtual string Name { get; set; } = null!;

    /// <summary>
    /// Gets/sets an optional description of this channel item. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 2, Name = "description"), JsonPropertyOrder(2), JsonPropertyName("description"), YamlMember(Order = 2, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="List{T}"/> containing additional external documentation for this tag.
    /// </summary>
    [DataMember(Order = 3, Name = "externalDocs"), JsonPropertyOrder(3), JsonPropertyName("externalDocs"), YamlMember(Order = 3, Alias = "externalDocs")]
    public virtual EquatableList<ExternalDocumentationDefinition>? ExternalDocs { get; set; }

    /// <inheritdoc/>
    public override string ToString() => Name;

}
