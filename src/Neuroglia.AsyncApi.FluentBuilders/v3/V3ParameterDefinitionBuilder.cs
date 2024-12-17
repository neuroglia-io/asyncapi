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
/// Represents the default implementation of the <see cref="IV3ParameterDefinitionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V3ParameterDefinition"/>s</param>
public class V3ParameterDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V3ParameterDefinition>> validators)
    : IV3ParameterDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="V3ParameterDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V3ParameterDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V3ParameterDefinition"/> to configure
    /// </summary>
    protected V3ParameterDefinition Parameter { get; } = new();

    /// <inheritdoc/>
    public virtual void Use(string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        Parameter.Reference = reference;
    }

    /// <inheritdoc/>
    public virtual IV3ParameterDefinitionBuilder WithEnumValues(params string[]? values)
    {
        Parameter.Enum = values == null ? null : new(values);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ParameterDefinitionBuilder WithDefaultValue(string? value)
    {
        Parameter.Default = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ParameterDefinitionBuilder WithDescription(string? description)
    {
        Parameter.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ParameterDefinitionBuilder WithExample(string example)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(example);
        Parameter.Examples ??= [];
        Parameter.Examples.Add(example);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ParameterDefinitionBuilder WithLocation(string location)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(location);
        Parameter.Location = location;
        return this;
    }

    /// <inheritdoc/>
    public virtual V3ParameterDefinition Build()
    {
        var validationResults = Validators.Select(v => v.Validate(Parameter));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors!));
        return Parameter;
    }

}
