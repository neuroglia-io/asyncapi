using Neuroglia.AsyncApi.v2;
using Neuroglia.Json.Schema.Generation;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IParameterDefinitionBuilder"/> interface
/// </summary>
/// <remarks>
/// Initializes a new <see cref="ParameterDefinitionBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="Models.ParameterDefinition"/>s</param>
public class ParameterDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<ParameterDefinition>> validators)
    : IParameterDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="Models.ParameterDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<ParameterDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="Models.ParameterDefinition"/> to configure
    /// </summary>
    protected virtual ParameterDefinition Parameter { get; } = new();

    /// <inheritdoc/>
    public virtual IParameterDefinitionBuilder OfType<TParameter>() => this.OfType(typeof(TParameter));

    /// <inheritdoc/>
    public virtual IParameterDefinitionBuilder OfType(Type parameterType)
    {
        ArgumentNullException.ThrowIfNull(parameterType);
        return this.WithSchema(new JsonSchemaBuilder().FromType(parameterType, JsonSchemaGeneratorConfiguration.Default));
    }

    /// <inheritdoc/>
    public virtual IParameterDefinitionBuilder WithSchema(JsonSchema schema)
    {
        this.Parameter.Schema = schema ?? throw new ArgumentNullException(nameof(schema));
        return this;
    }

    /// <inheritdoc/>
    public virtual IParameterDefinitionBuilder WithDescription(string description)
    {
        this.Parameter.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IParameterDefinitionBuilder WithLocation(string location)
    {
        if (string.IsNullOrWhiteSpace(location)) throw new ArgumentNullException(nameof(location));
        this.Parameter.Location = location;
        return this;
    }

    /// <inheritdoc/>
    public virtual ParameterDefinition Build()
    {
        var validationResults = this.Validators.Select(v => v.Validate(this.Parameter));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return this.Parameter;
    }

}
