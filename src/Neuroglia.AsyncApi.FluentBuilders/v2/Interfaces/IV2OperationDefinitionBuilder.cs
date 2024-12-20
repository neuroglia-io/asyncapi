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
/// Defines the fundamentals of a service used to build <see cref="V2OperationDefinition"/>s
/// </summary>
public interface IV2OperationDefinitionBuilder
    : IV2OperationTraitDefinitionBuilder<IV2OperationDefinitionBuilder, V2OperationDefinition>
{

    /// <summary>
    /// Configures the <see cref="V2OperationDefinition"/> to build to use the specified <see cref="V2OperationTraitDefinition"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V2OperationTraitDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV2OperationDefinitionBuilder"/></returns>
    IV2OperationDefinitionBuilder WithTrait(Action<IV2OperationTraitDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="V2OperationDefinition"/> to build to use the specified <see cref="V2MessageDefinition"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V2MessageDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV2OperationDefinitionBuilder"/></returns>
    IV2OperationDefinitionBuilder WithMessage(Action<IV2MessageDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="V2OperationDefinition"/> to build to use the specified <see cref="V2MessageDefinition"/>s
    /// </summary>
    /// <param name="setups">An array containing the <see cref="Action{T}"/> used to setup the <see cref="V2MessageDefinition"/>s to use</param>
    /// <returns>The configured <see cref="IV2OperationDefinitionBuilder"/></returns>
    IV2OperationDefinitionBuilder WithMessages(params Action<IV2MessageDefinitionBuilder>[] setups);

}
