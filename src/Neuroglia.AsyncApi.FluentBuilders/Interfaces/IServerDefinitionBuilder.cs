namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="ServerDefinition"/>s
/// </summary>
public interface IServerDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="ServerDefinition"/> to use the specified url
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> of the <see cref="ServerDefinition"/> to build</param>
    /// <returns>The configured <see cref="IServerDefinitionBuilder"/></returns>
    IServerDefinitionBuilder WithUrl(Uri uri);

    /// <summary>
    /// Configures the <see cref="ServerDefinition"/> to use the specified protocol
    /// </summary>
    /// <param name="protocol">The protocol to use</param>
    /// <param name="version">The protocol version to use</param>
    /// <returns>The configured <see cref="IServerDefinitionBuilder"/></returns>
    IServerDefinitionBuilder WithProtocol(string protocol, string? version = null);

    /// <summary>
    /// Configures the <see cref="ServerDefinition"/> to use the specified description
    /// </summary>
    /// <param name="description">The description to use</param>
    /// <returns>The configured <see cref="IServerDefinitionBuilder"/></returns>
    IServerDefinitionBuilder WithDescription(string description);

    /// <summary>
    /// Adds the specified <see cref="VariableDefinition"/> to the <see cref="ServerDefinition"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="VariableDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="VariableDefinition"/> to add</param>
    /// <returns>The configured <see cref="IServerDefinitionBuilder"/></returns>
    IServerDefinitionBuilder WithVariable(string name, Action<IVariableDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="ServerDefinition"/> to build to use the specified <see cref="SecuritySchemeDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="SecuritySchemeDefinition"/> to add</param>
    /// <param name="requirement">The security requirement object, if any</param>
    /// <returns>The configured <see cref="IServerDefinitionBuilder"/></returns>
    IServerDefinitionBuilder WithSecurityRequirement(string name, object? requirement = null);

    /// <summary>
    /// Adds the specified <see cref="IServerBindingDefinition"/> to the <see cref="ServerDefinition"/> to build
    /// </summary>
    /// <param name="binding">The <see cref="IServerBindingDefinition"/> to add</param>
    /// <returns>The configured <see cref="IServerDefinitionBuilder"/></returns>
    IServerDefinitionBuilder WithBinding(IServerBindingDefinition binding);

    /// <summary>
    /// Builds a new <see cref="ServerDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="ServerDefinition"/></returns>
    ServerDefinition Build();

}
