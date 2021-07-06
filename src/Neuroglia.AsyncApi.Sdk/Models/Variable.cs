using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Sdk.Models
{
    /// <summary>
    /// Represents an object used to describe a Server Variable for server URL template substitution.
    /// </summary>
    public class Variable
    {

        /// <summary>
        /// Gets/sets an <see cref="IEnumerable{T}"/> of values to be used if the substitution options are from a limited set.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("enum")]
        [YamlDotNet.Serialization.YamlMember(Alias = "enum")]
        [System.Text.Json.Serialization.JsonPropertyName("enum")]
        public virtual IEnumerable<string> Enum { get; set; }

        /// <summary>
        /// Gets/sets the default value to use for substitution, and to send, if an alternate value is not supplied.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("default")]
        [YamlDotNet.Serialization.YamlMember(Alias = "default")]
        [System.Text.Json.Serialization.JsonPropertyName("default")]
        public virtual string Default { get; set; }

        /// <summary>
        /// Gets/sets an optional string describing the server variable. <see href="https://spec.commonmark.org/">CommonMark</see> syntax MAY be used for rich text representation.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("description")]
        [YamlDotNet.Serialization.YamlMember(Alias = "description")]
        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets/sets an <see cref="IEnumerable{T}"/> of examples of the server variable.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("examples")]
        [YamlDotNet.Serialization.YamlMember(Alias = "examples")]
        [System.Text.Json.Serialization.JsonPropertyName("examples")]
        public virtual IEnumerable<string> Examples { get; set; }

    }

}
