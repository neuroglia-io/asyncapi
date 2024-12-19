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
        var asyncApiAttribute = type.GetCustomAttribute<AsyncApiV3Attribute>() ?? throw new ArgumentException($"The specified type '{type.Name}' is not marked with the {nameof(AsyncApiV3Attribute)}", nameof(type));
        var document = this.ServiceProvider.GetRequiredService<IV3AsyncApiDocumentBuilder>();
        options.V3BuilderSetup?.Invoke(document);
        document
            .WithId(asyncApiAttribute.Id!)
            .WithTitle(asyncApiAttribute.Title)
            .WithVersion(asyncApiAttribute.Version);
        if (!string.IsNullOrWhiteSpace(asyncApiAttribute.Description)) document.WithDescription(asyncApiAttribute.Description);
        if (!string.IsNullOrWhiteSpace(asyncApiAttribute.LicenseName) && !string.IsNullOrWhiteSpace(asyncApiAttribute.LicenseUrl)) document.WithLicense(asyncApiAttribute.LicenseName, new Uri(asyncApiAttribute.LicenseUrl, UriKind.RelativeOrAbsolute));
        if (!string.IsNullOrWhiteSpace(asyncApiAttribute.TermsOfServiceUrl)) document.WithTermsOfService(new Uri(asyncApiAttribute.TermsOfServiceUrl, UriKind.RelativeOrAbsolute));
        if (!string.IsNullOrWhiteSpace(asyncApiAttribute.ContactName)) document.WithContact(asyncApiAttribute.ContactName, string.IsNullOrWhiteSpace(asyncApiAttribute.ContactUrl) ? null : new Uri(asyncApiAttribute.ContactUrl, UriKind.RelativeOrAbsolute), asyncApiAttribute.ContactEmail);
        if (asyncApiAttribute.Tags != null)
        {
            foreach (var tagReference in asyncApiAttribute.Tags)
            {
                document.WithTag(tag => tag.Use(tagReference));
            }
        }
        var channelParameters = type.GetCustomAttributes<ChannelParameterV3Attribute>();
        foreach (var channel in type.GetCustomAttributes<ChannelV3Attribute>())
        {
            await this.GenerateV3ChannelForAsync(document, channel, channelParameters.Where(c => c.Channel == channel.Name), options, cancellationToken);
        }
        foreach (var method in type.GetMethods(BindingFlags.Default | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy).Where(m => m.GetCustomAttribute<OperationV3Attribute>() != null))
        {
            var operation = method.GetCustomAttribute<OperationV3Attribute>()!;
            await this.GenerateV3OperationForAsync(document, operation, method, options, cancellationToken);
        }
        return document.Build();
    }

    /// <summary>
    /// Builds a new <see cref="V3ChannelDefinition"/>
    /// </summary>
    /// <param name="document">The <see cref="IV3AsyncApiDocumentBuilder"/> to configure</param>
    /// <param name="channelAttribute">The attribute used to describe the <see cref="V3ChannelDefinition"/> to build</param>
    /// <param name="channelParameterAttributes">An <see cref="IEnumerable{T}"/> containing the <see cref="ChannelParameterV3Attribute"/>s that apply to the <see cref="V3ChannelDefinition"/> to build</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    protected virtual Task GenerateV3ChannelForAsync(IV3AsyncApiDocumentBuilder document, ChannelV3Attribute channelAttribute, IEnumerable<ChannelParameterV3Attribute>? channelParameterAttributes, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
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
            if (channelParameterAttributes != null)
            {
                foreach (var parameterAttribute in channelParameterAttributes)
                {
                    channel.WithParameter(parameterAttribute.Name, parameter =>
                    {
                        parameter
                            .WithDefaultValue(parameterAttribute.Default)
                            .WithEnumValues(parameterAttribute.Enum)
                            .WithDescription(parameterAttribute.Description)
                            .WithLocation(parameterAttribute.Location);
                        if (parameterAttribute.Examples != null)
                        {
                            foreach(var example in parameterAttribute.Examples)
                            {
                                parameter.WithExample(example);
                            }
                        }
                    });
                }
            }
            if (!string.IsNullOrEmpty(channelAttribute.Bindings))
            {
                channel.WithBindings(bindings => bindings.Use(channelAttribute.Bindings));
            }
            if (channelAttribute.ExternalDocumentationUrl != null)
            {
                channel.WithExternalDocumentation(doc => doc.WithUrl(channelAttribute.ExternalDocumentationUrl));
            }
            if (channelAttribute.Tags != null)
            {
                foreach(var tagReference in channelAttribute.Tags)
                {
                    channel.WithTag(tag => tag.Use(tagReference));
                }
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
        var requestMessagePayloadType = operationAttribute.MessagePayloadType;
        var requestMessageAttribute = operationMethod.GetCustomAttribute<MessageV3Attribute>() ?? requestMessagePayloadType?.GetCustomAttribute<MessageV3Attribute>();
        var requestMessagePayloadSchema = (JsonSchema?)null;
        var requestMessageHeadersSchema = operationAttribute.HeadersPayloadType == null ? null : new JsonSchemaBuilder().FromType(operationAttribute.HeadersPayloadType, Data.Schemas.Json.JsonSchemaGeneratorConfiguration.Default);
        if (requestMessageAttribute == null)
        {
            if (requestMessagePayloadType == null)
            {
                if (operationParameters.Count == 1)
                {
                    requestMessagePayloadType = operationParameters[0].ParameterType;
                    requestMessagePayloadSchema = new JsonSchemaBuilder().FromType(requestMessagePayloadType, Data.Schemas.Json.JsonSchemaGeneratorConfiguration.Default);
                }
                else
                {
                    var schemaBuilder = new JsonSchemaBuilder();
                    var messageSchemaProperties = new Dictionary<string, JsonSchema>();
                    var requiredProperties = new List<string>();
                    requestMessagePayloadType = typeof(object);
                    foreach (var parameter in operationParameters)
                    {
                        if (parameter.TryGetCustomAttribute<ExcludeAttribute>(out _)) continue;
                        var parameterSchema = schemaBuilder.FromType(parameter.ParameterType, Data.Schemas.Json.JsonSchemaGeneratorConfiguration.Default);
                        messageSchemaProperties.Add(parameter.Name!, parameterSchema);
                        if (parameter.TryGetCustomAttribute<RequiredAttribute>(out _) || !parameter.ParameterType.IsNullable() || parameter.DefaultValue == DBNull.Value) requiredProperties.Add(parameter.Name!);
                    }
                    schemaBuilder.Properties(messageSchemaProperties);
                    schemaBuilder.Required(requiredProperties);
                    requestMessagePayloadSchema = schemaBuilder.Build();
                }
            }
            else
            {
                requestMessagePayloadSchema = new JsonSchemaBuilder().FromType(requestMessagePayloadType, Data.Schemas.Json.JsonSchemaGeneratorConfiguration.Default).Build();
            }
        }
        var requestMessageName = $"{operationAttribute.Name}Request";
        var replyMessageName = $"{operationAttribute.Name}Reply";
        var requestMessageReference = operationAttribute.Message ?? $"#/components/messages/{requestMessageName}";
        document.WithOperation(operationAttribute.Name, operation => 
        {
            operation
                .WithAction(operationAttribute.Action)
                .WithChannel(operationAttribute.Channel)
                .WithTitle(operationAttribute.Title)
                .WithSummary(operationAttribute.Summary)
                .WithDescription(operationAttribute.Description)
                .WithMessage(requestMessageReference);
            if (!string.IsNullOrEmpty(operationAttribute.Bindings))
            {
                operation.WithBindings(bindings => bindings.Use(operationAttribute.Bindings));
            }
            if (operationAttribute.ExternalDocumentationUrl != null)
            {
                operation.WithExternalDocumentation(doc => doc.WithUrl(operationAttribute.ExternalDocumentationUrl));
            }
            if (operationAttribute.Tags != null)
            {
                foreach (var tagReference in operationAttribute.Tags)
                {
                    operation.WithTag(tag => tag.Use(tagReference));
                }
            }
            foreach (var tagAttribute in operationMethod.GetCustomAttributes<TagV2Attribute>())
            {
                operation
                    .WithTag(tag => tag
                    .WithName(tagAttribute.Name)
                    .WithDescription(tagAttribute.Description));
            }
        });
        if (string.IsNullOrWhiteSpace(operationAttribute.Message))
        {
            document.WithMessageComponent(requestMessageName, message =>
            {
                message
                    .WithName(requestMessageAttribute?.Name)
                    .WithTitle(requestMessageAttribute?.Title)
                    .WithDescription(requestMessageAttribute?.Description)
                    .WithSummary(requestMessageAttribute?.Summary)
                    .WithDescription(requestMessageAttribute?.Description);
                if (!string.IsNullOrEmpty(requestMessageAttribute?.Bindings))
                {
                    message.WithBindings(bindings => bindings.Use(requestMessageAttribute.Bindings));
                }
                if (!string.IsNullOrWhiteSpace(requestMessageAttribute?.PayloadSchema))
                {
                    message.WithPayloadSchema(schema => schema.Use(requestMessageAttribute.PayloadSchema));
                }
                else if (requestMessagePayloadSchema != null)
                {
                    message
                        .WithPayloadSchema(schema => schema
                            .WithFormat("application/schema+json")
                            .WithSchema(requestMessagePayloadSchema));
                }
                if (!string.IsNullOrWhiteSpace(requestMessageAttribute?.HeadersSchema))
                {
                    message.WithHeadersSchema(schema => schema.Use(requestMessageAttribute.HeadersSchema));
                }
                else if (requestMessageHeadersSchema != null)
                {
                    message
                        .WithHeadersSchema(schema => schema
                            .WithFormat("application/schema+json")
                            .WithSchema(requestMessageHeadersSchema));
                }
                if (requestMessageAttribute?.ExternalDocumentationUrl != null)
                {
                    message.WithExternalDocumentation(doc => doc.WithUrl(requestMessageAttribute.ExternalDocumentationUrl));
                }
                if (requestMessageAttribute?.Tags != null)
                {
                    foreach (var tagReference in requestMessageAttribute.Tags)
                    {
                        message.WithTag(tag => tag.Use(tagReference));
                    }
                }
            });
        }
        return Task.CompletedTask;
    }

}
