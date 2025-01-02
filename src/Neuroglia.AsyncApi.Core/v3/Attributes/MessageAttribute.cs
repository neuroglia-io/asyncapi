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
/// Represents an <see cref="Attribute"/> used to configure an <see cref="V3OperationDefinition"/>'s <see cref="V3MessageDefinition"/>
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class MessageAttribute
    : Attribute
{

    /// <summary>
    /// Gets/sets the <see cref="V3MessageDefinition"/>'s name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="V3MessageDefinition"/>'s title
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="V3MessageDefinition"/>'s description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="V3MessageDefinition"/>'s summary
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="V3MessageDefinition"/>'s content type
    /// </summary>
    public string? ContentType { get; set; }

    /// <summary>
    /// Gets/sets a reference to the message's bindings, if any
    /// </summary>
    public string? Bindings { get; set; }

    /// <summary>
    /// Gets/sets a reference to the message's payload schema, if any
    /// </summary>
    public string? PayloadSchema { get; init; }

    /// <summary>
    /// Gets/sets a reference to the message's headers schema, if any
    /// </summary>
    public string? HeadersSchema { get; init; }

    /// <summary>
    /// Gets/sets the url at which to get the <see cref="V3MessageDefinition"/>'s external documentation, if any
    /// </summary>
    public Uri? ExternalDocumentationUrl { get; init; }

    /// <summary>
    /// Gets/sets references to the tags to mark the <see cref="V3MessageDefinition"/> with
    /// </summary>
    public string[]? Tags { get; set; }

}
