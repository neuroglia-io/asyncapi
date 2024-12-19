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
/// Represents the default implementation of the <see cref="IV2ServerVariableDefinitionBuilder"/> interface
/// </summary>
/// <remarks>
/// Initializes a new <see cref="V2ServerVariableDefinitionBuilder"/>
/// </remarks>
/// <param name="validators">The services used to validate <see cref="V2ServerVariableDefinition"/>s</param>
public class V2ServerVariableDefinitionBuilder(IEnumerable<IValidator<V2ServerVariableDefinition>> validators)
    : IV2ServerVariableDefinitionBuilder
{

    /// <summary>
    /// Gets the services used to validate <see cref="V2ServerVariableDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V2ServerVariableDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V2ServerVariableDefinition"/> to build
    /// </summary>
    protected V2ServerVariableDefinition Variable { get; } = new();

    /// <inheritdoc/>
    public virtual IV2ServerVariableDefinitionBuilder WithEnumValues(params string[] values)
    {
        Variable.Enum = new(values);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ServerVariableDefinitionBuilder WithDefaultValue(string value)
    {
        Variable.Default = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ServerVariableDefinitionBuilder WithDescription(string? description)
    {
        Variable.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ServerVariableDefinitionBuilder WithExample(string example)
    {
        if (string.IsNullOrWhiteSpace(example)) throw new ArgumentNullException(nameof(example));
        Variable.Examples ??= [];
        Variable.Examples.Add(example);
        return this;
    }

    /// <inheritdoc/>
    public virtual V2ServerVariableDefinition Build() => Variable;

}
