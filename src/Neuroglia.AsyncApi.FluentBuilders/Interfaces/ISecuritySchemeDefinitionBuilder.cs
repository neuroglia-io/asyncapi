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

using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="SecuritySchemeDefinition"/>s
/// </summary>
public interface ISecuritySchemeDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="SecuritySchemeDefinition"/> to build to be of the specified <see cref="SecuritySchemeType"/>
    /// </summary>
    /// <param name="type">The <see cref="SecuritySchemeType"/> of the <see cref="SecuritySchemeDefinition"/> to build</param>
    /// <returns>The configured <see cref="ISecuritySchemeDefinitionBuilder"/></returns>
    ISecuritySchemeDefinitionBuilder WithType(SecuritySchemeType type);

    /// <summary>
    /// Configures the <see cref="SecuritySchemeDefinition"/> to build to use the specified name
    /// </summary>
    /// <param name="name">The <see cref="SecuritySchemeDefinition"/>'s name</param>
    /// <returns>The configured <see cref="ISecuritySchemeDefinitionBuilder"/></returns>
    ISecuritySchemeDefinitionBuilder WithParameterName(string? name);

    /// <summary>
    /// Configures the <see cref="SecuritySchemeDefinition"/> to build to use the specified description
    /// </summary>
    /// <param name="location">The <see cref="SecuritySchemeDefinition"/>'s description</param>
    /// <returns>The configured <see cref="ISecuritySchemeDefinitionBuilder"/></returns>
    ISecuritySchemeDefinitionBuilder WithDescription(string? location);

    /// <summary>
    /// Configures the <see cref="SecuritySchemeDefinition"/> to build to store API keys at the specified location
    /// </summary>
    /// <param name="location">The location to store the API key at</param>
    /// <returns>The configured <see cref="ISecuritySchemeDefinitionBuilder"/></returns>
    ISecuritySchemeDefinitionBuilder WithApiKeyLocation(string? location);

    /// <summary>
    /// Configures the <see cref="SecuritySchemeDefinition"/> to build to use the specified scheme name in the Authorization header, as defined in RFC7235.
    /// </summary>
    /// <param name="scheme">The name of the authorization scheme to use</param>
    /// <returns>The configured <see cref="ISecuritySchemeDefinitionBuilder"/></returns>
    ISecuritySchemeDefinitionBuilder WithAuthorizationScheme(string? scheme);

    /// <summary>
    /// Configures the <see cref="SecuritySchemeDefinition"/> to build to use the specified object hint for the client to identify how the bearer token is formatted
    /// </summary>
    /// <param name="format">The bearer format to use</param>
    /// <returns>The configured <see cref="ISecuritySchemeDefinitionBuilder"/></returns>
    ISecuritySchemeDefinitionBuilder WithBearerFormat(string? format);

    /// <summary>
    /// Configures the <see cref="SecuritySchemeDefinition"/> to build to use the specified OpenId Connect <see cref="Uri"/> to discover OAuth2 configuration values
    /// </summary>
    /// <param name="uri">The OpenId Connect <see cref="Uri"/> at which to discover OAuth2 configuration values</param>
    /// <returns>The configured <see cref="ISecuritySchemeDefinitionBuilder"/></returns>
    ISecuritySchemeDefinitionBuilder WithOpenIdConnectUrl(Uri? uri);

    /// <summary>
    /// Configures the <see cref="SecuritySchemeDefinition"/> to build to use the specified OAUTH flows
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to build the <see cref="OAuthFlowDefinition"/>s to use</param>
    /// <returns>The configured <see cref="ISecuritySchemeDefinitionBuilder"/></returns>
    ISecuritySchemeDefinitionBuilder WithOAuthFlows(Action<IOAuthFlowDefinitionCollectionBuilder> setup);

    /// <summary>
    /// Builds a new <see cref="SecuritySchemeDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="SecuritySchemeDefinition"/></returns>
    SecuritySchemeDefinition Build();

}
