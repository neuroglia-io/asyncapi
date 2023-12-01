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
using Neuroglia.AsyncApi.v2.Bindings;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="AsyncApiDocument"/>s
/// </summary>
public interface IAsyncApiDocumentBuilder
{

    /// <summary>
    /// Configures the <see cref="AsyncApiDocument"/> to use the specified Async Api Specification version
    /// </summary>
    /// <param name="version">The Async Api Specification version to use</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithSpecVersion(string version);

    /// <summary>
    /// Configures the <see cref="AsyncApiDocument"/> to use the specified id
    /// </summary>
    /// <param name="id">The id of the Async Api document to build</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithId(string id);

    /// <summary>
    /// Configures the <see cref="AsyncApiDocument"/> to use the specified API title
    /// </summary>
    /// <param name="title">The title of the Async Api document to build</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithTitle(string title);

    /// <summary>
    /// Configures the <see cref="AsyncApiDocument"/> to use the specified API version
    /// </summary>
    /// <param name="version">The version of the Async Api document to build</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithVersion(string version);

    /// <summary>
    /// Configures the <see cref="AsyncApiDocument"/> to use the specified API description
    /// </summary>
    /// <param name="description">The description of the Async Api document to build</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithDescription(string description);

    /// <summary>
    /// Configures the <see cref="AsyncApiDocument"/> to use the specified <see cref="Uri"/> for the API's terms of service
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> of the API's terms of service</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithTermsOfService(Uri uri);

    /// <summary>
    /// Configures the <see cref="AsyncApiDocument"/> to use the specified contact for the API
    /// </summary>
    /// <param name="name">The contact name</param>
    /// <param name="uri">The contact <see cref="Uri"/></param>
    /// <param name="email">The contact email</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithContact(string name, Uri? uri = null, string? email = null);

    /// <summary>
    /// Configures the <see cref="AsyncApiDocument"/> to use the specified license
    /// </summary>
    /// <param name="name">The name of the license to use</param>
    /// <param name="uri">The license's <see cref="Uri"/></param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithLicense(string name, Uri? uri = null);

    /// <summary>
    /// Configures the <see cref="AsyncApiDocument"/> to use the specified license
    /// </summary>
    /// <param name="contentType">The content type to use by default</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithDefaultContentType(string contentType);

    /// <summary>
    /// Marks the <see cref="AsyncApiDocument"/> to build with the specified tag
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the tag to use</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithTag(Action<ITagDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified external documentation to the <see cref="AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> to the documentation to add</param>
    /// <param name="description">The description of the documentation to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithExternalDocumentation(Uri uri, string? description = null);

    /// <summary>
    /// Adds the specified <see cref="ServerDefinition"/> to the <see cref="AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="ServerDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="ServerDefinition"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithServer(string name, Action<IServerDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="ChannelDefinition"/> to the <see cref="AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="ChannelDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="ChannelDefinition"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithChannel(string name, Action<IChannelDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="SecuritySchemeDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="SecuritySchemeDefinition"/> to add</param>
    /// <param name="scheme">The <see cref="SecuritySchemeDefinition"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithSecurityScheme(string name, SecuritySchemeDefinition scheme);

    /// <summary>
    /// Adds the specified <see cref="SecuritySchemeDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="SecuritySchemeDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="SecuritySchemeDefinition"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithSecurityScheme(string name, Action<ISecuritySchemeDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="JsonSchema"/> to add</param>
    /// <param name="schema">The <see cref="JsonSchema"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithSchemaComponent(string name, JsonSchema schema);

    /// <summary>
    /// Adds the specified <see cref="MessageDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="MessageDefinition"/> to add</param>
    /// <param name="message">The <see cref="MessageDefinition"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithMessageComponent(string name, MessageDefinition message);

    /// <summary>
    /// Adds the specified <see cref="MessageDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="MessageDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="MessageDefinition"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithMessageComponent(string name, Action<IMessageDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="ParameterDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="ParameterDefinition"/> to add</param>
    /// <param name="parameter">The <see cref="ParameterDefinition"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithParameterComponent(string name, ParameterDefinition parameter);

    /// <summary>
    /// Adds the specified <see cref="ParameterDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="ParameterDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="ParameterDefinition"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithParameterComponent(string name, Action<IParameterDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="CorrelationIdDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="CorrelationIdDefinition"/> to add</param>
    /// <param name="correlationId">The <see cref="CorrelationIdDefinition"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithCorrelationIdComponent(string name, CorrelationIdDefinition correlationId);

    /// <summary>
    /// Adds the specified <see cref="OperationTraitDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="OperationTraitDefinition"/> to add</param>
    /// <param name="trait">The <see cref="OperationTraitDefinition"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithOperationTraitComponent(string name, OperationTraitDefinition trait);

    /// <summary>
    /// Adds the specified <see cref="OperationTraitDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="OperationTraitDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="OperationTraitDefinition"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithOperationTraitComponent(string name, Action<IOperationTraitBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="MessageTraitDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="MessageTraitDefinition"/> to add</param>
    /// <param name="trait">The <see cref="MessageTraitDefinition"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithMessageTraitComponent(string name, MessageTraitDefinition trait);

    /// <summary>
    /// Adds the specified <see cref="MessageTraitDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="MessageTraitDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="MessageTraitDefinition"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithMessageTraitComponent(string name, Action<IMessageTraitBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="ServerBindingDefinitionCollection"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="ServerBindingDefinitionCollection"/> to add</param>
    /// <param name="bindings">The <see cref="ServerBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithServerBindingComponent(string name, ServerBindingDefinitionCollection bindings);

    /// <summary>
    /// Adds the specified <see cref="ChannelBindingDefinitionCollection"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="ChannelBindingDefinitionCollection"/> to add</param>
    /// <param name="bindings">The <see cref="ChannelBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithChannelBindingComponent(string name, ChannelBindingDefinitionCollection bindings);

    /// <summary>
    /// Adds the specified <see cref="OperationBindingDefinitionCollection"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="OperationBindingDefinitionCollection"/> to add</param>
    /// <param name="bindings">The <see cref="OperationBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithOperationBindingComponent(string name, OperationBindingDefinitionCollection bindings);

    /// <summary>
    /// Adds the specified <see cref="MessageBindingCollection"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="MessageBindingCollection"/> to add</param>
    /// <param name="bindings">The <see cref="MessageBindingCollection"/> to add</param>
    /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
    IAsyncApiDocumentBuilder WithMessageBindingComponent(string name, MessageBindingCollection bindings);

    /// <summary>
    /// Builds a new <see cref="AsyncApiDocument"/>
    /// </summary>
    /// <returns>A new <see cref="AsyncApiDocument"/></returns>
    AsyncApiDocument Build();

}
