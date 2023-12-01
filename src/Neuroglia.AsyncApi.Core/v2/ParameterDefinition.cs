using Json.Schema;

namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Represents an object used to define an Async API parameter
/// </summary>
[DataContract]
public record ParameterDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets a runtime expression that specifies the location of the parameter value. 
    /// Even when a definition for the target field exists, it MUST NOT be used to validate this parameter but, instead, the <see cref="Schema"/> property MUST be used.
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "location"), JsonPropertyOrder(1), JsonPropertyName("location"), YamlMember(Order = 1, Alias = "location")]
    public virtual string Location { get; set; } = null!;

    /// <summary>
    /// Gets/sets a short description of the parameter. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 2, Name = "description"), JsonPropertyOrder(2), JsonPropertyName("description"), YamlMember(Order = 2, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="ParameterDefinition"/>'s <see cref="JsonSchema"/>
    /// </summary>
    [DataMember(Order = 3, Name = "schema"), JsonPropertyOrder(3), JsonPropertyName("schema"), YamlMember(Order = 3, Alias = "schema")]
    public virtual JsonSchema? Schema { get; set; }

    private RuntimeExpression _LocationExpression = null!;
    /// <summary>
    /// Gets the location's <see cref="RuntimeExpression"/>
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual RuntimeExpression LocationExpression
    {
        get
        {
            if (_LocationExpression == null && !string.IsNullOrWhiteSpace(Location)) _LocationExpression = RuntimeExpression.Parse(Location);
            return _LocationExpression!;
        }
    }

    /// <inheritdoc/>
    public override string ToString() => Location;

}
