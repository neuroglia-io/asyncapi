namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="OAuthFlowDefinition"/>s
/// </summary>
public interface IOAuthFlowDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="OAuthFlowDefinition"/> to build to use the specified authorization URL
    /// </summary>
    /// <param name="uri">The authorization URL to use</param>
    /// <returns>The configured <see cref="IOAuthFlowDefinitionBuilder"/></returns>
    IOAuthFlowDefinitionBuilder WithAuthorizationUrl(Uri? uri);

    /// <summary>
    /// Configures the <see cref="OAuthFlowDefinition"/> to build to use the specified token URL
    /// </summary>
    /// <param name="uri">The token URL to use</param>
    /// <returns>The configured <see cref="IOAuthFlowDefinitionBuilder"/></returns>
    IOAuthFlowDefinitionBuilder WithTokenUrl(Uri? uri);

    /// <summary>
    /// Configures the <see cref="OAuthFlowDefinition"/> to build to use the specified refresh URL
    /// </summary>
    /// <param name="uri">The refresh URL to use</param>
    /// <returns>The configured <see cref="IOAuthFlowDefinitionBuilder"/></returns>
    IOAuthFlowDefinitionBuilder WithRefreshUrl(Uri? uri);

    /// <summary>
    /// Configures the <see cref="OAuthFlowDefinition"/> to build to use the specified scopes
    /// </summary>
    /// <param name="scopes">A name/description mapping of the scopes to use</param>
    /// <returns>The configured <see cref="IOAuthFlowDefinitionBuilder"/></returns>
    IOAuthFlowDefinitionBuilder WithScopes(IDictionary<string, string> scopes);

    /// <summary>
    /// Configures the <see cref="OAuthFlowDefinition"/> to build to use the specified scopes
    /// </summary>
    /// <param name="scopes">An array containing the name/description mappings of the scopes to use</param>
    /// <returns>The configured <see cref="IOAuthFlowDefinitionBuilder"/></returns>
    IOAuthFlowDefinitionBuilder WithScopes(params KeyValuePair<string, string>[] scopes);

    /// <summary>
    /// Configures the <see cref="OAuthFlowDefinition"/> to build to use the specified scopes
    /// </summary>
    /// <param name="name">The name of the scope to use</param>
    /// <param name="description">The description, if any, of the scope to use</param>
    /// <returns>The configured <see cref="IOAuthFlowDefinitionBuilder"/></returns>
    IOAuthFlowDefinitionBuilder WithScope(string name, string? description = null);

    /// <summary>
    /// Builds the configured <see cref="OAuthFlowDefinition"/>
    /// </summary>
    /// <returns>The configured <see cref="OAuthFlowDefinition"/></returns>
    OAuthFlowDefinition Build();

}
