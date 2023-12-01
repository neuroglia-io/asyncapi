
using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IOAuthFlowDefinitionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerator{T}"/> containing the <see cref="IValidator"/>s used to validate built <see cref="OAuthFlowDefinitionBuilder"/>s</param>
public class OAuthFlowDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<OAuthFlowDefinition>> validators)
    : IOAuthFlowDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="OAuthFlowDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<OAuthFlowDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="OAuthFlowDefinition"/> to configure
    /// </summary>
    protected OAuthFlowDefinition Flow { get; } = new();

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionBuilder WithAuthorizationUrl(Uri? uri)
    {
        this.Flow.AuthorizationUrl = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionBuilder WithTokenUrl(Uri? uri)
    {
        this.Flow.TokenUrl = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionBuilder WithRefreshUrl(Uri? uri)
    {
        this.Flow.RefreshUrl = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionBuilder WithScope(string name, string? description = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        this.Flow.Scopes ??= [];
        this.Flow.Scopes[name] = description!;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionBuilder WithScopes(IDictionary<string, string> scopes)
    {
        ArgumentNullException.ThrowIfNull(scopes);
        this.Flow.Scopes = new(scopes);
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionBuilder WithScopes(params KeyValuePair<string, string>[] scopes)
    {
        ArgumentNullException.ThrowIfNull(scopes);
        this.Flow.Scopes = new(scopes);
        return this;
    }

    /// <inheritdoc/>
    public virtual OAuthFlowDefinition Build()
    {
        var validationResults = this.Validators.Select(v => v.Validate(this.Flow));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return this.Flow;
    }

}
