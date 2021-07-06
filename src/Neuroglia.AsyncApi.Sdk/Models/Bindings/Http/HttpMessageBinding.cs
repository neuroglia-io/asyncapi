using Newtonsoft.Json.Schema;

namespace Neuroglia.AsyncApi.Sdk.Models.Bindings.Http
{

    /// <summary>
    /// Represents the object used to configure an http message binding
    /// </summary>
    public class HttpMessageBinding
    {

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
