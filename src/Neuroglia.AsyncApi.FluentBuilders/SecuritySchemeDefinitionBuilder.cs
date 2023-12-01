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

using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="ISecuritySchemeDefinitionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerator{T}"/> containing the <see cref="IValidator"/>s used to validate built <see cref="SecuritySchemeDefinition"/>s</param>
public class SecuritySchemeDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<SecuritySchemeDefinition>> validators)
    : ISecuritySchemeDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="SecuritySchemeDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<SecuritySchemeDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="SecuritySchemeDefinition"/> to configure
    /// </summary>
    protected SecuritySchemeDefinition SecurityScheme { get; } = new();

    /// <inheritdoc/>
    public virtual ISecuritySchemeDefinitionBuilder WithType(SecuritySchemeType type)
    {
        this.SecurityScheme.Type = type;
        return this;
    }

    /// <inheritdoc/>
    public virtual ISecuritySchemeDefinitionBuilder WithParameterName(string? parameterName)
    {
        this.SecurityScheme.Name = parameterName;
        return this;
    }

    /// <inheritdoc/>
    public virtual ISecuritySchemeDefinitionBuilder WithDescription(string? description)
    {
        this.SecurityScheme.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual ISecuritySchemeDefinitionBuilder WithApiKeyLocation(string? location)
    {
        if (string.IsNullOrWhiteSpace(location)) { this.SecurityScheme.In = null; return this; }

        switch (this.SecurityScheme.Type)
        {
            case SecuritySchemeType.ApiKey:
                if (!ApiKeyLocation.GetLocations().Contains(location)) throw new ArgumentException($"The 'in' values supported when scheme type is '{EnumHelper.Stringify(this.SecurityScheme.Type)}' are: {string.Join(", ", ApiKeyLocation.GetLocations())}", nameof(location));
                break;
            case SecuritySchemeType.HttpApiKey:
                if (!HttpApiKeyLocation.GetLocations().Contains(location)) throw new ArgumentException($"The 'in' values supported when scheme type is '{EnumHelper.Stringify(this.SecurityScheme.Type)}' are: {string.Join(", ", HttpApiKeyLocation.GetLocations())}", nameof(location));

                break;
            default: throw new Exception($"The API key location can only be set on schemes of type '{EnumHelper.Stringify(SecuritySchemeType.ApiKey)}' and '{EnumHelper.Stringify(SecuritySchemeType.HttpApiKey)}'");
        }

        this.SecurityScheme.In = location;

        return this;
    }

    /// <inheritdoc/>
    public virtual ISecuritySchemeDefinitionBuilder WithAuthorizationScheme(string? scheme)
    {
        this.SecurityScheme.Scheme = scheme;
        return this;
    }

    /// <inheritdoc/>
    public virtual ISecuritySchemeDefinitionBuilder WithBearerFormat(string? format)
    {
        this.SecurityScheme.BearerFormat = format;
        return this;
    }

    /// <inheritdoc/>
    public virtual ISecuritySchemeDefinitionBuilder WithOAuthFlows(Action<IOAuthFlowDefinitionCollectionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(nameof(setup));

        var builder = ActivatorUtilities.CreateInstance<OAuthFlowDefinitionCollectionBuilder>(this.ServiceProvider);
        setup(builder);
        this.SecurityScheme.Flows = builder.Build();

        return this;
    }

    /// <inheritdoc/>
    public virtual ISecuritySchemeDefinitionBuilder WithOpenIdConnectUrl(Uri? uri)
    {
        this.SecurityScheme.OpenIdConnectUrl = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual SecuritySchemeDefinition Build()
    {
        var validationResults = this.Validators.Select(v => v.Validate(this.SecurityScheme));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return this.SecurityScheme;
    }

}
