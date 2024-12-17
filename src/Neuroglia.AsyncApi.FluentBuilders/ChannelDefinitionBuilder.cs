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

using Neuroglia.AsyncApi.Bindings;
using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IChannelDefinitionBuilder"/>
/// </summary>
/// <remarks>
/// Initializes a new <see cref="ChannelDefinitionBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V2ChannelDefinition"/>s</param>
public class ChannelDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V2ChannelDefinition>> validators)
    : IChannelDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="V2ChannelDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V2ChannelDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V2ChannelDefinition"/> to build
    /// </summary>
    protected virtual V2ChannelDefinition Channel { get; } = new();

    /// <inheritdoc/>
    public virtual IChannelDefinitionBuilder WithDescription(string? description)
    {
        this.Channel.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IChannelDefinitionBuilder WithParameter(string name, Action<IParameterDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        this.Channel.Parameters ??= [];
        var builder = ActivatorUtilities.CreateInstance<ParameterDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        this.Channel.Parameters.Add(name, builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IChannelDefinitionBuilder WithBinding(IChannelBindingDefinition binding)
    {
        ArgumentNullException.ThrowIfNull(binding);
        this.Channel.Bindings ??= new();
        this.Channel.Bindings.Add(binding);
        return this;
    }

    /// <inheritdoc/>
    public virtual IChannelDefinitionBuilder WithOperation(V2OperationType type, Action<IOperationDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<OperationDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        switch (type)
        {
            case V2OperationType.Publish:
                this.Channel.Publish = builder.Build();
                break;
            case V2OperationType.Subscribe:
                this.Channel.Subscribe = builder.Build();
                break;
            default:
                throw new NotSupportedException($"The specified operation type '{type}' is not supported");
        }
        return this;
    }

    /// <inheritdoc/>
    public virtual IChannelDefinitionBuilder WithSubscribeOperation(Action<IOperationDefinitionBuilder> setup) => this.WithOperation(V2OperationType.Subscribe, setup);

    /// <inheritdoc/>
    public virtual IChannelDefinitionBuilder WithPublishOperation(Action<IOperationDefinitionBuilder> setup) => this.WithOperation(V2OperationType.Publish, setup);

    /// <inheritdoc/>
    public virtual V2ChannelDefinition Build()
    {
        var validationResults = this.Validators.Select(v => v.Validate(this.Channel));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return this.Channel;
    }

}
