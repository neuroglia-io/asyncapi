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
/// Defines the fundamentals of a service used to build <see cref="V3MessageDefinition"/>s
/// </summary>
public interface IV3MessageDefinitionBuilder
    : IV3MessageTraitDefinitionBuilder<IV3MessageDefinitionBuilder, V3MessageDefinition>
{

    /// <summary>
    /// Configures the <see cref="V3MessageTraitDefinition"/> to use the specified payload schema
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the payload schema to use</param>
    /// <returns>The configured <see cref="IV3MessageTraitDefinitionBuilder{TBuilder, TTrait}"/></returns>
    IV3MessageDefinitionBuilder WithPayloadSchema(Action<IV3SchemaDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="V3MessageDefinition"/> to build to use the specified <see cref="V3MessageDefinition"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V3MessageDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV3MessageDefinitionBuilder"/></returns>
    IV3MessageDefinitionBuilder WithTrait(Action<IV3MessageTraitDefinitionBuilder> setup);

}
