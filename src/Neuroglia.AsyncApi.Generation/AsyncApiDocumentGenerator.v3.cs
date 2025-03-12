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

namespace Neuroglia.AsyncApi.Generation;

public partial class AsyncApiDocumentGenerator
{

    /// <summary>
    /// Generates a new <see cref="V3AsyncApiDocument"/> for the specified type
    /// </summary>
    /// <param name="types">An <see cref="IEnumerable{T}"/> containing the types to generate a code-first <see cref="V3AsyncApiDocument"/> for</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="V3AsyncApiDocument"/></returns>
    protected virtual async Task<V3AsyncApiDocument> GenerateV3DocumentForAsync(IEnumerable<Type> types, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(types);
        ArgumentOutOfRangeException.ThrowIfLessThan(types.Count(), 1);
        ArgumentNullException.ThrowIfNull(options);
        var asyncApiAttributes = types.Select(t => t.GetCustomAttribute<v3.AsyncApiAttribute>()!);
        var document = this.ServiceProvider.GetRequiredService<IV3AsyncApiDocumentBuilder>();
        options.V3BuilderSetup?.Invoke(document);
        document
            .WithId(asyncApiAttributes.First()!.Id!)
            .WithTitle(asyncApiAttributes.First()!.Title)
            .WithVersion(asyncApiAttributes.First()!.Version);
        var description = asyncApiAttributes.Select(a => a.Description).FirstOrDefault(d => !string.IsNullOrWhiteSpace(d));
        var licenseName = asyncApiAttributes.Select(a => a.LicenseName).FirstOrDefault(l => !string.IsNullOrWhiteSpace(l));
        var licenseUrl = asyncApiAttributes.Select(a => a.LicenseUrl).FirstOrDefault(l => !string.IsNullOrWhiteSpace(l));
        var termsOfServiceUrl = asyncApiAttributes.Select(a => a.TermsOfServiceUrl).FirstOrDefault(t => !string.IsNullOrWhiteSpace(t));
        var contactName = asyncApiAttributes.Select(a => a.ContactName).FirstOrDefault(c => !string.IsNullOrWhiteSpace(c));
        var contactUrl = asyncApiAttributes.Select(a => a.ContactUrl).FirstOrDefault(c => !string.IsNullOrWhiteSpace(c));
        var contactEmail = asyncApiAttributes.Select(a => a.ContactEmail).FirstOrDefault(c => !string.IsNullOrWhiteSpace(c));
        var tags = asyncApiAttributes.Where(a => a.Tags != null).SelectMany(a => a.Tags!);
        if (!string.IsNullOrWhiteSpace(description)) document.WithDescription(description);
        if (!string.IsNullOrWhiteSpace(licenseName) && !string.IsNullOrWhiteSpace(licenseUrl)) document.WithLicense(licenseName, new Uri(licenseUrl, UriKind.RelativeOrAbsolute));
        if (!string.IsNullOrWhiteSpace(termsOfServiceUrl)) document.WithTermsOfService(new Uri(termsOfServiceUrl, UriKind.RelativeOrAbsolute));
        if (!string.IsNullOrWhiteSpace(contactName)) document.WithContact(contactName, string.IsNullOrWhiteSpace(contactUrl) ? null : new Uri(contactUrl, UriKind.RelativeOrAbsolute), contactEmail);
        if (tags != null && tags.Any()) foreach (var tagReference in tags) document.WithTag(tag => tag.Use(tagReference));
        var serverVariables = types.SelectMany(t => t.GetCustomAttributes<ServerVariableAttribute>());
        foreach (var server in types.SelectMany(t => t.GetCustomAttributes<ServerAttribute>())) await this.GenerateV3ServerForAsync(document, server, serverVariables.Where(c => c.Server == server.Name), options, cancellationToken);
        var channelParameters = types.SelectMany(t => t.GetCustomAttributes<ChannelParameterAttribute>());
        var operationMethods = types.SelectMany(t => t.GetMethods(BindingFlags.Default | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy).Where(m => m.GetCustomAttribute<v3.OperationAttribute>() != null));
        foreach (var channel in types.SelectMany(t => t.GetCustomAttributes<v3.ChannelAttribute>())) await this.GenerateV3ChannelForAsync(document, channel, channelParameters.Where(c => c.Channel == channel.Name), operationMethods.Where(o => o.GetCustomAttribute<v3.OperationAttribute>()!.Channel.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last() == channel.Name), options, cancellationToken);
        foreach (var bindingAttribute in types.SelectMany(t => t.GetCustomAttributes()).OfType<IBindingAttribute>())
        {
            var binding = bindingAttribute.Build();
            switch (binding)
            {
                case IServerBindingDefinition serverBinding:
                    var serverBindings = new ServerBindingDefinitionCollection();
                    serverBindings.Add(serverBinding);
                    document.WithServerBindingsComponent(bindingAttribute.Name, serverBindings);
                    break;
                case IChannelBindingDefinition channelBinding:
                    var channelBindings = new ChannelBindingDefinitionCollection();
                    channelBindings.Add(channelBinding);
                    document.WithChannelBindingsComponent(bindingAttribute.Name, channelBindings);
                    break;
                case IOperationBindingDefinition operationBinding:
                    var operationBindings = new OperationBindingDefinitionCollection();
                    operationBindings.Add(operationBinding);
                    document.WithOperationBindingsComponent(bindingAttribute.Name, operationBindings);
                    break;
                case IMessageBindingDefinition messageBinding:
                    var messageBindings = new MessageBindingDefinitionCollection();
                    messageBindings.Add(messageBinding);
                    document.WithMessageBindingsComponent(bindingAttribute.Name, messageBindings);
                    break;
                default:
                    throw new NotSupportedException($"The specified binding definition type '{binding.GetType().Name}' is not supported");
            }
        }
        return document.Build();
    }

    /// <summary>
    /// Builds a new <see cref="V3ServerDefinition"/>
    /// </summary>
    /// <param name="document">The <see cref="IV3AsyncApiDocumentBuilder"/> to configure</param>
    /// <param name="serverAttribute">The attribute used to describe the <see cref="V3ChannelDefinition"/> to build</param>
    /// <param name="serverVariableAttributes">An <see cref="IEnumerable{T}"/> containing the <see cref="ChannelParameterAttribute"/>s that apply to the <see cref="V3ChannelDefinition"/> to build</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    protected virtual Task GenerateV3ServerForAsync(IV3AsyncApiDocumentBuilder document, ServerAttribute serverAttribute, IEnumerable<ServerVariableAttribute>? serverVariableAttributes, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(serverAttribute);
        ArgumentNullException.ThrowIfNull(options);
        document.WithServer(serverAttribute.Name, server =>
        {
            server
                .WithHost(serverAttribute.Host)
                .WithProtocol(serverAttribute.Protocol, serverAttribute.ProtocolVersion)
                .WithPathName(serverAttribute.PathName)
                .WithDescription(serverAttribute.Description)
                .WithTitle(serverAttribute.Title)
                .WithSummary(serverAttribute.Summary);
            if (serverAttribute.Security != null)
            {
                foreach (var securityReference in serverAttribute.Security)
                {
                    server.WithSecurityRequirement(security => security.Use(securityReference));
                }
            }
            if (serverVariableAttributes != null)
            {
                foreach (var variableAttribute in serverVariableAttributes)
                {
                    server.WithVariable(variableAttribute.Name, variable =>
                    {
                        variable
                            .WithDefaultValue(variableAttribute.Default)
                            .WithEnumValues(variableAttribute.Enum)
                            .WithDescription(variableAttribute.Description);
                        if (variableAttribute.Examples != null)
                        {
                            foreach (var example in variableAttribute.Examples)
                            {
                                variable.WithExample(example);
                            }
                        }
                    });
                }
            }
            if (!string.IsNullOrEmpty(serverAttribute.Bindings))
            {
                server.WithBindings(bindings => bindings.Use(serverAttribute.Bindings));
            }
            if (serverAttribute.ExternalDocumentationUrl != null)
            {
                server.WithExternalDocumentation(doc => doc.WithUrl(serverAttribute.ExternalDocumentationUrl));
            }
            if (serverAttribute.Tags != null)
            {
                foreach (var tagReference in serverAttribute.Tags)
                {
                    server.WithTag(tag => tag.Use(tagReference));
                }
            }
        });
        return Task.CompletedTask;
    }

    /// <summary>
    /// Builds a new <see cref="V3ChannelDefinition"/>
    /// </summary>
    /// <param name="document">The <see cref="IV3AsyncApiDocumentBuilder"/> to configure</param>
    /// <param name="channelAttribute">The attribute used to describe the <see cref="V3ChannelDefinition"/> to build</param>
    /// <param name="channelParameterAttributes">An <see cref="IEnumerable{T}"/> containing the <see cref="ChannelParameterAttribute"/>s that apply to the <see cref="V3ChannelDefinition"/> to build</param>
    /// <param name="operationMethods">An <see cref="IEnumerable{T}"/> containing the operation methods that belongs to the <see cref="V3ChannelDefinition"/> to build</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    protected virtual async Task GenerateV3ChannelForAsync(IV3AsyncApiDocumentBuilder document, v3.ChannelAttribute channelAttribute, IEnumerable<ChannelParameterAttribute>? channelParameterAttributes, IEnumerable<MethodInfo> operationMethods, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(channelAttribute);
        ArgumentNullException.ThrowIfNull(options);
        IV3ChannelDefinitionBuilder channelBuilder = null!;
        document.WithChannel(channelAttribute.Name, channel =>
        {
            channelBuilder = channel;
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
        foreach (var method in operationMethods)
        {
            var operation = method.GetCustomAttribute<v3.OperationAttribute>()!;
            await this.GenerateV3OperationForAsync(document, channelAttribute.Name, channelBuilder, operation, method, options, cancellationToken);
        }
    }

    /// <summary>
    /// Builds a new <see cref="V3OperationDefinition"/>
    /// </summary>
    /// <param name="document">The <see cref="IV3AsyncApiDocumentBuilder"/> to configure</param>
    /// <param name="channelName">The name of the <see cref="V3ChannelDefinition"/> the <see cref="V3OperationDefinition"/> to build belongs to</param>
    /// <param name="channel">The <see cref="IV3ChannelDefinitionBuilder"/> used to configure the <see cref="V3ChannelDefinition"/> the <see cref="V3OperationDefinition"/> to build belongs to</param>
    /// <param name="operationAttribute">The attribute used to describe the <see cref="V3OperationDefinition"/> to configure</param>
    /// <param name="operationMethod">The <see cref="V3OperationDefinition"/>s <see cref="MethodInfo"/>s</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    protected virtual Task GenerateV3OperationForAsync(IV3AsyncApiDocumentBuilder document, string channelName, IV3ChannelDefinitionBuilder channel, v3.OperationAttribute operationAttribute, MethodInfo operationMethod, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentException.ThrowIfNullOrWhiteSpace(channelName);
        ArgumentNullException.ThrowIfNull(channel);
        ArgumentNullException.ThrowIfNull(operationAttribute);
        ArgumentNullException.ThrowIfNull(operationMethod);
        ArgumentNullException.ThrowIfNull(options);
        var operationParameters = operationMethod.GetParameters().Where(p => p.ParameterType != typeof(CancellationToken)).ToList();
        var requestMessagePayloadType = operationAttribute.MessagePayloadType;
        var requestMessageAttribute = operationMethod.GetCustomAttribute<v3.MessageAttribute>() ?? requestMessagePayloadType?.GetCustomAttribute<v3.MessageAttribute>();
        var requestMessagePayloadSchema = (JsonSchema?)null;
        var requestMessageHeadersSchema = operationAttribute.HeadersPayloadType == null ? null : new JsonSchemaBuilder().FromType(operationAttribute.HeadersPayloadType, JsonSchemaGeneratorConfiguration.Default);
        if (requestMessageAttribute == null)
        {
            if (requestMessagePayloadType == null)
            {
                if (operationParameters.Count == 1)
                {
                    requestMessagePayloadType = operationParameters[0].ParameterType;
                    requestMessagePayloadSchema = new JsonSchemaBuilder().FromType(requestMessagePayloadType, JsonSchemaGeneratorConfiguration.Default);
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
                        var parameterSchema = schemaBuilder.FromType(parameter.ParameterType, JsonSchemaGeneratorConfiguration.Default);
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
                requestMessagePayloadSchema = new JsonSchemaBuilder().FromType(requestMessagePayloadType, JsonSchemaGeneratorConfiguration.Default).Build();
            }
        }
        var requestMessageName = $"{operationAttribute.Name}Request";
        var replyMessageName = $"{operationAttribute.Name}Reply";
        var requestMessageReference = $"#/channels/{channelName}/messages/{requestMessageName}";
        channel.WithMessage(requestMessageName, message =>
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
            foreach (var tagAttribute in operationMethod.GetCustomAttributes<v2.TagAttribute>())
            {
                operation
                    .WithTag(tag => tag
                    .WithName(tagAttribute.Name)
                    .WithDescription(tagAttribute.Description));
            }
        });
        return Task.CompletedTask;
    }

}
