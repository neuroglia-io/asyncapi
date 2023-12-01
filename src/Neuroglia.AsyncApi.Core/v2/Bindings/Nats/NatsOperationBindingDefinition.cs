using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2.Bindings.Nats;

/// <summary>
/// Represents the object used to configure a <see href="https://nats.io/">NATS</see> operation binding
/// </summary>
[DataContract]
public record NatsOperationBindingDefinition
    : NatsBindingDefinition, IOperationBindingDefinition
{

    /// <summary>
    /// Gets/sets the name of the queue to use. It MUST NOT exceed 255 characters.
    /// </summary>
    [DataMember(Order = 1, Name = "queue"), JsonPropertyOrder(1), JsonPropertyName("queue"), YamlMember(Order = 1, Alias = "queue")]
    public virtual string? Queue { get; set; }

}
