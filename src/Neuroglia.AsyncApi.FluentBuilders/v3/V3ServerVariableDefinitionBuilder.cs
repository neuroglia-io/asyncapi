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
/// Represents the default implementation of the <see cref="IV3ServerVariableDefinitionBuilder"/> interface
/// </summary>
/// <param name="validators">The services used to validate <see cref="V3ServerVariableDefinition"/>s</param>
public class V3ServerVariableDefinitionBuilder(IEnumerable<IValidator<V3ServerVariableDefinition>> validators)
    : IV3ServerVariableDefinitionBuilder
{

    /// <summary>
    /// Gets the services used to validate <see cref="V3ServerVariableDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V3ServerVariableDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V3ServerVariableDefinition"/> to build
    /// </summary>
    protected V3ServerVariableDefinition Variable { get; } = new();

    /// <inheritdoc/>
    public virtual void Use(string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        Variable.Reference = reference;
    }

    /// <inheritdoc/>
    public virtual IV3ServerVariableDefinitionBuilder WithEnumValues(params string[] values)
    {
        Variable.Enum = new(values);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ServerVariableDefinitionBuilder WithDefaultValue(string value)
    {
        Variable.Default = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ServerVariableDefinitionBuilder WithDescription(string? description)
    {
        Variable.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ServerVariableDefinitionBuilder WithExample(string example)
    {
        if (string.IsNullOrWhiteSpace(example)) throw new ArgumentNullException(nameof(example));
        Variable.Examples ??= [];
        Variable.Examples.Add(example);
        return this;
    }

    /// <inheritdoc/>
    public virtual V3ServerVariableDefinition Build() => Variable;

}
