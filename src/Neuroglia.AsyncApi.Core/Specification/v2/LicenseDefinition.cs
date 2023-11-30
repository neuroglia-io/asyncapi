namespace Neuroglia.AsyncApi.Specification.v2;

/// <summary>
/// Represents an object used to provide license information for the exposed API
/// </summary>
[DataContract]
public record LicenseDefinition
{

    /// <summary>
    /// Gets/sets the license name used for the API.
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name"), YamlMember(Order = 1, Alias = "name")]
    public virtual string Name { get; set; } = null!;

    /// <summary>
    /// Gets/sets the <see cref="Uri"/> to the license used for the API.
    /// </summary>
    [Required]
    [DataMember(Order = 2, Name = "url"), JsonPropertyOrder(2), JsonPropertyName("url"), YamlMember(Order = 2, Alias = "url")]
    public virtual Uri? Url { get; set; }

    /// <inheritdoc/>
    public override string ToString() => Name;

}