using Neuroglia.Json.Schema.Generation;

namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Represents the default implementation of the <see cref="IAsyncApiDocumentGenerator"/> interface
/// </summary>
/// <remarks>
/// Initializes a new <see cref="AsyncApiDocumentGenerator"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="exampleGenerator">The service used to generate example values based on a JSON Schema</param>
public class AsyncApiDocumentGenerator(IServiceProvider serviceProvider, IJsonSchemaExampleGenerator exampleGenerator)
    : IAsyncApiDocumentGenerator
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the service used to generate example values based on a JSON Schema
    /// </summary>
    protected IJsonSchemaExampleGenerator ExampleGenerator { get; } = exampleGenerator;

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<AsyncApiDocument>> GenerateAsync(IEnumerable<Type> markupTypes, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(markupTypes);

        var types = markupTypes
            .Select(t => t.Assembly)
            .Distinct()
            .SelectMany(t => t.GetTypes()).Where(t => t.GetCustomAttribute<AsyncApiAttribute>() != null);
        var documents = new List<AsyncApiDocument>(types.Count());
        foreach (var type in types) documents.Add(await this.GenerateDocumentForAsync(type, options, cancellationToken).ConfigureAwait(false));

        return documents;
    }

    /// <summary>
    /// Generates a new <see cref="AsyncApiDocument"/> for the specified type
    /// </summary>
    /// <param name="type">The type to generate a code-first <see cref="AsyncApiDocument"/> for</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="AsyncApiDocument"/></returns>
    protected virtual async Task<AsyncApiDocument> GenerateDocumentForAsync(Type type, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(type);

        var asyncApi = type.GetCustomAttribute<AsyncApiAttribute>() ?? throw new ArgumentException($"The specified type '{type.Name}' is not marked with the {nameof(AsyncApiAttribute)}", nameof(type));
        var builder = this.ServiceProvider.GetRequiredService<IAsyncApiDocumentBuilder>();
        options.DefaultConfiguration?.Invoke(builder);
        builder
            .WithId(asyncApi.Id!)
            .WithTitle(asyncApi.Title)
            .WithVersion(asyncApi.Version);
        if (!string.IsNullOrWhiteSpace(asyncApi.Description)) builder.WithDescription(asyncApi.Description);
        if (!string.IsNullOrWhiteSpace(asyncApi.LicenseName) && !string.IsNullOrWhiteSpace(asyncApi.LicenseUrl)) builder.WithLicense(asyncApi.LicenseName, new Uri(asyncApi.LicenseUrl, UriKind.RelativeOrAbsolute));
        if (!string.IsNullOrWhiteSpace(asyncApi.TermsOfServiceUrl)) builder.WithTermsOfService(new Uri(asyncApi.TermsOfServiceUrl, UriKind.RelativeOrAbsolute));
        if (!string.IsNullOrWhiteSpace(asyncApi.ContactName)) builder.WithContact(asyncApi.ContactName, string.IsNullOrWhiteSpace(asyncApi.ContactUrl) ? null : new Uri(asyncApi.ContactUrl, UriKind.RelativeOrAbsolute), asyncApi.ContactEmail);
        foreach (var operationsPerChannel in type.GetMethods(BindingFlags.Default | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
            .Where(m => m.GetCustomAttribute<OperationAttribute>() != null && m.GetCustomAttribute<ChannelAttribute>() != null)
            .GroupBy(m => m.GetCustomAttribute<ChannelAttribute>()!.Name))
        {
            var channel = operationsPerChannel.First().GetCustomAttribute<ChannelAttribute>()!;
            await ConfigureChannelForAsync(builder, channel, [.. operationsPerChannel], options, cancellationToken).ConfigureAwait(false);
        }
        return builder.Build();
    }

    /// <summary>
    /// Builds a new <see cref="ChannelDefinition"/>
    /// </summary>
    /// <param name="builder">The <see cref="IAsyncApiDocumentBuilder"/> to configure</param>
    /// <param name="channel">The attribute used to describe the <see cref="ChannelDefinition"/> to configure</param>
    /// <param name="methods">A <see cref="List{T}"/> containing the <see cref="ChannelDefinition"/>'s <see cref="OperationDefinition"/>s <see cref="MethodInfo"/>s</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    protected virtual async Task ConfigureChannelForAsync(IAsyncApiDocumentBuilder builder, ChannelAttribute channel, List<MethodInfo> methods, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(channel);
        ArgumentNullException.ThrowIfNull(methods);

        builder.WithChannel(channel.Name, channelBuilder =>
        {
            channelBuilder.WithDescription(channel.Description!);
            foreach (var method in methods)
            {
                var operation = method.GetCustomAttribute<OperationAttribute>()!;
                channelBuilder.WithOperation(operation.OperationType, operationBuilder =>
                {
                    var operationId = operation.OperationId;
                    if (string.IsNullOrWhiteSpace(operationId)) operationId = (method.Name.EndsWith("Async") ? method.Name[..^5] : method.Name).ToCamelCase();
                    var description = operation.Description;
                    if (string.IsNullOrWhiteSpace(description)) description = XmlDocumentationHelper.SummaryOf(method);
                    var summary = operation.Summary;
                    if (string.IsNullOrWhiteSpace(summary)) summary = description;
                    operationBuilder
                        .WithOperationId(operationId)
                        .WithDescription(description!)
                        .WithSummary(summary!);
                    foreach (var tag in method.GetCustomAttributes<TagAttribute>()) operationBuilder.WithTag(tagBuilder => tagBuilder.WithName(tag.Name).WithDescription(tag.Description!));
                    operationBuilder.WithMessage(messageBuilder => this.ConfigureOperationMessageFor(messageBuilder, operation, method, options));
                });
            }
        });
        await Task.CompletedTask;
    }

    /// <summary>
    /// Configures and builds a new <see cref="MessageDefinition"/> for the specified <see cref="OperationDefinition"/>
    /// </summary>
    /// <param name="messageBuilder">The <see cref="IMessageDefinitionBuilder"/> to configure</param>
    /// <param name="operation">The attribute used to describe the <see cref="OperationDefinition"/> to configure</param>
    /// <param name="operationMethod">The <see cref="MethodInfo"/> marked with the specified <see cref="OperationDefinition"/> attribute</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    protected virtual void ConfigureOperationMessageFor(IMessageDefinitionBuilder messageBuilder, OperationAttribute operation, MethodInfo operationMethod, AsyncApiDocumentGenerationOptions options)
    {
        ArgumentNullException.ThrowIfNull(messageBuilder);
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(operationMethod);

        var messageType = operation.MessageType;
        var parameters = operationMethod.GetParameters();
        JsonSchema messageSchema;
        if (messageType == null)
        {
            if (parameters.Length == 1)
            {
                messageType = parameters.First().ParameterType;
                messageSchema = new JsonSchemaBuilder().FromType(parameters.First().ParameterType, JsonSchemaGeneratorConfiguration.Default);
            }
            else
            {
                var messageSchemaBuilder = new JsonSchemaBuilder();
                var messageSchemaProperties = new Dictionary<string, JsonSchema>();
                var requiredProperties = new List<string>();
                messageType = typeof(object);
                foreach (var parameter in parameters)
                {
                    var parameterSchema = messageSchemaBuilder.FromType(parameter.ParameterType, JsonSchemaGeneratorConfiguration.Default);
                    messageSchemaProperties.Add(parameter.Name!, parameterSchema);
                    if (parameter.TryGetCustomAttribute<RequiredAttribute>(out _)|| !parameter.ParameterType.IsNullable()|| parameter.DefaultValue == DBNull.Value) requiredProperties.Add(parameter.Name!);
                }
                messageSchemaBuilder.Properties(messageSchemaProperties);
                messageSchemaBuilder.Required(requiredProperties);
                messageSchema = messageSchemaBuilder.Build();
            }
        }
        else messageSchema = new JsonSchemaBuilder().FromType(messageType, JsonSchemaGeneratorConfiguration.Default);
        messageBuilder.WithPayloadSchema(messageSchema);
        var message = operationMethod.GetCustomAttribute<MessageAttribute>();
        message ??= messageType?.GetCustomAttribute<MessageAttribute>();
        var name = message?.Name;
        if (string.IsNullOrWhiteSpace(name)) name = messageType?.Name.ToCamelCase();
        var title = message?.Title;
        if (string.IsNullOrWhiteSpace(title)) title = name?.SplitCamelCase(false, true);
        var description = message?.Description;
        if (string.IsNullOrWhiteSpace(description)) description = messageType == null ? null : XmlDocumentationHelper.SummaryOf(messageType);
        var summary = message?.Summary;
        if (string.IsNullOrWhiteSpace(summary)) summary = description;
        var contentType = message?.ContentType;
        messageBuilder
            .WithName(name!)
            .WithTitle(title!)
            .WithSummary(summary!)
            .WithDescription(description!)
            .WithContentType(contentType!);
        if(messageType != null) foreach (var tag in messageType.GetCustomAttributes<TagAttribute>()) messageBuilder.WithTag(tagBuilder => tagBuilder.WithName(tag.Name).WithDescription(tag.Description!));
        if (options == null || options.AutomaticallyGenerateExamples)
        {
            messageBuilder.WithExample("Minimal", this.ExampleGenerator.GenerateExample(messageSchema, requiredPropertiesOnly: true)!);
            messageBuilder.WithExample("Extended", this.ExampleGenerator.GenerateExample(messageSchema, requiredPropertiesOnly: false)!);
        }
    }

}
