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
/// Defines the fundamentals of a service used to build <see cref="V2OperationTraitDefinition"/>s
/// </summary>
/// <typeparam name="TBuilder">The type of <see cref="IV2OperationTraitDefinitionBuilder{TBuilder, TTrait}"/> to return for chaining purposes</typeparam>
/// <typeparam name="TTrait">The type of <see cref="V2OperationTraitDefinition"/> to build</typeparam>
public interface IV2OperationTraitDefinitionBuilder<TBuilder, TTrait>
    where TBuilder : IV2OperationTraitDefinitionBuilder<TBuilder, TTrait>
    where TTrait : V2OperationTraitDefinition
{

    /// <summary>
    /// Configures the <see cref="V2OperationTraitDefinition"/> to use the specified id
    /// </summary>
    /// <param name="operationId">The id of the Async Api document to build</param>
    /// <returns>The configured <see cref="IV2OperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithOperationId(string operationId);

    /// <summary>
    /// Configures the <see cref="V2OperationTraitDefinition"/> to use the specified API description
    /// </summary>
    /// <param name="summary">The summary of the Async Api document to build</param>
    /// <returns>The configured <see cref="IV2OperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithSummary(string summary);

    /// <summary>
    /// Configures the <see cref="V2OperationTraitDefinition"/> to use the specified API description
    /// </summary>
    /// <param name="description">The description of the Async Api document to build</param>
    /// <returns>The configured <see cref="IV2OperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithDescription(string? description);

    /// <summary>
    /// Marks the <see cref="V2OperationTraitDefinition"/> to build with the specified tag
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the tag to use</param>
    /// <returns>The configured <see cref="IV2OperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithTag(Action<IV2TagDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified external documentation to the <see cref="V2OperationTraitDefinition"/> to build
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> to the documentation to add</param>
    /// <param name="description">The description of the documentation to add</param>
    /// <returns>The configured <see cref="IV2OperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithExternalDocumentation(Uri uri, string? description = null);

    /// <summary>
    /// Adds the specified <see cref="IOperationBindingDefinition"/> to the <see cref="V2OperationTraitDefinition"/> to build
    /// </summary>
    /// <param name="binding">The <see cref="IOperationBindingDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2OperationTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    TBuilder WithBinding(IOperationBindingDefinition binding);

    /// <summary>
    /// Builds a new <see cref="V2OperationTraitDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="V2OperationTraitDefinition"/></returns>
    TTrait Build();

}

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="V2OperationTraitDefinition"/>s
/// </summary>
public interface IV2OperationTraitDefinitionBuilder
    : IV2OperationTraitDefinitionBuilder<IV2OperationTraitDefinitionBuilder, V2OperationTraitDefinition>
{



}
