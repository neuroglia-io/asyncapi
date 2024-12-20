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
/// Represents an object used to describe a message broker, a server or any other kind of computer program capable of sending and/or receiving data
/// </summary>
[DataContract]
public record V3ServerDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets the server host name. It MAY include the port. This field supports Server Variables. Variable substitutions will be made when a variable is named in {braces}.
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "host"), JsonPropertyOrder(1), JsonPropertyName("host"), YamlMember(Order = 1, Alias = "host")]
    public virtual string Host { get; set; } = null!;

    /// <summary>
    /// Gets/sets the protocol this server supports for connection.
    /// </summary>
    [Required]
    [DataMember(Order = 2, Name = "protocol"), JsonPropertyOrder(2), JsonPropertyName("protocol"), YamlMember(Order = 2, Alias = "protocol")]
    public virtual string Protocol { get; set; } = null!;

    /// <summary>
    /// Gets/sets the version of the protocol used for connection. For instance: AMQP 0.9.1, HTTP 2.0, Kafka 1.0.0, etc.
    /// </summary>
    [DataMember(Order = 3, Name = "protocolVersion"), JsonPropertyOrder(3), JsonPropertyName("protocolVersion"), YamlMember(Order = 3, Alias = "protocolVersion")]
    public virtual string? ProtocolVersion { get; set; }

    /// <summary>
    /// Gets/sets the path to a resource in the host. This field supports Server Variables. Variable substitutions will be made when a variable is named in {braces}.
    /// </summary>
    [DataMember(Order = 4, Name = "pathname"), JsonPropertyOrder(4), JsonPropertyName("pathname"), YamlMember(Order = 4, Alias = "pathname")]
    public virtual string? PathName { get; set; }

    /// <summary>
    /// Gets/sets an optional string describing the server. CommonMark syntax MAY be used for rich text representation.
    /// </summary>
    [DataMember(Order = 5, Name = "description"), JsonPropertyOrder(5), JsonPropertyName("description"), YamlMember(Order = 5, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets a human-friendly title for the server.
    /// </summary>
    [DataMember(Order = 6, Name = "title"), JsonPropertyOrder(6), JsonPropertyName("title"), YamlMember(Order = 6, Alias = "title")]
    public virtual string? Title { get; set; }

    /// <summary>
    /// Gets/sets a short summary of the server.short summary of the server.
    /// </summary>
    [DataMember(Order = 7, Name = "summary"), JsonPropertyOrder(7), JsonPropertyName("summary"), YamlMember(Order = 7, Alias = "summary")]
    public virtual string? Summary { get; set; }

    /// <summary>
    /// Gets/sets a map between a variable name and its value. The value is used for substitution in the server's host and pathname template.
    /// </summary>
    [DataMember(Order = 8, Name = "variables"), JsonPropertyOrder(8), JsonPropertyName("variables"), YamlMember(Order = 8, Alias = "variables")]
    public virtual EquatableDictionary<string, V3ServerVariableDefinition>? Variables { get; set; }

    /// <summary>
    /// Gets/sets a declaration of which security schemes can be used with this server. The list of values includes alternative security scheme objects that can be used. Only one of the security scheme objects need to be satisfied to authorize a connection or operation.
    /// </summary>
    [DataMember(Order = 9, Name = "security"), JsonPropertyOrder(9), JsonPropertyName("security"), YamlMember(Order = 9, Alias = "security")]
    public virtual EquatableList<V3SecuritySchemeDefinition>? Security { get; set; }

    /// <summary>
    /// Gets/sets a list of tags for logical grouping and categorization of servers.
    /// </summary>
    [DataMember(Order = 10, Name = "tags"), JsonPropertyOrder(10), JsonPropertyName("tags"), YamlMember(Order = 10, Alias = "tags")]
    public virtual EquatableList<V3TagDefinition>? Tags { get; set; }

    /// <summary>
    /// Gets/sets additional external documentation for this server.
    /// </summary>
    [DataMember(Order = 11, Name = "externalDocs"), JsonPropertyOrder(11), JsonPropertyName("externalDocs"), YamlMember(Order = 11, Alias = "externalDocs")]
    public virtual V3ExternalDocumentationDefinition? ExternalDocs { get; set; }

    /// <summary>
    /// Gets/sets a map where the keys describe the name of the protocol and the values describe protocol-specific definitions for the server.
    /// </summary>
    [DataMember(Order = 12, Name = "bindings"), JsonPropertyOrder(12), JsonPropertyName("bindings"), YamlMember(Order = 12, Alias = "bindings")]
    public virtual ServerBindingDefinitionCollection? Bindings { get; set; }

    /// <inheritdoc/>
    public override string ToString() => this.Host;

}
