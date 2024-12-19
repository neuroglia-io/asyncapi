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
/// Represents the base class for all <see cref="IV3MessageTraitDefinitionBuilder{TBuilder, TTrait}"/> implementations
/// </summary>
/// <typeparam name="TBuilder">The type of <see cref="IV3MessageTraitDefinitionBuilder{TBuilder, TTrait}"/> to return for chaining purposes</typeparam>
/// <typeparam name="TTrait">The type of <see cref="V3MessageTraitDefinition"/> to build</typeparam>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V3MessageTraitDefinition"/>s</param>
public abstract class V3MessageTraitDefinitionBuilder<TBuilder, TTrait>(IServiceProvider serviceProvider, IEnumerable<IValidator<TTrait>> validators)
    : IV3MessageTraitDefinitionBuilder<TBuilder, TTrait>
    where TBuilder : IV3MessageTraitDefinitionBuilder<TBuilder, TTrait>
    where TTrait : V3MessageTraitDefinition, new()
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="V3MessageTraitDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<TTrait>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V3MessageDefinition"/> to configure
    /// </summary>
    protected TTrait Trait { get; } = new();

    /// <inheritdoc/>
    public virtual void Use(string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        Trait.Reference = reference;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithHeadersSchema(Action<IV3SchemaDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3SchemaDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Trait.Headers = builder.Build();
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithCorrelationId(Action<IV3CorrelationIdDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3CorrelationIdDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Trait.CorrelationId = builder.Build();
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithContentType(string? contentType)
    {
        Trait.ContentType = contentType;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithName(string? name)
    {
        Trait.Name = name;
        return (TBuilder)(object)this;
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
    public virtual TBuilder WithTag(Action<IV3TagDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3TagDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Trait.Tags ??= [];
        Trait.Tags.Add(builder.Build());
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithExternalDocumentation(Action<IV3ExternalDocumentationDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3ExternalDocumentationDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Trait.ExternalDocs = builder.Build();
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithBinding(IMessageBindingDefinition binding)
    {
        ArgumentNullException.ThrowIfNull(binding);
        Trait.Bindings ??= new();
        Trait.Bindings.Add(binding);
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithBindings(Action<IMessageBindingDefinitionCollectionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<MessageBindingDefinitionCollectionBuilder>(this.ServiceProvider);
        setup(builder);
        Trait.Bindings = builder.Build();
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithExample(Action<IV3MessageExampleDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3MessageExampleDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Trait.Examples ??= [];
        Trait.Examples.Add(builder.Build());
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
/// Represents the default implementation of the <see cref="IV3MessageTraitDefinitionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="V3MessageTraitDefinition"/>s</param>
public class V3MessageTraitDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V3MessageTraitDefinition>> validators)
    : V3MessageTraitDefinitionBuilder<IV3MessageTraitDefinitionBuilder, V3MessageTraitDefinition>(serviceProvider, validators), IV3MessageTraitDefinitionBuilder
{

}
