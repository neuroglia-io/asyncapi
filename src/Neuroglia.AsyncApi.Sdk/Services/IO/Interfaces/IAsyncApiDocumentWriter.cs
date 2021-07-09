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
using System.IO;

namespace Neuroglia.AsyncApi.Sdk.Services.IO
{
    /// <summary>
    /// Defines the fundamentals of a service used to write <see cref="AsyncApiDocument"/>s
    /// </summary>
    public interface IAsyncApiDocumentWriter
    {

        /// <summary>
        /// Writes the specified <see cref="AsyncApiDocument"/> to a <see cref="Stream"/>
        /// </summary>
        /// <param name="document">The <see cref="AsyncApiDocument"/> to write</param>
        /// <param name="stream">The <see cref="Stream"/> to read the <see cref="AsyncApiDocument"/> from</param>
        /// <param name="format">The format of the <see cref="AsyncApiDocument"/> to read. Defaults to '<see cref="AsyncApiDocumentFormat.Yaml"/>'</param>
        /// <returns>A new <see cref="AsyncApiDocument"/></returns>
        void Write(AsyncApiDocument document, Stream stream, AsyncApiDocumentFormat format = AsyncApiDocumentFormat.Yaml);

    }

}
