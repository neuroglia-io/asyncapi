namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IAsyncApiDocumentBuilder"/>
/// </summary>
/// <remarks>
/// Initializes a new <see cref="AsyncApiDocumentBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="AsyncApiDocument"/>s</param>
public class AsyncApiDocumentBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<AsyncApiDocument>> validators)
    : IAsyncApiDocumentBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="AsyncApiDocument"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<AsyncApiDocument>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="AsyncApiDocument"/> to configure
    /// </summary>
    protected virtual AsyncApiDocument Document { get; } = new();

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder WithSpecVersion(string version)
    {
        if (string.IsNullOrWhiteSpace(version)) throw new ArgumentNullException(nameof(version));
        if (!SemanticVersion.TryParse(version, out _)) throw new ArgumentException($"The specified value '{version}' is not a valid semantic version", nameof(version));
        this.Document.AsyncApi = version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder WithId(string id)
    {
        this.Document.Id = id;
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder WithTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        this.Document.Info ??= new();
        this.Document.Info.Title = title;
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder WithVersion(string version)
    {
        if (string.IsNullOrWhiteSpace(version)) throw new ArgumentNullException(nameof(version));
        if (!SemanticVersion.TryParse(version, out _)) throw new ArgumentException($"The specified value '{version}' is not a valid semantic version", nameof(version));
        if (this.Document.Info == null)this.Document.Info = new();
        this.Document.Info.Version = version;
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder WithDescription(string description)
    {
        if (this.Document.Info == null) this.Document.Info = new();
        this.Document.Info.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder WithTermsOfService(Uri uri)
    {
        if (this.Document.Info == null) this.Document.Info = new();
        this.Document.Info.TermsOfService = uri;
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder WithContact(string name, Uri? uri = null, string? email = null)
    {
        if (this.Document.Info == null) this.Document.Info = new();
        this.Document.Info.Contact = new() { Name = name, Url = uri, Email = email };
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder WithLicense(string name, Uri? uri = null)
    {
        if (this.Document.Info == null) this.Document.Info = new();
        this.Document.Info.License = new() { Name = name, Url = uri };
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder WithDefaultContentType(string contentType)
    {
        this.Document.DefaultContentType = contentType;
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder WithTag(Action<ITagDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        this.Document.Tags ??= [];
        var builder = ActivatorUtilities.CreateInstance<TagDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        this.Document.Tags.Add(builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder WithExternalDocumentation(Uri uri, string? description = null)
    {
        ArgumentNullException.ThrowIfNull(uri);
        this.Document.ExternalDocs ??= [];
        this.Document.ExternalDocs.Add(new() { Url = uri, Description = description });
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder WithServer(string name, Action<IServerDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        if (this.Document.Servers == null) this.Document.Servers = [];
        var builder = ActivatorUtilities.CreateInstance<ServerDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        this.Document.Servers.Add(name, builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder WithChannel(string name, Action<IChannelDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        if (this.Document.Channels == null) this.Document.Channels = [];
        var builder = ActivatorUtilities.CreateInstance<ChannelDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        this.Document.Channels.Add(name, builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddSchema(string name, JsonSchema schema)
    {
        if (string.IsNullOrWhiteSpace(name))throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(schema);
        if (this.Document.Components == null) this.Document.Components = new();
        if (this.Document.Components.Schemas == null) this.Document.Components.Schemas = [];
        this.Document.Components.Schemas.Add(name, schema);
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddMessage(string name, MessageDefinition message)
    {
        if (string.IsNullOrWhiteSpace(name))throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(message);
        if (this.Document.Components == null) this.Document.Components = new();
        if (this.Document.Components.Messages == null)this.Document.Components.Messages = [];
        this.Document.Components.Messages.Add(name, message);
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddMessage(string name, Action<IMessageDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name))throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<MessageDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        return this.AddMessage(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddSecurityScheme(string name, SecuritySchemeDefinition scheme)
    {
        if (string.IsNullOrWhiteSpace(name))throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(scheme);
        if (this.Document.Components == null)this.Document.Components = new();
        if (this.Document.Components.SecuritySchemes == null) this.Document.Components.SecuritySchemes = [];
        this.Document.Components.SecuritySchemes.Add(name, scheme);
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddParameter(string name, ParameterDefinition parameter)
    {
        if (string.IsNullOrWhiteSpace(name))throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(parameter);
        if (this.Document.Components == null)this.Document.Components = new();
        if (this.Document.Components.Parameters == null)this.Document.Components.Parameters = [];
        this.Document.Components.Parameters.Add(name, parameter);
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddParameter(string name, Action<IParameterDefinitionBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<ParameterDefinitionBuilder>(this.ServiceProvider);
        setup(builder);
        return this.AddParameter(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddCorrelationId(string name, CorrelationIdDefinition correlationId)
    {
        if (string.IsNullOrWhiteSpace(name))throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(correlationId);
        if (this.Document.Components == null)this.Document.Components = new();
        if (this.Document.Components.CorrelationIds == null)this.Document.Components.CorrelationIds = [];
        this.Document.Components.CorrelationIds.Add(name, correlationId);
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddOperationTrait(string name, OperationTraitDefinition trait)
    {
        if (string.IsNullOrWhiteSpace(name))throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(trait);
        if (this.Document.Components == null)this.Document.Components = new();
        if (this.Document.Components.OperationTraits == null)this.Document.Components.OperationTraits = [];
        this.Document.Components.OperationTraits.Add(name, trait);
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddOperationTrait(string name, Action<IOperationTraitBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name))throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<OperationTraitBuilder>(this.ServiceProvider);
        setup(builder);
        return this.AddOperationTrait(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddMessageTrait(string name, MessageTraitDefinition trait)
    {
        if (string.IsNullOrWhiteSpace(name))throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(trait);
        if (this.Document.Components == null) this.Document.Components = new();
        if (this.Document.Components.MessageTraits == null)this.Document.Components.MessageTraits = [];
        this.Document.Components.MessageTraits.Add(name, trait);
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddMessageTrait(string name, Action<IMessageTraitBuilder> setup)
    {
        if (string.IsNullOrWhiteSpace(name))throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(setup);
        var builder = ActivatorUtilities.CreateInstance<MessageTraitBuilder>(this.ServiceProvider);
        setup(builder);
        return this.AddMessageTrait(name, builder.Build());
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddServerBinding(string name, ServerBindingDefinitionCollection bindings)
    {
        if (string.IsNullOrWhiteSpace(name))throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(bindings);
        if (this.Document.Components == null)this.Document.Components = new();
        if (this.Document.Components.ServerBindings == null)this.Document.Components.ServerBindings = [];
        this.Document.Components.ServerBindings.Add(name, bindings);
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddChannelBinding(string name, ChannelBindingDefinitionCollection bindings)
    {
        if (string.IsNullOrWhiteSpace(name))throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(bindings);
        if (this.Document.Components == null)this.Document.Components = new();
        if (this.Document.Components.ChannelBindings == null)this.Document.Components.ChannelBindings = [];
        this.Document.Components.ChannelBindings.Add(name, bindings);
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddOperationBinding(string name, OperationBindingDefinitionCollection bindings)
    {
        if (string.IsNullOrWhiteSpace(name))throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(bindings);
        if (this.Document.Components == null)this.Document.Components = new();
        if (this.Document.Components.OperationBindings == null)this.Document.Components.OperationBindings = [];
        this.Document.Components.OperationBindings.Add(name, bindings);
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocumentBuilder AddMessageBinding(string name, MessageBindingCollection bindings)
    {
        if (string.IsNullOrWhiteSpace(name))throw new ArgumentNullException(nameof(name));
        ArgumentNullException.ThrowIfNull(bindings);
        if (this.Document.Components == null) this.Document.Components = new();
        if (this.Document.Components.MessageBindings == null)this.Document.Components.MessageBindings = [];
        this.Document.Components.MessageBindings.Add(name, bindings);
        return this;
    }

    /// <inheritdoc/>
    public virtual AsyncApiDocument Build()
    {
        var validationResults = this.Validators.Select(v => v.Validate(this.Document));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
        return this.Document;
    }

}
