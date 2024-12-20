// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
        Flow.AuthorizationUrl = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionBuilder WithTokenUrl(Uri? uri)
    {
        Flow.TokenUrl = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionBuilder WithRefreshUrl(Uri? uri)
    {
        Flow.RefreshUrl = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionBuilder WithScope(string name, string? description = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Flow.Scopes ??= [];
        Flow.Scopes[name] = description!;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionBuilder WithScopes(IDictionary<string, string> scopes)
    {
        ArgumentNullException.ThrowIfNull(scopes);
        Flow.Scopes = new(scopes);
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionBuilder WithScopes(params KeyValuePair<string, string>[] scopes)
    {
        ArgumentNullException.ThrowIfNull(scopes);
        Flow.Scopes = new(scopes);
        return this;
    }

    /// <inheritdoc/>
    public virtual OAuthFlowDefinition Build()
    {
        var validationResults = Validators.Select(v => v.Validate(Flow));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return Flow;
    }

}
