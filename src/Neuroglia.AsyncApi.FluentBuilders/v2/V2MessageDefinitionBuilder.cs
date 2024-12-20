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

using Neuroglia.Data.Schemas.Json;

namespace Neuroglia.AsyncApi.FluentBuilders.v2;

/// <summary>
/// Represents the default implementation of the <see cref="IV2MessageDefinitionBuilder"/> interface
/// </summary>
/// <remarks>
/// Initializes a new <see cref="V2MessageDefinitionBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="V2MessageTraitDefinition"/>s</param>
public class V2MessageDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V2MessageDefinition>> validators)
    : V2MessageTraitDefinitionBuilder<IV2MessageDefinitionBuilder, V2MessageDefinition>(serviceProvider, validators), IV2MessageDefinitionBuilder
{

    /// <inheritdoc/>
    public virtual IV2MessageDefinitionBuilder WithPayloadOfType<TPayload>() => WithPayloadOfType(typeof(TPayload));

    /// <inheritdoc/>
    public virtual IV2MessageDefinitionBuilder WithPayloadOfType(Type payloadType)
    {
        ArgumentNullException.ThrowIfNull(payloadType);
        return WithPayloadSchema(new JsonSchemaBuilder().FromType(payloadType, JsonSchemaGeneratorConfiguration.Default));
    }

    /// <inheritdoc/>
    public virtual IV2MessageDefinitionBuilder WithPayloadSchema(JsonSchema payloadSchema)
    {
        ArgumentNullException.ThrowIfNull(payloadSchema);
        Trait.Payload = payloadSchema;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2MessageDefinitionBuilder WithTrait(Action<IV2MessageTraitDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);

        Trait.Traits ??= [];
        var builder = ActivatorUtilities.CreateInstance<V2MessageTraitDefinitionBuilder>(ServiceProvider);
        setup(builder);
        Trait.Traits.Add(builder.Build());

        return this;
    }

    /// <inheritdoc/>
    public virtual IV2MessageDefinitionBuilder WithTrait(V2MessageTraitDefinition trait)
    {
        ArgumentNullException.ThrowIfNull(trait);

        Trait.Traits ??= [];
        Trait.Traits.Add(trait);

        return this;
    }

    /// <inheritdoc/>
    public virtual IV2MessageDefinitionBuilder WithTraitReference(string reference)
    {
        if (string.IsNullOrWhiteSpace(reference)) throw new ArgumentNullException(nameof(reference));
        return WithTrait(new V2MessageTraitDefinition() { Reference = reference });
    }

}
