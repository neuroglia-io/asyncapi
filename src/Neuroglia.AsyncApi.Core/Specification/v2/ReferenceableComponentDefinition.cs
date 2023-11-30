namespace Neuroglia.AsyncApi.Specification.v2;

/// <summary>
/// Represents the base record for all <see cref="IReferenceable"/> Async API components
/// </summary>
[DataContract]
public abstract record ReferenceableComponentDefinition
    : IReferenceable
{

    /// <summary>
    /// Gets/sets an an external definition of the <see cref="ReferenceableComponentDefinition"/>.
    /// </summary>
    [DataMember(Order = 1, Name = "$ref"), JsonPropertyOrder(1), JsonPropertyName("$ref"), YamlMember(Order = 1, Alias = "$ref")]
    public virtual string? Reference { get; set; }

}