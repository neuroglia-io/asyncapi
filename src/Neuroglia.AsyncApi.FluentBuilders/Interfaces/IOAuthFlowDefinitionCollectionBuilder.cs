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
/// Defines the fundamentals of a service used to build <see cref="OAuthFlowDefinitionCollection"/>s
/// </summary>
public interface IOAuthFlowDefinitionCollectionBuilder
{

    /// <summary>
    /// Configures the <see cref="OAuthFlowDefinitionCollection"/> to use the specified 'implicit' OAUTH flow
    /// </summary>
    /// <param name="flow">The <see cref="OAuthFlowDefinition"/> use to configure the 'implicit' for</param>
    /// <returns>The configured <see cref="IOAuthFlowDefinitionCollectionBuilder"/></returns>
    IOAuthFlowDefinitionCollectionBuilder WithImplicitFlow(OAuthFlowDefinition? flow);

    /// <summary>
    /// Configures the <see cref="OAuthFlowDefinitionCollection"/> to use the specified 'implicit' OAUTH flow
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="OAuthFlowDefinition"/> use</param>
    /// <returns>The configured <see cref="IOAuthFlowDefinitionCollectionBuilder"/></returns>
    IOAuthFlowDefinitionCollectionBuilder WithImplicitFlow(Action<IOAuthFlowDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="OAuthFlowDefinitionCollection"/> to use the specified 'password' OAUTH flow
    /// </summary>
    /// <param name="flow">The <see cref="OAuthFlowDefinition"/> use to configure the 'password' for</param>
    /// <returns>The configured <see cref="IOAuthFlowDefinitionCollectionBuilder"/></returns>
    IOAuthFlowDefinitionCollectionBuilder WithPasswordFlow(OAuthFlowDefinition? flow);

    /// <summary>
    /// Configures the <see cref="OAuthFlowDefinitionCollection"/> to use the specified 'password' OAUTH flow
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="OAuthFlowDefinition"/> use</param>
    /// <returns>The configured <see cref="IOAuthFlowDefinitionCollectionBuilder"/></returns>
    IOAuthFlowDefinitionCollectionBuilder WithPasswordFlow(Action<IOAuthFlowDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="OAuthFlowDefinitionCollection"/> to use the specified 'client_credentials' OAUTH flow
    /// </summary>
    /// <param name="flow">The <see cref="OAuthFlowDefinition"/> use to configure the 'client_credentials' for</param>
    /// <returns>The configured <see cref="IOAuthFlowDefinitionCollectionBuilder"/></returns>
    IOAuthFlowDefinitionCollectionBuilder WithClientCredentialsFlow(OAuthFlowDefinition? flow);

    /// <summary>
    /// Configures the <see cref="OAuthFlowDefinitionCollection"/> to use the specified 'client_credentials' OAUTH flow
    /// </summary>
    /// <param name="setup">The <see cref="OAuthFlowDefinition"/> use to configure the 'client_credentials' for</param>
    /// <returns>The configured <see cref="IOAuthFlowDefinitionCollectionBuilder"/></returns>
    IOAuthFlowDefinitionCollectionBuilder WithClientCredentialsFlow(Action<IOAuthFlowDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="OAuthFlowDefinitionCollection"/> to use the specified 'authorization_code' OAUTH flow
    /// </summary>
    /// <param name="flow">The <see cref="OAuthFlowDefinition"/> use</param>
    /// <returns>The configured <see cref="IOAuthFlowDefinitionCollectionBuilder"/></returns>
    IOAuthFlowDefinitionCollectionBuilder WithAuthorizationCodeFlow(OAuthFlowDefinition? flow);

    /// <summary>
    /// Configures the <see cref="OAuthFlowDefinitionCollection"/> to use the specified 'authorization_code' OAUTH flow
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="OAuthFlowDefinition"/> use</param>
    /// <returns>The configured <see cref="IOAuthFlowDefinitionCollectionBuilder"/></returns>
    IOAuthFlowDefinitionCollectionBuilder WithAuthorizationCodeFlow(Action<IOAuthFlowDefinitionBuilder> setup);

    /// <summary>
    /// Builds the configured <see cref="OAuthFlowDefinitionCollection"/>
    /// </summary>
    /// <returns>The configured <see cref="OAuthFlowDefinitionCollection"/></returns>
    OAuthFlowDefinitionCollection Build();

}
