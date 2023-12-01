using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Defines the fundamentals of the service used to build <see cref="ParameterDefinition"/>s
/// </summary>
public interface IParameterDefinitionBuilder
{

    /// <summary>
    /// Configures the type of the <see cref="ParameterDefinition"/> to build
    /// </summary>
    /// <typeparam name="TParameter">The type of the <see cref="ParameterDefinition"/> to build</typeparam>
    /// <returns>The configured <see cref="IParameterDefinitionBuilder"/></returns>
    IParameterDefinitionBuilder OfType<TParameter>();

    /// <summary>
    /// Configures the type of the <see cref="ParameterDefinition"/> to build
    /// </summary>
    /// <param name="parameterType">The type of the <see cref="ParameterDefinition"/> to build</param>
    /// <returns>The configured <see cref="IParameterDefinitionBuilder"/></returns>
    IParameterDefinitionBuilder OfType(Type parameterType);

    /// <summary>
    /// Configures the <see cref="JsonSchema"/> of the <see cref="ParameterDefinition"/> to build
    /// </summary>
    /// <param name="schema">The <see cref="JsonSchema"/> of the <see cref="ParameterDefinition"/> to build</param>
    /// <returns>The configured <see cref="IParameterDefinitionBuilder"/></returns>
    IParameterDefinitionBuilder WithSchema(JsonSchema schema);

    /// <summary>
    /// Configures the <see cref="ParameterDefinition"/> to build to use the specified description
    /// </summary>
    /// <param name="description">The description to use</param>
    /// <returns>The configured <see cref="IParameterDefinitionBuilder"/></returns>
    IParameterDefinitionBuilder WithDescription(string description);

    /// <summary>
    /// Sets the location of the <see cref="ParameterDefinition"/> to build
    /// </summary>
    /// <param name="location">A runtime expression that specifies the location of the parameter value</param>
    /// <returns>The configured <see cref="IParameterDefinitionBuilder"/></returns>
    IParameterDefinitionBuilder WithLocation(string location);

    /// <summary>
    /// Builds a new <see cref="ParameterDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="ParameterDefinition"/></returns>
    ParameterDefinition Build();

}
