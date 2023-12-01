using Neuroglia.Serialization.Json.Converters;
using System.ComponentModel;

namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Enumerates all Async API operation types
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum OperationType
{
    /// <summary>
    /// Indicates a PUB operation
    /// </summary>
    [EnumMember(Value = "pub")]
    Publish = 1,
    /// <summary>
    /// Indicates a SUB operation
    /// </summary>
    [EnumMember(Value = "sub")]
    Subscribe = 2
}
