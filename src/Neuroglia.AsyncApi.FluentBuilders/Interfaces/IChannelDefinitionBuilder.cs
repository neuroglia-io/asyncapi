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
/// Defines the fundamentals of a service used to build <see cref="ChannelDefinition"/>s
/// </summary>
public interface IChannelDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="ChannelDefinition"/> to build to use the specified description
    /// </summary>
    /// <param name="description">The <see cref="ChannelDefinition"/>'s description</param>
    /// <returns>The configured <see cref="IChannelDefinitionBuilder"/></returns>
    IChannelDefinitionBuilder WithDescription(string? description);

    /// <summary>
    /// Adds a new <see cref="ParameterDefinition"/> to the <see cref="ChannelDefinition"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="ParameterDefinition"/> to add</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="ParameterDefinition"/> to add</param>
    /// <returns>The configured <see cref="IChannelDefinitionBuilder"/></returns>
    IChannelDefinitionBuilder WithParameter(string name, Action<IParameterDefinitionBuilder> setup);

    /// <summary>
    /// Adds the specified <see cref="IChannelBindingDefinition"/> to the <see cref="ChannelDefinition"/> to build
    /// </summary>
    /// <param name="binding">The <see cref="IChannelBindingDefinition"/> to add</param>
    /// <returns>The configured <see cref="IChannelDefinitionBuilder"/></returns>
    IChannelDefinitionBuilder WithBinding(IChannelBindingDefinition binding);

    /// <summary>
    /// Defines and configures an operation of the <see cref="ChannelDefinition"/> to build
    /// </summary>
    /// <param name="type">The <see cref="OperationDefinition"/>'s type</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the Subscribe <see cref="OperationDefinition"/></param>
    /// <returns>The configured <see cref="IChannelDefinitionBuilder"/></returns>
    IChannelDefinitionBuilder WithOperation(OperationType type, Action<IOperationDefinitionBuilder> setup);

    /// <summary>
    /// Defines and configures the Subscribe operation of the <see cref="ChannelDefinition"/> to build
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the Subscribe <see cref="OperationDefinition"/></param>
    /// <returns>The configured <see cref="IChannelDefinitionBuilder"/></returns>
    IChannelDefinitionBuilder WithSubscribeOperation(Action<IOperationDefinitionBuilder> setup);

    /// <summary>
    /// Defines and configures the Publish operation of the <see cref="ChannelDefinition"/> to build
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the Publish <see cref="OperationDefinition"/></param>
    /// <returns>The configured <see cref="IChannelDefinitionBuilder"/></returns>
    IChannelDefinitionBuilder WithPublishOperation(Action<IOperationDefinitionBuilder> setup);

    /// <summary>
    /// Builds a new <see cref="ChannelDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="ChannelDefinition"/></returns>
    ChannelDefinition Build();

}
