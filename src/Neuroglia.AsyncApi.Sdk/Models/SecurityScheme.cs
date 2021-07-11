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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Neuroglia.AsyncApi.Models
{
    /// <summary>
    /// Represents an object used to define a security scheme
    /// </summary>
    public class SecurityScheme
        : ReferenceableComponent
    {

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing additional external documentation.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("type")]
        [YamlDotNet.Serialization.YamlMember(Alias = "type")]
        [System.Text.Json.Serialization.JsonPropertyName("type")]
        public virtual SecuritySchemeType Type { get; set; }

        /// <summary>
        /// Gets/sets a short description of the security scheme. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("type")]
        [YamlDotNet.Serialization.YamlMember(Alias = "type")]
        [System.Text.Json.Serialization.JsonPropertyName("type")]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets/sets the name of the header, query or cookie parameter to be used.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name")]
        [YamlDotNet.Serialization.YamlMember(Alias = "name")]
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets the location of the API key. Valid values are "user" and "password" for <see cref="SecuritySchemeType.ApiKey"/> and "query", "header" or "cookie" for <see cref="SecuritySchemeType.HttpApiKey"/>.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("in")]
        [YamlDotNet.Serialization.YamlMember(Alias = "in")]
        [System.Text.Json.Serialization.JsonPropertyName("in")]
        public virtual string In { get; set; }

        /// <summary>
        /// Gets/sets the name of the HTTP Authorization scheme to be used in the Authorization header as defined in RFC7235.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("scheme")]
        [YamlDotNet.Serialization.YamlMember(Alias = "scheme")]
        [System.Text.Json.Serialization.JsonPropertyName("scheme")]
        public virtual string Scheme { get; set; }

        /// <summary>
        /// Gets/sets an object containing configuration information for the flow types supported.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bearerFormat")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bearerFormat")]
        [System.Text.Json.Serialization.JsonPropertyName("bearerFormat")]
        public virtual string BearerFormat { get; set; }

        /// <summary>
        /// Gets/sets an object containing configuration information for the flow types supported.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("flows")]
        [YamlDotNet.Serialization.YamlMember(Alias = "flows")]
        [System.Text.Json.Serialization.JsonPropertyName("flows")]
        public virtual OAuthFlowCollection Flows { get; set; }

        /// <summary>
        /// Gets/sets the OpenId Connect <see cref="Uri"/> to discover OAuth2 configuration values.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("openIdConnectUrl")]
        [YamlDotNet.Serialization.YamlMember(Alias = "openIdConnectUrl")]
        [System.Text.Json.Serialization.JsonPropertyName("openIdConnectUrl")]
        public virtual Uri OpenIdConnectUrl { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Type.ToString();
        }

    }

}
