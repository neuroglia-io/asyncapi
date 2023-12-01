namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Represents an object used to define the message(s) supported by the operation
/// </summary>
[DataContract]
public record OperationMessageDefinition
    : MessageDefinition
{

    /// <summary>
    /// Gets/sets a list, if any, of all messages supported by the operation
    /// </summary>
    [DataMember(Order = 99, Name = "oneOf"), JsonPropertyOrder(99), JsonPropertyName("oneOf"), YamlMember(Order = 99, Alias = "oneOf")]
    public virtual EquatableList<MessageDefinition>? OneOf { get; set; }

    /// <summary>
    /// Creates a new <see cref="OperationMessageDefinition"/> based on the specified <see cref="MessageDefinition"/>
    /// </summary>
    /// <param name="messageDefinition">The <see cref="MessageDefinition"/> to create a new <see cref="OperationMessageDefinition"/> for</param>
    /// <returns>A new <see cref="OperationMessageDefinition"/></returns>
    public static OperationMessageDefinition From(MessageDefinition messageDefinition)
    {
        if (messageDefinition is OperationMessageDefinition operationMessageDefinition) return operationMessageDefinition;
        return new()
        {
            Bindings = messageDefinition.Bindings,
            ContentType = messageDefinition.ContentType,
            CorrelationId = messageDefinition.CorrelationId,
            Description = messageDefinition.Description,
            Examples = messageDefinition.Examples,
            ExternalDocs = messageDefinition.ExternalDocs,
            Headers = messageDefinition.Headers,
            Name = messageDefinition.Name,
            Payload = messageDefinition.Payload,
            Reference = messageDefinition.Reference,
            SchemaFormat = messageDefinition.SchemaFormat,
            Summary = messageDefinition.Summary,
            Tags = messageDefinition.Tags,
            Title = messageDefinition.Title,
            Traits = messageDefinition.Traits,
        };
    }

}