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

using Neuroglia.AsyncApi.FluentBuilders;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IServerVariableDefinitionBuilder"/> interface
/// </summary>
/// <remarks>
/// Initializes a new <see cref="ServerVariableDefinitionBuilder"/>
/// </remarks>
/// <param name="validators">The services used to validate <see cref="ServerVariableDefinition"/>s</param>
public class ServerVariableDefinitionBuilder(IEnumerable<IValidator<ServerVariableDefinition>> validators)
    : IServerVariableDefinitionBuilder
{

    /// <summary>
    /// Gets the services used to validate <see cref="ServerVariableDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<ServerVariableDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="ServerVariableDefinition"/> to build
    /// </summary>
    protected ServerVariableDefinition Variable { get; } = new();

    /// <inheritdoc/>
    public virtual IServerVariableDefinitionBuilder WithEnumValues(params string[] values)
    {
        Variable.Enum = new(values);
        return this;
    }

    /// <inheritdoc/>
    public virtual IServerVariableDefinitionBuilder WithDefaultValue(string value)
    {
        Variable.Default = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IServerVariableDefinitionBuilder WithDescription(string? description)
    {
        Variable.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IServerVariableDefinitionBuilder WithExample(string example)
    {
        if (string.IsNullOrWhiteSpace(example)) throw new ArgumentNullException(nameof(example));
        Variable.Examples ??= [];
        Variable.Examples.Add(example);
        return this;
    }

    /// <inheritdoc/>
    public virtual ServerVariableDefinition Build() => Variable;

}
