namespace Neuroglia.AsyncApi.Specification.v2;

/// <summary>
/// Represents an object used to provide contact information for the exposed API
/// </summary>
[DataContract]
public record ContactDefinition
{

    /// <summary>
    /// Gets/sets the identifying name of the contact person/organization.
    /// </summary>
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name"), YamlMember(Order = 1, Alias = "name")]
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="Uri"/> pointing to the contact information.
    /// </summary>
    [DataMember(Order = 2, Name = "url"), JsonPropertyOrder(2), JsonPropertyName("url"), YamlMember(Order = 2, Alias = "url")]
    public virtual Uri? Url { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="Uri"/> pointing to the contact information.
    /// </summary>
    [DataType(DataType.EmailAddress)]
    [DataMember(Order = 3, Name = "email"), JsonPropertyOrder(3), JsonPropertyName("email"), YamlMember(Order = 3, Alias = "email")]
    public virtual string? Email { get; set; }

}
