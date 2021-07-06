using System;
using System.ComponentModel.DataAnnotations;

namespace Neuroglia.AsyncApi.Sdk.Models
{
    /// <summary>
    /// Represents an object used to provide contact information for the exposed API
    /// </summary>
    public class Contact
    {

        /// <summary>
        /// Gets/sets the identifying name of the contact person/organization.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name")]
        [YamlDotNet.Serialization.YamlMember(Alias = "name")]
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="Uri"/> pointing to the contact information.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("url")]
        [YamlDotNet.Serialization.YamlMember(Alias = "url")]
        [System.Text.Json.Serialization.JsonPropertyName("url")]
        public virtual Uri Url { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="Uri"/> pointing to the contact information.
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [Newtonsoft.Json.JsonProperty("url")]
        [YamlDotNet.Serialization.YamlMember(Alias = "url")]
        [System.Text.Json.Serialization.JsonPropertyName("url")]
        public virtual string Email { get; set; }

    }

}
