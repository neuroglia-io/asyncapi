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
/// Represents an <see cref="Attribute"/> used to mark a method as a <see cref="V3OperationDefinition"/>
/// </summary>
/// <param name="name">The <see cref="V3OperationDefinition"/>'s name</param>
/// <param name="action">The <see cref="V3OperationDefinition"/>'s action</param>
/// <param name="channel">A reference to the channel the <see cref="V3OperationDefinition"/> belongs to</param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class OperationV3Attribute(string name, V3OperationAction action, string channel)
    : Attribute
{

    /// <summary>
    /// Gets the <see cref="V3OperationDefinition"/>'s name
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Gets the <see cref="V3OperationDefinition"/>'s action
    /// </summary>
    public V3OperationAction Action { get; } = action;

    /// <summary>
    /// Gets a reference to the channel the <see cref="V3OperationDefinition"/> belongs to
    /// </summary>
    public string Channel { get; } = channel;

    /// <summary>
    /// Gets/sets the <see cref="V3OperationDefinition"/>'s title
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Gets/sets the <see cref="V3OperationDefinition"/>'s summary
    /// </summary>
    public string? Summary { get; init; }

    /// <summary>
    /// Gets/sets the <see cref="V3OperationDefinition"/>'s summary
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets/sets a reference to the operation's bindings, if any
    /// </summary>
    public string? Bindings { get; set; }

    /// <summary>
    /// Gets/sets the url at which to get the <see cref="V3OperationDefinition"/>'s external documentation, if any
    /// </summary>
    public Uri? ExternalDocumentationUrl { get; init; }

    /// <summary>
    /// Gets/sets references to the tags to mark the <see cref="V3OperationDefinition"/> with
    /// </summary>
    public string[]? Tags { get; set; }

    /// <summary>
    /// Gets/sets a reference to the operation's <see cref="V3MessageDefinition"/>. Ignored if either <see cref="MessagePayloadType"/> or <see cref="HeadersPayloadType"/> have been set.
    /// </summary>
    public string? Message { get; init; }

    /// <summary>
    /// Gets/sets the type of the <see cref="V3OperationDefinition"/>'s message payload. Ignored if <see cref="Message"/> has been set.
    /// </summary>
    public Type? MessagePayloadType { get; init; }

    /// <summary>
    /// Gets/sets the type of the <see cref="V3OperationDefinition"/>'s message headers. Ignored if <see cref="Message"/> has been set.
    /// </summary>
    public Type? HeadersPayloadType { get; init; }

}
