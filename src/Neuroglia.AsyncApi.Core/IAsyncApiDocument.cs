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

namespace Neuroglia.AsyncApi;

/// <summary>
/// Defines the fundamentals of an Async API document
/// </summary>
public interface IAsyncApiDocument
{

    /// <summary>
    /// Gets the the AsyncAPI Specification version being used. It can be used by tooling Specifications and clients to interpret the version. 
    /// </summary>
    /// <remarks>
    /// The structure shall be major.minor.patch, where patch versions must be compatible with the existing major.minor tooling. 
    /// Typically patch versions will be introduced to address errors in the documentation, and tooling should typically be compatible with the corresponding major.minor (1.0.*). 
    /// Patch versions will correspond to patches of this document.
    /// </remarks>
    string AsyncApi { get; }

}