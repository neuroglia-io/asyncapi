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
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Configuration
{

    /// <summary>
    /// Represents the options used to configure code-first AsyncAPI document generation
    /// </summary>
    public class AsyncApiGenerationOptions
    {

        /// <summary>
        /// Gets the default path prefix for generated <see cref="AsyncApiDocument"/>s
        /// </summary>
        public const string DefaultPathPrefix = "/asyncapi/";

        /// <summary>
        /// Gets/sets the path prefix for all generated <see cref="AsyncApiDocument"/>s
        /// </summary>
        public virtual string PathPrefix { get; set; } = DefaultPathPrefix;

        /// <summary>
        /// Gets/sets an <see cref="List{T}"/> containing the types used to markup assemblies to scan for Async Api declarations
        /// </summary>
        public virtual List<Type> MarkupTypes { get; set; } = new();

        /// <summary>
        /// Gets/sets the <see cref="Action{T}"/> used to apply a default configuration to generated <see cref="AsyncApiDocument"/>s
        /// </summary>
        public virtual Action<IAsyncApiDocumentBuilder> DefaultDocumentConfiguration { get; set; }

    }

}
