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
/// Represents an <see cref="Attribute"/> used to define a <see cref="V3ServerVariableDefinition"/>
/// </summary>
/// <param name="server">The server the <see cref="V3ServerVariableDefinition"/> applies to</param>
/// <param name="name">The <see cref="V3ServerVariableDefinition"/>'s name</param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property, AllowMultiple = true)]
public class ServerVariableAttribute(string server, string name)
    : Attribute
{

    /// <summary>
    /// Gets/sets the server the <see cref="V3ServerVariableDefinition"/> applies to
    /// </summary>
    public string Server { get; init; } = server;

    /// <summary>
    /// Gets/sets the <see cref="V3ServerVariableDefinition"/>'s name
    /// </summary>
    public string Name { get; init; } = name;

    /// <summary>
    /// Gets/sets the <see cref="V3ServerVariableDefinition"/>'s description
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets/sets the <see cref="V3ServerVariableDefinition"/>'s enum values, if any
    /// </summary>
    public string[]? Enum { get; init; }

    /// <summary>
    /// Gets/sets the <see cref="V3ServerVariableDefinition"/>'s default value, if any
    /// </summary>
    public string? Default { get; init; }

    /// <summary>
    /// Gets/sets the <see cref="V3ServerVariableDefinition"/>'s examples, if any
    /// </summary>
    public string[]? Examples { get; init; }

}