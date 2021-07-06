using Newtonsoft.Json.Schema;

namespace Neuroglia.AsyncApi.Sdk.Models.Bindings.WebSockets
{
    /// <summary>
    /// Represents the object used to configure an WebSocket channel binding
    /// </summary>
    public class WsChannelBinding
        : IChannelBinding
    {

        /// <summary>
        /// Gets/sets the <see cref="WsChannelBinding"/>'s operation type
        /// </summary>
        [Newtonsoft.Json.JsonProperty("type")]
        [YamlDotNet.Serialization.YamlMember(Alias = "type")]
        [System.Text.Json.Serialization.JsonPropertyName("type")]
        public virtual HttpBindingOperationType Type { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="JSchema"/> containing the definitions for each query parameter. This schema MUST be of type object and have a properties key.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("query")]
        [YamlDotNet.Serialization.YamlMember(Alias = "query")]
        [System.Text.Json.Serialization.JsonPropertyName("query")]
        public virtual JSchema Query { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="JSchema"/> containing the definitions for HTTP-specific headers. This schema MUST be of type object and have a properties key.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("headers")]
        [YamlDotNet.Serialization.YamlMember(Alias = "headers")]
        [System.Text.Json.Serialization.JsonPropertyName("headers")]
        public virtual JSchema Headers { get; set; }

        /// <summary>
        /// Gets/sets the version of this binding. Defaults to 'latest'.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindingVersion")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindingVersion")]
        [System.Text.Json.Serialization.JsonPropertyName("bindingVersion")]
        public virtual string BindingVersion { get; set; } = "latest";

    }

}
