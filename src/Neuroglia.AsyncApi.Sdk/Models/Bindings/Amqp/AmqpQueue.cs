namespace Neuroglia.AsyncApi.Sdk.Models.Bindings.Amqp
{
    /// <summary>
    /// Represents the object used to configure an AMQP queue based channel
    /// </summary>
    public class AmqpQueue
    {

        /// <summary>
        /// Gets/sets the name of the queue. It MUST NOT exceed 255 characters long.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name")]
        [YamlDotNet.Serialization.YamlMember(Alias = "name")]
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether the queue should survive broker restarts or not.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("durable")]
        [YamlDotNet.Serialization.YamlMember(Alias = "durable")]
        [System.Text.Json.Serialization.JsonPropertyName("durable")]
        public virtual bool Durable { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether the queue should be used only by one connection or not.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("exclusive")]
        [YamlDotNet.Serialization.YamlMember(Alias = "exclusive")]
        [System.Text.Json.Serialization.JsonPropertyName("exclusive")]
        public virtual bool Exclusive { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether the queue should be deleted when the last queue is unbound from it.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("autoDelete")]
        [YamlDotNet.Serialization.YamlMember(Alias = "autoDelete")]
        [System.Text.Json.Serialization.JsonPropertyName("autoDelete")]
        public virtual bool AutoDelete { get; set; }

        /// <summary>
        /// Gets/sets the virtual host of the queue. Defaults to '/'.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("vhost")]
        [YamlDotNet.Serialization.YamlMember(Alias = "vhost")]
        [System.Text.Json.Serialization.JsonPropertyName("vhost")]
        public virtual string VirtualHost { get; set; } = "/";

    }

}
