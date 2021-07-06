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
using System;
using System.ComponentModel.DataAnnotations;

namespace Neuroglia.AsyncApi.Sdk.Models
{
    /// <summary>
    /// Represents an object used to provide license information for the exposed API
    /// </summary>
    public class LicenseDefinition
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

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}
