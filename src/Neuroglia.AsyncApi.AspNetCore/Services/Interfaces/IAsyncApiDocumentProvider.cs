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
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Services
{

    /// <summary>
    /// Defines the fundamentals of a service used to provide <see cref="AsyncApiDocument"/>s
    /// </summary>
    public interface IAsyncApiDocumentProvider
    {

        /// <summary>
        /// Reads the contents of the specified <see cref="AsyncApiDocument"/> file
        /// </summary>
        /// <param name="title">The title of the <see cref="AsyncApiDocument"/> to read the contents of</param>
        /// <param name="version">The version of the <see cref="AsyncApiDocument"/> to read the contents of</param>
        /// <param name="format">The format to write the <see cref="AsyncApiDocument"/> to</param>
        /// <returns>A byte array that represents the contents of the <see cref="AsyncApiDocument"/> file</returns>
        Task<byte[]> ReadDocumentContentsAsync(string title, string version, AsyncApiDocumentFormat format);

    }

}
