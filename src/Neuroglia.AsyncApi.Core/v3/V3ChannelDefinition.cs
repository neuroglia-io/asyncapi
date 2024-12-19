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
/// Represents an object used to describe a shared communication channel.
/// </summary>
[DataContract]
public record V3ChannelDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets n optional string representation of this channel's address. 
    /// The address is typically the "topic name", "routing key", "event type", or "path". W
    /// hen null or absent, it MUST be interpreted as unknown. This is useful when the address is generated dynamically at runtime or can't be known upfront. 
    /// It MAY contain Channel Address Expressions. Query parameters and fragments SHALL NOT be used, instead use bindings to define them.
    /// </summary>
    [DataMember(Order = 1, Name = "address"), JsonPropertyOrder(1), JsonPropertyName("address"), YamlMember(Order = 1, Alias = "address", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual string? Address { get; set; }

    /// <summary>
    /// Gets/sets a map of the messages that will be sent to this channel by any application at any time. Every message sent to this channel MUST be valid against one, and only one, of the message objects defined in this map.
    /// </summary>
    [DataMember(Order = 2, Name = "messages"), JsonPropertyOrder(2), JsonPropertyName("messages"), YamlMember(Order = 2, Alias = "messages", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual EquatableDictionary<string, V3MessageDefinition>? Messages { get; set; }

    /// <summary>
    /// Gets/sets a human-friendly title for the channel.
    /// </summary>
    [DataMember(Order = 3, Name = "title"), JsonPropertyOrder(3), JsonPropertyName("title"), YamlMember(Order = 3, Alias = "title", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual string? Title { get; set; }

    /// <summary>
    /// Gets/sets a short summary of the channel.
    /// </summary>
    [DataMember(Order = 4, Name = "summary"), JsonPropertyOrder(4), JsonPropertyName("summary"), YamlMember(Order = 4, Alias = "summary", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual string? Summary { get; set; }

    /// <summary>
    /// Gets/sets an optional description of this channel. CommonMark syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 5, Name = "description"), JsonPropertyOrder(5), JsonPropertyName("description"), YamlMember(Order = 5, Alias = "description", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets an array of $ref pointers to the definition of the servers in which this channel is available
    /// </summary>
    [DataMember(Order = 6, Name = "servers"), JsonPropertyOrder(6), JsonPropertyName("servers"), YamlMember(Order = 6, Alias = "servers", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual EquatableList<V3ReferenceDefinition> Servers { get; set; } = [];

    /// <summary>
    /// Gets/sets a map of the parameters included in the channel address. It MUST be present only when the address contains Channel Address Expressions.
    /// </summary>
    [DataMember(Order = 7, Name = "parameters"), JsonPropertyOrder(7), JsonPropertyName("parameters"), YamlMember(Order = 7, Alias = "parameters", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual EquatableDictionary<string, V3ParameterDefinition>? Parameters { get; set; }

    /// <summary>
    /// Gets/sets a list of tags for logical grouping of channels.
    /// </summary>
    [DataMember(Order = 8, Name = "tags"), JsonPropertyOrder(8), JsonPropertyName("tags"), YamlMember(Order = 8, Alias = "tags", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual EquatableList<V3TagDefinition>? Tags { get; set; }

    /// <summary>
    /// Gets/sets additional external documentation for this channel.
    /// </summary>
    [DataMember(Order = 9, Name = "externalDocs"), JsonPropertyOrder(9), JsonPropertyName("externalDocs"), YamlMember(Order = 9, Alias = "externalDocs", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual V3ExternalDocumentationDefinition? ExternalDocs { get; set; }

    /// <summary>
    /// Gets/sets additional external documentation for this channel.
    /// </summary>
    [DataMember(Order = 10, Name = "bindings"), JsonPropertyOrder(10), JsonPropertyName("bindings"), YamlMember(Order = 10, Alias = "bindings", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual ChannelBindingDefinitionCollection? Bindings { get; set; }

    /// <inheritdoc/>
    public override string ToString() => string.IsNullOrWhiteSpace(this.Address) ? base.ToString() : this.Address;

}
