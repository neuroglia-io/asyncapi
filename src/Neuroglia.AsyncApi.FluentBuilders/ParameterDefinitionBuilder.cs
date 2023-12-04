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

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IParameterDefinitionBuilder"/> interface
/// </summary>
/// <remarks>
/// Initializes a new <see cref="ParameterDefinitionBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="ParameterDefinition"/>s</param>
public class ParameterDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<ParameterDefinition>> validators)
    : IParameterDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="ParameterDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<ParameterDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="ParameterDefinition"/> to configure
    /// </summary>
    protected virtual ParameterDefinition Parameter { get; } = new();

    /// <inheritdoc/>
    public virtual IParameterDefinitionBuilder OfType<TParameter>() => this.OfType(typeof(TParameter));

    /// <inheritdoc/>
    public virtual IParameterDefinitionBuilder OfType(Type parameterType)
    {
        ArgumentNullException.ThrowIfNull(parameterType);
        return this.WithSchema(new JsonSchemaBuilder().FromType(parameterType, JsonSchemaGeneratorConfiguration.Default));
    }

    /// <inheritdoc/>
    public virtual IParameterDefinitionBuilder WithSchema(JsonSchema schema)
    {
        this.Parameter.Schema = schema ?? throw new ArgumentNullException(nameof(schema));
        return this;
    }

    /// <inheritdoc/>
    public virtual IParameterDefinitionBuilder WithDescription(string? description)
    {
        this.Parameter.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IParameterDefinitionBuilder WithLocation(string location)
    {
        if (string.IsNullOrWhiteSpace(location)) throw new ArgumentNullException(nameof(location));
        this.Parameter.Location = location;
        return this;
    }

    /// <inheritdoc/>
    public virtual ParameterDefinition Build()
    {
        var validationResults = this.Validators.Select(v => v.Validate(this.Parameter));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return this.Parameter;
    }

}
