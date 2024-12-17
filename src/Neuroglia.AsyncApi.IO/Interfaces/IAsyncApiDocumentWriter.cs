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

namespace Neuroglia.AsyncApi.IO;

/// <summary>
/// Defines the fundamentals of a service used to write <see cref="IAsyncApiDocument"/>s
/// </summary>
public interface IAsyncApiDocumentWriter
{

    /// <summary>
    /// Writes the specified <see cref="IAsyncApiDocument"/> to a <see cref="Stream"/>
    /// </summary>
    /// <param name="document">The <see cref="IAsyncApiDocument"/> to write</param>
    /// <param name="stream">The <see cref="Stream"/> to read the <see cref="IAsyncApiDocument"/> from</param>
    /// <param name="format">The format of the <see cref="IAsyncApiDocument"/> to read. Defaults to '<see cref="AsyncApiDocumentFormat.Yaml"/>'</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IAsyncApiDocument"/></returns>
    Task WriteAsync(IAsyncApiDocument document, Stream stream, AsyncApiDocumentFormat format = AsyncApiDocumentFormat.Yaml, CancellationToken cancellationToken = default);

}
