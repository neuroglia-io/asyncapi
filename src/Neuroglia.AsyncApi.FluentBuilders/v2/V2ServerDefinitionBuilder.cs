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
/// Represents the default implementation of the <see cref="IV2ServerDefinitionBuilder"/>
/// </summary>
/// <remarks>
/// Initializes a new <see cref="V2ServerDefinitionBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V2ServerDefinition"/>s</param>
public class V2ServerDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V2ServerDefinition>> validators)
    : IV2ServerDefinitionBuilder
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
    public virtual IV2ServerDefinitionBuilder WithUrl(Uri uri)
    {
        Server.Url = uri ?? throw new ArgumentNullException(nameof(uri));
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ServerDefinitionBuilder WithProtocol(string protocol, string? version = null)
    {
        if (string.IsNullOrWhiteSpace(protocol)) throw new ArgumentNullException(nameof(protocol));
        Server.Protocol = protocol;
        Server.ProtocolVersion = string.IsNullOrWhiteSpace(version) ? "latest" : version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ServerDefinitionBuilder WithDescription(string? description)
    {
        Server.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ServerDefinitionBuilder WithVariable(string name, Action<IV2ServerVariableDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        var builder = ActivatorUtilities.CreateInstance<V2ServerVariableDefinitionBuilder>(ServiceProvider);
        setup(builder);
        Server.Variables ??= [];
        Server.Variables.Add(name, builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ServerDefinitionBuilder WithBinding(IServerBindingDefinition binding)
    {
        ArgumentNullException.ThrowIfNull(binding);
        Server.Bindings ??= new();
        Server.Bindings.Add(binding);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ServerDefinitionBuilder WithSecurityRequirement(string name, object? requirement = null)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        requirement ??= new { };
        Server.Security ??= [];
        Server.Security.Add(name, requirement);
        return this;
    }

    /// <inheritdoc/>
    public virtual V2ServerDefinition Build()
    {
        var validationResults = Validators.Select(v => v.Validate(Server));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors!));
        return Server;
    }

}
