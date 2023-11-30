namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="TagDefinition"/>s
/// </summary>
public interface ITagDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="TagDefinition"/> to use the specified name
    /// </summary>
    /// <param name="name">The name to use</param>
    /// <returns>The configured <see cref="ITagDefinitionBuilder"/></returns>
    ITagDefinitionBuilder WithName(string name);

    /// <summary>
    /// Configures the <see cref="TagDefinition"/> to use the specified description
    /// </summary>
    /// <param name="description">The description to use</param>
    /// <returns>The configured <see cref="ITagDefinitionBuilder"/></returns>
    ITagDefinitionBuilder WithDescription(string description);

    /// <summary>
    /// Adds the specified external documentation to the <see cref="TagDefinition"/> to build
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> to the documentation to add</param>
    /// <param name="description">The description of the documentation to add</param>
    /// <returns>The configured <see cref="ITagDefinitionBuilder"/></returns>
    ITagDefinitionBuilder WithExternalDocumentation(Uri uri, string? description = null);

    /// <summary>
    /// Builds a new <see cref="TagDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="TagDefinition"/></returns>
    TagDefinition Build();

}
