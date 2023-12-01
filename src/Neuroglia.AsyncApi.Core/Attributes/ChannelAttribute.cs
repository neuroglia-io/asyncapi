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
/// Represents an <see cref="Attribute"/> used to mark a class or a method as a <see cref="ChannelDefinition"/>
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class ChannelAttribute
    : Attribute
{
    
    /// <summary>
    /// Initializes a new <see cref="ChannelAttribute"/>
    /// </summary>
    /// <param name="name">The <see cref="ChannelDefinition"/>'s name</param>
    /// <param name="description">The <see cref="ChannelDefinition"/>'s description</param>
    public ChannelAttribute(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.Name = name;
        this.Description = description;
    }

    /// <summary>
    /// Initializes a new <see cref="ChannelAttribute"/>
    /// </summary>
    /// <param name="name">The <see cref="ChannelDefinition"/>'s name</param>
    public ChannelAttribute(string name) : this(name, null) { }

    /// <summary>
    /// Gets the <see cref="ChannelDefinition"/>'s name
    /// </summary>
    public virtual string Name { get; }

    /// <summary>
    /// Gets/sets the <see cref="ChannelDefinition"/>'s description
    /// </summary>
    public virtual string? Description { get; set; }

}
