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
using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Services.FluentBuilders;
using System;
using System.Reflection;

namespace Neuroglia.AsyncApi.Configuration
{

    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="AsyncApiGenerationOptions"/>
    /// </summary>
    public interface IAsyncApiGenerationOptionsBuilder
    {

        /// <summary>
        /// Configures the <see cref="AsyncApiGenerationOptions"/> to use the specified type to markup an <see cref="Assembly"/> to scan for Async API definitions
        /// </summary>
        /// <typeparam name="TMarkup">The type used to markup an <see cref="Assembly"/> to scan for Async API definitions</typeparam>
        /// <returns>The configured <see cref="IAsyncApiGenerationOptionsBuilder"/></returns>
        IAsyncApiGenerationOptionsBuilder WithMarkupType<TMarkup>();

        /// <summary>
        /// Configures the <see cref="AsyncApiGenerationOptions"/> to use the specified type to markup an <see cref="Assembly"/> to scan for Async API definitions
        /// </summary>
        /// <param name="markupType">The type used to markup an <see cref="Assembly"/> to scan for Async API definitions</param>
        /// <returns>The configured <see cref="IAsyncApiGenerationOptionsBuilder"/></returns>
        IAsyncApiGenerationOptionsBuilder WithMarkupType(Type markupType);

        /// <summary>
        /// Configures the <see cref="AsyncApiGenerationOptions"/> to use the specified prefix for all generated <see cref="AsyncApiDocument"/>s paths
        /// </summary>
        /// <param name="prefix">The prefix to use</param>
        /// <returns>The configured <see cref="IAsyncApiGenerationOptionsBuilder"/></returns>
        IAsyncApiGenerationOptionsBuilder UsePathPrefix(string prefix);

        /// <summary>
        /// Configures the <see cref="AsyncApiGenerationOptions"/> to use the specified <see cref="Action{T}"/> to setup the default configuration for generated <see cref="AsyncApiDocument"/>s
        /// </summary>
        /// <param name="configurationAction">The <see cref="Action{T}"/> used to configure the <see cref="IAsyncApiDocumentBuilder"/> used to build <see cref="AsyncApiDocument"/>s</param>
        /// <returns>The configured <see cref="IAsyncApiGenerationOptionsBuilder"/></returns>
        IAsyncApiGenerationOptionsBuilder UseDefaultConfiguration(Action<IAsyncApiDocumentBuilder> configurationAction);

        /// <summary>
        /// Builds new <see cref="AsyncApiGenerationOptions"/>
        /// </summary>
        /// <returns>New <see cref="AsyncApiGenerationOptions"/></returns>
        AsyncApiGenerationOptions Build();

    }

}
