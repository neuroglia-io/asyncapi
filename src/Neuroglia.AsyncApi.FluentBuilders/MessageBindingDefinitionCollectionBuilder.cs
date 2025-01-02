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

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IMessageBindingDefinitionCollectionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerator{T}"/> containing the <see cref="IValidator"/>s used to validate built <see cref="MessageBindingDefinitionCollection"/>s</param>
public class MessageBindingDefinitionCollectionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<MessageBindingDefinitionCollection>> validators)
    : IMessageBindingDefinitionCollectionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="MessageBindingDefinitionCollection"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<MessageBindingDefinitionCollection>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="MessageBindingDefinitionCollection"/> to configure
    /// </summary>
    protected MessageBindingDefinitionCollection Bindings { get; } = new();

    /// <inheritdoc/>
    public virtual void Use(string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        Bindings.Reference = reference;
    }

    /// <inheritdoc/>
    public virtual IMessageBindingDefinitionCollectionBuilder WithBinding(IMessageBindingDefinition binding)
    {
        ArgumentNullException.ThrowIfNull(binding);
        Bindings.Add(binding);
        return this;
    }

    /// <inheritdoc/>
    public virtual MessageBindingDefinitionCollection Build()
    {
        var validationResults = Validators.Select(v => v.Validate(Bindings));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return Bindings;
    }

}