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
/// Represents the default implementation of the <see cref="IV3SchemaDefinitionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V3SchemaDefinition"/>s</param>
public class V3SchemaDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V3SchemaDefinition>> validators)
    : IV3SchemaDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="V3SchemaDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V3SchemaDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V3SchemaDefinition"/> to configure
    /// </summary>
    protected V3SchemaDefinition Schema { get; } = new();

    /// <inheritdoc/>
    public virtual void Use(string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        Schema.Reference = reference;
    }

    /// <inheritdoc/>
    public virtual IV3SchemaDefinitionBuilder WithFormat(string format)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(format);
        Schema.SchemaFormat = format;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3SchemaDefinitionBuilder WithSchema(object schema)
    {
        ArgumentNullException.ThrowIfNull(schema);
        Schema.Schema = schema;
        return this;
    }

    /// <inheritdoc/>
    public virtual V3SchemaDefinition Build()
    {
        var validationResults = Validators.Select(v => v.Validate(Schema));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors!));
        return Schema;
    }

}