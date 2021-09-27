/*
 * Copyright © 2021 Neuroglia SPRL. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Models
{
    /// <summary>
    /// Represents an object used to describe a Server Variable for server URL template substitution.
    /// </summary>
    public class VariableDefinition
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
        /// Gets/sets an <see cref="List{T}"/> of examples of the server variable.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("examples")]
        [YamlDotNet.Serialization.YamlMember(Alias = "examples")]
        [System.Text.Json.Serialization.JsonPropertyName("examples")]
        public virtual List<string> Examples { get; set; }

    }

}
