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

namespace Neuroglia.AsyncApi.FluentBuilders.v2;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="V2SecuritySchemeDefinition"/>s
/// </summary>
public interface IV2SecuritySchemeDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="V2SecuritySchemeDefinition"/> to build to be of the specified <see cref="SecuritySchemeType"/>
    /// </summary>
    /// <param name="type">The <see cref="SecuritySchemeType"/> of the <see cref="V2SecuritySchemeDefinition"/> to build</param>
    /// <returns>The configured <see cref="IV2SecuritySchemeDefinitionBuilder"/></returns>
    IV2SecuritySchemeDefinitionBuilder WithType(SecuritySchemeType type);

    /// <summary>
    /// Configures the <see cref="V2SecuritySchemeDefinition"/> to build to use the specified name
    /// </summary>
    /// <param name="name">The <see cref="V2SecuritySchemeDefinition"/>'s name</param>
    /// <returns>The configured <see cref="IV2SecuritySchemeDefinitionBuilder"/></returns>
    IV2SecuritySchemeDefinitionBuilder WithParameterName(string? name);

    /// <summary>
    /// Configures the <see cref="V2SecuritySchemeDefinition"/> to build to use the specified description
    /// </summary>
    /// <param name="location">The <see cref="V2SecuritySchemeDefinition"/>'s description</param>
    /// <returns>The configured <see cref="IV2SecuritySchemeDefinitionBuilder"/></returns>
    IV2SecuritySchemeDefinitionBuilder WithDescription(string? location);

    /// <summary>
    /// Configures the <see cref="V2SecuritySchemeDefinition"/> to build to store API keys at the specified location
    /// </summary>
    /// <param name="location">The location to store the API key at</param>
    /// <returns>The configured <see cref="IV2SecuritySchemeDefinitionBuilder"/></returns>
    IV2SecuritySchemeDefinitionBuilder WithApiKeyLocation(string? location);

    /// <summary>
    /// Configures the <see cref="V2SecuritySchemeDefinition"/> to build to use the specified scheme name in the Authorization header, as defined in RFC7235.
    /// </summary>
    /// <param name="scheme">The name of the authorization scheme to use</param>
    /// <returns>The configured <see cref="IV2SecuritySchemeDefinitionBuilder"/></returns>
    IV2SecuritySchemeDefinitionBuilder WithAuthorizationScheme(string? scheme);

    /// <summary>
    /// Configures the <see cref="V2SecuritySchemeDefinition"/> to build to use the specified object hint for the client to identify how the bearer token is formatted
    /// </summary>
    /// <param name="format">The bearer format to use</param>
    /// <returns>The configured <see cref="IV2SecuritySchemeDefinitionBuilder"/></returns>
    IV2SecuritySchemeDefinitionBuilder WithBearerFormat(string? format);

    /// <summary>
    /// Configures the <see cref="V2SecuritySchemeDefinition"/> to build to use the specified OpenId Connect <see cref="Uri"/> to discover OAuth2 configuration values
    /// </summary>
    /// <param name="uri">The OpenId Connect <see cref="Uri"/> at which to discover OAuth2 configuration values</param>
    /// <returns>The configured <see cref="IV2SecuritySchemeDefinitionBuilder"/></returns>
    IV2SecuritySchemeDefinitionBuilder WithOpenIdConnectUrl(Uri? uri);

    /// <summary>
    /// Configures the <see cref="V2SecuritySchemeDefinition"/> to build to use the specified OAUTH flows
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to build the <see cref="OAuthFlowDefinition"/>s to use</param>
    /// <returns>The configured <see cref="IV2SecuritySchemeDefinitionBuilder"/></returns>
    IV2SecuritySchemeDefinitionBuilder WithOAuthFlows(Action<IOAuthFlowDefinitionCollectionBuilder> setup);

    /// <summary>
    /// Builds a new <see cref="V2SecuritySchemeDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="V2SecuritySchemeDefinition"/></returns>
    V2SecuritySchemeDefinition Build();

}
