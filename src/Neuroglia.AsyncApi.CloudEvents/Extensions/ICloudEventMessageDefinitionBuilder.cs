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

using Json.Schema;
using Neuroglia.AsyncApi.v2;
using Neuroglia.Eventing.CloudEvents;

namespace Neuroglia.AsyncApi.CloudEvents;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="CloudEvent"/> <see cref="V2MessageDefinition"/>s
/// </summary>
public interface ICloudEventMessageDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="V2MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> spec version
    /// </summary>
    /// <param name="specVersion">The <see cref="CloudEvent"/> spec version to use</param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithSpecVersion(string specVersion);

    /// <summary>
    /// Configures the <see cref="V2MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> source
    /// </summary>
    /// <param name="source">The <see cref="CloudEvent"/>'s source <see cref="Uri"/></param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithSource(Uri source);

    /// <summary>
    /// Configures the <see cref="V2MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> type
    /// </summary>
    /// <param name="type">The <see cref="CloudEvent"/>'s type</param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithType(string type);

    /// <summary>
    /// Configures the <see cref="V2MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> subject
    /// </summary>
    /// <param name="subject">The <see cref="CloudEvent"/>'s subject</param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithSubject(string? subject);

    /// <summary>
    /// Configures the <see cref="V2MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> data content type
    /// </summary>
    /// <param name="contentType">The <see cref="CloudEvent"/>'s data content type</param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithDataContentType(string? contentType);

    /// <summary>
    /// Configures the <see cref="V2MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> data <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="schemaUri">An <see cref="Uri"/> that references the <see cref="CloudEvent"/>'s data <see cref="JsonSchema"/></param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithDataSchemaUri(Uri? schemaUri);

    /// <summary>
    /// Configures the <see cref="V2MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> data <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="schema">The <see cref="CloudEvent"/>'s data <see cref="JsonSchema"/> to use</param>
    /// <param name="schemaUri">An <see cref="Uri"/> that references the <see cref="CloudEvent"/>'s data <see cref="JsonSchema"/></param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithDataSchema(JsonSchema schema, Uri? schemaUri = null);

    /// <summary>
    /// Configures the <see cref="V2MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> data type
    /// </summary>
    /// <param name="type">The type of data transported by the <see cref="CloudEvent"/> to build a new <see cref="V2MessageDefinition"/> for</param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithDataOfType(Type type);

    /// <summary>
    /// Configures the <see cref="V2MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> data type
    /// </summary>
    /// <typeparam name="TData">The type of data transported by the <see cref="CloudEvent"/> to build a new <see cref="V2MessageDefinition"/> for</typeparam>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithDataOfType<TData>();

    /// <summary>
    /// Configures the <see cref="V2MessageDefinition"/> to build to use the specified <see cref="CloudEvent"/> extension attribute
    /// </summary>
    /// <param name="name">The name of the <see cref="CloudEvent"/> extension attribute to add</param>
    /// <param name="value">The value of the <see cref="CloudEvent"/> extensions attribute to add</param>
    /// <returns>The configured <see cref="ICloudEventMessageDefinitionBuilder"/></returns>
    ICloudEventMessageDefinitionBuilder WithExtensionAttribute(string name, string value);

    /// <summary>
    /// Builds the <see cref="V2MessageDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="V2MessageDefinition"/></returns>
    V2MessageDefinition Build();

}
