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
using System.Collections;
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Models
{
    /// <summary>
    /// Represents a collection of <see cref="OAuthFlow"/>s
    /// </summary>
    public class OAuthFlowCollection
        : IEnumerable<KeyValuePair<string, OAuthFlow>>
    {

        /// <summary>
        /// Gets/sets the configuration for the OAuth Implicit flow
        /// </summary>
        [Newtonsoft.Json.JsonProperty("implicit")]
        [YamlDotNet.Serialization.YamlMember(Alias = "implicit")]
        [System.Text.Json.Serialization.JsonPropertyName("implicit")]
        public virtual OAuthFlow Implicit { get; set; }

        /// <summary>
        /// Gets/sets the configuration for the OAuth Resource Owner Protected Credentials flow
        /// </summary>
        [Newtonsoft.Json.JsonProperty("password")]
        [YamlDotNet.Serialization.YamlMember(Alias = "password")]
        [System.Text.Json.Serialization.JsonPropertyName("password")]
        public virtual OAuthFlow Password { get; set; }

        /// <summary>
        /// Gets/sets the configuration for the OAuth Client Credentials flow
        /// </summary>
        [Newtonsoft.Json.JsonProperty("clientCredentials")]
        [YamlDotNet.Serialization.YamlMember(Alias = "clientCredentials")]
        [System.Text.Json.Serialization.JsonPropertyName("clientCredentials")]
        public virtual OAuthFlow ClientCredentials { get; set; }

        /// <summary>
        /// Gets/sets the configuration for the OAuth Authorization Code flow
        /// </summary>
        [Newtonsoft.Json.JsonProperty("authorizationCode")]
        [YamlDotNet.Serialization.YamlMember(Alias = "authorizationCode")]
        [System.Text.Json.Serialization.JsonPropertyName("authorizationCode")]
        public virtual OAuthFlow AuthorizationCode { get; set; }

        /// <inheritdoc/>
        public virtual IEnumerator<KeyValuePair<string, OAuthFlow>> GetEnumerator()
        {
            if (this.Implicit != null)
                yield return new(nameof(this.Implicit), this.Implicit);
            if (this.Password != null)
                yield return new(nameof(this.Password), this.Password);
            if (this.ClientCredentials != null)
                yield return new(nameof(this.ClientCredentials), this.ClientCredentials);
            if (this.AuthorizationCode != null)
                yield return new(nameof(this.AuthorizationCode), this.AuthorizationCode);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }

}
