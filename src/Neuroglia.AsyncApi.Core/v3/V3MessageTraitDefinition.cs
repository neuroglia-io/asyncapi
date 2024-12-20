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
/// Represents an object that describes a trait that MAY be applied to a Message Object.
/// </summary>
[DataContract]
public record V3MessageTraitDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets the schema definition of the application headers. 
    /// Schema MUST be a map of key-value pairs. 
    /// It MUST NOT define the protocol headers. 
    /// If this is a Schema Object, then the schemaFormat will be assumed to be "application/vnd.aai.asyncapi+json;version=asyncapi" where the version is equal to the AsyncAPI Version String.
    /// </summary>
    [DataMember(Order = 1, Name = "headers"), JsonPropertyOrder(1), JsonPropertyName("headers"), YamlMember(Order = 1, Alias = "headers")]
    public virtual V3SchemaDefinition? Headers { get; set; }

    /// <summary>
    /// Gets/sets the definition of the correlation ID used for message tracing or matching.
    /// </summary>
    [DataMember(Order = 2, Name = "correlationId"), JsonPropertyOrder(2), JsonPropertyName("correlationId"), YamlMember(Order = 2, Alias = "correlationId")]
    public virtual V3CorrelationIdDefinition? CorrelationId { get; set; }

    /// <summary>
    /// Gets/sets the content type to use when encoding/decoding a message's payload. 
    /// The value MUST be a specific media type (e.g. application/json). 
    /// When omitted, the value MUST be the one specified on the defaultContentType field.
    /// </summary>
    [DataMember(Order = 3, Name = "contentType"), JsonPropertyOrder(3), JsonPropertyName("contentType"), YamlMember(Order = 3, Alias = "contentType")]
    public virtual string? ContentType { get; set; }

    /// <summary>
    /// Gets/sets a machine-friendly name for the message.
    /// </summary>
    [DataMember(Order = 4, Name = "name"), JsonPropertyOrder(4), JsonPropertyName("name"), YamlMember(Order = 4, Alias = "name")]
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets/sets a human-friendly name for the message.
    /// </summary>
    [DataMember(Order = 5, Name = "title"), JsonPropertyOrder(5), JsonPropertyName("title"), YamlMember(Order = 5, Alias = "title")]
    public virtual string? Title { get; set; }

    /// <summary>
    /// Gets/sets a short summary of what the message is about.
    /// </summary>
    [DataMember(Order = 6, Name = "summary"), JsonPropertyOrder(6), JsonPropertyName("summary"), YamlMember(Order = 6, Alias = "summary")]
    public virtual string? Summary { get; set; }

    /// <summary>
    /// Gets/sets a verbose explanation of the message. CommonMark syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 7, Name = "description"), JsonPropertyOrder(7), JsonPropertyName("description"), YamlMember(Order = 7, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets a list of tags for logical grouping and categorization of messages.
    /// </summary>
    [DataMember(Order = 8, Name = "tags"), JsonPropertyOrder(8), JsonPropertyName("tags"), YamlMember(Order = 8, Alias = "tags")]
    public virtual EquatableList<V3TagDefinition>? Tags { get; set; }

    /// <summary>
    /// Gets/sets additional external documentation for this message.
    /// </summary>
    [DataMember(Order = 9, Name = "externalDocs"), JsonPropertyOrder(9), JsonPropertyName("externalDocs"), YamlMember(Order = 9, Alias = "externalDocs")]
    public virtual V3ExternalDocumentationDefinition? ExternalDocs { get; set; }

    /// <summary>
    /// Gets/sets a map where the keys describe the name of the protocol and the values describe protocol-specific definitions for the message.
    /// </summary>
    [DataMember(Order = 10, Name = "bindings"), JsonPropertyOrder(10), JsonPropertyName("bindings"), YamlMember(Order = 10, Alias = "bindings")]
    public virtual MessageBindingDefinitionCollection? Bindings { get; set; }

    /// <summary>
    /// Gets/sets a list of examples.
    /// </summary>
    [DataMember(Order = 11, Name = "examples"), JsonPropertyOrder(11), JsonPropertyName("examples"), YamlMember(Order = 11, Alias = "examples")]
    public virtual EquatableList<V3MessageExampleDefinition>? Examples { get; set; }

}