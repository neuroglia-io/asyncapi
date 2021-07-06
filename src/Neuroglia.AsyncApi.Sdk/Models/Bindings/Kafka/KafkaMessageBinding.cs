using Newtonsoft.Json.Schema;

namespace Neuroglia.AsyncApi.Sdk.Models.Bindings.Kafka
{

    /// <summary>
    /// Represents the object used to configure a Kafka message binding
    /// </summary>
    public class KafkaMessageBinding
        : IMessageBinding
    {

        /// <summary>
        /// Gets/sets a <see cref="JSchema"/> that defines the message key.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("key")]
        [YamlDotNet.Serialization.YamlMember(Alias = "key")]
        [System.Text.Json.Serialization.JsonPropertyName("key")]
        public virtual JSchema Key { get; set; }

        /// <summary>
        /// Gets/sets the version of this binding. Defaults to 'latest'.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindingVersion")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindingVersion")]
        [System.Text.Json.Serialization.JsonPropertyName("bindingVersion")]
        public virtual string BindingVersion { get; set; } = "latest";

    }

}
