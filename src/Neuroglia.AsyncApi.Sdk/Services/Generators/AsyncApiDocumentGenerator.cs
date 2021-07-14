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
using Newtonsoft.Json.Schema;
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
                .WithVersion(asyncApi.Version)
                .WithDescription(asyncApi.Description)
                .WithLicense(asyncApi.LicenseName, string.IsNullOrWhiteSpace(asyncApi.LicenseUrl) ? null : new Uri(asyncApi.LicenseUrl, UriKind.RelativeOrAbsolute))
                .WithTermsOfService(string.IsNullOrWhiteSpace(asyncApi.TermsOfServiceUrl) ? null : new Uri(asyncApi.TermsOfServiceUrl, UriKind.RelativeOrAbsolute))
                .WithContact(asyncApi.ContactName, string.IsNullOrWhiteSpace(asyncApi.ContactUrl) ? null : new Uri(asyncApi.ContactUrl, UriKind.RelativeOrAbsolute), asyncApi.ContactEmail);
            foreach (IGrouping<string, MethodInfo> channelOperations in type.GetMethods(BindingFlags.Default | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .Where(m => m.GetCustomAttribute<OperationAttribute>() != null
                    && m.GetCustomAttribute<ChannelAttribute>() != null)
                .GroupBy(m => m.GetCustomAttribute<ChannelAttribute>().Name))
            {
                ChannelAttribute channel = channelOperations.First().GetCustomAttribute<ChannelAttribute>();
                await ConfigureChannelForAsync(builder, channel, channelOperations.ToList(), options, cancellationToken);
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
            builder.UseChannel(channel.Name, builder =>
            {
                builder.WithDescription(channel.Description);
                foreach (MethodInfo method in methods)
                {
                    OperationAttribute operation = method.GetCustomAttribute<OperationAttribute>();
                    builder.DefineOperation(operation.OperationType, builder =>
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
                        builder
                            .WithOperationId(operationId)
                            .WithDescription(description)
                            .WithSummary(summary);
                        foreach (TagAttribute tag in method.GetCustomAttributes<TagAttribute>())
                        {
                            builder.TagWith(tagBuilder => tagBuilder
                                .WithName(tag.Name)
                                .WithDescription(tag.Description));
                        }
                        builder.UseMessage(messageBuilder => this.ConfigureOperationMessageFor(messageBuilder, operation, method, options));
                    });
                }
            });
            await Task.CompletedTask;
        }

        /// <summary>
        /// Configures and builds a new <see cref="Message"/> for the specified <see cref="Operation"/>
        /// </summary>
        /// <param name="builder">The <see cref="IMessageBuilder"/> to configure</param>
        /// <param name="operation">The attribute used to describe the <see cref="Operation"/> to configure</param>
        /// <param name="operationMethod">The <see cref="MethodInfo"/> marked with the specified <see cref="Operation"/> attribute</param>
        /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
        protected virtual void ConfigureOperationMessageFor(IMessageBuilder builder, OperationAttribute operation, MethodInfo operationMethod, AsyncApiDocumentGenerationOptions options)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
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
            builder.OfType(messageType);
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
            builder
                .WithName(name)
                .WithTitle(title)
                .WithSummary(summary)
                .WithDescription(description)
                .WithContentType(contentType);
            foreach (TagAttribute tag in messageType.GetCustomAttributes<TagAttribute>())
            {
                builder.TagWith(tagBuilder => tagBuilder
                    .WithName(tag.Name)
                    .WithDescription(tag.Description));
            }
            if (options.AutomaticallyGenerateExamples)
            {
                foreach(KeyValuePair<string, JObject> example in this.GenerateExamplesFor(messageType))
                {
                    builder.AddExample(example.Key, example.Value);
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
            JSchema schema = new JSchemaGenerator().Generate(type);
            Dictionary<string, JObject> examples = new();
            JObject minimalExample = this.GenerateExampleObjectFor(schema, "minimal", true);
            if (schema.Properties.All(p => schema.Required.Contains(p.Key)))
            {
                examples.Add("Default payload", minimalExample);
            }
            else
            {
                JObject extendedExample = this.GenerateExampleObjectFor(schema, "extended", false);
                examples.Add("Minimal payload", minimalExample);
                examples.Add("Extended payload", extendedExample);
            }
            return examples;
        }

        protected virtual JToken GenerateExampleTokenFor(JSchema schema, string name, bool requiredPropertiesOnly)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            if (!schema.Type.HasValue)
                return null;
            JSchemaType schemaType = schema.Type.Value;
            if (schemaType.HasFlag(JSchemaType.Null))
                if(requiredPropertiesOnly)
                    return null;
                else
                    schemaType &= ~JSchemaType.Null;
            if (schemaType.HasFlag(JSchemaType.None))
                schemaType &= ~JSchemaType.None;
            switch (schemaType)
            {
                case JSchemaType.Array:
                    return this.GenerateExampleArrayFor(schema, name, requiredPropertiesOnly);
                case JSchemaType.String:
                    return this.GenerateExampleStringFor(schema, name);
                case JSchemaType.Boolean:
                    return JToken.FromObject(new Random().Next(0, 1) == 1 ? true : false);
                case JSchemaType.Integer:
                    return this.GenerateExampleIntegerFor(schema);
                case JSchemaType.Object:
                    return this.GenerateExampleObjectFor(schema, name, requiredPropertiesOnly);
                default:
                    return null;
            }
        }

        protected virtual JObject GenerateExampleObjectFor(JSchema schema, string name, bool requiredPropertiesOnly)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            JObject example = new();
            if (!schema.Properties.Any())
            {
                schema.Properties.Add("property1", new JSchema() { Type = JSchemaType.String });
                schema.Properties.Add("property2", new JSchema() { Type = JSchemaType.Integer });
                schema.Properties.Add("property3", new JSchema() { Type = JSchemaType.Boolean });
            }
            foreach (KeyValuePair<string, JSchema> property in schema.Properties.Where(kvp => requiredPropertiesOnly ? schema.Required.Contains(kvp.Key) : true))
            {
                example.Add(property.Key, this.GenerateExampleTokenFor(property.Value, property.Key, requiredPropertiesOnly));
            }
            return example;
        }

        protected virtual JArray GenerateExampleArrayFor(JSchema schema, string name, bool requiredPropertiesOnly)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            int min = 1;
            int max = 3;
            if (schema.MinimumItems.HasValue
                && schema.MinimumItems.Value > 0)
                min = (int)schema.MinimumItems.Value;
            if (schema.MaximumItems.HasValue
             && schema.MaximumItems.Value > min 
             && schema.MaximumItems.Value < 5)
                max = (int)schema.MaximumItems.Value;
            int itemsCount = new Random().Next(min, max);
            JArray array = new JArray();
            for(int i = 0; i < itemsCount; i++)
            {
                array.Add(this.GenerateExampleTokenFor(schema, string.Empty, requiredPropertiesOnly));
            }
            return array;
        }

        protected virtual JToken GenerateExampleIntegerFor(JSchema schema)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            int min = 0;
            int max = int.MaxValue;
            if (schema.Minimum.HasValue)
                min = (int)schema.Minimum.Value;
            if (schema.Maximum.HasValue)
                max = (int)schema.Maximum.Value;
            return JToken.FromObject(new Random().Next(min, max));
        }

        protected virtual JToken GenerateExampleStringFor(JSchema schema, string name)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            switch (schema.Format)
            {
                case "date":
                case "date-time":
                    return JToken.FromObject(DateTime.Now);
                case "email":
                    return "fake@email.com";
                default:
                    return name;
            }
        }

    }

}
