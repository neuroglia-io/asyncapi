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
/// Defines the fundamentals of a service used to build <see cref="V3OperationTraitDefinition"/>s
/// </summary>
/// <typeparam name="TBuilder">The type of <see cref="IV3OperationTraitDefinitionBuilder{TBuilder, TTrait}"/> to return for chaining purposes</typeparam>
/// <typeparam name="TTrait">The type of <see cref="V3OperationTraitDefinition"/> to build</typeparam>
public interface IV3OperationTraitDefinitionBuilder<TBuilder, TTrait>
    : IReferenceableComponentDefinitionBuilder<TTrait>
    where TBuilder : IV3OperationTraitDefinitionBuilder<TBuilder, TTrait>
    where TTrait : V3OperationTraitDefinition
{

    /// <summary>
    /// Configures the <see cref="V3OperationTraitDefinition"/> to use the specified title
    /// </summary>
    /// <param name="title">The title to use</param>
    /// <returns>The configured <see cref="IV3OperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithTitle(string? title);

    /// <summary>
    /// Configures the <see cref="V3OperationTraitDefinition"/> to use the specified API description
    /// </summary>
    /// <param name="summary">The summary of the Async Api document to build</param>
    /// <returns>The configured <see cref="IV3OperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithSummary(string? summary);

    /// <summary>
    /// Configures the <see cref="V3OperationTraitDefinition"/> to use the specified API description
    /// </summary>
    /// <param name="description">The description of the Async Api document to build</param>
    /// <returns>The configured <see cref="IV3OperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithDescription(string? description);

    /// <summary>
    /// Configures the <see cref="V3OperationTraitDefinition"/> to use the specified <see cref="V3SecuritySchemeDefinition"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V3SecuritySchemeDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV3OperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithSecurity(Action<IV3SecuritySchemeDefinitionBuilder> setup);

    /// <summary>
    /// Marks the <see cref="V3OperationTraitDefinition"/> to build with the specified tag
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the tag to use</param>
    /// <returns>The configured <see cref="IV3OperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithTag(Action<IV3TagDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified external documentation to the <see cref="V3ExternalDocumentationDefinition"/> to build
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V2ExternalDocumentationDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV3OperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithExternalDocumentation(Action<IV3ExternalDocumentationDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="IOperationBindingDefinition"/> to the <see cref="V3OperationTraitDefinition"/> to build
    /// </summary>
    /// <param name="binding">The <see cref="IOperationBindingDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3OperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithBinding(IOperationBindingDefinition binding);

    /// <summary>
    /// Builds a new <see cref="V3OperationTraitDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="V3OperationTraitDefinition"/></returns>
    TTrait Build();

}

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="V3OperationTraitDefinition"/>s
/// </summary>
public interface IV3OperationTraitDefinitionBuilder
    : IV3OperationTraitDefinitionBuilder<IV3OperationTraitDefinitionBuilder, V3OperationTraitDefinition>
{



}