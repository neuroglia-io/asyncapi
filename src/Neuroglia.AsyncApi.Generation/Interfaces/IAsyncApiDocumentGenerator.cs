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

using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Defines the fundamentals of a service used to generate <see cref="V2AsyncApiDocument"/>s in a code-first fashion
/// </summary>
public interface IAsyncApiDocumentGenerator
{

    /// <summary>
    /// Generates code-first <see cref="V2AsyncApiDocument"/>s for types reflected in the specified assemblies
    /// </summary>
    /// <param name="markupTypes">An <see cref="IEnumerable{T}"/> containing the mark up types belonging to the assemblies to scan</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing the generated <see cref="V2AsyncApiDocument"/>s</returns>
    Task<IEnumerable<V2AsyncApiDocument>> GenerateAsync(IEnumerable<Type> markupTypes, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default);

}
