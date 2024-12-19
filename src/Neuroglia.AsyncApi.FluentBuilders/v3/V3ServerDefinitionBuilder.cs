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
/// Represents the default implementation of the <see cref="IV3ServerDefinitionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V3ServerDefinition"/>s</param>
public class V3ServerDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V3ServerDefinition>> validators)
    : IV3ServerDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="V3ServerDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V3ServerDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V3ServerDefinition"/> to configure
    /// </summary>
    protected virtual V3ServerDefinition Server { get; } = new();

    /// <inheritdoc/>
    public virtual void Use(string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        this.Server.Reference = reference;
    }

    /// <inheritdoc/>
    public virtual IV3ServerDefinitionBuilder WithHost(string host)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(host);
        Server.Host = host;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ServerDefinitionBuilder WithProtocol(string protocol, string? version = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(protocol);
        Server.Protocol = protocol;
        Server.ProtocolVersion = version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ServerDefinitionBuilder WithPathName(string? pathName)
    {
        Server.PathName = pathName;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ServerDefinitionBuilder WithDescription(string? description)
    {
        Server.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ServerDefinitionBuilder WithTitle(string? title)
    {
        Server.Title = title;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ServerDefinitionBuilder WithSummary(string? summary)
    {
        Server.Summary = summary;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ServerDefinitionBuilder WithVariable(string name, Action<IV3ServerVariableDefinitionBuilder> setup)
    {
        var builder = ActivatorUtilities.CreateInstance<V3ServerVariableDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Server.Variables ??= [];
        Server.Variables[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ServerDefinitionBuilder WithExternalDocumentation(Action<IV3ExternalDocumentationDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3ExternalDocumentationDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Server.ExternalDocs = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ServerDefinitionBuilder WithTag(Action<IV3TagDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3TagDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Server.Tags ??= [];
        Server.Tags.Add(builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ServerDefinitionBuilder WithSecurity(Action<IV3SecuritySchemeDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3SecuritySchemeDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Server.Security ??= [];
        Server.Security.Add(builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3ServerDefinitionBuilder WithBinding(IServerBindingDefinition binding)
    {
        ArgumentNullException.ThrowIfNull(binding);
        Server.Bindings ??= new();
        Server.Bindings.Add(binding);
        return this;
    }

    /// <inheritdoc/>
    public virtual V3ServerDefinition Build()
    {
        var validationResults = Validators.Select(v => v.Validate(Server));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors!));
        return Server;
    }

}
