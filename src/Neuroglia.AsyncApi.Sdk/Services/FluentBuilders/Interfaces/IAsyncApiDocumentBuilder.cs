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
        IAsyncApiDocumentBuilder TagWith(Action<ITagBuilder> setup);

        /// <summary>
        /// Adds the specified external documentation to the <see cref="AsyncApiDocument"/> to build
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> to the documentation to add</param>
        /// <param name="description">The description of the documentation to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder DocumentWith(Uri uri, string description = null);

        /// <summary>
        /// Adds the specified <see cref="Server"/> to the <see cref="AsyncApiDocument"/> to build
        /// </summary>
        /// <param name="name">The name of the <see cref="Server"/> to add</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="Server"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder UseServer(string name, Action<IServerBuilder> setup);

        /// <summary>
        /// Adds the specified <see cref="Channel"/> to the <see cref="AsyncApiDocument"/> to build
        /// </summary>
        /// <param name="name">The name of the <see cref="Channel"/> to add</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="Channel"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder UseChannel(string name, Action<IChannelBuilder> setup);

        /// <summary>
        /// Adds the specified <see cref="JSchema"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="JSchema"/> to add</param>
        /// <param name="schema">The <see cref="JSchema"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddSchema(string name, JSchema schema);

        /// <summary>
        /// Adds the specified <see cref="Message"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="Message"/> to add</param>
        /// <param name="message">The <see cref="Message"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddMessage(string name, Message message);

        /// <summary>
        /// Adds the specified <see cref="Message"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="Message"/> to add</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="Message"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddMessage(string name, Action<IMessageBuilder> setup);

        /// <summary>
        /// Adds the specified <see cref="SecurityScheme"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="SecurityScheme"/> to add</param>
        /// <param name="scheme">The <see cref="SecurityScheme"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddSecurityScheme(string name, SecurityScheme scheme);

        /// <summary>
        /// Adds the specified <see cref="Parameter"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="Parameter"/> to add</param>
        /// <param name="parameter">The <see cref="Parameter"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddParameter(string name, Parameter parameter);

        /// <summary>
        /// Adds the specified <see cref="Parameter"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="Parameter"/> to add</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="Parameter"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddParameter(string name, Action<IParameterBuilder> setup);

        /// <summary>
        /// Adds the specified <see cref="CorrelationId"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="CorrelationId"/> to add</param>
        /// <param name="correlationId">The <see cref="CorrelationId"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddCorrelationId(string name, CorrelationId correlationId);

        /// <summary>
        /// Adds the specified <see cref="OperationTrait"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="OperationTrait"/> to add</param>
        /// <param name="trait">The <see cref="OperationTrait"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddOperationTrait(string name, OperationTrait trait);

        /// <summary>
        /// Adds the specified <see cref="OperationTrait"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="OperationTrait"/> to add</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="OperationTrait"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddOperationTrait(string name, Action<IOperationTraitBuilder> setup);

        /// <summary>
        /// Adds the specified <see cref="MessageTrait"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="MessageTrait"/> to add</param>
        /// <param name="trait">The <see cref="MessageTrait"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddMessageTrait(string name, MessageTrait trait);

        /// <summary>
        /// Adds the specified <see cref="MessageTrait"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="MessageTrait"/> to add</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="MessageTrait"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddMessageTrait(string name, Action<IMessageTraitBuilder> setup);

        /// <summary>
        /// Adds the specified <see cref="ServerBindingCollection"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="ServerBindingCollection"/> to add</param>
        /// <param name="bindings">The <see cref="ServerBindingCollection"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddServerBinding(string name, ServerBindingCollection bindings);

        /// <summary>
        /// Adds the specified <see cref="ChannelBindingCollection"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="ChannelBindingCollection"/> to add</param>
        /// <param name="bindings">The <see cref="ChannelBindingCollection"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddChannelBinding(string name, ChannelBindingCollection bindings);

        /// <summary>
        /// Adds the specified <see cref="OperationBindingCollection"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="OperationBindingCollection"/> to add</param>
        /// <param name="bindings">The <see cref="OperationBindingCollection"/> to add</param>
        /// <returns>The configured <see cref="IAsyncApiDocumentBuilder"/></returns>
        IAsyncApiDocumentBuilder AddOperationBinding(string name, OperationBindingCollection bindings);

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
