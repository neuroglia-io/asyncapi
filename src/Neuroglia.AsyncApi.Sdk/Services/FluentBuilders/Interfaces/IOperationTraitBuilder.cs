/*
 * Copyright © 2021 Neuroglia SPRL. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using Neuroglia.AsyncApi.Sdk.Models;
using Neuroglia.AsyncApi.Sdk.Models.Bindings;
using System;

namespace Neuroglia.AsyncApi.Sdk.Services.FluentBuilders
{

    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="OperationTrait"/>s
    /// </summary>
    /// <typeparam name="TBuilder">The type of <see cref="IOperationTraitBuilder{TBuilder, TTrait}"/> to return for chaining purposes</typeparam>
    /// <typeparam name="TTrait">The type of <see cref="OperationTrait"/> to build</typeparam>
    public interface IOperationTraitBuilder<TBuilder, TTrait>
        where TBuilder : IOperationTraitBuilder<TBuilder, TTrait>
        where TTrait : OperationTrait
    {

        /// <summary>
        /// Configures the <see cref="OperationTrait"/> to use the specified id
        /// </summary>
        /// <param name="operationId">The id of the Async Api document to build</param>
        /// <returns>The configured <see cref="IOperationTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder WithId(string operationId);

        /// <summary>
        /// Configures the <see cref="OperationTrait"/> to use the specified API description
        /// </summary>
        /// <param name="summary">The summary of the Async Api document to build</param>
        /// <returns>The configured <see cref="IOperationTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder WithSummary(string summary);

        /// <summary>
        /// Configures the <see cref="OperationTrait"/> to use the specified API description
        /// </summary>
        /// <param name="description">The description of the Async Api document to build</param>
        /// <returns>The configured <see cref="IOperationTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder WithDescription(string description);

        /// <summary>
        /// Marks the <see cref="OperationTrait"/> to build with the specified tag
        /// </summary>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the tag to use</param>
        /// <returns>The configured <see cref="IOperationTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder TagWith(Action<ITagBuilder> setup);

        /// <summary>
        /// Adds the specified external documentation to the <see cref="OperationTrait"/> to build
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> to the documentation to add</param>
        /// <param name="description">The description of the documentation to add</param>
        /// <returns>The configured <see cref="IOperationTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder DocumentWith(Uri uri, string description = null);

        /// <summary>
        /// Adds the specified <see cref="IOperationBinding"/> to the <see cref="OperationTrait"/> to build
        /// </summary>
        /// <param name="binding">The <see cref="IOperationBinding"/> to add</param>
        /// <returns>The configured <see cref="IOperationTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder UseBinding(IOperationBinding binding);

        /// <summary>
        /// Builds a new <see cref="OperationTrait"/>
        /// </summary>
        /// <returns>A new <see cref="OperationTrait"/></returns>
        TTrait Build();

    }

    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="OperationTrait"/>s
    /// </summary>
    public interface IOperationTraitBuilder
        : IOperationTraitBuilder<IOperationTraitBuilder, OperationTrait>
    {



    }

}
