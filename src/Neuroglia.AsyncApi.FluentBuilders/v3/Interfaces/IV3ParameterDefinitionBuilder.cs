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
/// Defines the fundamentals of a service used to build <see cref="V3ParameterDefinition"/>s
/// </summary>
public interface IV3ParameterDefinitionBuilder
    : IReferenceableComponentDefinitionBuilder<V3ParameterDefinition>
{

    /// <summary>
    /// Configures the <see cref="V3ParameterDefinition"/> to use the specified enum values
    /// </summary>
    /// <param name="values">The enum values to use</param>
    /// <returns>The configured <see cref="IV3ParameterDefinitionBuilder"/></returns>
    IV3ParameterDefinitionBuilder WithEnumValues(params string[]? values);

    /// <summary>
    /// Configures the <see cref="V3ParameterDefinition"/> to use the specified default value
    /// </summary>
    /// <param name="value">The default value to use</param>
    /// <returns>The configured <see cref="IV3ParameterDefinitionBuilder"/></returns>
    IV3ParameterDefinitionBuilder WithDefaultValue(string? value);

    /// <summary>
    /// Configures the <see cref="V3ParameterDefinition"/> to use the specified description
    /// </summary>
    /// <param name="description">The description to use</param>
    /// <returns>The configured <see cref="IV3ParameterDefinitionBuilder"/></returns>
    IV3ParameterDefinitionBuilder WithDescription(string? description);

    /// <summary>
    /// Configures the <see cref="V3ParameterDefinition"/> to use the specified example
    /// </summary>
    /// <param name="example">The example to use</param>
    /// <returns>The configured <see cref="IV3ParameterDefinitionBuilder"/></returns>
    IV3ParameterDefinitionBuilder WithExample(string example);

    /// <summary>
    /// Configures the <see cref="V3ParameterDefinition"/> to use the specified location
    /// </summary>
    /// <param name="location">The location to use</param>
    /// <returns>The configured <see cref="IV3ParameterDefinitionBuilder"/></returns>
    IV3ParameterDefinitionBuilder WithLocation(string? location);

    /// <summary>
    /// Builds the configured <see cref="V3ParameterDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="V3ParameterDefinition"/></returns>
    V3ParameterDefinition Build();

}
