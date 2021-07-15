/*
 * Copyright © 2021 Neuroglia SPRL. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Configuration;
using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Services.FluentBuilders;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Services.Generators
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IAsyncApiDocumentGenerator"/> interface
    /// </summary>
    public class AsyncApiDocumentGenerator
        : IAsyncApiDocumentGenerator
    {

        /// <summary>
        /// Initializes a new <see cref="AsyncApiDocumentGenerator"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        public AsyncApiDocumentGenerator(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<AsyncApiDocument>> GenerateAsync(IEnumerable<Type> markupTypes, AsyncApiDocumentGenerationOptions options)
        {
            if (markupTypes == null)
                throw new ArgumentNullException(nameof(markupTypes));
            IEnumerable<Type> types = markupTypes
                .Select(t => t.Assembly)
                .Distinct()
                .SelectMany(t => t.GetTypes()).Where(t => t.GetCustomAttribute<AsyncApiAttribute>() != null);
            List<AsyncApiDocument> documents = new(types.Count());
            foreach (Type type in types)
            {
                documents.Add(await this.GenerateDocumentForAsync(type, options));
            }
            return documents;
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<AsyncApiDocument>> GenerateAsync(params Type[] markupTypes)
        {
            return await this.GenerateAsync(markupTypes, null);
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
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            AsyncApiAttribute asyncApi = type.GetCustomAttribute<AsyncApiAttribute>();
            IAsyncApiDocumentBuilder builder = this.ServiceProvider.GetRequiredService<IAsyncApiDocumentBuilder>();
            options.DefaultConfiguration?.Invoke(builder);
            builder
                .WithId(asyncApi.Id)
                .WithTitle(asyncApi.Title)
                .WithVersion(asyncApi.Version);
            if (!string.IsNullOrWhiteSpace(asyncApi.Description))
                builder.WithDescription(asyncApi.Description);
            if (!string.IsNullOrWhiteSpace(asyncApi.LicenseName)
                && !string.IsNullOrWhiteSpace(asyncApi.LicenseUrl))
                builder.WithLicense(asyncApi.LicenseName, new Uri(asyncApi.LicenseUrl, UriKind.RelativeOrAbsolute));
            if (!string.IsNullOrWhiteSpace(asyncApi.TermsOfServiceUrl))
                builder.WithTermsOfService(new Uri(asyncApi.TermsOfServiceUrl, UriKind.RelativeOrAbsolute));
            if(!string.IsNullOrWhiteSpace(asyncApi.ContactName))
                builder.WithContact(asyncApi.ContactName, string.IsNullOrWhiteSpace(asyncApi.ContactUrl) ? null : new Uri(asyncApi.ContactUrl, UriKind.RelativeOrAbsolute), asyncApi.ContactEmail);
            foreach (IGrouping<string, MethodInfo> operationsPerChannel in type.GetMethods(BindingFlags.Default | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .Where(m => m.GetCustomAttribute<OperationAttribute>() != null
                    && m.GetCustomAttribute<ChannelAttribute>() != null)
                .GroupBy(m => m.GetCustomAttribute<ChannelAttribute>().Name))
            {
                ChannelAttribute channel = operationsPerChannel.First().GetCustomAttribute<ChannelAttribute>();
                await ConfigureChannelForAsync(builder, channel, operationsPerChannel.ToList(), options, cancellationToken);
            }
            return builder.Build();
        }

        /// <summary>
        /// Builds a new <see cref="Channel"/>
        /// </summary>
        /// <param name="builder">The <see cref="IAsyncApiDocumentBuilder"/> to configure</param>
        /// <param name="channel">The attribute used to describe the <see cref="Channel"/> to configure</param>
        /// <param name="methods">A <see cref="List{T}"/> containing the <see cref="Channel"/>'s <see cref="Operation"/>s <see cref="MethodInfo"/>s</param>
        /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        protected virtual async Task ConfigureChannelForAsync(IAsyncApiDocumentBuilder builder, ChannelAttribute channel, List<MethodInfo> methods, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));
            if (methods == null)
                throw new ArgumentNullException(nameof(methods));
            builder.UseChannel(channel.Name, channelBuilder =>
            {
                channelBuilder.WithDescription(channel.Description);
                foreach (MethodInfo method in methods)
                {
                    OperationAttribute operation = method.GetCustomAttribute<OperationAttribute>();
                    channelBuilder.DefineOperation(operation.OperationType, operationBuilder =>
                    {
                        string operationId = operation.OperationId;
                        if (string.IsNullOrWhiteSpace(operationId))
                            operationId = (method.Name.EndsWith("Async") ? method.Name[..^5] : method.Name).ToCamelCase();
                        string description = operation.Description;
                        if (string.IsNullOrWhiteSpace(description))
                            description = method.GetDocumentationSummary();
                        string summary = operation.Summary;
                        if (string.IsNullOrWhiteSpace(summary))
                            summary = description;
                        operationBuilder
                            .WithOperationId(operationId)
                            .WithDescription(description)
                            .WithSummary(summary);
                        foreach (TagAttribute tag in method.GetCustomAttributes<TagAttribute>())
                        {
                            operationBuilder.TagWith(tagBuilder => tagBuilder
                                .WithName(tag.Name)
                                .WithDescription(tag.Description));
                        }
                        operationBuilder.UseMessage(messageBuilder => this.ConfigureOperationMessageFor(messageBuilder, operation, method, options));
                    });
                }
            });
            await Task.CompletedTask;
        }

        /// <summary>
        /// Configures and builds a new <see cref="Message"/> for the specified <see cref="Operation"/>
        /// </summary>
        /// <param name="messageBuilder">The <see cref="IMessageBuilder"/> to configure</param>
        /// <param name="operation">The attribute used to describe the <see cref="Operation"/> to configure</param>
        /// <param name="operationMethod">The <see cref="MethodInfo"/> marked with the specified <see cref="Operation"/> attribute</param>
        /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
        protected virtual void ConfigureOperationMessageFor(IMessageBuilder messageBuilder, OperationAttribute operation, MethodInfo operationMethod, AsyncApiDocumentGenerationOptions options)
        {
            if (messageBuilder == null)
                throw new ArgumentNullException(nameof(messageBuilder));
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));
            if (operationMethod == null)
                throw new ArgumentNullException(nameof(operationMethod));
            Type messageType = operation.MessageType;
            ParameterInfo[] parameters = operationMethod.GetParameters();
            if (messageType == null)
                if (parameters.Length == 1)
                    messageType = parameters.First().ParameterType;
                else
                    return;
            messageBuilder.OfType(messageType);
            MessageAttribute message = operationMethod.GetCustomAttribute<MessageAttribute>();
            if (message == null)
                message = messageType.GetCustomAttribute<MessageAttribute>();
            string name = message?.Name;
            if (string.IsNullOrWhiteSpace(name))
                name = messageType.Name.ToCamelCase();
            string title = message?.Title;
            if (string.IsNullOrWhiteSpace(title))
                title = name?.SplitCamelCase(false, true);
            string description = message?.Description;
            if (string.IsNullOrWhiteSpace(description))
                description = messageType.GetDocumentationSummary();
            string summary = message?.Summary;
            if (string.IsNullOrWhiteSpace(summary))
                summary = description;
            string contentType = message?.ContentType;
            messageBuilder
                .WithName(name)
                .WithTitle(title)
                .WithSummary(summary)
                .WithDescription(description)
                .WithContentType(contentType);
            foreach (TagAttribute tag in messageType.GetCustomAttributes<TagAttribute>())
            {
                messageBuilder.TagWith(tagBuilder => tagBuilder
                    .WithName(tag.Name)
                    .WithDescription(tag.Description));
            }
            if (options.AutomaticallyGenerateExamples)
            {
                foreach(KeyValuePair<string, JObject> example in this.GenerateExamplesFor(messageType))
                {
                    messageBuilder.AddExample(example.Key, example.Value);
                }
            }
        }

        /// <summary>
        /// Generates examples for the specified data type
        /// </summary>
        /// <param name="type">The type to generate examples for</param>
        /// <returns>A new <see cref="Dictionary{TKey, TValue}"/> containing the generated examples mapped by name</returns>
        protected virtual Dictionary<string, JObject> GenerateExamplesFor(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            return new JSchemaGenerator().Generate(type).GenerateExamples();
        }

    }

}