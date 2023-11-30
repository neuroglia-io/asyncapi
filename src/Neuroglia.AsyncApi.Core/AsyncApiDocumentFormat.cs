using Neuroglia.Serialization.Json.Converters;
using System.ComponentModel;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Enumerates all Async API document formats
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum AsyncApiDocumentFormat
{
    /// <summary>
    /// Indicates YAML
    /// </summary>
    [EnumMember(Value = "yaml")]
    Yaml = 1,
    /// <summary>
    /// Indicates JSON
    /// </summary>
    [EnumMember(Value = "json")]
    Json = 2
}
