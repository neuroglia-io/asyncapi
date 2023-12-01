using Neuroglia.Serialization.Json.Converters;
using System.ComponentModel;

namespace Neuroglia.AsyncApi.v2.Bindings.Mqtt;

/// <summary>
/// Enumerates all supported MQTT Quality of Service types
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum MqttQoSLevel
{
    /// <summary>
    /// Indicates that messages should be sent at most once
    /// </summary>
    [EnumMember(Value = "AtMostOne")]
    AtMostOne = 0,
    /// <summary>
    /// Indicates that messages should be sent at least once
    /// </summary>
    [EnumMember(Value = "AtLeastOne")]
    AtLeastOne = 1,
    /// <summary>
    /// Indicates that MQTT messages should be sent exactly once
    /// </summary>
    [EnumMember(Value = "ExactlyOne")]
    ExactlyOne = 2
}
