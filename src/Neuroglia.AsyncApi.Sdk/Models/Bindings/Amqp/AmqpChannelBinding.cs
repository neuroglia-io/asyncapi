namespace Neuroglia.AsyncApi.Sdk.Models.Bindings.Amqp
{
    /// <summary>
    /// Represents the object used to configure an AMQP channel binding
    /// </summary>
    public class AmqpChannelBinding
        : IChannelBinding
    {

        /// <summary>
        /// Gets/sets AMQP channel's type
        /// </summary>
        [Newtonsoft.Json.JsonProperty("is")]
        [YamlDotNet.Serialization.YamlMember(Alias = "is")]
        [System.Text.Json.Serialization.JsonPropertyName("is")]
        public virtual AmqpChannelType Type { get; set; }

        /// <summary>
        /// Gets/sets the object used to configure the AMQP exchange, when <see cref="Type"/> is set to <see cref="AmqpChannelType.RoutingKey"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty("exchange")]
        [YamlDotNet.Serialization.YamlMember(Alias = "exchange")]
        [System.Text.Json.Serialization.JsonPropertyName("exchange")]
        public virtual AmqpExchange Exchange { get; set; }

        /// <summary>
        /// Gets/sets the object used to configure the AMQP queue, when <see cref="Type"/> is set to <see cref="AmqpChannelType.Queue"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty("queue")]
        [YamlDotNet.Serialization.YamlMember(Alias = "queue")]
        [System.Text.Json.Serialization.JsonPropertyName("queue")]
        public virtual AmqpQueue Queue { get; set; }

        /// <summary>
        /// Gets/sets the version of this binding. Defaults to 'latest'.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindingVersion")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindingVersion")]
        [System.Text.Json.Serialization.JsonPropertyName("bindingVersion")]
        public virtual string BindingVersion { get; set; } = "latest";

    }

}
