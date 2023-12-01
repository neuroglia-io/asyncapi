namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IServerDefinitionBuilder"/>
/// </summary>
/// <remarks>
/// Initializes a new <see cref="ServerDefinitionBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="ServerDefinition"/>s</param>
public class ServerDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<ServerDefinition>> validators)
    : IServerDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="Models.ServerDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<ServerDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="ServerDefinition"/> to configure
    /// </summary>
    protected virtual ServerDefinition Server { get; } = new();

    /// <inheritdoc/>
    public virtual IServerDefinitionBuilder WithUrl(Uri uri)
    {
        this.Server.Url = uri ?? throw new ArgumentNullException(nameof(uri));
        return this;
    }

    /// <inheritdoc/>
    public virtual IServerDefinitionBuilder WithProtocol(string protocol, string? version = null)
    {
        if (string.IsNullOrWhiteSpace(protocol)) throw new ArgumentNullException(nameof(protocol));
        this.Server.Protocol = protocol;
        this.Server.ProtocolVersion = string.IsNullOrWhiteSpace(version) ? "latest" : version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IServerDefinitionBuilder WithDescription(string description)
    {
        this.Server.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IServerDefinitionBuilder WithVariable(string name, Action<IVariableDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        var builder = ActivatorUtilities.CreateInstance<VariableDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        this.Server.Variables ??= [];
        this.Server.Variables.Add(name, builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IServerDefinitionBuilder WithBinding(IServerBindingDefinition binding)
    {
        ArgumentNullException.ThrowIfNull(binding);
        this.Server.Bindings ??= new();
        this.Server.Bindings.Add(binding);
        return this;
    }

    /// <inheritdoc/>
    public virtual IServerDefinitionBuilder WithSecurityRequirement(string name, object? requirement = null)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        requirement ??= new { };
        this.Server.Security ??= [];
        this.Server.Security.Add(name, requirement);
        return this;
    }

    /// <inheritdoc/>
    public virtual ServerDefinition Build()
    {
        var validationResults = this.Validators.Select(v => v.Validate(this.Server));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors!));
        return this.Server;
    }

}
