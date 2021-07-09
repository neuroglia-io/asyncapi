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
using System;

namespace Neuroglia.AsyncApi.Sdk.Services.FluentBuilders
{

    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="TagDefinition"/>s
    /// </summary>
    public interface ITagDefinitionBuilder
    {

        /// <summary>
        /// Configures the <see cref="TagDefinition"/> to use the specified name
        /// </summary>
        /// <param name="name">The name to use</param>
        /// <returns>The configured <see cref="ITagDefinitionBuilder"/></returns>
        ITagDefinitionBuilder WithName(string name);

        /// <summary>
        /// Configures the <see cref="TagDefinition"/> to use the specified description
        /// </summary>
        /// <param name="description">The description to use</param>
        /// <returns>The configured <see cref="ITagDefinitionBuilder"/></returns>
        ITagDefinitionBuilder WithDescription(string description);

        /// <summary>
        /// Adds the specified external documentation to the <see cref="TagDefinition"/> to build
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> to the documentation to add</param>
        /// <param name="description">The description of the documentation to add</param>
        /// <returns>The configured <see cref="ITagDefinitionBuilder"/></returns>
        ITagDefinitionBuilder DocumentWith(Uri uri, string description = null);

        /// <summary>
        /// Builds a new <see cref="TagDefinition"/>
        /// </summary>
        /// <returns>A new <see cref="TagDefinition"/></returns>
        TagDefinition Build();

    }

}
