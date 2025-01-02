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
/// Represents the default implementation of the <see cref="IV3CorrelationIdDefinitionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="V3CorrelationIdDefinition"/></param>
public class V3CorrelationIdDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V3CorrelationIdDefinition>> validators)
    : IV3CorrelationIdDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="V3CorrelationIdDefinition"/>
    /// </summary>
    protected IEnumerable<IValidator<V3CorrelationIdDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V3CorrelationIdDefinition"/> to configure
    /// </summary>
    protected V3CorrelationIdDefinition CorrelationId { get; } = new();

    /// <inheritdoc/>
    public virtual void Use(string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        CorrelationId.Reference = reference;
    }

    /// <inheritdoc/>
    public virtual IV3CorrelationIdDefinitionBuilder WithLocation(string location)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(location);
        CorrelationId.Location = location;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3CorrelationIdDefinitionBuilder WithDescription(string? description)
    {
        CorrelationId.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual V3CorrelationIdDefinition Build()
    {
        var validationResults = Validators.Select(v => v.Validate(CorrelationId));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return CorrelationId;
    }

}
