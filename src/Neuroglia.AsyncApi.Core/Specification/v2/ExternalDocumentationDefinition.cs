namespace Neuroglia.AsyncApi.Specification.v2;

/// <summary>
/// Represents an object used to define a reference to an external documentation
/// </summary>
[DataContract]
public record ExternalDocumentationDefinition
{

    /// <summary>
    /// Gets/sets the <see cref="Uri"/> for the target documentation.
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "url"), JsonPropertyOrder(1), JsonPropertyName("url"), YamlMember(Order = 1, Alias = "url")]
    public virtual Uri Url { get; set; } = null!;

    /// <summary>
    /// Gets/sets an optional description of this documentation. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 2, Name = "description"), JsonPropertyOrder(2), JsonPropertyName("description"), YamlMember(Order = 2, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <inheritdoc/>
    public override string ToString() => Url.ToString();

}
