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
/// Defines the fundamentals of a service used to build <see cref="V2AsyncApiDocument"/>s
/// </summary>
public interface IV2AsyncApiDocumentBuilder
{

    /// <summary>
    /// Configures the <see cref="V2AsyncApiDocument"/> to use the specified Async Api Specification version
    /// </summary>
    /// <param name="version">The Async Api Specification version to use</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithSpecVersion(string version);

    /// <summary>
    /// Configures the <see cref="V2AsyncApiDocument"/> to use the specified id
    /// </summary>
    /// <param name="id">The id of the Async Api document to build</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithId(string id);

    /// <summary>
    /// Configures the <see cref="V2AsyncApiDocument"/> to use the specified API title
    /// </summary>
    /// <param name="title">The title of the Async Api document to build</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithTitle(string title);

    /// <summary>
    /// Configures the <see cref="V2AsyncApiDocument"/> to use the specified API version
    /// </summary>
    /// <param name="version">The version of the Async Api document to build</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithVersion(string version);

    /// <summary>
    /// Configures the <see cref="V2AsyncApiDocument"/> to use the specified API description
    /// </summary>
    /// <param name="description">The description of the Async Api document to build</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithDescription(string? description);

    /// <summary>
    /// Configures the <see cref="V2AsyncApiDocument"/> to use the specified <see cref="Uri"/> for the API's terms of service
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> of the API's terms of service</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithTermsOfService(Uri uri);

    /// <summary>
    /// Configures the <see cref="V2AsyncApiDocument"/> to use the specified contact for the API
    /// </summary>
    /// <param name="name">The contact name</param>
    /// <param name="uri">The contact <see cref="Uri"/></param>
    /// <param name="email">The contact email</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithContact(string name, Uri? uri = null, string? email = null);

    /// <summary>
    /// Configures the <see cref="V2AsyncApiDocument"/> to use the specified license
    /// </summary>
    /// <param name="name">The name of the license to use</param>
    /// <param name="uri">The license's <see cref="Uri"/></param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithLicense(string name, Uri? uri = null);

    /// <summary>
    /// Configures the <see cref="V2AsyncApiDocument"/> to use the specified default content type
    /// </summary>
    /// <param name="contentType">The content type to use by default</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithDefaultContentType(string contentType);

    /// <summary>
    /// Marks the <see cref="V2AsyncApiDocument"/> to build with the specified tag
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the tag to use</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithTag(Action<ITagDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified external documentation to the <see cref="V2AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="uri">The <see cref="Uri"/> to the documentation to add</param>
    /// <param name="description">The description of the documentation to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithExternalDocumentation(Uri uri, string? description = null);

    /// <summary>
    /// Adds the specified <see cref="V2ServerDefinition"/> to the <see cref="V2AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="V2ServerDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V2ServerDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithServer(string name, Action<IV2ServerDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="V2ChannelDefinition"/> to the <see cref="V2AsyncApiDocument"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="V2ChannelDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V2ChannelDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithChannel(string name, Action<IV2ChannelDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="V2SecuritySchemeDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="V2SecuritySchemeDefinition"/> to add</param>
    /// <param name="scheme">The <see cref="V2SecuritySchemeDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithSecurityScheme(string name, V2SecuritySchemeDefinition scheme);

    /// <summary>
    /// Adds the specified <see cref="V2SecuritySchemeDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="V2SecuritySchemeDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V2SecuritySchemeDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithSecurityScheme(string name, Action<IV2SecuritySchemeDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="JsonSchema"/> to add</param>
    /// <param name="schema">The <see cref="JsonSchema"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithSchemaComponent(string name, JsonSchema schema);

    /// <summary>
    /// Adds the specified <see cref="V2MessageDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="V2MessageDefinition"/> to add</param>
    /// <param name="message">The <see cref="V2MessageDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithMessageComponent(string name, V2MessageDefinition message);

    /// <summary>
    /// Adds the specified <see cref="V2MessageDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="V2MessageDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V2MessageDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithMessageComponent(string name, Action<IV2MessageDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="V2ParameterDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="V2ParameterDefinition"/> to add</param>
    /// <param name="parameter">The <see cref="V2ParameterDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithParameterComponent(string name, V2ParameterDefinition parameter);

    /// <summary>
    /// Adds the specified <see cref="V2ParameterDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="V2ParameterDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V2ParameterDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithParameterComponent(string name, Action<IV2ParameterDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="CorrelationIdDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="CorrelationIdDefinition"/> to add</param>
    /// <param name="correlationId">The <see cref="CorrelationIdDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithCorrelationIdComponent(string name, CorrelationIdDefinition correlationId);

    /// <summary>
    /// Adds the specified <see cref="V2OperationTraitDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="V2OperationTraitDefinition"/> to add</param>
    /// <param name="trait">The <see cref="V2OperationTraitDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithOperationTraitComponent(string name, V2OperationTraitDefinition trait);

    /// <summary>
    /// Adds the specified <see cref="V2OperationTraitDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="V2OperationTraitDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V2OperationTraitDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithOperationTraitComponent(string name, Action<IV2OperationTraitDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="V2MessageTraitDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="V2MessageTraitDefinition"/> to add</param>
    /// <param name="trait">The <see cref="V2MessageTraitDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithMessageTraitComponent(string name, V2MessageTraitDefinition trait);

    /// <summary>
    /// Adds the specified <see cref="V2MessageTraitDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="V2MessageTraitDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V2MessageTraitDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithMessageTraitComponent(string name, Action<IV2MessageTraitDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="ServerBindingDefinitionCollection"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="ServerBindingDefinitionCollection"/> to add</param>
    /// <param name="bindings">The <see cref="ServerBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithServerBindingComponent(string name, ServerBindingDefinitionCollection bindings);

    /// <summary>
    /// Adds the specified <see cref="ChannelBindingDefinitionCollection"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="ChannelBindingDefinitionCollection"/> to add</param>
    /// <param name="bindings">The <see cref="ChannelBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithChannelBindingComponent(string name, ChannelBindingDefinitionCollection bindings);

    /// <summary>
    /// Adds the specified <see cref="OperationBindingDefinitionCollection"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="OperationBindingDefinitionCollection"/> to add</param>
    /// <param name="bindings">The <see cref="OperationBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithOperationBindingComponent(string name, OperationBindingDefinitionCollection bindings);

    /// <summary>
    /// Adds the specified <see cref="MessageBindingDefinitionCollection"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="MessageBindingDefinitionCollection"/> to add</param>
    /// <param name="bindings">The <see cref="MessageBindingDefinitionCollection"/> to add</param>
    /// <returns>The configured <see cref="IV2AsyncApiDocumentBuilder"/></returns>
    IV2AsyncApiDocumentBuilder WithMessageBindingComponent(string name, MessageBindingDefinitionCollection bindings);

    /// <summary>
    /// Builds a new <see cref="V2AsyncApiDocument"/>
    /// </summary>
    /// <returns>A new <see cref="V2AsyncApiDocument"/></returns>
    V2AsyncApiDocument Build();

}
