using Newtonsoft.Json.Schema;

namespace Neuroglia.AsyncApi.Sdk.Models.Bindings.Kafka
{
    /// <summary>
    /// Represents the object used to configure a Kafka operation binding
    /// </summary>
    public class KafkaOperationBinding
        : IOperationBinding
    {

        /// <summary>
        /// Gets/sets the <see cref="JSchema"/> of the consumer group.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("groupId")]
        [YamlDotNet.Serialization.YamlMember(Alias = "groupId")]
        [System.Text.Json.Serialization.JsonPropertyName("groupId")]
        public virtual JSchema GroupId { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="JSchema"/> of the consumer inside a consumer group.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("clientId")]
        [YamlDotNet.Serialization.YamlMember(Alias = "clientId")]
        [System.Text.Json.Serialization.JsonPropertyName("clientId")]
        public virtual JSchema ClientId { get; set; }

        /// <summary>
        /// Gets/sets the version of this binding. Defaults to 'latest'.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindingVersion")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindingVersion")]
        [System.Text.Json.Serialization.JsonPropertyName("bindingVersion")]
        public virtual string BindingVersion { get; set; } = "latest";

    }

}
