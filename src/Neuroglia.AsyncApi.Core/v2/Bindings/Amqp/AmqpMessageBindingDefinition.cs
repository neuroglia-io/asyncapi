using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.Amqp;

/// <summary>
/// Represents the object used to configure an AMQP 0.9+ message binding
/// </summary>
[DataContract]
public record AmqpMessageBindingDefinition
    : AmqpBindingDefinition, IMessageBindingDefinition
{

    /// <summary>
    /// Gets/sets a MIME encoding for the message content.
    /// </summary>
    [DataMember(Order = 1, Name = "contentEncoding"), JsonPropertyOrder(1), JsonPropertyName("contentEncoding"), YamlMember(Order = 1, Alias = "contentEncoding")]
    public virtual string? ContentEncoding { get; set; }

    /// <summary>
    /// Gets/sets a string that represents an application-specific message type.
    /// </summary>
    [DataMember(Order = 2, Name = "messageType"), JsonPropertyOrder(2), JsonPropertyName("messageType"), YamlMember(Order = 2, Alias = "messageType")]
    public virtual string? MessageType { get; set; }

}
