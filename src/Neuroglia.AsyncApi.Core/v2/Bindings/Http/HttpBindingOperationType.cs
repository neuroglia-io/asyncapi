using Neuroglia.Serialization.Json.Converters;
using System.ComponentModel;

namespace Neuroglia.AsyncApi.v2.Bindings.Http;

/// <summary>
/// Enumerates all types of http binding operation types
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum HttpBindingOperationType
{
    /// <summary>
    /// Indicates a request
    /// </summary>
    [EnumMember(Value = "request")]
    Request,
    /// <summary>
    /// Indicates a response
    /// </summary>
    [EnumMember(Value = "response")]
    Response
}
