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
/// Defines the fundamentals of a service used to build <see cref="MessageDefinition"/>s
/// </summary>
public interface IMessageDefinitionBuilder
    : IMessageTraitDefinitionBuilder<IMessageDefinitionBuilder, MessageDefinition>
{

    /// <summary>
    /// Configures the <see cref="MessageTraitDefinition"/> to build to use the specified payload
    /// </summary>
    /// <param name="payloadSchema">The <see cref="JsonSchema"/> used to define the <see cref="MessageTraitDefinition"/>'s payload</param>
    /// <returns>The configured <see cref="IMessageDefinitionBuilder"/></returns>
    IMessageDefinitionBuilder WithPayloadSchema(JsonSchema payloadSchema);

    /// <summary>
    /// Configures the <see cref="MessageTraitDefinition"/> to build to use the specified payload
    /// </summary>
    /// <typeparam name="TPayload">The type used to define the <see cref="MessageTraitDefinition"/>'s payload</typeparam>
    /// <returns>The configured <see cref="IMessageDefinitionBuilder"/></returns>
    IMessageDefinitionBuilder WithPayloadOfType<TPayload>();

    /// <summary>
    /// Configures the <see cref="MessageTraitDefinition"/> to build to use the specified payload
    /// </summary>
    /// <param name="payloadType">The type used to define the <see cref="MessageTraitDefinition"/>'s payload</param>
    /// <returns>The configured <see cref="IMessageDefinitionBuilder"/></returns>
    IMessageDefinitionBuilder WithPayloadOfType(Type payloadType);

    /// <summary>
    /// Configures the <see cref="MessageDefinition"/> to build to use the specified <see cref="MessageTraitDefinition"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="MessageTraitDefinition"/> to use</param>
    /// <returns>The configured <see cref="IMessageDefinitionBuilder"/></returns>
    IMessageDefinitionBuilder WithTrait(Action<IMessageTraitBuilder> setup);

    /// <summary>
    /// Configures the <see cref="MessageDefinition"/> to build to use the specified <see cref="MessageTraitDefinition"/>
    /// </summary>
    /// <param name="trait">An <see cref="Action{T}"/> used to setup the <see cref="MessageTraitDefinition"/> to use</param>
    /// <returns>The configured <see cref="IMessageDefinitionBuilder"/></returns>
    IMessageDefinitionBuilder WithTrait(MessageTraitDefinition trait);

    /// <summary>
    /// Configures the <see cref="MessageDefinition"/> to build to use the specified <see cref="MessageTraitDefinition"/>
    /// </summary>
    /// <param name="reference">The reference to the <see cref="MessageTraitDefinition"/> to use</param>
    /// <returns>The configured <see cref="IMessageDefinitionBuilder"/></returns>
    IMessageDefinitionBuilder WithTraitReference(string reference);

}
