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
/// Represents the default implementation of the <see cref="IV3MessageDefinitionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V3MessageDefinition"/>s</param>
public class V3MessageDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V3MessageDefinition>> validators)
    : V3MessageTraitDefinitionBuilder<IV3MessageDefinitionBuilder, V3MessageDefinition>(serviceProvider, validators), IV3MessageDefinitionBuilder
{

    /// <inheritdoc/>
    public virtual IV3MessageDefinitionBuilder WithPayloadSchema(Action<IV3SchemaDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3SchemaDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Trait.Payload = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3MessageDefinitionBuilder WithTrait(Action<IV3MessageTraitDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3MessageTraitDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Trait.Traits ??= [];
        Trait.Traits.Add(builder.Build());
        return this;
    }

}
