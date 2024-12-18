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

namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Represents an <see cref="Attribute"/> used to tag an Async API component
/// </summary>
/// <param name="name">The name of the <see cref="TagDefinition"/> to generate</param>
/// <param name="description">The description of the <see cref="TagDefinition"/> to generate</param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class TagV2Attribute(string name, string? description = null)
        : Attribute
{

    /// <summary>
    /// Gets the name of the <see cref="TagDefinition"/> to generate
    /// </summary>
    public virtual string Name { get; } = name;

    /// <summary>
    /// Gets the description of the <see cref="TagDefinition"/> to generate
    /// </summary>
    public virtual string? Description { get; set; } = description;

}
