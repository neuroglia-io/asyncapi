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
/// Represents the base class for all <see cref="IV2OperationDefinitionBuilder"/> implementations
/// </summary>
/// <remarks>
/// Initializes a new <see cref="V2OperationDefinitionBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="V2OperationTraitDefinition"/>s</param>
public class V2OperationDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V2OperationTraitDefinition>> validators)
    : V2OperationTraitDefinitionBuilder<IV2OperationDefinitionBuilder, V2OperationDefinition>(serviceProvider, validators), IV2OperationDefinitionBuilder
{

    /// <inheritdoc/>
    public virtual IV2OperationDefinitionBuilder WithMessage(Action<IV2MessageDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);

        var builder = ActivatorUtilities.CreateInstance<V2MessageDefinitionBuilder>(ServiceProvider);
        setup(builder);
        Trait.Message = V2OperationMessageDefinition.From(builder.Build());

        return this;
    }

    /// <inheritdoc/>
    public virtual IV2OperationDefinitionBuilder WithMessages(params Action<IV2MessageDefinitionBuilder>[] setups)
    {
        if (setups == null || setups.Length == 0) return this;

        Trait.Message = new()
        {
            OneOf = new(setups.Select(setup =>
            {
                var builder = ActivatorUtilities.CreateInstance<V2MessageDefinitionBuilder>(ServiceProvider);
                setup(builder);
                return V2OperationMessageDefinition.From(builder.Build());
            }))
        };

        return this;
    }

    /// <inheritdoc/>
    public virtual IV2OperationDefinitionBuilder WithTrait(Action<IV2OperationTraitDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);

        Trait.Traits ??= [];
        var builder = ActivatorUtilities.CreateInstance<V2OperationTraitDefinitionBuilder>(ServiceProvider);
        setup(builder);
        Trait.Traits.Add(builder.Build());

        return this;
    }

}
