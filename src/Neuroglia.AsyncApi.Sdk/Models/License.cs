using System;
using System.ComponentModel.DataAnnotations;

namespace Neuroglia.AsyncApi.Sdk.Models
{
    /// <summary>
    /// Represents an object used to provide license information for the exposed API
    /// </summary>
    public class License
    {

        /// <summary>
        /// Gets/sets the license name used for the API.
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonProperty("name")]
        [YamlDotNet.Serialization.YamlMember(Alias = "name")]
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="Uri"/> to the license used for the API.
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonProperty("url")]
        [YamlDotNet.Serialization.YamlMember(Alias = "url")]
        [System.Text.Json.Serialization.JsonPropertyName("url")]
        public virtual Uri Url { get; set; }

    }

}
