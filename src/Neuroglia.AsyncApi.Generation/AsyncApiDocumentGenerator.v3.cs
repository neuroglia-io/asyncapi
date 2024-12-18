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

namespace Neuroglia.AsyncApi.Generation;

public partial class AsyncApiDocumentGenerator
{

    /// <summary>
    /// Generates a new <see cref="V3AsyncApiDocument"/> for the specified type
    /// </summary>
    /// <param name="type">The type to generate a code-first <see cref="V3AsyncApiDocument"/> for</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="V3AsyncApiDocument"/></returns>
    protected virtual async Task<V3AsyncApiDocument> GenerateV3DocumentForAsync(Type type, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(options);
        var asyncApi = type.GetCustomAttribute<AsyncApiV3Attribute>() ?? throw new ArgumentException($"The specified type '{type.Name}' is not marked with the {nameof(AsyncApiV3Attribute)}", nameof(type));
        var builder = this.ServiceProvider.GetRequiredService<IV3AsyncApiDocumentBuilder>();
        options.V3BuilderSetup?.Invoke(builder);
        builder
            .WithId(asyncApi.Id!)
            .WithTitle(asyncApi.Title)
            .WithVersion(asyncApi.Version);
        if (!string.IsNullOrWhiteSpace(asyncApi.Description)) builder.WithDescription(asyncApi.Description);
        if (!string.IsNullOrWhiteSpace(asyncApi.LicenseName) && !string.IsNullOrWhiteSpace(asyncApi.LicenseUrl)) builder.WithLicense(asyncApi.LicenseName, new Uri(asyncApi.LicenseUrl, UriKind.RelativeOrAbsolute));
        if (!string.IsNullOrWhiteSpace(asyncApi.TermsOfServiceUrl)) builder.WithTermsOfService(new Uri(asyncApi.TermsOfServiceUrl, UriKind.RelativeOrAbsolute));
        if (!string.IsNullOrWhiteSpace(asyncApi.ContactName)) builder.WithContact(asyncApi.ContactName, string.IsNullOrWhiteSpace(asyncApi.ContactUrl) ? null : new Uri(asyncApi.ContactUrl, UriKind.RelativeOrAbsolute), asyncApi.ContactEmail);
        foreach(var channel in type.GetCustomAttributes<ChannelV3Attribute>())
        {
            await this.GenerateV3ChannelForAsync(builder, channel, options, cancellationToken);
        }
        foreach (var method in type.GetMethods(BindingFlags.Default | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy).Where(m => m.GetCustomAttribute<OperationV3Attribute>() != null))
        {
            var operation = method.GetCustomAttribute<OperationV3Attribute>()!;
            await this.GenerateV3OperationForAsync(builder, operation, method, options, cancellationToken);
        }
        return builder.Build();
    }

    /// <summary>
    /// Builds a new <see cref="V3ChannelDefinition"/>
    /// </summary>
    /// <param name="document">The <see cref="IV3AsyncApiDocumentBuilder"/> to configure</param>
    /// <param name="channelAttribute">The attribute used to describe the <see cref="V3ChannelDefinition"/> to configure</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    protected virtual Task GenerateV3ChannelForAsync(IV3AsyncApiDocumentBuilder document, ChannelV3Attribute channelAttribute, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(channelAttribute);
        ArgumentNullException.ThrowIfNull(options);
        document.WithChannel(channelAttribute.Name, channel =>
        {
            channel
                .WithAddress(channelAttribute.Address)
                .WithTitle(channelAttribute.Title)
                .WithSummary(channelAttribute.Summary)
                .WithDescription(channelAttribute.Description);
            if (channelAttribute.Servers != null)
            {
                foreach (var server in channelAttribute.Servers)
                {
                    channel.WithServer(server);
                }
            }
            if (channelAttribute?.ExternalDocumentationUrl != null)
            {
                channel.WithExternalDocumentation(doc => doc.WithUrl(channelAttribute.ExternalDocumentationUrl));
            }
        });
        return Task.CompletedTask;
    }

    /// <summary>
    /// Builds a new <see cref="V3OperationDefinition"/>
    /// </summary>
    /// <param name="document">The <see cref="IV3AsyncApiDocumentBuilder"/> to configure</param>
    /// <param name="operationAttribute">The attribute used to describe the <see cref="V3OperationDefinition"/> to configure</param>
    /// <param name="operationMethod">The <see cref="V3OperationDefinition"/>s <see cref="MethodInfo"/>s</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    protected virtual Task GenerateV3OperationForAsync(IV3AsyncApiDocumentBuilder document, OperationV3Attribute operationAttribute, MethodInfo operationMethod, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(operationAttribute);
        ArgumentNullException.ThrowIfNull(operationMethod);
        ArgumentNullException.ThrowIfNull(options);
        var operationParameters = operationMethod.GetParameters().Where(p => p.ParameterType != typeof(CancellationToken)).ToList();
        var requestMessageType = operationParameters.Count == 1 ? operationParameters[0].ParameterType : null;
        var requestMessageAttribute = operationMethod.GetCustomAttribute<MessageV3Attribute>();
        JsonSchema? requestMessagePayloadSchema = null;
        var schemaGeneratorConfiguration = new SchemaGeneratorConfiguration()
        {
            PropertyNameResolver = PropertyNameResolvers.CamelCase,
            Optimize = false
        };
        schemaGeneratorConfiguration.Generators.Add(new DateTimeOffsetJsonSchemaGenerator());
        schemaGeneratorConfiguration.Refiners.Add(new XmlDocumentationJsonSchemaRefiner());
        if (requestMessageType == null)
        {
            var schemaBuilder = new JsonSchemaBuilder();
            var messageSchemaProperties = new Dictionary<string, JsonSchema>();
            var requiredProperties = new List<string>();
            requestMessageType = typeof(object);
            foreach (var parameter in operationParameters)
            {
                if (parameter.TryGetCustomAttribute<ExcludeAttribute>(out _)) continue;
                var parameterSchema = schemaBuilder.FromType(parameter.ParameterType, schemaGeneratorConfiguration);
                messageSchemaProperties.Add(parameter.Name!, parameterSchema);
                if (parameter.TryGetCustomAttribute<RequiredAttribute>(out _) || !parameter.ParameterType.IsNullable() || parameter.DefaultValue == DBNull.Value) requiredProperties.Add(parameter.Name!);
            }
            schemaBuilder.Properties(messageSchemaProperties);
            schemaBuilder.Required(requiredProperties);
            requestMessagePayloadSchema = schemaBuilder.Build();
        }
        else
        {
            requestMessagePayloadSchema = new JsonSchemaBuilder().FromType(requestMessageType, schemaGeneratorConfiguration).Build();
        }
        requestMessageAttribute ??= requestMessageType?.GetCustomAttribute<MessageV3Attribute>();
        var requestMessageName = $"{operationAttribute.Name}-request";
        var replyMessageName = $"{operationAttribute.Name}-reply";
        document.WithOperation(operationAttribute.Name, operation => 
        {
            operation
                .WithAction(operationAttribute.Action)
                .WithChannel(operationAttribute.Channel)
                .WithTitle(operationAttribute.Title)
                .WithSummary(operationAttribute.Summary)
                .WithDescription(operationAttribute.Description)
                .WithMessage($"#/components/messages/{requestMessageName}");
            if (operationAttribute.ExternalDocumentationUrl != null)
            {
                operation.WithExternalDocumentation(doc => doc.WithUrl(operationAttribute.ExternalDocumentationUrl));
            }
            foreach(var tagAttribute in operationMethod.GetCustomAttributes<TagV2Attribute>())
            {
                operation.WithTag(tag => tag
                    .WithName(tagAttribute.Name)
                    .WithDescription(tagAttribute.Description));
            }
        });
        document.WithMessageComponent(requestMessageName, message =>
        {
            message
                .WithName(requestMessageAttribute?.Name)
                .WithTitle(requestMessageAttribute?.Title)
                .WithDescription(requestMessageAttribute?.Description)
                .WithSummary(requestMessageAttribute?.Summary)
                .WithDescription(requestMessageAttribute?.Description);
            if (requestMessagePayloadSchema != null)
            {
                message
                    .WithPayloadSchema(schema => schema
                        .WithFormat("application/schema+json")
                        .WithSchema(requestMessagePayloadSchema));
            }
            if (requestMessageAttribute?.ExternalDocumentationUrl != null)
            {
                message.WithExternalDocumentation(doc => doc.WithUrl(requestMessageAttribute.ExternalDocumentationUrl));
            }
        });
        return Task.CompletedTask;
    }

}
