// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Represents an object used to define a supported OAuth Flow
/// </summary>
[DataContract]
public record OAuthFlowDefinition
{

    /// <summary>
    /// Gets/sets the authorization URL to be used for this flow.
    /// </summary>
    [DataMember(Order = 1, Name = "authorizationUrl"), JsonPropertyOrder(1), JsonPropertyName("authorizationUrl"), YamlMember(Order = 1, Alias = "authorizationUrl")]
    public virtual Uri? AuthorizationUrl { get; set; }

    /// <summary>
    /// Gets/sets the token URL to be used for this flow.
    /// </summary>
    [DataMember(Order = 2, Name = "tokenUrl"), JsonPropertyOrder(2), JsonPropertyName("tokenUrl"), YamlMember(Order = 2, Alias = "tokenUrl")]
    public virtual Uri? TokenUrl { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="Uri"/> to be used for obtaining refresh tokens.
    /// </summary>
    [DataMember(Order = 3, Name = "refreshUrl"), JsonPropertyOrder(3), JsonPropertyName("refreshUrl"), YamlMember(Order = 3, Alias = "refreshUrl")]
    public virtual Uri? RefreshUrl { get; set; }

    /// <summary>
    /// Gets/sets the available scopes for the OAuth2 security scheme. A map between the scope name and a short description for it.
    /// </summary>
    [DataMember(Order = 4, Name = "scopes"), JsonPropertyOrder(4), JsonPropertyName("scopes"), YamlMember(Order = 4, Alias = "scopes")]
    public virtual EquatableDictionary<string, string>? Scopes { get; set; }

}
