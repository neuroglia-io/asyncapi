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
using Newtonsoft.Json.Schema;
using System;

namespace Neuroglia.AsyncApi.Sdk.Services.FluentBuilders
{

    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="MessageTrait"/>s
    /// </summary>
    /// <typeparam name="TBuilder">The type of <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/> to return for chaining purposes</typeparam>
    /// <typeparam name="TTrait">The type of <see cref="MessageTrait"/> to build</typeparam>
    public interface IMessageTraitBuilder<TBuilder, TTrait>
        where TBuilder : IMessageTraitBuilder<TBuilder, TTrait>
        where TTrait : MessageTrait
    {

        /// <summary>
        /// Configures the <see cref="MessageTrait"/> to build to use the specified headers
        /// </summary>
        /// <typeparam name="THeaders">The type used to define the <see cref="MessageTrait"/>'s headers</typeparam>
        /// <returns>The configured <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder WithHeaders<THeaders>()
            where THeaders : class;

        /// <summary>
        /// Configures the <see cref="MessageTrait"/> to build to use the specified headers
        /// </summary>
        /// <param name="headersType">The type used to define the <see cref="MessageTrait"/>'s headers</param>
        /// <returns>The configured <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder WithHeaders(Type headersType);

        /// <summary>
        /// Configures the <see cref="MessageTrait"/> to build to use the specified headers
        /// </summary>
        /// <param name="headersSchema">The <see cref="JSchema"/> used to define the <see cref="MessageTrait"/>'s headers</param>
        /// <returns>The configured <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder WithHeaders(JSchema headersSchema);

        /// <summary>
        /// Configures the <see cref="MessageTrait"/> to build to use the specified correlation id
        /// </summary>
        /// <param name="location">The location of the correlation id to use</param>
        /// <param name="description">The description of the correlation id to use</param>
        /// <returns>The configured <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder WithCorrelationId(string location, string description = null);

        /// <summary>
        /// Configures the <see cref="MessageTrait"/> to build to use the specified schema format
        /// </summary>
        /// <param name="schemaFormat">The schema format to use</param>
        /// <returns>The configured <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder WithSchemaFormat(string schemaFormat);

        /// <summary>
        /// Configures the <see cref="MessageTrait"/> to build to use the specified content type
        /// </summary>
        /// <param name="contentType">The content type to use</param>
        /// <returns>The configured <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder WithContentType(string contentType);

        /// <summary>
        /// Configures the <see cref="MessageTrait"/> to build to use the specified content type
        /// </summary>
        /// <param name="name">The name to use</param>
        /// <returns>The configured <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder WithName(string name);

        /// <summary>
        /// Configures the <see cref="MessageTrait"/> to build to use the specified title
        /// </summary>
        /// <param name="title">The title to use</param>
        /// <returns>The configured <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder WithTitle(string title);

        /// <summary>
        /// Configures the <see cref="MessageTrait"/> to build to use the specified summary
        /// </summary>
        /// <param name="summary">The summary to use</param>
        /// <returns>The configured <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder WithSummary(string summary);

        /// <summary>
        /// Configures the <see cref="MessageTrait"/> to build to use the specified description
        /// </summary>
        /// <param name="description">The description to use</param>
        /// <returns>The configured <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder WithDescription(string description);

        /// <summary>
        /// Adds the specified example to the <see cref="MessageTrait"/> to build
        /// </summary>
        /// <param name="name">The name of the example to add</param>
        /// <param name="example">The example to use</param>
        /// <returns>The configured <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder AddExample(string name, object example);

        /// <summary>
        /// Marks the <see cref="MessageTrait"/> to build with the specified tag
        /// </summary>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the tag to use</param>
        /// <returns>The configured <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder TagWith(Action<ITagBuilder> setup);

        /// <summary>
        /// Adds the specified external documentation to the <see cref="MessageTrait"/> to build
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> to the documentation to add</param>
        /// <param name="description">The description of the documentation to add</param>
        /// <returns>The configured <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder DocumentWith(Uri uri, string description = null);

        /// <summary>
        /// Adds the specified <see cref="IMessageBinding"/> to the <see cref="MessageTrait"/> to build
        /// </summary>
        /// <param name="binding">The <see cref="IMessageBinding"/> to add</param>
        /// <returns>The configured <see cref="IMessageTraitBuilder{TBuilder, TTrait}"/></returns>
        TBuilder UseBinding(IMessageBinding binding);

        /// <summary>
        /// Builds a new <see cref="MessageTrait"/>
        /// </summary>
        /// <returns>A new <see cref="MessageTrait"/></returns>
        TTrait Build();

    }

    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="MessageTrait"/>s
    /// </summary>
    public interface IMessageTraitBuilder
        : IMessageTraitBuilder<IMessageTraitBuilder, MessageTrait>
    {


    }

}
