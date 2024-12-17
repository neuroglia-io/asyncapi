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

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents the options used to configure code-first AsyncAPI document generation
/// </summary>
public class AsyncApiGenerationOptions
{

    /// <summary>
    /// Gets/sets an <see cref="List{T}"/> containing the types used to markup assemblies to scan for Async Api declarations
    /// </summary>
    public virtual List<Type> MarkupTypes { get; set; } = [];

    /// <summary>
    /// Gets/sets the <see cref="Action{T}"/> used to apply a default configuration to generated <see cref="V2AsyncApiDocument"/>s
    /// </summary>
    public virtual Action<IAsyncApiDocumentBuilder>? DefaultDocumentConfiguration { get; set; }

}