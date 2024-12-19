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

namespace Neuroglia.AsyncApi.FluentBuilders.v3;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="V3AsyncApiDocument"/>s
/// </summary>
public interface IV3AsyncApiDocumentBuilder
{

    /// <summary>
    /// Configures the <see cref="V3AsyncApiDocument"/> to use the specified Async Api Specification version
    /// </summary>
    /// <param name="version">The Async Api Specification version to use</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithSpecVersion(string version);

    /// <summary>
    /// Configures the <see cref="V3AsyncApiDocument"/> to use the specified id
    /// </summary>
    /// <param name="id">The id of the Async Api document to build</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithId(string? id);

    /// <summary>
    /// Configures the <see cref="V3AsyncApiDocument"/> to use the specified API title
    /// </summary>
    /// <param name="title">The title of the Async Api document to build</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithTitle(string title);

    /// <summary>
    /// Configures the <see cref="V3AsyncApiDocument"/> to use the specified API version
    /// </summary>
    /// <param name="version">The version of the Async Api document to build</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithVersion(string version);

    /// <summary>
    /// Configures the <see cref="V3AsyncApiDocument"/> to use the specified API description
    /// </summary>
    /// <param name="description">The description of the Async Api document to build</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithDescription(string? description);

    /// <summary>
    /// Configures the <see cref="V3AsyncApiDocument"/> to use the specified <see cref="Uri"/> for the API's terms of service
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> of the API's terms of service</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithTermsOfService(Uri? uri);

    /// <summary>
    /// Configures the <see cref="V3AsyncApiDocument"/> to use the specified contact for the API
    /// </summary>
    /// <param name="name">The contact name</param>
    /// <param name="uri">The contact <see cref="Uri"/></param>
    /// <param name="email">The contact email</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithContact(string name, Uri? uri = null, string? email = null);

    /// <summary>
    /// Configures the <see cref="V3AsyncApiDocument"/> to use the specified license
    /// </summary>
    /// <param name="name">The name of the license to use</param>
    /// <param name="uri">The license's <see cref="Uri"/></param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithLicense(string name, Uri? uri = null);

    /// <summary>
    /// Configures the <see cref="V3AsyncApiDocument"/> to use the specified default content type
    /// </summary>
    /// <param name="contentType">The content type to use by default</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithDefaultContentType(string contentType);

    /// <summary>
    /// Marks the <see cref="V3AsyncApiDocument"/> to build with the specified tag
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the tag to use</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithTag(Action<IV3TagDefinitionBuilder> setup);

    /// <summary>
    /// Marks the <see cref="V3AsyncApiDocument"/> to use the specified external documentation
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the external documentation to use</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithExternalDocumentation(Action<IV3ExternalDocumentationDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="V3ServerDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="V3ServerDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V3ServerDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithServer(string name, Action<IV3ServerDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="V3ChannelDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="V3ChannelDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V3ChannelDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithChannel(string name, Action<IV3ChannelDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="V3OperationDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="V3OperationDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V3OperationDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithOperation(string name, Action<IV3OperationDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V3SchemaDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V3SchemaDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V3SchemaDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithSchemaComponent(string name, Action<IV3SchemaDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V3ServerDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V3ServerDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V3ServerDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithServerComponent(string name, Action<IV3ServerDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V3ChannelDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V3ChannelDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V3ChannelDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithChannelComponent(string name, Action<IV3ChannelDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V3OperationDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V3OperationDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V3OperationDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithOperationComponent(string name, Action<IV3OperationDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V3MessageDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V3MessageDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V3MessageDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithMessageComponent(string name, Action<IV3MessageDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V3SecuritySchemeDefinition"/>
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V3SecuritySchemeDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V3SecuritySchemeDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithSecuritySchemeComponent(string name, Action<IV3SecuritySchemeDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V3ServerVariableDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V3MessageDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V3ServerVariableDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithServerVariableComponent(string name, Action<IV3ServerVariableDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V3ParameterDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V3ParameterDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V3ParameterDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithParameterComponent(string name, Action<IV3ParameterDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V2CorrelationIdDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V2CorrelationIdDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V2CorrelationIdDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithCorrelationIdComponent(string name, Action<IV3CorrelationIdDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V3OperationReplyDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V3OperationReplyDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V3OperationReplyDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithReplyComponent(string name, Action<IV3OperationReplyDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V3OperationReplyAddressDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V3OperationReplyAddressDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V3OperationReplyAddressDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithReplyAddressComponent(string name, Action<IV3OperationReplyAddressDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V3ExternalDocumentationDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V3ExternalDocumentationDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V3ExternalDocumentationDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithExternalDocumentationComponent(string name, Action<IV3ExternalDocumentationDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V2TagDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V2TagDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V2TagDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithTagComponent(string name, Action<IV3TagDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V3OperationTraitDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V3OperationTraitDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V3OperationTraitDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithOperationTraitComponent(string name, Action<IV3OperationTraitDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="V3MessageTraitDefinition"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="V3MessageTraitDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the reusable <see cref="V3MessageTraitDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithMessageTraitComponent(string name, Action<IV3MessageTraitDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="ServerBindingDefinitionCollection"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="ServerBindingDefinitionCollection"/> to add</param>
    /// <param name="bindings">The reusable <see cref="ServerBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithServerBindingsComponent(string name, ServerBindingDefinitionCollection bindings);

    /// <summary>
    /// Adds the specified reusable <see cref="ServerBindingDefinitionCollection"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="ServerBindingDefinitionCollection"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="ServerBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithServerBindingsComponent(string name, Action<IServerBindingDefinitionCollectionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="ChannelBindingDefinitionCollection"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="ChannelBindingDefinitionCollection"/> to add</param>
    /// <param name="bindings">The reusable <see cref="ChannelBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithChannelBindingsComponent(string name, ChannelBindingDefinitionCollection bindings);

    /// <summary>
    /// Adds the specified reusable <see cref="ChannelBindingDefinitionCollection"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="ChannelBindingDefinitionCollection"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="ChannelBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithChannelBindingsComponent(string name, Action<IChannelBindingDefinitionCollectionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="OperationBindingDefinitionCollection"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="OperationBindingDefinitionCollection"/> to add</param>
    /// <param name="bindings">The reusable <see cref="OperationBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithOperationBindingsComponent(string name, OperationBindingDefinitionCollection bindings);

    /// <summary>
    /// Adds the specified reusable <see cref="OperationBindingDefinitionCollection"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="OperationBindingDefinitionCollection"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="OperationBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithOperationBindingsComponent(string name, Action<IOperationBindingDefinitionCollectionBuilder> setup);

    /// <summary>
    /// Adds the specified reusable <see cref="MessageBindingDefinitionCollection"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="MessageBindingDefinitionCollection"/> to add</param>
    /// <param name="bindings">The reusable <see cref="MessageBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithMessageBindingsComponent(string name, MessageBindingDefinitionCollection bindings);

    /// <summary>
    /// Adds the specified reusable <see cref="MessageBindingDefinitionCollection"/> to the <see cref="V3AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the reusable <see cref="MessageBindingDefinitionCollection"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="MessageBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IV3AsyncApiDocumentBuilder"/></returns>
    IV3AsyncApiDocumentBuilder WithMessageBindingsComponent(string name, Action<IMessageBindingDefinitionCollectionBuilder> setup);

    /// <summary>
    /// Builds the configured <see cref="V3AsyncApiDocument"/>
    /// </summary>
    /// <returns>A new <see cref="V3AsyncApiDocument"/></returns>
    V3AsyncApiDocument Build();

}
