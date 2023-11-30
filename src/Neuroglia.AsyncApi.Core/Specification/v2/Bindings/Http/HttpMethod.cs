using Neuroglia.Serialization.Json.Converters;
using System.ComponentModel;

namespace Neuroglia.AsyncApi.Specification.v2.Bindings.Http;

/// <summary>
/// Enumerates all supported HTTP methods
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum HttpMethod
{
    /// <summary>
    /// Indicates the GET http method
    /// </summary>
    [EnumMember(Value = "GET")]
    GET,
    /// <summary>
    /// Indicates the POST http method
    /// </summary>
    [EnumMember(Value = "POST")]
    POST,
    /// <summary>
    /// Indicates the PUT http method
    /// </summary>
    [EnumMember(Value = "PUT")]
    PUT,
    /// <summary>
    /// Indicates the PATCH http method
    /// </summary>
    [EnumMember(Value = "PATCH")]
    PATCH,
    /// <summary>
    /// Indicates the DELETE http method
    /// </summary>
    [EnumMember(Value = "DELETE")]
    DELETE,
    /// <summary>
    /// Indicates the HEAD http method
    /// </summary>
    [EnumMember(Value = "HEAD")]
    HEAD,
    /// <summary>
    /// Indicates the OPTIONS http method
    /// </summary>
    [EnumMember(Value = "OPTIONS")]
    OPTIONS,
    /// <summary>
    /// Indicates the CONNECT http method
    /// </summary>
    [EnumMember(Value = "CONNECT")]
    CONNECT,
    /// <summary>
    /// Indicates the TRACE http method
    /// </summary>
    [EnumMember(Value = "TRACE")]
    TRACE
}
