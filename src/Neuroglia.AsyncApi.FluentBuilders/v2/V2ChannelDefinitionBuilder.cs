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
/// Represents the default implementation of the <see cref="IV2ChannelDefinitionBuilder"/>
/// </summary>
/// <remarks>
/// Initializes a new <see cref="V2ChannelDefinitionBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V2ChannelDefinition"/>s</param>
public class V2ChannelDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V2ChannelDefinition>> validators)
    : IV2ChannelDefinitionBuilder
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
    public virtual IV2ChannelDefinitionBuilder WithDescription(string? description)
    {
        Channel.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ChannelDefinitionBuilder WithParameter(string name, Action<IV2ParameterDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        Channel.Parameters ??= [];
        var builder = ActivatorUtilities.CreateInstance<ParameterDefinitionBuilder>(ServiceProvider);
        setup(builder);
        Channel.Parameters.Add(name, builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ChannelDefinitionBuilder WithBinding(IChannelBindingDefinition binding)
    {
        ArgumentNullException.ThrowIfNull(binding);
        Channel.Bindings ??= new();
        Channel.Bindings.Add(binding);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ChannelDefinitionBuilder WithOperation(V2OperationType type, Action<IV2OperationDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V2OperationDefinitionBuilder>(ServiceProvider);
        setup(builder);
        switch (type)
        {
            case V2OperationType.Publish:
                Channel.Publish = builder.Build();
                break;
            case V2OperationType.Subscribe:
                Channel.Subscribe = builder.Build();
                break;
            default:
                throw new NotSupportedException($"The specified operation type '{type}' is not supported");
        }
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ChannelDefinitionBuilder WithSubscribeOperation(Action<IV2OperationDefinitionBuilder> setup) => WithOperation(V2OperationType.Subscribe, setup);

    /// <inheritdoc/>
    public virtual IV2ChannelDefinitionBuilder WithPublishOperation(Action<IV2OperationDefinitionBuilder> setup) => WithOperation(V2OperationType.Publish, setup);

    /// <inheritdoc/>
    public virtual V2ChannelDefinition Build()
    {
        var validationResults = Validators.Select(v => v.Validate(Channel));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return Channel;
    }

}
