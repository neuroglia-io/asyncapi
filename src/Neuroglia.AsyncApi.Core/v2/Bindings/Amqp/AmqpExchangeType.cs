using Neuroglia.Serialization.Json.Converters;
using System.ComponentModel;

namespace Neuroglia.AsyncApi.v2.Bindings.Amqp;

/// <summary>
/// Enumerates all supported AMQP exchange types
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum AmqpExchangeType
{
    /// <summary>
    /// Indicates a topic AMQP exchange
    /// </summary>
    [EnumMember(Value = "topic")]
    Topic = 1,
    /// <summary>
    /// Indicates a direct AMQP exchange
    /// </summary>
    [EnumMember(Value = "direct")]
    Direct = 2,
    /// <summary>
    /// Indicates a fanout AMQP exchange
    /// </summary>
    [EnumMember(Value = "fanout")]
    Fanout = 4,
    /// <summary>
    /// Indicates a default AMQP exchange
    /// </summary>
    [EnumMember(Value = "default")]
    Default = 8,
    /// <summary>
    /// Indicates a headers AMQP exchange
    /// </summary>
    [EnumMember(Value = "headers")]
    Headers = 16
}
