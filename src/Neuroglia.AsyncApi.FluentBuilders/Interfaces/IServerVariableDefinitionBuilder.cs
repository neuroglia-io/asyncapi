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

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="ServerVariableDefinition"/>s
/// </summary>
public interface IServerVariableDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="ServerVariableDefinition"/> to build to use the specified enum values
    /// </summary>
    /// <param name="values">An array of values to be used if the substitution options are from a limited set.</param>
    /// <returns>The configured <see cref="IServerVariableDefinitionBuilder"/></returns>
    IServerVariableDefinitionBuilder WithEnumValues(params string[] values);

    /// <summary>
    /// Configures the <see cref="ServerVariableDefinition"/> to build to use the specified default value
    /// </summary>
    /// <param name="value">The value to use by default for substitution, and to send, if an alternate value is not supplied.</param>
    /// <returns>The configured <see cref="IServerVariableDefinitionBuilder"/></returns>
    IServerVariableDefinitionBuilder WithDefaultValue(string value);

    /// <summary>
    /// Configures the <see cref="ServerVariableDefinition"/> to use the specified description
    /// </summary>
    /// <param name="description">The description to use</param>
    /// <returns>The configured <see cref="IServerVariableDefinitionBuilder"/></returns>
    IServerVariableDefinitionBuilder WithDescription(string? description);

    /// <summary>
    /// Adds the specified example to the <see cref="ServerVariableDefinition"/> to build
    /// </summary>
    /// <param name="example">The example to add</param>
    /// <returns>The configured <see cref="IServerVariableDefinitionBuilder"/></returns>
    IServerVariableDefinitionBuilder WithExample(string example);

    /// <summary>
    /// Builds a new <see cref="ServerVariableDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="ServerVariableDefinition"/></returns>
    ServerVariableDefinition Build();

}
