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
    /// Represents an object used to provide contact information for the exposed API
    /// </summary>
    public class ContactDefinition
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
        [Newtonsoft.Json.JsonProperty("email")]
        [YamlDotNet.Serialization.YamlMember(Alias = "email")]
        [System.Text.Json.Serialization.JsonPropertyName("email")]
        public virtual string Email { get; set; }

    }

}
