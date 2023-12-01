using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IVariableDefinitionBuilder"/> interface
/// </summary>
/// <remarks>
/// Initializes a new <see cref="VariableDefinitionBuilder"/>
/// </remarks>
/// <param name="validators">The services used to validate <see cref="Models.VariableDefinition"/>s</param>
public class VariableDefinitionBuilder(IEnumerable<IValidator<VariableDefinition>> validators)
    : IVariableDefinitionBuilder
{

    /// <summary>
    /// Gets the services used to validate <see cref="Models.VariableDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<VariableDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="Models.VariableDefinition"/> to build
    /// </summary>
    protected VariableDefinition Variable { get; } = new();

    /// <inheritdoc/>
    public virtual IVariableDefinitionBuilder WithEnumValues(params string[] values)
    {
        this.Variable.Enum = new(values);
        return this;
    }

    /// <inheritdoc/>
    public virtual IVariableDefinitionBuilder WithDefaultValue(string value)
    {
        this.Variable.Default = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IVariableDefinitionBuilder WithDescription(string description)
    {
        this.Variable.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IVariableDefinitionBuilder WithExample(string example)
    {
        if (string.IsNullOrWhiteSpace(example)) throw new ArgumentNullException(nameof(example));
        this.Variable.Examples ??= [];
        this.Variable.Examples.Add(example);
        return this;
    }

    /// <inheritdoc/>
    public virtual VariableDefinition Build() => this.Variable;

}
