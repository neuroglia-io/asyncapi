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
/// Represents an <see cref="Attribute"/> used to define a <see cref="V3ServerDefinition"/>
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property, AllowMultiple = true)]
public class ServerAttribute(string name, string host, string protocol, string? protocolVersion = null)
    : Attribute
{

    /// <summary>
    /// Gets the server's name
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Gets the server host name. It MAY include the port. This field supports Server Variables. Variable substitutions will be made when a variable is named in {braces}.
    /// </summary>
    public string Host { get; } = host;

    /// <summary>
    /// Gets/sets the protocol this server supports for connection.
    /// </summary>
    public string Protocol { get; } = protocol;

    /// <summary>
    /// Gets/sets the version of the protocol used for connection. For instance: AMQP 0.9.1, HTTP 2.0, Kafka 1.0.0, etc.
    /// </summary>
    public string? ProtocolVersion { get; init; } = protocolVersion;

    /// <summary>
    /// Gets/sets the path to a resource in the host. This field supports Server Variables. Variable substitutions will be made when a variable is named in {braces}.
    /// </summary>
    public string? PathName { get; init; }

    /// <summary>
    /// Gets/sets an optional string describing the server. CommonMark syntax MAY be used for rich text representation.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets/sets a human-friendly title for the server.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Gets/sets a short summary of the server.short summary of the server.
    /// </summary>
    public string? Summary { get; init; }

    /// <summary>
    /// Gets/sets references to the security schemes used by the <see cref="V3ServerDefinition"/>
    /// </summary>
    public string[]? Security { get; init; }

    /// <summary>
    /// Gets/sets references to the tags to mark the <see cref="V3ServerDefinition"/> with
    /// </summary>
    public string[]? Tags { get; init; }

    /// <summary>
    /// Gets/sets the url at which to get the <see cref="V3ServerDefinition"/>'s external documentation, if any
    /// </summary>
    public virtual Uri? ExternalDocumentationUrl { get; init; }

    /// <summary>
    /// Gets/sets a reference to the server's bindings, if any
    /// </summary>
    public string? Bindings { get; init; }

}
