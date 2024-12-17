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
/// Represents an object used to define a Async API operation message trait
/// </summary>
[DataContract]
public record V2MessageTraitDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets a unique string used to identify the message. 
    /// The id MUST be unique among all messages described in the API. 
    /// The messageId value is case-sensitive. 
    /// Tools and libraries MAY use the messageId to uniquely identify a message, therefore, it is RECOMMENDED to follow common programming naming conventions.
    /// </summary>
    [DataMember(Order = 1, Name = "messageId"), JsonPropertyOrder(1), JsonPropertyName("messageId"), YamlMember(Order = 1, Alias = "messageId")]
    public virtual string? MessageId { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="JsonSchema"/> of the application headers. Schema MUST be of type "object". It MUST NOT define the protocol headers.
    /// </summary>
    [DataMember(Order = 2, Name = "headers"), JsonPropertyOrder(2), JsonPropertyName("headers"), YamlMember(Order = 2, Alias = "headers")]
    public virtual JsonSchema? Headers { get; set; }

    /// <summary>
    /// Gets/sets the definition of the correlation ID used for message tracing or matching.
    /// </summary>
    [DataMember(Order = 3, Name = "correlationId"), JsonPropertyOrder(3), JsonPropertyName("correlationId"), YamlMember(Order = 3, Alias = "correlationId")]
    public virtual CorrelationIdDefinition? CorrelationId { get; set; }

    /// <summary>
    /// Gets/sets a string containing the name of the schema format used to define the message payload. 
    /// If omitted, implementations should parse the payload as a <see cref="JsonSchema"/>.
    /// When the payload is defined using a $ref to a remote file, it is RECOMMENDED the schema format includes the file encoding type to allow implementations to parse the file correctly. E.g., adding +yaml if content type is application/vnd.apache.avro results in application/vnd.apache.avro+yaml.
    /// </summary>
    [DataMember(Order = 4, Name = "schemaFormat"), JsonPropertyOrder(4), JsonPropertyName("schemaFormat"), YamlMember(Order = 4, Alias = "schemaFormat")]
    public virtual string? SchemaFormat { get; set; }

    /// <summary>
    /// Gets/sets the content type to use when encoding/decoding a message's payload. The value MUST be a specific media type (e.g. application/json). When omitted, the value MUST be the one specified on the <see cref="V2AsyncApiDocument.DefaultContentType"/> property.
    /// </summary>
    [DataMember(Order = 5, Name = "contentType"), JsonPropertyOrder(5), JsonPropertyName("contentType"), YamlMember(Order = 5, Alias = "contentType")]
    public virtual string? ContentType { get; set; }

    /// <summary>
    /// Gets/sets a machine-friendly name for the message.
    /// </summary>
    [DataMember(Order = 6, Name = "name"), JsonPropertyOrder(6), JsonPropertyName("name"), YamlMember(Order = 6, Alias = "name")]
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets/sets a human-friendly title for the message.
    /// </summary>
    [DataMember(Order = 7, Name = "title"), JsonPropertyOrder(7), JsonPropertyName("title"), YamlMember(Order = 7, Alias = "title")]
    public virtual string? Title { get; set; }

    /// <summary>
    /// Gets/sets a short summary of what the message is about.
    /// </summary>
    [DataMember(Order = 8, Name = "summary"), JsonPropertyOrder(8), JsonPropertyName("summary"), YamlMember(Order = 8, Alias = "summary")]
    public virtual string? Summary { get; set; }

    /// <summary>
    /// Gets/sets an optional description of the message. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 9, Name = "description"), JsonPropertyOrder(9), JsonPropertyName("description"), YamlMember(Order = 9, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// gets/sets a <see cref="EquatableList{T}"/> of tags for API documentation control. Tags can be used for logical grouping of operations.
    /// </summary>
    [DataMember(Order = 10, Name = "tags"), JsonPropertyOrder(10), JsonPropertyName("tags"), YamlMember(Order = 10, Alias = "tags")]
    public virtual EquatableList<TagDefinition>? Tags { get; set; }

    /// <summary>
    /// Gets/sets an object containing additional external documentation for this message.
    /// </summary>
    [DataMember(Order = 11, Name = "externalDocs"), JsonPropertyOrder(11), JsonPropertyName("externalDocs"), YamlMember(Order = 11, Alias = "externalDocs")]
    public virtual ExternalDocumentationDefinition? ExternalDocs { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the <see cref="V2MessageTraitDefinition"/>'s <see cref="IMessageBindingDefinition"/>s
    /// </summary>
    [DataMember(Order = 12, Name = "bindings"), JsonPropertyOrder(12), JsonPropertyName("bindings"), YamlMember(Order = 12, Alias = "bindings")]
    public virtual MessageBindingDefinitionCollection? Bindings { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> where keys MUST be either headers and/or payload. 
    /// Values MUST contain examples that validate against the headers or payload fields, respectively. 
    /// Example MAY also have the name and summary additional keys to provide respectively a machine-friendly name and a short summary of what the example is about.
    /// </summary>
    [DataMember(Order = 13, Name = "examples"), JsonPropertyOrder(13), JsonPropertyName("examples"), YamlMember(Order = 13, Alias = "examples")]
    public virtual EquatableDictionary<string, object>? Examples { get; set; }

    /// <inheritdoc/>
    public override string ToString() => Name ?? base.ToString();

}
