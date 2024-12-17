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

using Neuroglia.AsyncApi.FluentBuilders.v2;
using Neuroglia.Data.Schemas.Json;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IV2ParameterDefinitionBuilder"/> interface
/// </summary>
/// <remarks>
/// Initializes a new <see cref="ParameterDefinitionBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V2ParameterDefinition"/>s</param>
public class ParameterDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V2ParameterDefinition>> validators)
    : IV2ParameterDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="V2ParameterDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V2ParameterDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V2ParameterDefinition"/> to configure
    /// </summary>
    protected virtual V2ParameterDefinition Parameter { get; } = new();

    /// <inheritdoc/>
    public virtual IV2ParameterDefinitionBuilder OfType<TParameter>() => OfType(typeof(TParameter));

    /// <inheritdoc/>
    public virtual IV2ParameterDefinitionBuilder OfType(Type parameterType)
    {
        ArgumentNullException.ThrowIfNull(parameterType);
        return WithSchema(new JsonSchemaBuilder().FromType(parameterType, JsonSchemaGeneratorConfiguration.Default));
    }

    /// <inheritdoc/>
    public virtual IV2ParameterDefinitionBuilder WithSchema(JsonSchema schema)
    {
        Parameter.Schema = schema ?? throw new ArgumentNullException(nameof(schema));
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ParameterDefinitionBuilder WithDescription(string? description)
    {
        Parameter.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ParameterDefinitionBuilder WithLocation(string location)
    {
        if (string.IsNullOrWhiteSpace(location)) throw new ArgumentNullException(nameof(location));
        Parameter.Location = location;
        return this;
    }

    /// <inheritdoc/>
    public virtual V2ParameterDefinition Build()
    {
        var validationResults = Validators.Select(v => v.Validate(Parameter));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return Parameter;
    }

}
