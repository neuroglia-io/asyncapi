using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Neuroglia.AsyncApi.Sdk.Models
{

    /// <summary>
    /// Represents an <see href="https://www.asyncapi.com">Async API</see> document
    /// </summary>
    public class AsyncApiDocument
    {

        /// <summary>
        /// Gets/sets the the AsyncAPI Specification version being used. It can be used by tooling Specifications and clients to interpret the version. 
        /// </summary>
        /// <remarks>
        /// The structure shall be major.minor.patch, where patch versions must be compatible with the existing major.minor tooling. 
        /// Typically patch versions will be introduced to address errors in the documentation, and tooling should typically be compatible with the corresponding major.minor (1.0.*). 
        /// Patch versions will correspond to patches of this document.
        /// </remarks>
        [Required]
        [Newtonsoft.Json.JsonProperty("asyncapi")]
        [YamlDotNet.Serialization.YamlMember(Alias = "asyncapi")]
        [System.Text.Json.Serialization.JsonPropertyName("asyncapi")]
        public virtual string AsyncApi { get; set; }

        /// <summary>
        /// Gets/sets the identifier of the application the AsyncAPI document is defining.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("id")]
        [YamlDotNet.Serialization.YamlMember(Alias = "id")]
        [System.Text.Json.Serialization.JsonPropertyName("id")]
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets/sets the object that provides metadata about the API. The metadata can be used by the clients if needed. 
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonProperty("info")]
        [YamlDotNet.Serialization.YamlMember(Alias = "info")]
        [System.Text.Json.Serialization.JsonPropertyName("info")]
        public virtual ApiInfo Info { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> containing name/configuration mappings for the application's servers
        /// </summary>
        [Newtonsoft.Json.JsonProperty("servers")]
        [YamlDotNet.Serialization.YamlMember(Alias = "servers")]
        [System.Text.Json.Serialization.JsonPropertyName("servers")]
        public virtual Dictionary<string, Server> Servers { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Id;
        }

    }

}
