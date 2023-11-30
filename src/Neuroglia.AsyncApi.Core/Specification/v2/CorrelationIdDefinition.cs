namespace Neuroglia.AsyncApi.Specification.v2;

/// <summary>
/// Represents an object used to define an Async API correlation ID
/// </summary>
[DataContract]
public record CorrelationIdDefinition
    : ReferenceableComponentDefinition
{

    RuntimeExpression _locationExpression = null!;

    /// <summary>
    /// Gets/sets a runtime expression that specifies the location of the correlation ID.
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "location"), JsonPropertyOrder(1), JsonPropertyName("location"), YamlMember(Order = 1, Alias = "location")]
    public virtual string Location { get; set; } = null!;

    /// <summary>
    /// Gets/sets a short description of the application. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 2, Name = "description"), JsonPropertyOrder(2), JsonPropertyName("description"), YamlMember(Order = 2, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets the location's <see cref="RuntimeExpression"/>
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual RuntimeExpression LocationExpression
    {
        get
        {
            if (_locationExpression == null && !string.IsNullOrWhiteSpace(Location)) _locationExpression = RuntimeExpression.Parse(Location);
            return _locationExpression!;
        }
    }

}
