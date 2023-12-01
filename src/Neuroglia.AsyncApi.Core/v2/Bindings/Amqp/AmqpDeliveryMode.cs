using Neuroglia.Serialization.Json.Converters;
using System.ComponentModel;

namespace Neuroglia.AsyncApi.v2.Bindings.Amqp;

/// <summary>
/// Enumerates all supported AMQP delivery modes
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum AmqpDeliveryMode
{
    /// <summary>
    /// Indicates a transient delivert mode
    /// </summary>
    [EnumMember(Value = "transient")]
    Transient = 1,
    /// <summary>
    /// Indicates a persistent delivery mode
    /// </summary>
    [EnumMember(Value = "persistent")]
    Persistent = 2
}
