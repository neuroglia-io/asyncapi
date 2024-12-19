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
/// Represents an <see cref="Attribute"/> used to tag an Async API component
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class TagV3Attribute
    : Attribute
{

    /// <summary>
    /// Gets a reference to the <see cref="V3TagDefinition"/> to use
    /// </summary>
    public virtual string? Reference { get; init; }

    /// <summary>
    /// Gets the name of the <see cref="V3TagDefinition"/> to generate
    /// </summary>
    public virtual string? Name { get; init; }

    /// <summary>
    /// Gets the description of the <see cref="V3TagDefinition"/> to generate
    /// </summary>
    public virtual string? Description { get; init; }

}