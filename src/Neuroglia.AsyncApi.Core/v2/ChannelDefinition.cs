using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Represents an object used to define an Async API channel
/// </summary>
[DataContract]
public record ChannelDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets an optional description of this channel item. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
    /// </summary>
    [DataMember(Order = 1, Name = "description"), JsonPropertyOrder(1), JsonPropertyName("description"), YamlMember(Order = 1, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets a definition of the SUBSCRIBE operation, which defines the messages produced by the application and sent to the channel.
    /// </summary>
    [DataMember(Order = 2, Name = "subscribe"), JsonPropertyOrder(2), JsonPropertyName("subscribe"), YamlMember(Order = 2, Alias = "subscribe")]
    public virtual OperationDefinition? Subscribe { get; set; }

    /// <summary>
    /// Gets/sets a definition of the PUBLISH operation, which defines the messages consumed by the application from the channel.
    /// </summary>
    [DataMember(Order = 3, Name = "publish"), JsonPropertyOrder(3), JsonPropertyName("publish"), YamlMember(Order = 3, Alias = "publish")]
    public virtual OperationDefinition? Publish { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> of the parameters included in the channel name. It SHOULD be present only when using channels with expressions (as defined by RFC 6570 section 2.2).
    /// </summary>
    [DataMember(Order = 4, Name = "parameters"), JsonPropertyOrder(4), JsonPropertyName("parameters"), YamlMember(Order = 4, Alias = "parameters")]
    public virtual EquatableDictionary<string, ParameterDefinition>? Parameters { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the <see cref="ChannelDefinition"/>'s <see cref="IChannelBindingDefinition"/>s
    /// </summary>
    [DataMember(Order = 5, Name = "bindings"), JsonPropertyOrder(5), JsonPropertyName("bindings"), YamlMember(Order = 5, Alias = "bindings")]
    public virtual ChannelBindingDefinitionCollection? Bindings { get; set; }

    /// <summary>
    /// Gets a boolean indicating whether or not the <see cref="ChannelDefinition"/> defines an <see cref="OperationDefinition"/> of type <see cref="OperationType.Subscribe"/>
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual bool DefinesSubscribeOperation => DefinesOperationOfType(OperationType.Subscribe);

    /// <summary>
    /// Gets a boolean indicating whether or not the <see cref="ChannelDefinition"/> defines an <see cref="OperationDefinition"/> of type <see cref="OperationType.Publish"/>
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual bool DefinesPublishOperation => DefinesOperationOfType(OperationType.Publish);

    /// <summary>
    /// Determines whether or not the <see cref="ChannelDefinition"/> defines an <see cref="OperationDefinition"/> with the specified id
    /// </summary>
    /// <param name="operationId">The id of the operation to check</param>
    /// <returns>A boolean indicating whether or not the <see cref="ChannelDefinition"/> defines an <see cref="OperationDefinition"/> with the specified id</returns>
    public virtual bool DefinesOperationWithId(string operationId)
    {
        if (string.IsNullOrWhiteSpace(operationId)) throw new ArgumentNullException(nameof(operationId));
        return Publish?.OperationId == operationId || Subscribe?.OperationId == operationId;
    }

    /// <summary>
    /// Determines whether or not the <see cref="ChannelDefinition"/> defines an <see cref="OperationDefinition"/> of the specified type
    /// </summary>
    /// <param name="type">The type of the operation to check</param>
    /// <returns>A boolean indicating whether or not the <see cref="ChannelDefinition"/> defines an <see cref="OperationDefinition"/> of the specified type</returns>
    public virtual bool DefinesOperationOfType(OperationType type)
    {
        return type switch
        {
            OperationType.Publish => Publish != null,
            OperationType.Subscribe => Subscribe != null,
            _ => throw new NotSupportedException($"The specified operation type '{type}' is not supported"),
        };
    }

    /// <summary>
    /// Gets the <see cref="OperationDefinition"/> with the specified id
    /// </summary>
    /// <param name="operationId">The id of the <see cref="OperationDefinition"/> to get</param>
    /// <returns>The <see cref="OperationDefinition"/> with the specified id, if any</returns>
    public virtual OperationDefinition? GetOperationById(string operationId)
    {
        if (string.IsNullOrWhiteSpace(operationId)) throw new ArgumentNullException(nameof(operationId));
        if (Publish?.OperationId == operationId) return Publish;
        if (Subscribe?.OperationId == operationId) return Subscribe;
        return null;
    }

    /// <summary>
    /// Attempts to retrieve an <see cref="OperationDefinition"/> by id
    /// </summary>
    /// <param name="operationId">The id of the <see cref="OperationDefinition"/> to get</param>
    /// <param name="operation">The <see cref="OperationDefinition"/> with the specified id</param>
    /// <returns>A boolean indicating whether or not the <see cref="OperationDefinition"/> with the specified id could be found</returns>
    public virtual bool TryGetOperationById(string operationId, out OperationDefinition? operation)
    {
        if (string.IsNullOrWhiteSpace(operationId)) throw new ArgumentNullException(nameof(operationId));
        operation = GetOperationById(operationId);
        return operation != null;
    }

    /// <summary>
    /// Gets the <see cref="OperationDefinition"/> of the specified type
    /// </summary>
    /// <param name="type">The type of the <see cref="OperationDefinition"/> to get</param>
    /// <returns>The <see cref="OperationDefinition"/> of the specified type, if any</returns>
    public virtual OperationDefinition? GetOperationByType(OperationType type)
    {
        return type switch
        {
            OperationType.Publish => Publish,
            OperationType.Subscribe => Subscribe,
            _ => throw new NotSupportedException($"The specified operation type '{type}' is not supported"),
        };
    }

    /// <summary>
    /// Attempts to retrieve an <see cref="OperationDefinition"/> by type
    /// </summary>
    /// <param name="type">The type of the <see cref="OperationDefinition"/> to get</param>
    /// <param name="operation">The <see cref="OperationDefinition"/> of the specified type</param>
    /// <returns>A boolean indicating whether or not the <see cref="OperationDefinition"/> of the specified type could be found</returns>
    public virtual bool TryGetOperationById(OperationType type, out OperationDefinition? operation)
    {
        operation = GetOperationByType(type);
        return operation != null;
    }

}
