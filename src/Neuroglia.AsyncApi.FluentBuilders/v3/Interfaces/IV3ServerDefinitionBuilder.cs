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
/// Defines the fundamentals of a service used to build <see cref="V3ServerDefinition"/>s
/// </summary>
public interface IV3ServerDefinitionBuilder
    : IReferenceableComponentDefinitionBuilder<V3ServerDefinition>
{

    /// <summary>
    /// Configures the <see cref="V3ServerDefinition"/> to use the specified host
    /// </summary>
    /// <param name="host">The host to use</param>
    /// <returns>The configured <see cref="IV3ServerDefinitionBuilder"/></returns>
    IV3ServerDefinitionBuilder WithHost(string host);

    /// <summary>
    /// Configures the <see cref="V3ServerDefinition"/> to use the specified protocol
    /// </summary>
    /// <param name="protocol">The protocol to use</param>
    /// <param name="version">The protocol version to use</param>
    /// <returns>The configured <see cref="IV3ServerDefinitionBuilder"/></returns>
    IV3ServerDefinitionBuilder WithProtocol(string protocol, string? version = null);

    /// <summary>
    /// Configures the <see cref="V3ServerDefinition"/> to use the specified path name
    /// </summary>
    /// <param name="pathName">The path name to use</param>
    /// <returns>The configured <see cref="IV3ServerDefinitionBuilder"/></returns>
    IV3ServerDefinitionBuilder WithPathName(string pathName);

    /// <summary>
    /// Configures the <see cref="V3ServerDefinition"/> to use the specified description
    /// </summary>
    /// <param name="description">The description to use</param>
    /// <returns>The configured <see cref="IV3ServerDefinitionBuilder"/></returns>
    IV3ServerDefinitionBuilder WithDescription(string? description);

    /// <summary>
    /// Configures the <see cref="V3ServerDefinition"/> to use the specified title
    /// </summary>
    /// <param name="title">The title to use</param>
    /// <returns>The configured <see cref="IV3ServerDefinitionBuilder"/></returns>
    IV3ServerDefinitionBuilder WithTitle(string? title);

    /// <summary>
    /// Configures the <see cref="V3ServerDefinition"/> to use the specified summary
    /// </summary>
    /// <param name="summary">The summary to use</param>
    /// <returns>The configured <see cref="IV3ServerDefinitionBuilder"/></returns>
    IV3ServerDefinitionBuilder WithSummary(string? summary);

    /// <summary>
    /// Adds the specified <see cref="ServerVariableDefinition"/> to the <see cref="V3ServerDefinition"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="ServerVariableDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="ServerVariableDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3ServerDefinitionBuilder"/></returns>
    IV3ServerDefinitionBuilder WithVariable(string name, Action<IServerVariableDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="V3ServerDefinition"/> to use the specified <see cref="V3SecuritySchemeDefinition"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the <see cref="V3SecuritySchemeDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV3ServerDefinitionBuilder"/></returns>
    IV3ServerDefinitionBuilder WithSecurity(Action<IV3SecuritySchemeDefinitionBuilder> setup);

    /// <summary>
    /// Marks the <see cref="V3ServerDefinition"/> to build with the specified tag
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the tag to use</param>
    /// <returns>The configured <see cref="IV3ServerDefinitionBuilder"/></returns>
    IV3ServerDefinitionBuilder WithTag(Action<ITagDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="V3ServerDefinition"/> to use the specified external documentation
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the external documentation to use</param>
    /// <returns>The configured <see cref="IV3ServerDefinitionBuilder"/></returns>
    IV3ServerDefinitionBuilder WithExternalDocumentation(Action<IExternalDocumentationDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="IServerBindingDefinition"/> to the <see cref="V3ServerDefinition"/> to build
    /// </summary>
    /// <param name="binding">The <see cref="IServerBindingDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3ServerDefinitionBuilder"/></returns>
    IV3ServerDefinitionBuilder WithBinding(IServerBindingDefinition binding);

    /// <summary>
    /// Builds the configured <see cref="V3ServerDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="V3ServerDefinition"/></returns>
    V3ServerDefinition Build();

}
