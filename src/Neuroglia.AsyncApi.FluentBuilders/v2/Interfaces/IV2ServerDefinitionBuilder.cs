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
/// Defines the fundamentals of a service used to build <see cref="V2ServerDefinition"/>s
/// </summary>
public interface IV2ServerDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="V2ServerDefinition"/> to use the specified url
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> of the <see cref="V2ServerDefinition"/> to build</param>
    /// <returns>The configured <see cref="IV2ServerDefinitionBuilder"/></returns>
    IV2ServerDefinitionBuilder WithUrl(Uri uri);

    /// <summary>
    /// Configures the <see cref="V2ServerDefinition"/> to use the specified protocol
    /// </summary>
    /// <param name="protocol">The protocol to use</param>
    /// <param name="version">The protocol version to use</param>
    /// <returns>The configured <see cref="IV2ServerDefinitionBuilder"/></returns>
    IV2ServerDefinitionBuilder WithProtocol(string protocol, string? version = null);

    /// <summary>
    /// Configures the <see cref="V2ServerDefinition"/> to use the specified description
    /// </summary>
    /// <param name="description">The description to use</param>
    /// <returns>The configured <see cref="IV2ServerDefinitionBuilder"/></returns>
    IV2ServerDefinitionBuilder WithDescription(string? description);

    /// <summary>
    /// Adds the specified <see cref="V2ServerVariableDefinition"/> to the <see cref="V2ServerDefinition"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="V2ServerVariableDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V2ServerVariableDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2ServerDefinitionBuilder"/></returns>
    IV2ServerDefinitionBuilder WithVariable(string name, Action<IV2ServerVariableDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="V2ServerDefinition"/> to build to use the specified <see cref="V2SecuritySchemeDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="V2SecuritySchemeDefinition"/> to add</param>
    /// <param name="requirement">The security requirement object, if any</param>
    /// <returns>The configured <see cref="IV2ServerDefinitionBuilder"/></returns>
    IV2ServerDefinitionBuilder WithSecurityRequirement(string name, object? requirement = null);

    /// <summary>
    /// Adds the specified <see cref="IServerBindingDefinition"/> to the <see cref="V2ServerDefinition"/> to build
    /// </summary>
    /// <param name="binding">The <see cref="IServerBindingDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2ServerDefinitionBuilder"/></returns>
    IV2ServerDefinitionBuilder WithBinding(IServerBindingDefinition binding);

    /// <summary>
    /// Builds a new <see cref="V2ServerDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="V2ServerDefinition"/></returns>
    V2ServerDefinition Build();

}
