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
/// Defines the fundamentals of a service used to build <see cref="V3ChannelDefinition"/>s
/// </summary>
public interface IV3ChannelDefinitionBuilder
    : IReferenceableComponentDefinitionBuilder<V3ChannelDefinition>
{

    /// <summary>
    /// Configures the <see cref="V3ChannelDefinition"/> to use the specified address
    /// </summary>
    /// <param name="address">The address to use</param>
    /// <returns>The configured <see cref="IV3ChannelDefinitionBuilder"/></returns>
    IV3ChannelDefinitionBuilder WithAddress(string? address);

    /// <summary>
    /// Configures the <see cref="V3ChannelDefinition"/> to use the specified <see cref="V3MessageDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="V3MessageDefinition"/> to use</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the <see cref="V3MessageDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV3ChannelDefinitionBuilder"/></returns>
    IV3ChannelDefinitionBuilder WithMessage(string name, Action<IV3MessageDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="V3ChannelDefinition"/> to use the specified title
    /// </summary>
    /// <param name="title">The title to use</param>
    /// <returns>The configured <see cref="IV3ChannelDefinitionBuilder"/></returns>
    IV3ChannelDefinitionBuilder WithTitle(string? title);

    /// <summary>
    /// Configures the <see cref="V3ChannelDefinition"/> to use the specified summary
    /// </summary>
    /// <param name="summary">The summary to use</param>
    /// <returns>The configured <see cref="IV3ChannelDefinitionBuilder"/></returns>
    IV3ChannelDefinitionBuilder WithSummary(string? summary);

    /// <summary>
    /// Configures the <see cref="V3ChannelDefinition"/> to use the specified description
    /// </summary>
    /// <param name="description">The description to use</param>
    /// <returns>The configured <see cref="IV3ChannelDefinitionBuilder"/></returns>
    IV3ChannelDefinitionBuilder WithDescription(string? description);

    /// <summary>
    /// Configures the <see cref="V3ChannelDefinition"/> to use the specified <see cref="V3ServerDefinition"/>
    /// </summary>
    /// <param name="server">A reference to the <see cref="V3ServerDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV3ChannelDefinitionBuilder"/></returns>
    IV3ChannelDefinitionBuilder WithServer(string server);

    /// <summary>
    /// Configures the <see cref="V3ChannelDefinition"/> to use the specified <see cref="V3ParameterDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="V3ParameterDefinition"/> to use</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the <see cref="V3ParameterDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV3ChannelDefinitionBuilder"/></returns>
    IV3ChannelDefinitionBuilder WithParameter(string name, Action<IV3ParameterDefinitionBuilder> setup);

    /// <summary>
    /// Marks the <see cref="V3ChannelDefinition"/> to build with the specified tag
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the tag to use</param>
    /// <returns>The configured <see cref="IV3ChannelDefinitionBuilder"/></returns>
    IV3ChannelDefinitionBuilder WithTag(Action<IV3TagDefinitionBuilder> setup);

    /// <summary>
    /// Marks the <see cref="V3ChannelDefinition"/> to use the specified external documentation
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the external documentation to use</param>
    /// <returns>The configured <see cref="IV3ChannelDefinitionBuilder"/></returns>
    IV3ChannelDefinitionBuilder WithExternalDocumentation(Action<IV3ExternalDocumentationDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="V3ChannelDefinition"/> to use the specified binding
    /// </summary>
    /// <param name="binding">The <see cref="IChannelBindingDefinition"/> to add</param>
    /// <returns>The configured <see cref="IV3ChannelDefinitionBuilder"/></returns>
    IV3ChannelDefinitionBuilder WithBinding(IChannelBindingDefinition binding);

    /// <summary>
    /// Configures the <see cref="V3ChannelDefinition"/> to use the specified <see cref="ChannelBindingDefinitionCollection"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="ChannelBindingDefinitionCollection"/> to use</param>
    /// <returns>The configured <see cref="IV3ChannelDefinitionBuilder"/></returns>
    IV3ChannelDefinitionBuilder WithBindings(Action<IChannelBindingDefinitionCollectionBuilder> setup);

    /// <summary>
    /// Builds the configured <see cref="V3ChannelDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="V3ChannelDefinition"/></returns>
    V3ChannelDefinition Build();

}
