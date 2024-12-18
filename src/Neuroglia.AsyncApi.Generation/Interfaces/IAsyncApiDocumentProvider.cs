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

namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Defines the fundamentals of a service used to manage <see cref="IAsyncApiDocument"/>s
/// </summary>
public interface IAsyncApiDocumentProvider
    : IEnumerable<IAsyncApiDocument>
{

    /// <summary>
    /// Gets the <see cref="IAsyncApiDocument"/> with the specified title and version
    /// </summary>
    /// <param name="title">The title of the <see cref="IAsyncApiDocument"/> to get</param>
    /// <param name="version">The version of the <see cref="IAsyncApiDocument"/> to get</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The <see cref="IAsyncApiDocument"/> with the specified title and version, if any</returns>
    Task<IAsyncApiDocument?> GetDocumentAsync(string title, string version, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the <see cref="IAsyncApiDocument"/> with the specified id
    /// </summary>
    /// <param name="id">The id of the <see cref="IAsyncApiDocument"/> to get</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The <see cref="IAsyncApiDocument"/> with the specified id, if any</returns>
    Task<IAsyncApiDocument?> GetDocumentAsync(string id, CancellationToken cancellationToken = default);

}
