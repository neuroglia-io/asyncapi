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
using Newtonsoft.Json.Schema;
using System.ComponentModel.DataAnnotations;

namespace Neuroglia.AsyncApi.Sdk.Models
{
    /// <summary>
    /// Represents an object used to define an Async API parameter
    /// </summary>
    public class ParameterDefinition
    {

        /// <summary>
        /// Gets/sets a short description of the parameter. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("description")]
        [YamlDotNet.Serialization.YamlMember(Alias = "description")]
        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="ParameterDefinition"/>'s <see cref="JSchema"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty("schema")]
        [YamlDotNet.Serialization.YamlMember(Alias = "schema")]
        [System.Text.Json.Serialization.JsonPropertyName("schema")]
        public virtual JSchema Schema { get; set; }

        /// <summary>
        /// Gets/sets a runtime expression that specifies the location of the parameter value. 
        /// Even when a definition for the target field exists, it MUST NOT be used to validate this parameter but, instead, the <see cref="Schema"/> property MUST be used.
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonProperty("location")]
        [YamlDotNet.Serialization.YamlMember(Alias = "location")]
        [System.Text.Json.Serialization.JsonPropertyName("location")]
        public virtual string Location { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Location;
        }

    }

}
