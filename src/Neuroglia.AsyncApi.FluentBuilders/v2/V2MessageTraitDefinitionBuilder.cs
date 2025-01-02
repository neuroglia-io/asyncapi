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

using Neuroglia.Data.Schemas.Json;

namespace Neuroglia.AsyncApi.FluentBuilders.v2;

/// <summary>
/// Represents the base class for all <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/> implementations
/// </summary>
/// <typeparam name="TBuilder">The type of <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/> to return for chaining purposes</typeparam>
/// <typeparam name="TTrait">The type of <see cref="V2MessageTraitDefinition"/> to build</typeparam>
public abstract class V2MessageTraitDefinitionBuilder<TBuilder, TTrait>
    : IV2MessageTraitDefinitionBuilder<TBuilder, TTrait>
    where TBuilder : IV2MessageTraitDefinitionBuilder<TBuilder, TTrait>
    where TTrait : V2MessageTraitDefinition, new()
{

    /// <summary>
    /// Initializes a new <see cref="V2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/>
    /// </summary>
    /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
    /// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="V2MessageTraitDefinition"/>s</param>
    protected V2MessageTraitDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<TTrait>> validators)
    {
        ServiceProvider = serviceProvider;
        Validators = validators;
    }

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="V2MessageTraitDefinition"/>s
    /// </summary>
    protected IEnumerable<IValidator<TTrait>> Validators { get; }

    /// <summary>
    /// Gets the <see cref="V2MessageTraitDefinition"/> to configure
    /// </summary>
    protected virtual TTrait Trait { get; } = new();

    /// <inheritdoc/>
    public virtual TBuilder WithExample(string name, object example)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(example);
        Trait.Examples ??= [];
        Trait.Examples.Add(name, example);
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithExternalDocumentation(Uri uri, string? description = null)
    {
        ArgumentNullException.ThrowIfNull(uri);
        Trait.ExternalDocs = new V2ExternalDocumentationDefinition() { Url = uri, Description = description };
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithTag(Action<IV2TagDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        Trait.Tags ??= [];
        var builder = ActivatorUtilities.CreateInstance<V2TagDefinitionBuilder>(ServiceProvider);
        setup(builder);
        Trait.Tags.Add(builder.Build());
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
    public virtual TBuilder WithContentType(string contentType)
    {
        Trait.ContentType = contentType;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithCorrelationId(string location, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(location)) throw new ArgumentNullException(nameof(location));
        Trait.CorrelationId = new() { Location = location, Description = description };
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithCorrelationId(Action<IRuntimeExpressionBuilder> locationSetup, string? description = null)
    {
        var expressionBuilder = new RuntimeExpressionBuilder();
        locationSetup(expressionBuilder);
        return WithCorrelationId(expressionBuilder.Build().ToString(), description);
    }

    /// <inheritdoc/>
    public virtual TBuilder WithDescription(string? description)
    {
        Trait.Description = description;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithHeaders<THeaders>() where THeaders : class => WithHeaders(typeof(THeaders));

    /// <inheritdoc/>
    public virtual TBuilder WithHeaders(Type headersType)
    {
        ArgumentNullException.ThrowIfNull(headersType);
        return WithHeaders(new JsonSchemaBuilder().FromType(headersType, JsonSchemaGeneratorConfiguration.Default));
    }

    /// <inheritdoc/>
    public virtual TBuilder WithHeaders(JsonSchema headersSchema)
    {
        Trait.Headers = headersSchema ?? throw new ArgumentNullException(nameof(headersSchema));
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithName(string name)
    {
        Trait.Name = name;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithSchemaFormat(string schemaFormat)
    {
        Trait.SchemaFormat = schemaFormat;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithSummary(string summary)
    {
        Trait.Summary = summary;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TBuilder WithTitle(string title)
    {
        Trait.Title = title;
        return (TBuilder)(object)this;
    }

    /// <inheritdoc/>
    public virtual TTrait Build()
    {
        var validationResults = Validators.Select(v => v.Validate(Trait));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return Trait;
    }

}

/// <summary>
/// Represents the default implementation of the <see cref="IV2MessageTraitDefinitionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="V2MessageTraitDefinition"/>s</param>
public class V2MessageTraitDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V2MessageTraitDefinition>> validators)
    : V2MessageTraitDefinitionBuilder<IV2MessageTraitDefinitionBuilder, V2MessageTraitDefinition>(serviceProvider, validators), IV2MessageTraitDefinitionBuilder
{

}
