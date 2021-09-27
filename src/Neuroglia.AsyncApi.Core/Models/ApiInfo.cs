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

namespace Neuroglia.AsyncApi.Models
{

    /// <summary>
    /// Represents an object that provides metadata about the API. The metadata can be used by the clients if needed
    /// </summary>
    public class ApiInfo
    {

        /// <summary>
        /// Gets/sets the title of the application
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonProperty("title")]
        [YamlDotNet.Serialization.YamlMember(Alias = "title")]
        [System.Text.Json.Serialization.JsonPropertyName("title")]
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets/sets the version of the application API (not to be confused with the specification version)
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonProperty("version")]
        [YamlDotNet.Serialization.YamlMember(Alias = "version")]
        [System.Text.Json.Serialization.JsonPropertyName("version")]
        public virtual string Version { get; set; }

        /// <summary>
        /// Gets/sets a short description of the application. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("description")]
        [YamlDotNet.Serialization.YamlMember(Alias = "description")]
        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public virtual string Description { get; set; }
        
        /// <summary>
        /// Gets/sets a <see cref="Uri"/> to the Terms of Service for the API.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("termsOfService")]
        [YamlDotNet.Serialization.YamlMember(Alias = "termsOfService")]
        [System.Text.Json.Serialization.JsonPropertyName("termsOfService")]
        public virtual Uri TermsOfService { get; set; }

        /// <summary>
        /// Gets/sets the contact information for the exposed API.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("contact")]
        [YamlDotNet.Serialization.YamlMember(Alias = "contact")]
        [System.Text.Json.Serialization.JsonPropertyName("contact")]
        public virtual ContactDefinition Contact { get; set; }

        /// <summary>
        /// Gets/sets the license information for the exposed API.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("license")]
        [YamlDotNet.Serialization.YamlMember(Alias = "license")]
        [System.Text.Json.Serialization.JsonPropertyName("license")]
        public virtual LicenseDefinition License { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Title;
        }

    }

}
