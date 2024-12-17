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
/// Represents the default implementation of the <see cref="IOAuthFlowDefinitionCollectionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerator{T}"/> containing the <see cref="IValidator"/>s used to validate built <see cref="OAuthFlowDefinitionCollection"/>s</param>
public class OAuthFlowDefinitionCollectionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<OAuthFlowDefinitionCollection>> validators)
    : IOAuthFlowDefinitionCollectionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="OAuthFlowDefinitionCollection"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<OAuthFlowDefinitionCollection>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="OAuthFlowDefinitionCollection"/> to configure
    /// </summary>
    protected OAuthFlowDefinitionCollection Flows { get; } = new();

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionCollectionBuilder WithAuthorizationCodeFlow(OAuthFlowDefinition? flow)
    {
        this.Flows.AuthorizationCode = flow;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionCollectionBuilder WithAuthorizationCodeFlow(Action<IOAuthFlowDefinitionBuilder> setup)
    {
        var builder = ActivatorUtilities.CreateInstance<OAuthFlowDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        return this.WithAuthorizationCodeFlow(builder.Build());
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionCollectionBuilder WithClientCredentialsFlow(OAuthFlowDefinition? flow)
    {
        this.Flows.ClientCredentials = flow;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionCollectionBuilder WithClientCredentialsFlow(Action<IOAuthFlowDefinitionBuilder> setup)
    {
        var builder = ActivatorUtilities.CreateInstance<OAuthFlowDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        return this.WithClientCredentialsFlow(builder.Build());
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionCollectionBuilder WithImplicitFlow(OAuthFlowDefinition? flow)
    {
        this.Flows.Implicit = flow;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionCollectionBuilder WithImplicitFlow(Action<IOAuthFlowDefinitionBuilder> setup)
    {
        var builder = ActivatorUtilities.CreateInstance<OAuthFlowDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        return this.WithImplicitFlow(builder.Build());
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionCollectionBuilder WithPasswordFlow(OAuthFlowDefinition? flow)
    {
        this.Flows.Password = flow;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOAuthFlowDefinitionCollectionBuilder WithPasswordFlow(Action<IOAuthFlowDefinitionBuilder> setup)
    {
        var builder = ActivatorUtilities.CreateInstance<OAuthFlowDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        return this.WithPasswordFlow(builder.Build());
    }

    /// <inheritdoc/>
    public virtual OAuthFlowDefinitionCollection Build()
    {
        var validationResults = this.Validators.Select(v => v.Validate(this.Flows));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return this.Flows;
    }

}
