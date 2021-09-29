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
using Neuroglia.AsyncApi.Models;
using Newtonsoft.Json.Schema;
using System;

namespace Neuroglia.AsyncApi.Services.FluentBuilders
{

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
        IAsyncApiDocumentBuilder UseAsyncApi(string version);

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
        IAsyncApiDocumentBuilder WithContact(string name, Uri uri, string email);

        /// <summary>
        /// Configures the <see cref="AsyncApiDocument"/> to use the specified license
        /// </summary>
        /// <param name="name">The name of the license to use</param>
        /// <param name="uri">The license's <see cref="Uri"/></param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder WithLicense(string name, Uri uri);

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
        IAsyncApiDocumentBuilder TagWith(Action<ITagDefinitionBuilder> setup);

        /// <summary>
        /// Adds the specified external documentation to the <see cref="AsyncApiDocument"/> to build
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> to the documentation to add</param>
        /// <param name="description">The description of the documentation to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder DocumentWith(Uri uri, string description = null);

        /// <summary>
        /// Adds the specified <see cref="ServerDefinition"/> to the <see cref="AsyncApiDocument"/> to build
        /// </summary>
        /// <param name="name">The name of the <see cref="ServerDefinition"/> to add</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="ServerDefinition"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder UseServer(string name, Action<IServerDefinitionBuilder> setup);

        /// <summary>
        /// Adds the specified <see cref="ChannelDefinition"/> to the <see cref="AsyncApiDocument"/> to build
        /// </summary>
        /// <param name="name">The name of the <see cref="ChannelDefinition"/> to add</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="ChannelDefinition"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder UseChannel(string name, Action<IChannelDefinitionBuilder> setup);

        /// <summary>
        /// Adds the specified <see cref="JSchema"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="JSchema"/> to add</param>
        /// <param name="schema">The <see cref="JSchema"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddSchema(string name, JSchema schema);

        /// <summary>
        /// Adds the specified <see cref="MessageDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="MessageDefinition"/> to add</param>
        /// <param name="message">The <see cref="MessageDefinition"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddMessage(string name, MessageDefinition message);

        /// <summary>
        /// Adds the specified <see cref="MessageDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="MessageDefinition"/> to add</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="MessageDefinition"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddMessage(string name, Action<IMessageDefinitionBuilder> setup);

        /// <summary>
        /// Adds the specified <see cref="SecuritySchemeDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="SecuritySchemeDefinition"/> to add</param>
        /// <param name="scheme">The <see cref="SecuritySchemeDefinition"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddSecurityScheme(string name, SecuritySchemeDefinition scheme);

        /// <summary>
        /// Adds the specified <see cref="ParameterDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="ParameterDefinition"/> to add</param>
        /// <param name="parameter">The <see cref="ParameterDefinition"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddParameter(string name, ParameterDefinition parameter);

        /// <summary>
        /// Adds the specified <see cref="ParameterDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="ParameterDefinition"/> to add</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="ParameterDefinition"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddParameter(string name, Action<IParameterDefinitionBuilder> setup);

        /// <summary>
        /// Adds the specified <see cref="CorrelationIdDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="CorrelationIdDefinition"/> to add</param>
        /// <param name="correlationId">The <see cref="CorrelationIdDefinition"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddCorrelationId(string name, CorrelationIdDefinition correlationId);

        /// <summary>
        /// Adds the specified <see cref="OperationTraitDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="OperationTraitDefinition"/> to add</param>
        /// <param name="trait">The <see cref="OperationTraitDefinition"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddOperationTrait(string name, OperationTraitDefinition trait);

        /// <summary>
        /// Adds the specified <see cref="OperationTraitDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="OperationTraitDefinition"/> to add</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="OperationTraitDefinition"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddOperationTrait(string name, Action<IOperationTraitBuilder> setup);

        /// <summary>
        /// Adds the specified <see cref="MessageTraitDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="MessageTraitDefinition"/> to add</param>
        /// <param name="trait">The <see cref="MessageTraitDefinition"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddMessageTrait(string name, MessageTraitDefinition trait);

        /// <summary>
        /// Adds the specified <see cref="MessageTraitDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="MessageTraitDefinition"/> to add</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="MessageTraitDefinition"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddMessageTrait(string name, Action<IMessageTraitBuilder> setup);

        /// <summary>
        /// Adds the specified <see cref="ServerBindingDefinitionCollection"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="ServerBindingDefinitionCollection"/> to add</param>
        /// <param name="bindings">The <see cref="ServerBindingDefinitionCollection"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddServerBinding(string name, ServerBindingDefinitionCollection bindings);

        /// <summary>
        /// Adds the specified <see cref="ChannelBindingDefinitionCollection"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="ChannelBindingDefinitionCollection"/> to add</param>
        /// <param name="bindings">The <see cref="ChannelBindingDefinitionCollection"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddChannelBinding(string name, ChannelBindingDefinitionCollection bindings);

        /// <summary>
        /// Adds the specified <see cref="OperationBindingDefinitionCollection"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="OperationBindingDefinitionCollection"/> to add</param>
        /// <param name="bindings">The <see cref="OperationBindingDefinitionCollection"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddOperationBinding(string name, OperationBindingDefinitionCollection bindings);

        /// <summary>
        /// Adds the specified <see cref="MessageBindingCollection"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="MessageBindingCollection"/> to add</param>
        /// <param name="bindings">The <see cref="MessageBindingCollection"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddMessageBinding(string name, MessageBindingCollection bindings);

        /// <summary>
        /// Builds a new <see cref="AsyncApiDocument"/>
        /// </summary>
        /// <returns>A new <see cref="AsyncApiDocument"/></returns>
        AsyncApiDocument Build();

    }

}
