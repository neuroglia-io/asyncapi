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

namespace Neuroglia.AsyncApi.FluentBuilders.v2;

/// <summary>
/// Represents the base class for all <see cref="IV2OperationTraitDefinitionBuilder{TBuilder, TTrait}"/> implementations
/// </summary>
/// <typeparam name="TBuilder">The type of <see cref="IV2OperationTraitDefinitionBuilder{TBuilder, TTrait}"/> to return for chaining purposes</typeparam>
/// <typeparam name="TTrait">The type of <see cref="V2OperationTraitDefinition"/> to build</typeparam>
public abstract class V2OperationTraitDefinitionBuilder<TBuilder, TTrait>
    : IV2OperationTraitDefinitionBuilder<TBuilder, TTrait>
    where TBuilder : IV2OperationTraitDefinitionBuilder<TBuilder, TTrait>
    where TTrait : V2OperationTraitDefinition, new()
{

    /// <summary>
    /// Initializes a new <see cref="V2OperationTraitDefinitionBuilder{TBuilder, TTrait}"/>
    /// </summary>
    /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
    /// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="V2OperationTraitDefinition"/>s</param>
    protected V2OperationTraitDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<TTrait>> validators)
    {
        ServiceProvider = serviceProvider;
        Validators = validators;
    }

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="V2OperationTraitDefinition"/>s
    /// </summary>
    protected IEnumerable<IValidator<TTrait>> Validators { get; }

    /// <summary>
    /// Gets the <see cref="V2MessageTraitDefinition"/> to configure
    /// </summary>
    protected virtual TTrait Trait { get; } = new();

    /// <inheritdoc/>
    public virtual TBuilder WithExternalDocumentation(Uri uri, string? description = null)
    {
        ArgumentNullException.ThrowIfNull(uri);
        Trait.ExternalDocs = new ExternalDocumentationDefinition() { Url = uri, Description = description };
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithTag(Action<ITagDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        Trait.Tags ??= [];
        var builder = ActivatorUtilities.CreateInstance<TagDefinitionBuilder>(ServiceProvider);
        setup(builder);
        Trait.Tags.Add(builder.Build());
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
    public virtual TBuilder WithDescription(string? description)
    {
        Trait.Description = description;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithOperationId(string operationId)
    {
        Trait.OperationId = operationId;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithSummary(string summary)
    {
        Trait.Summary = summary;
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
/// Represents the base class for all <see cref="IV2OperationTraitDefinitionBuilder"/> implementations
/// </summary>
/// <remarks>
/// Initializes a new <see cref="V2OperationTraitDefinitionBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="V2OperationTraitDefinition"/>s</param>
public class V2OperationTraitDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V2OperationTraitDefinition>> validators)
    : V2OperationTraitDefinitionBuilder<IV2OperationTraitDefinitionBuilder, V2OperationTraitDefinition>(serviceProvider, validators), IV2OperationTraitDefinitionBuilder
{
}
