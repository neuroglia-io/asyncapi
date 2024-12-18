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

using Neuroglia.Data.Schemas.Json;

namespace Neuroglia.AsyncApi.Generation;

public partial class AsyncApiDocumentGenerator
{

    /// <summary>
    /// Generates a new <see cref="V2AsyncApiDocument"/> for the specified type
    /// </summary>
    /// <param name="type">The type to generate a code-first <see cref="V2AsyncApiDocument"/> for</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="V2AsyncApiDocument"/></returns>
    protected virtual async Task<V2AsyncApiDocument> GenerateV2DocumentForAsync(Type type, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(options);
        var asyncApi = type.GetCustomAttribute<AsyncApiV2Attribute>() ?? throw new ArgumentException($"The specified type '{type.Name}' is not marked with the {nameof(AsyncApiV2Attribute)}", nameof(type));
        var builder = ServiceProvider.GetRequiredService<IV2AsyncApiDocumentBuilder>();
        options.V2BuilderSetup?.Invoke(builder);
        builder
            .WithId(asyncApi.Id!)
            .WithTitle(asyncApi.Title)
            .WithVersion(asyncApi.Version);
        if (!string.IsNullOrWhiteSpace(asyncApi.Description)) builder.WithDescription(asyncApi.Description);
        if (!string.IsNullOrWhiteSpace(asyncApi.LicenseName) && !string.IsNullOrWhiteSpace(asyncApi.LicenseUrl)) builder.WithLicense(asyncApi.LicenseName, new Uri(asyncApi.LicenseUrl, UriKind.RelativeOrAbsolute));
        if (!string.IsNullOrWhiteSpace(asyncApi.TermsOfServiceUrl)) builder.WithTermsOfService(new Uri(asyncApi.TermsOfServiceUrl, UriKind.RelativeOrAbsolute));
        if (!string.IsNullOrWhiteSpace(asyncApi.ContactName)) builder.WithContact(asyncApi.ContactName, string.IsNullOrWhiteSpace(asyncApi.ContactUrl) ? null : new Uri(asyncApi.ContactUrl, UriKind.RelativeOrAbsolute), asyncApi.ContactEmail);
        foreach (var operationsPerChannel in type.GetMethods(BindingFlags.Default | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
            .Where(m => m.GetCustomAttribute<OperationV2Attribute>() != null && m.GetCustomAttribute<ChannelV2Attribute>() != null)
            .GroupBy(m => m.GetCustomAttribute<ChannelV2Attribute>()!.Name))
        {
            var channel = operationsPerChannel.First().GetCustomAttribute<ChannelV2Attribute>()!;
            await this.ConfigureV2ChannelForAsync(builder, channel, [.. operationsPerChannel], options, cancellationToken).ConfigureAwait(false);
        }
        return builder.Build();
    }

    /// <summary>
    /// Builds a new <see cref="V2ChannelDefinition"/>
    /// </summary>
    /// <param name="builder">The <see cref="IV2AsyncApiDocumentBuilder"/> to configure</param>
    /// <param name="channel">The attribute used to describe the <see cref="V2ChannelDefinition"/> to configure</param>
    /// <param name="methods">A <see cref="List{T}"/> containing the <see cref="V2ChannelDefinition"/>'s <see cref="V2OperationDefinition"/>s <see cref="MethodInfo"/>s</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    protected virtual async Task ConfigureV2ChannelForAsync(IV2AsyncApiDocumentBuilder builder, ChannelV2Attribute channel, List<MethodInfo> methods, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(channel);
        ArgumentNullException.ThrowIfNull(methods);

        builder.WithChannel(channel.Name, channelBuilder =>
        {
            channelBuilder.WithDescription(channel.Description!);
            foreach (var method in methods)
            {
                var operation = method.GetCustomAttribute<OperationV2Attribute>()!;
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
                    foreach (var tag in method.GetCustomAttributes<TagV2Attribute>()) operationBuilder.WithTag(tagBuilder => tagBuilder.WithName(tag.Name).WithDescription(tag.Description!));
                    operationBuilder.WithMessage(messageBuilder => this.ConfigureV2OperationMessageFor(messageBuilder, operation, method, options));
                });
            }
        });
        await Task.CompletedTask;
    }

    /// <summary>
    /// Configures and builds a new <see cref="V2MessageDefinition"/> for the specified <see cref="V2OperationDefinition"/>
    /// </summary>
    /// <param name="messageBuilder">The <see cref="IV2MessageDefinitionBuilder"/> to configure</param>
    /// <param name="operation">The attribute used to describe the <see cref="V2OperationDefinition"/> to configure</param>
    /// <param name="operationMethod">The <see cref="MethodInfo"/> marked with the specified <see cref="V2OperationDefinition"/> attribute</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    protected virtual void ConfigureV2OperationMessageFor(IV2MessageDefinitionBuilder messageBuilder, OperationV2Attribute operation, MethodInfo operationMethod, AsyncApiDocumentGenerationOptions options)
    {
        ArgumentNullException.ThrowIfNull(messageBuilder);
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(operationMethod);

        var messageType = operation.MessageType;
        var parameters = operationMethod.GetParameters();
        JsonSchema messageSchema;
        if (messageType == null)
        {
            if (parameters.Length == 1 || parameters.Length == 2 && parameters[1].ParameterType == typeof(CancellationToken))
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
                    if (parameter.TryGetCustomAttribute<ExcludeAttribute>(out _)) continue;
                    var parameterSchema = messageSchemaBuilder.FromType(parameter.ParameterType, JsonSchemaGeneratorConfiguration.Default);
                    messageSchemaProperties.Add(parameter.Name!, parameterSchema);
                    if (parameter.TryGetCustomAttribute<RequiredAttribute>(out _) || !parameter.ParameterType.IsNullable() || parameter.DefaultValue == DBNull.Value) requiredProperties.Add(parameter.Name!);
                }
                messageSchemaBuilder.Properties(messageSchemaProperties);
                messageSchemaBuilder.Required(requiredProperties);
                messageSchema = messageSchemaBuilder.Build();
            }
        }
        else messageSchema = new JsonSchemaBuilder().FromType(messageType, JsonSchemaGeneratorConfiguration.Default);
        messageBuilder.WithPayloadSchema(messageSchema);
        var message = operationMethod.GetCustomAttribute<MessageV2Attribute>();
        message ??= messageType?.GetCustomAttribute<MessageV2Attribute>();
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
        if (messageType != null) foreach (var tag in messageType.GetCustomAttributes<TagV2Attribute>()) messageBuilder.WithTag(tagBuilder => tagBuilder.WithName(tag.Name).WithDescription(tag.Description!));
        if (options == null || options.AutomaticallyGenerateExamples)
        {
            messageBuilder.WithExample("Minimal", ExampleGenerator.GenerateExample(messageSchema, requiredPropertiesOnly: true)!);
            messageBuilder.WithExample("Extended", ExampleGenerator.GenerateExample(messageSchema, requiredPropertiesOnly: false)!);
        }
    }

}
