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
/// Defines the fundamentals of a service used to build <see cref="V3OperationDefinition"/>s
/// </summary>
public interface IV3OperationDefinitionBuilder
    : IV3OperationTraitDefinitionBuilder<IV3OperationDefinitionBuilder, V3OperationDefinition>, IReferenceableComponentDefinitionBuilder<V3OperationDefinition>
{

    /// <summary>
    /// Configures the <see cref="V3OperationDefinition"/> to use the specified action
    /// </summary>
    /// <param name="action">The action to use</param>
    /// <returns>The configured <see cref="IV3OperationDefinitionBuilder"/></returns>
    IV3OperationDefinitionBuilder WithAction(V3OperationAction action);

    /// <summary>
    /// Configures the <see cref="V3OperationDefinition"/> to use the specified <see cref="V3ChannelDefinition"/>
    /// </summary>
    /// <param name="channel">A reference to the <see cref="V3ChannelDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV3OperationDefinitionBuilder"/></returns>
    IV3OperationDefinitionBuilder WithChannel(string channel);

    /// <summary>
    /// Configures the <see cref="V3OperationDefinition"/> to use the specified <see cref="V3OperationTraitDefinition"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to build the <see cref="V3OperationTraitDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV3OperationDefinitionBuilder"/></returns>
    IV3OperationDefinitionBuilder WithTrait(Action<IV3OperationTraitDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="V3MessageDefinition"/> to use the specified <see cref="V3MessageDefinition"/>
    /// </summary>
    /// <param name="message">A reference to the the <see cref="V3MessageDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV3OperationDefinitionBuilder"/></returns>
    IV3OperationDefinitionBuilder WithMessage(string message);

    /// <summary>
    /// Configures the <see cref="V3OperationDefinition"/> to use the specified <see cref="V3OperationReplyDefinition"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V3OperationReplyDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV3OperationDefinitionBuilder"/></returns>
    IV3OperationDefinitionBuilder WithReply(Action<IV3OperationReplyDefinitionBuilder> setup);

    /// <summary>
    /// Builds the configured <see cref="V3OperationDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="V3OperationDefinition"/></returns>
    V3OperationDefinition Build();

}
