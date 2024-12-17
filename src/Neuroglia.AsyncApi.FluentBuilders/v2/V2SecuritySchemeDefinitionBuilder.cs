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

using Neuroglia;

namespace Neuroglia.AsyncApi.FluentBuilders.v2;

/// <summary>
/// Represents the default implementation of the <see cref="IV2SecuritySchemeDefinitionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerator{T}"/> containing the <see cref="IValidator"/>s used to validate built <see cref="V2SecuritySchemeDefinition"/>s</param>
public class V2SecuritySchemeDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V2SecuritySchemeDefinition>> validators)
    : IV2SecuritySchemeDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="V2SecuritySchemeDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V2SecuritySchemeDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V2SecuritySchemeDefinition"/> to configure
    /// </summary>
    protected V2SecuritySchemeDefinition SecurityScheme { get; } = new();

    /// <inheritdoc/>
    public virtual IV2SecuritySchemeDefinitionBuilder WithType(SecuritySchemeType type)
    {
        SecurityScheme.Type = type;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2SecuritySchemeDefinitionBuilder WithParameterName(string? parameterName)
    {
        SecurityScheme.Name = parameterName;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2SecuritySchemeDefinitionBuilder WithDescription(string? description)
    {
        SecurityScheme.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2SecuritySchemeDefinitionBuilder WithApiKeyLocation(string? location)
    {
        if (string.IsNullOrWhiteSpace(location)) { SecurityScheme.In = null; return this; }

        switch (SecurityScheme.Type)
        {
            case SecuritySchemeType.ApiKey:
                if (!ApiKeyLocation.GetLocations().Contains(location)) throw new ArgumentException($"The 'in' values supported when scheme type is '{EnumHelper.Stringify(SecurityScheme.Type)}' are: {string.Join(", ", ApiKeyLocation.GetLocations())}", nameof(location));
                break;
            case SecuritySchemeType.HttpApiKey:
                if (!HttpApiKeyLocation.GetLocations().Contains(location)) throw new ArgumentException($"The 'in' values supported when scheme type is '{EnumHelper.Stringify(SecurityScheme.Type)}' are: {string.Join(", ", HttpApiKeyLocation.GetLocations())}", nameof(location));

                break;
            default: throw new Exception($"The API key location can only be set on schemes of type '{EnumHelper.Stringify(SecuritySchemeType.ApiKey)}' and '{EnumHelper.Stringify(SecuritySchemeType.HttpApiKey)}'");
        }

        SecurityScheme.In = location;

        return this;
    }

    /// <inheritdoc/>
    public virtual IV2SecuritySchemeDefinitionBuilder WithAuthorizationScheme(string? scheme)
    {
        SecurityScheme.Scheme = scheme;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2SecuritySchemeDefinitionBuilder WithBearerFormat(string? format)
    {
        SecurityScheme.BearerFormat = format;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2SecuritySchemeDefinitionBuilder WithOAuthFlows(Action<IOAuthFlowDefinitionCollectionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(nameof(setup));

        var builder = ActivatorUtilities.CreateInstance<OAuthFlowDefinitionCollectionBuilder>(ServiceProvider);
        setup(builder);
        SecurityScheme.Flows = builder.Build();

        return this;
    }

    /// <inheritdoc/>
    public virtual IV2SecuritySchemeDefinitionBuilder WithOpenIdConnectUrl(Uri? uri)
    {
        SecurityScheme.OpenIdConnectUrl = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual V2SecuritySchemeDefinition Build()
    {
        var validationResults = Validators.Select(v => v.Validate(SecurityScheme));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return SecurityScheme;
    }

}
