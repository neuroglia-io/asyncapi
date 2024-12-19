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
/// Represents the default implementation of the <see cref="IV3ChannelDefinitionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V3ChannelDefinition"/>s</param>
public class V3ChannelDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V3ChannelDefinition>> validators)
    : IV3ChannelDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="V3ChannelDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V3ChannelDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V3ChannelDefinition"/> to configure
    /// </summary>
    protected virtual V3ChannelDefinition Channel { get; } = new();

    /// <inheritdoc/>
    public virtual void Use(string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        Channel.Reference = reference;
    }

    /// <inheritdoc/>
    public virtual IV3ChannelDefinitionBuilder WithAddress(string? address)
    {
        Channel.Address = address;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ChannelDefinitionBuilder WithMessage(string name, Action<IV3MessageDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3MessageDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Channel.Messages ??= [];
        Channel.Messages[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ChannelDefinitionBuilder WithTitle(string? title)
    {
        Channel.Title = title;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ChannelDefinitionBuilder WithSummary(string? summary)
    {
        Channel.Summary = summary;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ChannelDefinitionBuilder WithDescription(string? description)
    {
        Channel.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ChannelDefinitionBuilder WithServer(string server)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(server);
        Channel.Servers ??= [];
        Channel.Servers.Add(new() 
        { 
            Reference = server 
        });
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ChannelDefinitionBuilder WithParameter(string name, Action<IV3ParameterDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3ParameterDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Channel.Parameters ??= [];
        Channel.Parameters[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ChannelDefinitionBuilder WithExternalDocumentation(Action<IV3ExternalDocumentationDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3ExternalDocumentationDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Channel.ExternalDocs = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ChannelDefinitionBuilder WithTag(Action<IV3TagDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3TagDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Channel.Tags ??= [];
        Channel.Tags.Add(builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ChannelDefinitionBuilder WithBinding(IChannelBindingDefinition binding)
    {
        ArgumentNullException.ThrowIfNull(binding);
        Channel.Bindings ??= new();
        Channel.Bindings.Add(binding);
        return this;
    }

    /// <inheritdoc/>
    public virtual V3ChannelDefinition Build()
    {
        var validationResults = Validators.Select(v => v.Validate(Channel));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors!));
        return Channel;
    }

}