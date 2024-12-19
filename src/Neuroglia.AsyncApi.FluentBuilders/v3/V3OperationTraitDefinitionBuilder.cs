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

namespace Neuroglia.AsyncApi.FluentBuilders.v3;

/// <summary>
/// Represents the base class for all <see cref="IV3OperationTraitDefinitionBuilder{TBuilder, TTrait}"/> implementations
/// </summary>
/// <typeparam name="TBuilder">The type of <see cref="IV3OperationTraitDefinitionBuilder{TBuilder, TTrait}"/> to return for chaining purposes</typeparam>
/// <typeparam name="TTrait">The type of <see cref="V3OperationTraitDefinition"/> to build</typeparam>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="V3OperationTraitDefinition"/>s</param>
public abstract class V3OperationTraitDefinitionBuilder<TBuilder, TTrait>(IServiceProvider serviceProvider, IEnumerable<IValidator<TTrait>> validators)
    : IV3OperationTraitDefinitionBuilder<TBuilder, TTrait>
    where TBuilder : IV3OperationTraitDefinitionBuilder<TBuilder, TTrait>
    where TTrait : V3OperationTraitDefinition, new()
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="V3OperationTraitDefinition"/>s
    /// </summary>
    protected IEnumerable<IValidator<TTrait>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V3MessageTraitDefinition"/> to configure
    /// </summary>
    protected virtual TTrait Trait { get; } = new();

    /// <inheritdoc/>
    public virtual void Use(string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        Trait.Reference = reference;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithTitle(string? title)
    {
        Trait.Title = title;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithSummary(string? summary)
    {
        Trait.Summary = summary;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithDescription(string? description)
    {
        Trait.Description = description;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithSecurity(Action<IV3SecuritySchemeDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3SecuritySchemeDefinitionBuilder>(ServiceProvider);
        setup(builder);
        Trait.Security ??= [];
        Trait.Security.Add(builder.Build());
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithTag(Action<IV3TagDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3TagDefinitionBuilder>(ServiceProvider);
        setup(builder);
        Trait.Tags ??= [];
        Trait.Tags.Add(builder.Build());
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithExternalDocumentation(Action<IV3ExternalDocumentationDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3ExternalDocumentationDefinitionBuilder>(ServiceProvider);
        setup(builder);
        Trait.ExternalDocs = builder.Build();
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithBinding(IOperationBindingDefinition binding)
    {
        ArgumentNullException.ThrowIfNull(binding);
        Trait.Bindings ??= new();
        Trait.Bindings.Add(binding);
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TTrait Build()
    {
        var validationResults = Validators.Select(v => v.Validate(Trait));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors!));
        return Trait;
    }

}

/// <summary>
/// Represents the base class for all <see cref="IV3OperationTraitDefinitionBuilder"/> implementations
/// </summary>
/// <remarks>
/// Initializes a new <see cref="V3OperationTraitDefinitionBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="V3OperationTraitDefinition"/>s</param>
public class V3OperationTraitDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V3OperationTraitDefinition>> validators)
    : V3OperationTraitDefinitionBuilder<IV3OperationTraitDefinitionBuilder, V3OperationTraitDefinition>(serviceProvider, validators), IV3OperationTraitDefinitionBuilder
{

}
