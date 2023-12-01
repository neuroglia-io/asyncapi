using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="ITagDefinitionBuilder"/>
/// </summary>
/// <remarks>
/// Initializes a new <see cref="TagDefinitionBuilder"/>
/// </remarks>
/// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="Models.TagDefinition"/>s</param>
public class TagDefinitionBuilder(IEnumerable<IValidator<TagDefinition>> validators)
    : ITagDefinitionBuilder
{

    /// <summary>
    /// Gets the services used to validate <see cref="Models.TagDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<TagDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="Models.TagDefinition"/> to configure
    /// </summary>
    protected virtual TagDefinition Tag { get; } = new();

    /// <inheritdoc/>
    public virtual ITagDefinitionBuilder WithName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.Tag.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual ITagDefinitionBuilder WithDescription(string description)
    {
        this.Tag.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual ITagDefinitionBuilder WithExternalDocumentation(Uri uri, string? description = null)
    {
        ArgumentNullException.ThrowIfNull(uri);
        this.Tag.ExternalDocs ??= [];
        this.Tag.ExternalDocs.Add(new() { Url = uri, Description = description });
        return this;
    }

    /// <inheritdoc/>
    public virtual TagDefinition Build()
    {
        var validationResults = this.Validators.Select(v => v.Validate(this.Tag));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return this.Tag;
    }

}
