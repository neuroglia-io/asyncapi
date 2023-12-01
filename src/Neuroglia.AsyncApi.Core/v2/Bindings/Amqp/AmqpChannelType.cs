using Neuroglia.Serialization.Json.Converters;
using System.ComponentModel;

namespace Neuroglia.AsyncApi.v2.Bindings.Amqp;

/// <summary>
/// Enumerates all supported AMQP channel types
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum AmqpChannelType
{
    /// <summary>
    /// Indicates a routing key based AMQP channel
    /// </summary>
    [EnumMember(Value = "routingKey")]
    RoutingKey = 1,
    /// <summary>
    /// Indicates a queue based AMQP channel
    /// </summary>
    [EnumMember(Value = "queue")]
    Queue = 2
}
