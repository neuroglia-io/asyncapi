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

namespace Neuroglia.AsyncApi.v3;

/// <summary>
/// Represents an <see cref="Attribute"/> used to mark a class as an Async Api to generate a new <see cref="V3AsyncApiDocument"/> for
/// </summary>
/// <param name="title">The <see cref="AsyncApiAttribute"/>'s title</param>
/// <param name="version">The <see cref="AsyncApiAttribute"/>'s version</param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
public class AsyncApiV3Attribute(string title, string version)
    : AsyncApiAttribute(title, version)
{

    /// <summary>
    /// Gets/sets references to the tags to mark the <see cref="V3AsyncApiDocument"/> with
    /// </summary>
    public string[]? Tags { get; set; }

}