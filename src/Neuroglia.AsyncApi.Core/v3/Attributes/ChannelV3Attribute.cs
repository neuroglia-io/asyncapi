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
/// Represents an <see cref="Attribute"/> used to define a <see cref="V3ChannelDefinition"/>
/// </summary>
/// <param name="name">The <see cref="V3ChannelDefinition"/>'s name</param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property, AllowMultiple = true)]
public class ChannelV3Attribute(string name)
    : Attribute
{

    /// <summary>
    /// Gets/sets the <see cref="V3ChannelDefinition"/>'s name
    /// </summary>
    public string Name { get; init; } = name;

    /// <summary>
    /// Gets/sets the <see cref="V3ChannelDefinition"/>'s address
    /// </summary>
    public string? Address { get; init; }

    /// <summary>
    /// Gets/sets the <see cref="V3ChannelDefinition"/>'s title
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Gets/sets the <see cref="V3ChannelDefinition"/>'s summary
    /// </summary>
    public string? Summary { get; init; }

    /// <summary>
    /// Gets/sets the <see cref="V3ChannelDefinition"/>'s description
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets/sets an array containing the references to the <see cref="V3ChannelDefinition"/>'s parameters
    /// </summary>
    public string[]? Parameters { get; init; }

    /// <summary>
    /// Gets/sets references to the servers the <see cref="V3ChannelDefinition"/> in which the channel is available
    /// </summary>
    public string[]? Servers { get; init; }

    /// <summary>
    /// Gets/sets the url at which to get the <see cref="V3ChannelDefinition"/>'s external documentation, if any
    /// </summary>
    public virtual Uri? ExternalDocumentationUrl { get; init; }

}
