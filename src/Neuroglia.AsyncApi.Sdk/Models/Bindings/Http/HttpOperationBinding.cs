using Newtonsoft.Json.Schema;

namespace Neuroglia.AsyncApi.Sdk.Models.Bindings.Http
{
    /// <summary>
    /// Represents the object used to configure an http operation binding
    /// </summary>
    public class HttpOperationBinding
        : IOperationBinding
    {

        /// <summary>
        /// Gets/sets the binding's operation type
        /// </summary>
        [Newtonsoft.Json.JsonProperty("type")]
        [YamlDotNet.Serialization.YamlMember(Alias = "type")]
        [System.Text.Json.Serialization.JsonPropertyName("type")]
        public virtual HttpBindingOperationType Type { get; set; }

        /// <summary>
        /// Gets/sets the binding's method
        /// </summary>
        [Newtonsoft.Json.JsonProperty("method")]
        [YamlDotNet.Serialization.YamlMember(Alias = "method")]
        [System.Text.Json.Serialization.JsonPropertyName("method")]
        public virtual HttpMethod Method { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="JSchema"/> containing the definitions for each query parameter. This schema MUST be of type object and have a properties key.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("query")]
        [YamlDotNet.Serialization.YamlMember(Alias = "query")]
        [System.Text.Json.Serialization.JsonPropertyName("query")]
        public virtual JSchema Query { get; set; }

        /// <summary>
        /// Gets/sets the version of this binding. Defaults to 'latest'.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindingVersion")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindingVersion")]
        [System.Text.Json.Serialization.JsonPropertyName("bindingVersion")]
        public virtual string BindingVersion { get; set; } = "latest";

    }

}
