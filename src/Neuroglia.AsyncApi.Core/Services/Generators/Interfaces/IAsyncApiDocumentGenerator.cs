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
using Neuroglia.AsyncApi.Configuration;
using Neuroglia.AsyncApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Services.Generators
{

    /// <summary>
    /// Defines the fundamentals of a service used to generate <see cref="AsyncApiDocument"/>s in a code-first fashion
    /// </summary>
    public interface IAsyncApiDocumentGenerator
    {

        /// <summary>
        /// Generates code-first <see cref="AsyncApiDocument"/>s for types reflected in the specified assemblies
        /// </summary>
        /// <param name="markupTypes">An <see cref="Array"/> containing the mark up types belonging to the assemblies to scan</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the generated <see cref="AsyncApiDocument"/>s</returns>
        Task<IEnumerable<AsyncApiDocument>> GenerateAsync(params Type[] markupTypes);

        /// <summary>
        /// Generates code-first <see cref="AsyncApiDocument"/>s for types reflected in the specified assemblies
        /// </summary>
        /// <param name="markupTypes">An <see cref="IEnumerable{T}"/> containing the mark up types belonging to the assemblies to scan</param>
        /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the generated <see cref="AsyncApiDocument"/>s</returns>
        Task<IEnumerable<AsyncApiDocument>> GenerateAsync(IEnumerable<Type> markupTypes, AsyncApiDocumentGenerationOptions options);

    }

}
