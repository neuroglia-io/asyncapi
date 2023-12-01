using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="VariableDefinition"/>s
/// </summary>
public interface IVariableDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="VariableDefinition"/> to build to use the specified enum values
    /// </summary>
    /// <param name="values">An array of values to be used if the substitution options are from a limited set.</param>
    /// <returns>The configured <see cref="IVariableDefinitionBuilder"/></returns>
    IVariableDefinitionBuilder WithEnumValues(params string[] values);

    /// <summary>
    /// Configures the <see cref="VariableDefinition"/> to build to use the specified default value
    /// </summary>
    /// <param name="value">The value to use by default for substitution, and to send, if an alternate value is not supplied.</param>
    /// <returns>The configured <see cref="IVariableDefinitionBuilder"/></returns>
    IVariableDefinitionBuilder WithDefaultValue(string value);

    /// <summary>
    /// Configures the <see cref="VariableDefinition"/> to use the specified description
    /// </summary>
    /// <param name="description">The description to use</param>
    /// <returns>The configured <see cref="IVariableDefinitionBuilder"/></returns>
    IVariableDefinitionBuilder WithDescription(string description);

    /// <summary>
    /// Adds the specified example to the <see cref="VariableDefinition"/> to build
    /// </summary>
    /// <param name="example">The example to add</param>
    /// <returns>The configured <see cref="IVariableDefinitionBuilder"/></returns>
    IVariableDefinitionBuilder WithExample(string example);

    /// <summary>
    /// Builds a new <see cref="VariableDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="VariableDefinition"/></returns>
    VariableDefinition Build();

}
