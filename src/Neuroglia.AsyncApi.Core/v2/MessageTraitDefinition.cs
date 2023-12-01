using Json.Schema;
using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Represents an object used to define a Async API operation message trait
/// </summary>
[DataContract]
public record MessageTraitDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets a <see cref="JsonSchema"/> of the application headers. Schema MUST be of type "object". It MUST NOT define the protocol headers.
    /// </summary>
    [DataMember(Order = 1, Name = "headers"), JsonPropertyOrder(1), JsonPropertyName("headers"), YamlMember(Order = 1, Alias = "headers")]
    public virtual JsonSchema? Headers { get; set; }

    /// <summary>
    /// Gets/sets the definition of the correlation ID used for message tracing or matching.
    /// </summary>
    [DataMember(Order = 2, Name = "correlationId"), JsonPropertyOrder(2), JsonPropertyName("correlationId"), YamlMember(Order = 2, Alias = "correlationId")]
    public virtual CorrelationIdDefinition? CorrelationId { get; set; }

    /// <summary>
    /// Gets/sets a string containing the name of the schema format used to define the message payload. 
    /// If omitted, implementations should parse the payload as a <see cref="JsonSchema"/>.
    /// When the payload is defined using a $ref to a remote file, it is RECOMMENDED the schema format includes the file encoding type to allow implementations to parse the file correctly. E.g., adding +yaml if content type is application/vnd.apache.avro results in application/vnd.apache.avro+yaml.
    /// </summary>
    [DataMember(Order = 3, Name = "schemaFormat"), JsonPropertyOrder(3), JsonPropertyName("schemaFormat"), YamlMember(Order = 3, Alias = "schemaFormat")]
    public virtual string? SchemaFormat { get; set; }

    /// <summary>
    /// Gets/sets the content type to use when encoding/decoding a message's payload. The value MUST be a specific media type (e.g. application/json). When omitted, the value MUST be the one specified on the <see cref="AsyncApiDocument.DefaultContentType"/> property.
    /// </summary>
    [DataMember(Order = 4, Name = "contentType"), JsonPropertyOrder(4), JsonPropertyName("contentType"), YamlMember(Order = 4, Alias = "contentType")]
    public virtual string? ContentType { get; set; }

    /// <summary>
    /// Gets/sets a machine-friendly name for the message.
    /// </summary>
    [DataMember(Order = 5, Name = "name"), JsonPropertyOrder(5), JsonPropertyName("name"), YamlMember(Order = 5, Alias = "name")]
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets/sets a human-friendly title for the message.
    /// </summary>
    [DataMember(Order = 6, Name = "title"), JsonPropertyOrder(6), JsonPropertyName("title"), YamlMember(Order = 6, Alias = "title")]
    public virtual string? Title { get; set; }

    /// <summary>
    /// Gets/sets a short summary of what the message is about.
    /// </summary>
    [DataMember(Order = 7, Name = "summary"), JsonPropertyOrder(7), JsonPropertyName("summary"), YamlMember(Order = 7, Alias = "summary")]
    public virtual string? Summary { get; set; }

    /// <summary>
    /// Gets/sets an optional description of the message. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 8, Name = "description"), JsonPropertyOrder(8), JsonPropertyName("description"), YamlMember(Order = 8, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// gets/sets a <see cref="EquatableList{T}"/> of tags for API documentation control. Tags can be used for logical grouping of operations.
    /// </summary>
    [DataMember(Order = 9, Name = "tags"), JsonPropertyOrder(9), JsonPropertyName("tags"), YamlMember(Order = 9, Alias = "tags")]
    public virtual EquatableList<TagDefinition>? Tags { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="EquatableList{T}"/> containing additional external documentation for this message.
    /// </summary>
    [DataMember(Order = 10, Name = "externalDocs"), JsonPropertyOrder(10), JsonPropertyName("externalDocs"), YamlMember(Order = 10, Alias = "externalDocs")]
    public virtual EquatableList<ExternalDocumentationDefinition>? ExternalDocs { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the <see cref="MessageTraitDefinition"/>'s <see cref="IMessageBindingDefinition"/>s
    /// </summary>
    [DataMember(Order = 11, Name = "bindings"), JsonPropertyOrder(11), JsonPropertyName("bindings"), YamlMember(Order = 11, Alias = "bindings")]
    public virtual MessageBindingCollection? Bindings { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> where keys MUST be either headers and/or payload. 
    /// Values MUST contain examples that validate against the headers or payload fields, respectively. 
    /// Example MAY also have the name and summary additional keys to provide respectively a machine-friendly name and a short summary of what the example is about.
    /// </summary>
    [DataMember(Order = 12, Name = "examples"), JsonPropertyOrder(12), JsonPropertyName("examples"), YamlMember(Order = 12, Alias = "examples")]
    public virtual EquatableDictionary<string, object>? Examples { get; set; }

    /// <inheritdoc/>
    public override string ToString() => Name ?? base.ToString();

}
