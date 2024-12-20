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
/// Defines the fundamentals of a service used to build <see cref="V2MessageDefinition"/>s
/// </summary>
public interface IV2MessageDefinitionBuilder
    : IV2MessageTraitDefinitionBuilder<IV2MessageDefinitionBuilder, V2MessageDefinition>
{

    /// <summary>
    /// Configures the <see cref="V2MessageTraitDefinition"/> to build to use the specified payload
    /// </summary>
    /// <param name="payloadSchema">The <see cref="JsonSchema"/> used to define the <see cref="V2MessageTraitDefinition"/>'s payload</param>
    /// <returns>The configured <see cref="IV2MessageDefinitionBuilder"/></returns>
    IV2MessageDefinitionBuilder WithPayloadSchema(JsonSchema payloadSchema);

    /// <summary>
    /// Configures the <see cref="V2MessageTraitDefinition"/> to build to use the specified payload
    /// </summary>
    /// <typeparam name="TPayload">The type used to define the <see cref="V2MessageTraitDefinition"/>'s payload</typeparam>
    /// <returns>The configured <see cref="IV2MessageDefinitionBuilder"/></returns>
    IV2MessageDefinitionBuilder WithPayloadOfType<TPayload>();

    /// <summary>
    /// Configures the <see cref="V2MessageTraitDefinition"/> to build to use the specified payload
    /// </summary>
    /// <param name="payloadType">The type used to define the <see cref="V2MessageTraitDefinition"/>'s payload</param>
    /// <returns>The configured <see cref="IV2MessageDefinitionBuilder"/></returns>
    IV2MessageDefinitionBuilder WithPayloadOfType(Type payloadType);

    /// <summary>
    /// Configures the <see cref="V2MessageDefinition"/> to build to use the specified <see cref="V2MessageTraitDefinition"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V2MessageTraitDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV2MessageDefinitionBuilder"/></returns>
    IV2MessageDefinitionBuilder WithTrait(Action<IV2MessageTraitDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="V2MessageDefinition"/> to build to use the specified <see cref="V2MessageTraitDefinition"/>
    /// </summary>
    /// <param name="trait">An <see cref="Action{T}"/> used to setup the <see cref="V2MessageTraitDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV2MessageDefinitionBuilder"/></returns>
    IV2MessageDefinitionBuilder WithTrait(V2MessageTraitDefinition trait);

    /// <summary>
    /// Configures the <see cref="V2MessageDefinition"/> to build to use the specified <see cref="V2MessageTraitDefinition"/>
    /// </summary>
    /// <param name="reference">The reference to the <see cref="V2MessageTraitDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV2MessageDefinitionBuilder"/></returns>
    IV2MessageDefinitionBuilder WithTraitReference(string reference);

}
