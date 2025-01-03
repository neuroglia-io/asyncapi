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
/// Represents the default implementation of the <see cref="IV2AsyncApiDocumentBuilder"/>
/// </summary>
/// <remarks>
/// Initializes a new <see cref="V2AsyncApiDocumentBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V2AsyncApiDocument"/>s</param>
public class V2AsyncApiDocumentBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V2AsyncApiDocument>> validators)
    : IV2AsyncApiDocumentBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="V2AsyncApiDocument"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V2AsyncApiDocument>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V2AsyncApiDocument"/> to configure
    /// </summary>
    protected virtual V2AsyncApiDocument Document { get; } = new()
    { 
        AsyncApi =  AsyncApiSpecVersion.V2
    };

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithSpecVersion(string version)
    {
        if (string.IsNullOrWhiteSpace(version)) throw new ArgumentNullException(nameof(version));
        if (!SemanticVersion.TryParse(version, out _)) throw new ArgumentException($"The specified value '{version}' is not a valid semantic version", nameof(version));
        Document.AsyncApi = version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithId(string id)
    {
        Document.Id = id;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        Document.Info ??= new();
        Document.Info.Title = title;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithVersion(string version)
    {
        if (string.IsNullOrWhiteSpace(version)) throw new ArgumentNullException(nameof(version));
        if (!SemanticVersion.TryParse(version, out _)) throw new ArgumentException($"The specified value '{version}' is not a valid semantic version", nameof(version));
        if (Document.Info == null) Document.Info = new();
        Document.Info.Version = version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithDescription(string? description)
    {
        if (Document.Info == null) Document.Info = new();
        Document.Info.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithTermsOfService(Uri uri)
    {
        if (Document.Info == null) Document.Info = new();
        Document.Info.TermsOfService = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithContact(string name, Uri? uri = null, string? email = null)
    {
        if (Document.Info == null) Document.Info = new();
        Document.Info.Contact = new() { Name = name, Url = uri, Email = email };
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithLicense(string name, Uri? uri = null)
    {
        if (Document.Info == null) Document.Info = new();
        Document.Info.License = new() { Name = name, Url = uri };
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithDefaultContentType(string contentType)
    {
        Document.DefaultContentType = contentType;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithTag(Action<IV2TagDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        Document.Tags ??= [];
        var builder = ActivatorUtilities.CreateInstance<V2TagDefinitionBuilder>(ServiceProvider);
        setup(builder);
        Document.Tags.Add(builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithExternalDocumentation(Uri uri, string? description = null)
    {
        ArgumentNullException.ThrowIfNull(uri);
        Document.ExternalDocs = new() { Url = uri, Description = description };
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithServer(string name, Action<IV2ServerDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        if (Document.Servers == null) Document.Servers = [];
        var builder = ActivatorUtilities.CreateInstance<V2ServerDefinitionBuilder>(ServiceProvider);
        setup(builder);
        Document.Servers.Add(name, builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithChannel(string name, Action<IV2ChannelDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        Document.Channels ??= [];
        var builder = ActivatorUtilities.CreateInstance<V2ChannelDefinitionBuilder>(ServiceProvider);
        setup(builder);
        Document.Channels.Add(name, builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithSecurityScheme(string name, V2SecuritySchemeDefinition scheme)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(scheme);
        if (Document.Components == null) Document.Components = new();
        if (Document.Components.SecuritySchemes == null) Document.Components.SecuritySchemes = [];
        Document.Components.SecuritySchemes.Add(name, scheme);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithSecurityScheme(string name, Action<IV2SecuritySchemeDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        Document.Components ??= new();
        Document.Components.SecuritySchemes ??= [];
        var builder = ActivatorUtilities.CreateInstance<V2SecuritySchemeDefinitionBuilder>(ServiceProvider);
        setup(builder);
        Document.Components.SecuritySchemes.Add(name, builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithSchemaComponent(string name, JsonSchema schema)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(schema);
        if (Document.Components == null) Document.Components = new();
        if (Document.Components.Schemas == null) Document.Components.Schemas = [];
        Document.Components.Schemas.Add(name, schema);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithMessageComponent(string name, V2MessageDefinition message)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(message);
        if (Document.Components == null) Document.Components = new();
        if (Document.Components.Messages == null) Document.Components.Messages = [];
        Document.Components.Messages.Add(name, message);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithMessageComponent(string name, Action<IV2MessageDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V2MessageDefinitionBuilder>(ServiceProvider);
        setup(builder);
        return WithMessageComponent(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithParameterComponent(string name, V2ParameterDefinition parameter)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(parameter);
        if (Document.Components == null) Document.Components = new();
        if (Document.Components.Parameters == null) Document.Components.Parameters = [];
        Document.Components.Parameters.Add(name, parameter);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithParameterComponent(string name, Action<IV2ParameterDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<ParameterDefinitionBuilder>(ServiceProvider);
        setup(builder);
        return WithParameterComponent(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithCorrelationIdComponent(string name, V2CorrelationIdDefinition correlationId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(correlationId);
        if (Document.Components == null) Document.Components = new();
        if (Document.Components.CorrelationIds == null) Document.Components.CorrelationIds = [];
        Document.Components.CorrelationIds.Add(name, correlationId);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithOperationTraitComponent(string name, V2OperationTraitDefinition trait)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(trait);
        if (Document.Components == null) Document.Components = new();
        if (Document.Components.OperationTraits == null) Document.Components.OperationTraits = [];
        Document.Components.OperationTraits.Add(name, trait);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithOperationTraitComponent(string name, Action<IV2OperationTraitDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V2OperationTraitDefinitionBuilder>(ServiceProvider);
        setup(builder);
        return WithOperationTraitComponent(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithMessageTraitComponent(string name, V2MessageTraitDefinition trait)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(trait);
        if (Document.Components == null) Document.Components = new();
        if (Document.Components.MessageTraits == null) Document.Components.MessageTraits = [];
        Document.Components.MessageTraits.Add(name, trait);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithMessageTraitComponent(string name, Action<IV2MessageTraitDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V2MessageTraitDefinitionBuilder>(ServiceProvider);
        setup(builder);
        return WithMessageTraitComponent(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithServerBindingComponent(string name, ServerBindingDefinitionCollection bindings)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(bindings);
        if (Document.Components == null) Document.Components = new();
        if (Document.Components.ServerBindings == null) Document.Components.ServerBindings = [];
        Document.Components.ServerBindings.Add(name, bindings);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithChannelBindingComponent(string name, ChannelBindingDefinitionCollection bindings)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(bindings);
        if (Document.Components == null) Document.Components = new();
        if (Document.Components.ChannelBindings == null) Document.Components.ChannelBindings = [];
        Document.Components.ChannelBindings.Add(name, bindings);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithOperationBindingComponent(string name, OperationBindingDefinitionCollection bindings)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(bindings);
        if (Document.Components == null) Document.Components = new();
        if (Document.Components.OperationBindings == null) Document.Components.OperationBindings = [];
        Document.Components.OperationBindings.Add(name, bindings);
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder WithMessageBindingComponent(string name, MessageBindingDefinitionCollection bindings)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(bindings);
        if (Document.Components == null) Document.Components = new();
        if (Document.Components.MessageBindings == null) Document.Components.MessageBindings = [];
        Document.Components.MessageBindings.Add(name, bindings);
        return this;
    }

    /// <inheritdoc/>
    public virtual V2AsyncApiDocument Build()
    {
        var validationResults = Validators.Select(v => v.Validate(Document));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return Document;
    }

    IAsyncApiDocument IVersionedApiDocumentBuilder.Build() => this.Build();

}
