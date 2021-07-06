using System.Runtime.Serialization;

namespace Neuroglia.AsyncApi.Sdk
{
    /// <summary>
    /// Enumerates all supported AMQP exchange types
    /// </summary>
    public enum AmqpExchangeType
    {
        /// <summary>
        /// Indicates a topic AMQP exchange
        /// </summary>
        [EnumMember(Value = "topic")]
        Topic,
        /// <summary>
        /// Indicates a direct AMQP exchange
        /// </summary>
        [EnumMember(Value = "direct")]
        Direct,
        /// <summary>
        /// Indicates a fanout AMQP exchange
        /// </summary>
        [EnumMember(Value = "fanout")]
        Fanout,
        /// <summary>
        /// Indicates a default AMQP exchange
        /// </summary>
        [EnumMember(Value = "default")]
        Default,
        /// <summary>
        /// Indicates a headers AMQP exchange
        /// </summary>
        [EnumMember(Value = "headers")]
        Headers
    }

}
