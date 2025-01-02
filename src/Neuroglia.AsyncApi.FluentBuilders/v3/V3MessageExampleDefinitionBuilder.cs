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
/// Represents the base class for all <see cref="IV3MessageExampleDefinitionBuilder"/> implementations
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V3MessageExampleDefinition"/>s</param>
public class V3MessageExampleDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V3MessageExampleDefinition>> validators)
    : IV3MessageExampleDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="V3MessageExampleDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V3MessageExampleDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V3MessageExampleDefinition"/> to configure
    /// </summary>
    protected V3MessageExampleDefinition MessageExample { get; } = new();

    /// <inheritdoc/>
    public virtual IV3MessageExampleDefinitionBuilder WithHeader(string name, object value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(value);
        MessageExample.Headers ??= [];
        MessageExample.Headers[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3MessageExampleDefinitionBuilder WithHeaders(IDictionary<string, object>? headers)
    {
        MessageExample.Headers = headers == null ? null : new(headers);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3MessageExampleDefinitionBuilder WithPayload(IDictionary<string, object>? payload)
    {
        MessageExample.Payload = payload == null ? null : new(payload);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3MessageExampleDefinitionBuilder WithName(string? name)
    {
        MessageExample.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3MessageExampleDefinitionBuilder WithSummary(string? summary)
    {
        MessageExample.Summary = summary;
        return this;
    }

    /// <inheritdoc/>
    public virtual V3MessageExampleDefinition Build()
    {
        var validationResults = Validators.Select(v => v.Validate(MessageExample));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors!));
        return MessageExample;
    }

}
