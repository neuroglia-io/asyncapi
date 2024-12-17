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
/// Represents the default implementation of the <see cref="IServerDefinitionBuilder"/>
/// </summary>
/// <remarks>
/// Initializes a new <see cref="ServerDefinitionBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V2ServerDefinition"/>s</param>
public class ServerDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V2ServerDefinition>> validators)
    : IServerDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="V2ServerDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V2ServerDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V2ServerDefinition"/> to configure
    /// </summary>
    protected virtual V2ServerDefinition Server { get; } = new();

    /// <inheritdoc/>
    public virtual IServerDefinitionBuilder WithUrl(Uri uri)
    {
        this.Server.Url = uri ?? throw new ArgumentNullException(nameof(uri));
        return this;
    }

    /// <inheritdoc/>
    public virtual IServerDefinitionBuilder WithProtocol(string protocol, string? version = null)
    {
        if (string.IsNullOrWhiteSpace(protocol)) throw new ArgumentNullException(nameof(protocol));
        this.Server.Protocol = protocol;
        this.Server.ProtocolVersion = string.IsNullOrWhiteSpace(version) ? "latest" : version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IServerDefinitionBuilder WithDescription(string? description)
    {
        this.Server.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IServerDefinitionBuilder WithVariable(string name, Action<IVariableDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        var builder = ActivatorUtilities.CreateInstance<VariableDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        this.Server.Variables ??= [];
        this.Server.Variables.Add(name, builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IServerDefinitionBuilder WithBinding(IServerBindingDefinition binding)
    {
        ArgumentNullException.ThrowIfNull(binding);
        this.Server.Bindings ??= new();
        this.Server.Bindings.Add(binding);
        return this;
    }

    /// <inheritdoc/>
    public virtual IServerDefinitionBuilder WithSecurityRequirement(string name, object? requirement = null)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        requirement ??= new { };
        this.Server.Security ??= [];
        this.Server.Security.Add(name, requirement);
        return this;
    }

    /// <inheritdoc/>
    public virtual V2ServerDefinition Build()
    {
        var validationResults = this.Validators.Select(v => v.Validate(this.Server));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors!));
        return this.Server;
    }

}
