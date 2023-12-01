using Json.Schema;
using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Represents a set of reusable objects for different aspects of the AsyncAPI specification. 
/// All objects defined within the components object will have no effect on the API unless they are explicitly referenced from properties outside the components object.
/// </summary>
[DataContract]
public record ComponentDefinitionCollection
{

    /// <summary>
    /// Gets/sets a <see cref="IDictionary{TKey, TValue}"/> used to hold reusable <see cref="JSchema"/>s
    /// </summary>
    [DataMember(Order = 1, Name = "schemas"), JsonPropertyOrder(1), JsonPropertyName("schemas"), YamlMember(Order = 1, Alias = "schemas")]
    public virtual EquatableDictionary<string, JsonSchema>? Schemas { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="IDictionary{TKey, TValue}"/> used to hold reusable <see cref="MessageDefinition"/>s
    /// </summary>
    [DataMember(Order = 2, Name = "messages"), JsonPropertyOrder(2), JsonPropertyName("messages"), YamlMember(Order = 2, Alias = "messages")]
    public virtual EquatableDictionary<string, MessageDefinition>? Messages { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="IDictionary{TKey, TValue}"/> used to hold reusable <see cref="SecuritySchemeDefinition"/>s
    /// </summary>
    [DataMember(Order = 3, Name = "securitySchemes"), JsonPropertyOrder(3), JsonPropertyName("securitySchemes"), YamlMember(Order = 3, Alias = "securitySchemes")]
    public virtual EquatableDictionary<string, SecuritySchemeDefinition>? SecuritySchemes { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="IDictionary{TKey, TValue}"/> used to hold reusable <see cref="ParameterDefinition"/>s
    /// </summary>
    [DataMember(Order = 4, Name = "parameters"), JsonPropertyOrder(4), JsonPropertyName("parameters"), YamlMember(Order = 4, Alias = "parameters")]
    public virtual EquatableDictionary<string, ParameterDefinition>? Parameters { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="IDictionary{TKey, TValue}"/> used to hold reusable <see cref="CorrelationIdDefinition"/>s
    /// </summary>
    [DataMember(Order = 5, Name = "correlationIds"), JsonPropertyOrder(5), JsonPropertyName("correlationIds"), YamlMember(Order = 5, Alias = "correlationIds")]
    public virtual EquatableDictionary<string, CorrelationIdDefinition>? CorrelationIds { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="IDictionary{TKey, TValue}"/> used to hold reusable <see cref="OperationTraitDefinition"/>s
    /// </summary>
    [DataMember(Order = 6, Name = "operationTraits"), JsonPropertyOrder(6), JsonPropertyName("operationTraits"), YamlMember(Order = 6, Alias = "operationTraits")]
    public virtual EquatableDictionary<string, OperationTraitDefinition>? OperationTraits { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="IDictionary{TKey, TValue}"/> used to hold reusable <see cref="MessageTraitDefinition"/>s
    /// </summary>
    [DataMember(Order = 7, Name = "messageTraits"), JsonPropertyOrder(7), JsonPropertyName("messageTraits"), YamlMember(Order = 7, Alias = "messageTraits")]
    public virtual EquatableDictionary<string, MessageTraitDefinition>? MessageTraits { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="IDictionary{TKey, TValue}"/> used to hold reusable <see cref="ServerBindingDefinitionCollection"/>s
    /// </summary>
    [DataMember(Order = 8, Name = "serverBindings"), JsonPropertyOrder(8), JsonPropertyName("serverBindings"), YamlMember(Order = 8, Alias = "serverBindings")]
    public virtual EquatableDictionary<string, ServerBindingDefinitionCollection>? ServerBindings { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="IDictionary{TKey, TValue}"/> used to hold reusable <see cref="ChannelBindingDefinitionCollection"/>s
    /// </summary>
    [DataMember(Order = 9, Name = "channelBindings"), JsonPropertyOrder(9), JsonPropertyName("channelBindings"), YamlMember(Order = 9, Alias = "channelBindings")]
    public virtual EquatableDictionary<string, ChannelBindingDefinitionCollection>? ChannelBindings { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="IDictionary{TKey, TValue}"/> used to hold reusable <see cref="OperationBindingDefinitionCollection"/>s
    /// </summary>
    [DataMember(Order = 10, Name = "operationBindings"), JsonPropertyOrder(10), JsonPropertyName("operationBindings"), YamlMember(Order = 10, Alias = "operationBindings")]
    public virtual EquatableDictionary<string, OperationBindingDefinitionCollection>? OperationBindings { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="IDictionary{TKey, TValue}"/> used to hold reusable <see cref="MessageBindingCollection"/>s
    /// </summary>
    [DataMember(Order = 11, Name = "messageBindings"), JsonPropertyOrder(11), JsonPropertyName("messageBindings"), YamlMember(Order = 11, Alias = "messageBindings")]
    public virtual EquatableDictionary<string, MessageBindingCollection>? MessageBindings { get; set; }

}
