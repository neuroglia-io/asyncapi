using Neuroglia.Serialization.Json.Converters;
using System.ComponentModel;

namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Enumerates all supported runtime expression sources
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum RuntimeExpressionSource
{
    /// <summary>
    /// Indicates an expression that targets message headers
    /// </summary>
    [EnumMember(Value = "header")]
    Header = 1,
    /// <summary>
    /// Indicates an expression that targets message payload
    /// </summary>
    [EnumMember(Value = "payload")]
    Payload = 2
}