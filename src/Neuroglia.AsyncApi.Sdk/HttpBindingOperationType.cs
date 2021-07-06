using System.Runtime.Serialization;

namespace Neuroglia.AsyncApi.Sdk
{

    /// <summary>
    /// Enumerates all types of http binding operation types
    /// </summary>
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

}
