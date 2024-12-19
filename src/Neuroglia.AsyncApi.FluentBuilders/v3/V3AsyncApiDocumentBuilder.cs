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
/// Represents the default implementation of the <see cref="IV3AsyncApiDocumentBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V3AsyncApiDocument"/>s</param>
public class V3AsyncApiDocumentBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V3AsyncApiDocument>> validators)
    : IV3AsyncApiDocumentBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="V3AsyncApiDocument"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V3AsyncApiDocument>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V3AsyncApiDocument"/> to configure
    /// </summary>
    protected virtual V3AsyncApiDocument Document { get; } = new();

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithSpecVersion(string version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        Document.AsyncApi = version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithId(string? id)
    {
        Document.Id = id;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithTitle(string title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        Document.Info ??= new();
        Document.Info.Title = title;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithVersion(string version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        Document.Info ??= new();
        Document.Info.Version = version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithDescription(string? description)
    {
        Document.Info ??= new();
        Document.Info.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithTermsOfService(Uri? uri)
    {
        Document.Info ??= new();
        Document.Info.TermsOfService = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithContact(string name, Uri? uri = null, string? email = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Document.Info ??= new();
        Document.Info.Contact = new()
        {
            Name = name, 
            Url = uri,
            Email = email
        };
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithLicense(string name, Uri? uri = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Document.Info ??= new();
        Document.Info.License = new()
        {
            Name = name,
            Url = uri
        };
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithDefaultContentType(string contentType)
    {
        ArgumentException.ThrowIfNullOrEmpty(contentType);
        Document.DefaultContentType = contentType;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithTag(Action<IV3TagDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3TagDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Info ??= new();
        Document.Info.Tags ??= [];
        Document.Info.Tags.Add(builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithExternalDocumentation(Action<IV3ExternalDocumentationDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3ExternalDocumentationDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Info ??= new();
        Document.Info.ExternalDocs = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithServer(string name, Action<IV3ServerDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3ServerDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Servers ??= [];
        Document.Servers[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithChannel(string name, Action<IV3ChannelDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3ChannelDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Channels ??= [];
        Document.Channels[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithOperation(string name, Action<IV3OperationDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3OperationDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Operations ??= [];
        Document.Operations[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithSchemaComponent(string name, Action<IV3SchemaDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3SchemaDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.Schemas ??= [];
        Document.Components.Schemas[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithServerComponent(string name, Action<IV3ServerDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3ServerDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.Servers ??= [];
        Document.Components.Servers[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithChannelComponent(string name, Action<IV3ChannelDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3ChannelDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.Channels ??= [];
        Document.Components.Channels[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithOperationComponent(string name, Action<IV3OperationDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3OperationDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.Operations ??= [];
        Document.Components.Operations[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithMessageComponent(string name, Action<IV3MessageDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3MessageDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.Messages ??= [];
        Document.Components.Messages[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithSecuritySchemeComponent(string name, Action<IV3SecuritySchemeDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3SecuritySchemeDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.SecuritySchemes ??= [];
        Document.Components.SecuritySchemes[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithServerVariableComponent(string name, Action<IV3ServerVariableDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3ServerVariableDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.ServerVariables ??= [];
        Document.Components.ServerVariables[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithParameterComponent(string name, Action<IV3ParameterDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3ParameterDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.Parameters ??= [];
        Document.Components.Parameters[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithCorrelationIdComponent(string name, Action<IV3CorrelationIdDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3CorrelationIdDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.CorrelationIds ??= [];
        Document.Components.CorrelationIds[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithReplyComponent(string name, Action<IV3OperationReplyDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3OperationReplyDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.Replies ??= [];
        Document.Components.Replies[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithReplyAddressComponent(string name, Action<IV3OperationReplyAddressDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3OperationReplyAddressDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.ReplyAddresses ??= [];
        Document.Components.ReplyAddresses[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithExternalDocumentationComponent(string name, Action<IV3ExternalDocumentationDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3ExternalDocumentationDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.ExternalDocs ??= [];
        Document.Components.ExternalDocs[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithTagComponent(string name, Action<IV3TagDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3TagDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.Tags ??= [];
        Document.Components.Tags[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithOperationTraitComponent(string name, Action<IV3OperationTraitDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3OperationTraitDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.OperationTraits ??= [];
        Document.Components.OperationTraits[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithMessageTraitComponent(string name, Action<IV3MessageTraitDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<V3MessageTraitDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        Document.Components ??= new();
        Document.Components.MessageTraits ??= [];
        Document.Components.MessageTraits[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithServerBindingsComponent(string name, ServerBindingDefinitionCollection bindings)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(bindings);
        Document.Components ??= new();
        Document.Components.ServerBindings ??= [];
        Document.Components.ServerBindings[name] = bindings;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithChannelBindingsComponent(string name, ChannelBindingDefinitionCollection bindings)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(bindings);
        Document.Components ??= new();
        Document.Components.ChannelBindings ??= [];
        Document.Components.ChannelBindings[name] = bindings;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithOperationBindingsComponent(string name, OperationBindingDefinitionCollection bindings)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(bindings);
        Document.Components ??= new();
        Document.Components.OperationBindings ??= [];
        Document.Components.OperationBindings[name] = bindings;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder WithMessageBindingsComponent(string name, MessageBindingDefinitionCollection bindings)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(bindings);
        Document.Components ??= new();
        Document.Components.MessageBindings ??= [];
        Document.Components.MessageBindings[name] = bindings;
        return this;
    }

    /// <inheritdoc/>
    public virtual V3AsyncApiDocument Build()
    {
        var validationResults = Validators.Select(v => v.Validate(Document));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors!));
        return Document;
    }

}