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
/// Defines the fundamentals of a service used to build <see cref="V2MessageTraitDefinition"/>s
/// </summary>
/// <typeparam name="TBuilder">The type of <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/> to return for chaining purposes</typeparam>
/// <typeparam name="TTrait">The type of <see cref="V2MessageTraitDefinition"/> to build</typeparam>
public interface IV2MessageTraitDefinitionBuilder<TBuilder, TTrait>
    where TBuilder : IV2MessageTraitDefinitionBuilder<TBuilder, TTrait>
    where TTrait : V2MessageTraitDefinition
{

    /// <summary>
    /// Configures the <see cref="V2MessageTraitDefinition"/> to build to use the specified headers
    /// </summary>
    /// <typeparam name="THeaders">The type used to define the <see cref="V2MessageTraitDefinition"/>'s headers</typeparam>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithHeaders<THeaders>()
        where THeaders : class;

    /// <summary>
    /// Configures the <see cref="V2MessageTraitDefinition"/> to build to use the specified headers
    /// </summary>
    /// <param name="headersType">The type used to define the <see cref="V2MessageTraitDefinition"/>'s headers</param>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithHeaders(Type headersType);

    /// <summary>
    /// Configures the <see cref="V2MessageTraitDefinition"/> to build to use the specified headers
    /// </summary>
    /// <param name="headersSchema">The <see cref="JsonSchema"/> used to define the <see cref="V2MessageTraitDefinition"/>'s headers</param>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithHeaders(JsonSchema headersSchema);

    /// <summary>
    /// Configures the <see cref="V2MessageTraitDefinition"/> to build to use the specified correlation id
    /// </summary>
    /// <param name="location">The location of the correlation id to use</param>
    /// <param name="description">The description of the correlation id to use</param>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithCorrelationId(string location, string? description = null);

    /// <summary>
    /// Configures the <see cref="V2MessageTraitDefinition"/> to build to use the specified correlation id
    /// </summary>
    /// <param name="locationSetup">An <see cref="Action{T}"/> used to build the runtime expression referencing the location of the correlation id to use</param>
    /// <param name="description">The description of the correlation id to use</param>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithCorrelationId(Action<IRuntimeExpressionBuilder> locationSetup, string? description = null);

    /// <summary>
    /// Configures the <see cref="V2MessageTraitDefinition"/> to build to use the specified schema format
    /// </summary>
    /// <param name="schemaFormat">The schema format to use</param>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithSchemaFormat(string schemaFormat);

    /// <summary>
    /// Configures the <see cref="V2MessageTraitDefinition"/> to build to use the specified content type
    /// </summary>
    /// <param name="contentType">The content type to use</param>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithContentType(string contentType);

    /// <summary>
    /// Configures the <see cref="V2MessageTraitDefinition"/> to build to use the specified content type
    /// </summary>
    /// <param name="name">The name to use</param>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithName(string name);

    /// <summary>
    /// Configures the <see cref="V2MessageTraitDefinition"/> to build to use the specified title
    /// </summary>
    /// <param name="title">The title to use</param>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithTitle(string title);

    /// <summary>
    /// Configures the <see cref="V2MessageTraitDefinition"/> to build to use the specified summary
    /// </summary>
    /// <param name="summary">The summary to use</param>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithSummary(string summary);

    /// <summary>
    /// Configures the <see cref="V2MessageTraitDefinition"/> to build to use the specified description
    /// </summary>
    /// <param name="description">The description to use</param>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithDescription(string? description);

    /// <summary>
    /// Adds the specified <see cref="IMessageBindingDefinition"/> to the <see cref="V2MessageTraitDefinition"/> to build
    /// </summary>
    /// <param name="binding">The <see cref="IMessageBindingDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithBinding(IMessageBindingDefinition binding);

    /// <summary>
    /// Marks the <see cref="V2MessageTraitDefinition"/> to build with the specified tag
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the tag to use</param>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithTag(Action<ITagDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified example to the <see cref="V2MessageTraitDefinition"/> to build
    /// </summary>
    /// <param name="name">The name of the example to add</param>
    /// <param name="example">The example to use</param>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithExample(string name, object example);

    /// <summary>
    /// Adds the specified external documentation to the <see cref="V2MessageTraitDefinition"/> to build
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> to the documentation to add</param>
    /// <param name="description">The description of the documentation to add</param>
    /// <returns>The configured <see cref="IV2MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithExternalDocumentation(Uri uri, string? description = null);

    /// <summary>
    /// Builds a new <see cref="V2MessageTraitDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="V2MessageTraitDefinition"/></returns>
    TTrait Build();

}

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="V2MessageTraitDefinition"/>s
/// </summary>
public interface IV2MessageTraitDefinitionBuilder
    : IV2MessageTraitDefinitionBuilder<IV2MessageTraitDefinitionBuilder, V2MessageTraitDefinition>
{


}
