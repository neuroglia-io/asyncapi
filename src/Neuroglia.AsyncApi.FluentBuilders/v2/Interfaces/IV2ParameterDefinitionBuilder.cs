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
/// Defines the fundamentals of the service used to build <see cref="V2ParameterDefinition"/>s
/// </summary>
public interface IV2ParameterDefinitionBuilder
{

    /// <summary>
    /// Configures the type of the <see cref="V2ParameterDefinition"/> to build
    /// </summary>
    /// <typeparam name="TParameter">The type of the <see cref="V2ParameterDefinition"/> to build</typeparam>
    /// <returns>The configured <see cref="IV2ParameterDefinitionBuilder"/></returns>
    IV2ParameterDefinitionBuilder OfType<TParameter>();

    /// <summary>
    /// Configures the type of the <see cref="V2ParameterDefinition"/> to build
    /// </summary>
    /// <param name="parameterType">The type of the <see cref="V2ParameterDefinition"/> to build</param>
    /// <returns>The configured <see cref="IV2ParameterDefinitionBuilder"/></returns>
    IV2ParameterDefinitionBuilder OfType(Type parameterType);

    /// <summary>
    /// Configures the <see cref="JsonSchema"/> of the <see cref="V2ParameterDefinition"/> to build
    /// </summary>
    /// <param name="schema">The <see cref="JsonSchema"/> of the <see cref="V2ParameterDefinition"/> to build</param>
    /// <returns>The configured <see cref="IV2ParameterDefinitionBuilder"/></returns>
    IV2ParameterDefinitionBuilder WithSchema(JsonSchema schema);

    /// <summary>
    /// Configures the <see cref="V2ParameterDefinition"/> to build to use the specified description
    /// </summary>
    /// <param name="description">The description to use</param>
    /// <returns>The configured <see cref="IV2ParameterDefinitionBuilder"/></returns>
    IV2ParameterDefinitionBuilder WithDescription(string? description);

    /// <summary>
    /// Sets the location of the <see cref="V2ParameterDefinition"/> to build
    /// </summary>
    /// <param name="location">A runtime expression that specifies the location of the parameter value</param>
    /// <returns>The configured <see cref="IV2ParameterDefinitionBuilder"/></returns>
    IV2ParameterDefinitionBuilder WithLocation(string location);

    /// <summary>
    /// Builds a new <see cref="V2ParameterDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="V2ParameterDefinition"/></returns>
    V2ParameterDefinition Build();

}
