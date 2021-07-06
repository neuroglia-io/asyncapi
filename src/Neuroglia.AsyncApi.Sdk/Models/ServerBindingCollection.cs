using Neuroglia.AsyncApi.Sdk.Models.Bindings;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.Amqp;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.AmqpV1;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.Http;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.Kafka;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.WebSockets;
using System.Collections;
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Sdk.Models
{
    /// <summary>
    /// Represents the object used to configure a <see cref="Server"/>'s <see cref="IServerBinding"/>s
    /// </summary>
    public class ServerBindingCollection
        : IEnumerable<IServerBinding>
    {

        /// <summary>
        /// Gets/sets the protocol-specific information for an HTTP server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("http")]
        [YamlDotNet.Serialization.YamlMember(Alias = "http")]
        [System.Text.Json.Serialization.JsonPropertyName("http")]
        public virtual HttpServerBinding Http { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an WebSockets server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("ws")]
        [YamlDotNet.Serialization.YamlMember(Alias = "ws")]
        [System.Text.Json.Serialization.JsonPropertyName("ws")]
        public virtual WsServerBinding Ws { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for a Kafka server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("kafka")]
        [YamlDotNet.Serialization.YamlMember(Alias = "kafka")]
        [System.Text.Json.Serialization.JsonPropertyName("kafka")]
        public virtual KafkaServerBinding Kafka { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an AMQP 0-9-1 server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("amqp")]
        [YamlDotNet.Serialization.YamlMember(Alias = "amqp")]
        [System.Text.Json.Serialization.JsonPropertyName("amqp")]
        public virtual AmqpServerBinding Amqp { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for an AMQP 1.0 server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("amqp1")]
        [YamlDotNet.Serialization.YamlMember(Alias = "amqp1")]
        [System.Text.Json.Serialization.JsonPropertyName("amqp1")]
        public virtual AmqpV1ServerBinding Amqp1 { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for an MQTT server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("mqtt")]
        [YamlDotNet.Serialization.YamlMember(Alias = "mqtt")]
        [System.Text.Json.Serialization.JsonPropertyName("mqtt")]
        public virtual MqttServerBinding Mqtt { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for an MQTT 5 server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("mqtt5")]
        [YamlDotNet.Serialization.YamlMember(Alias = "mqtt5")]
        [System.Text.Json.Serialization.JsonPropertyName("mqtt5")]
        public virtual Mqtt5ServerBinding Mqtt5 { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for a NATS server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("nats")]
        [YamlDotNet.Serialization.YamlMember(Alias = "nats")]
        [System.Text.Json.Serialization.JsonPropertyName("nats")]
        public virtual NatsServerBinding Nats { get; set; }

        /// <summary>
        /// Gets/sets the protocol-specific information for an information for a Redis server.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("redis")]
        [YamlDotNet.Serialization.YamlMember(Alias = "redis")]
        [System.Text.Json.Serialization.JsonPropertyName("redis")]
        public virtual RedisServerBinding Redis { get; set; }

        /// <inheritdoc/>
        public virtual IEnumerator<IServerBinding> GetEnumerator()
        {
            if (this.Http != null)
                yield return this.Http;
            if (this.Ws != null)
                yield return this.Ws;
            if(this.Amqp != null)
                yield return this.Amqp;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }

}
