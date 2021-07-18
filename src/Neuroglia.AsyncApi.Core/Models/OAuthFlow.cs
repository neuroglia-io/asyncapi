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

namespace Neuroglia.AsyncApi.Models
{
    /// <summary>
    /// Represents an object used to define a supported OAuth Flow
    /// </summary>
    public class OAuthFlow
    {

        /// <summary>
        /// Gets/sets the authorization URL to be used for this flow. This MUST be in the form of a URL.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("authorizationUrl")]
        [YamlDotNet.Serialization.YamlMember(Alias = "authorizationUrl")]
        [System.Text.Json.Serialization.JsonPropertyName("authorizationUrl")]
        public virtual string AuthorizationUrl { get; set; }

        /// <summary>
        /// Gets/sets the token URL to be used for this flow. This MUST be in the form of a URL.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("tokenUrl")]
        [YamlDotNet.Serialization.YamlMember(Alias = "tokenUrl")]
        [System.Text.Json.Serialization.JsonPropertyName("tokenUrl")]
        public virtual string TokenUrl { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="Uri"/> to be used for obtaining refresh tokens.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("refreshUrl")]
        [YamlDotNet.Serialization.YamlMember(Alias = "refreshUrl")]
        [System.Text.Json.Serialization.JsonPropertyName("refreshUrl")]
        public virtual Uri RefreshUrl { get; set; }

        /// <summary>
        /// Gets/sets the available scopes for the OAuth2 security scheme. A map between the scope name and a short description for it.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("scopes")]
        [YamlDotNet.Serialization.YamlMember(Alias = "scopes")]
        [System.Text.Json.Serialization.JsonPropertyName("scopes")]
        public virtual Dictionary<string, string> Description { get; set; }

    }

}
