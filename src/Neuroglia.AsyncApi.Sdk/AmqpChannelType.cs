using System.Runtime.Serialization;

namespace Neuroglia.AsyncApi.Sdk
{
    /// <summary>
    /// Enumerates all supported AMQP channel types
    /// </summary>
    public enum AmqpChannelType
    {
        /// <summary>
        /// Indicates a routing key based AMQP channel
        /// </summary>
        [EnumMember(Value = "routingKey")]
        RoutingKey,
        /// <summary>
        /// Indicates a queue based AMQP channel
        /// </summary>
        [EnumMember(Value = "queue")]
        Queue
    }

}
