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
/// Represents the default implementation of the <see cref="IV3OperationDefinitionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V3OperationDefinition"/>s</param>
public class V3OperationDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V3OperationDefinition>> validators)
    : V3OperationTraitDefinitionBuilder<IV3OperationDefinitionBuilder, V3OperationDefinition>(serviceProvider, validators), IV3OperationDefinitionBuilder
{

    /// <inheritdoc/>
    public virtual IV3OperationDefinitionBuilder WithAction(V3OperationAction action)
    {
        Trait.Action = action;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3OperationDefinitionBuilder WithChannel(string channel)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(channel);
        Trait.Channel = new()
        {
            Reference = channel
        };
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3OperationDefinitionBuilder WithMessage(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        Trait.Messages ??= [];
        Trait.Messages.Add(new()
        {
            Reference = message
        });
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3OperationDefinitionBuilder WithReply(Action<IV3OperationReplyDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3OperationReplyDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Trait.Reply = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3OperationDefinitionBuilder WithTrait(Action<IV3OperationTraitDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3OperationTraitDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Trait.Traits ??= [];
        Trait.Traits.Add(builder.Build());
        return this;
    }

}