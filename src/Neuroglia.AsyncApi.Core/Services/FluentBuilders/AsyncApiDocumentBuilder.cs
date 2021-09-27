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
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Models.Bindings;
using Newtonsoft.Json.Schema;
using SemVersion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neuroglia.AsyncApi.Services.FluentBuilders
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IAsyncApiDocumentBuilder"/>
    /// </summary>
    public class AsyncApiDocumentBuilder
        : IAsyncApiDocumentBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="AsyncApiDocumentBuilder"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        /// <param name="validators">The services used to validate <see cref="AsyncApiDocument"/>s</param>
        public AsyncApiDocumentBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<AsyncApiDocument>> validators)
        {
            this.ServiceProvider = serviceProvider;
            this.Validators = validators;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected virtual IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the services used to validate <see cref="AsyncApiDocument"/>s
        /// </summary>
        protected virtual IEnumerable<IValidator<AsyncApiDocument>> Validators { get; }

        /// <summary>
        /// Gets the <see cref="AsyncApiDocument"/> to configure
        /// </summary>
        protected virtual AsyncApiDocument Document { get; } = new();

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder UseAsyncApi(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentNullException(nameof(version));
            if (!SemanticVersion.TryParse(version, out _))
                throw new ArgumentException($"The specified value '{version}' is not a valid semantic version", nameof(version));
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
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));
            if (this.Document.Info == null)
                this.Document.Info = new();
            this.Document.Info.Title = title;
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder WithVersion(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentNullException(nameof(version));
            if (!SemanticVersion.TryParse(version, out _))
                throw new ArgumentException($"The specified value '{version}' is not a valid semantic version", nameof(version));
            if (this.Document.Info == null)
                this.Document.Info = new();
            this.Document.Info.Version = version;
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder WithDescription(string description)
        {
            if (this.Document.Info == null)
                this.Document.Info = new();
            this.Document.Info.Description = description;
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder WithTermsOfService(Uri uri)
        {
            if (this.Document.Info == null)
                this.Document.Info = new();
            this.Document.Info.TermsOfService = uri;
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder WithContact(string name, Uri uri, string email)
        {
            if (this.Document.Info == null)
                this.Document.Info = new();
            this.Document.Info.Contact = new() { Name = name, Url = uri, Email = email };
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder WithLicense(string name, Uri uri)
        {
            if (this.Document.Info == null)
                this.Document.Info = new();
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
        public virtual IAsyncApiDocumentBuilder TagWith(Action<ITagBuilder> setup)
        {
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            if (this.Document.Tags == null)
                this.Document.Tags = new();
            ITagBuilder builder = ActivatorUtilities.CreateInstance<TagBuilder>(this.ServiceProvider);
            setup(builder);
            this.Document.Tags.Add(builder.Build());
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder DocumentWith(Uri uri, string description = null)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            if (this.Document.ExternalDocs == null)
                this.Document.ExternalDocs = new();
            this.Document.ExternalDocs.Add(new() { Url = uri, Description = description });
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder UseServer(string name, Action<IServerBuilder> setup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            if (this.Document.Servers == null)
                this.Document.Servers = new();
            IServerBuilder builder = ActivatorUtilities.CreateInstance<ServerBuilder>(this.ServiceProvider);
            setup(builder);
            this.Document.Servers.Add(name, builder.Build());
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder UseChannel(string name, Action<IChannelBuilder> setup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            if (this.Document.Channels == null)
                this.Document.Channels = new();
            IChannelBuilder builder = ActivatorUtilities.CreateInstance<ChannelBuilder>(this.ServiceProvider);
            setup(builder);
            this.Document.Channels.Add(name, builder.Build());
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddSchema(string name, JSchema schema)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            if (this.Document.Components == null)
                this.Document.Components = new();
            if (this.Document.Components.Schemas == null)
                this.Document.Components.Schemas = new();
            this.Document.Components.Schemas.Add(name, schema);
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddMessage(string name, MessageDefinition message)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if(message == null)
                throw new ArgumentNullException(nameof(message));
            if (this.Document.Components == null)
                this.Document.Components = new();
            if (this.Document.Components.Messages == null)
                this.Document.Components.Messages = new();
            this.Document.Components.Messages.Add(name, message);
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddMessage(string name, Action<IMessageBuilder> setup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            IMessageBuilder builder = ActivatorUtilities.CreateInstance<MessageBuilder>(this.ServiceProvider);
            setup(builder);
            return this.AddMessage(name, builder.Build());
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddSecurityScheme(string name, SecuritySchemeDefinition scheme)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (scheme == null)
                throw new ArgumentNullException(nameof(scheme));
            if (this.Document.Components == null)
                this.Document.Components = new();
            if (this.Document.Components.SecuritySchemes == null)
                this.Document.Components.SecuritySchemes = new();
            this.Document.Components.SecuritySchemes.Add(name, scheme);
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddParameter(string name, ParameterDefinition parameter)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            if (this.Document.Components == null)
                this.Document.Components = new();
            if (this.Document.Components.Parameters == null)
                this.Document.Components.Parameters = new();
            this.Document.Components.Parameters.Add(name, parameter);
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddParameter(string name, Action<IParameterBuilder> setup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            IParameterBuilder builder = ActivatorUtilities.CreateInstance<ParameterBuilder>(this.ServiceProvider);
            setup(builder);
            return this.AddParameter(name, builder.Build());
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddCorrelationId(string name, CorrelationIdDefinition correlationId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (correlationId == null)
                throw new ArgumentNullException(nameof(correlationId));
            if (this.Document.Components == null)
                this.Document.Components = new();
            if (this.Document.Components.CorrelationIds == null)
                this.Document.Components.CorrelationIds = new();
            this.Document.Components.CorrelationIds.Add(name, correlationId);
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddOperationTrait(string name, OperationTraitDefinition trait)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (trait == null)
                throw new ArgumentNullException(nameof(trait));
            if (this.Document.Components == null)
                this.Document.Components = new();
            if (this.Document.Components.OperationTraits == null)
                this.Document.Components.OperationTraits = new();
            this.Document.Components.OperationTraits.Add(name, trait);
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddOperationTrait(string name, Action<IOperationTraitBuilder> setup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            IOperationTraitBuilder builder = ActivatorUtilities.CreateInstance<OperationTraitBuilder>(this.ServiceProvider);
            setup(builder);
            return this.AddOperationTrait(name, builder.Build());
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddMessageTrait(string name, MessageTraitDefinition trait)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (trait == null)
                throw new ArgumentNullException(nameof(trait));
            if (this.Document.Components == null)
                this.Document.Components = new();
            if (this.Document.Components.MessageTraits == null)
                this.Document.Components.MessageTraits = new();
            this.Document.Components.MessageTraits.Add(name, trait);
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddMessageTrait(string name, Action<IMessageTraitBuilder> setup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            IMessageTraitBuilder builder = ActivatorUtilities.CreateInstance<MessageTraitBuilder>(this.ServiceProvider);
            setup(builder);
            return this.AddMessageTrait(name, builder.Build());
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddServerBinding(string name, ServerBindingDefinitionCollection bindings)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (bindings == null)
                throw new ArgumentNullException(nameof(bindings));
            if (this.Document.Components == null)
                this.Document.Components = new();
            if (this.Document.Components.ServerBindings == null)
                this.Document.Components.ServerBindings = new();
            this.Document.Components.ServerBindings.Add(name, bindings);
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddChannelBinding(string name, ChannelBindingDefinitionCollection bindings)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (bindings == null)
                throw new ArgumentNullException(nameof(bindings));
            if (this.Document.Components == null)
                this.Document.Components = new();
            if (this.Document.Components.ChannelBindings == null)
                this.Document.Components.ChannelBindings = new();
            this.Document.Components.ChannelBindings.Add(name, bindings);
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddOperationBinding(string name, OperationBindingDefinitionCollection bindings)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (bindings == null)
                throw new ArgumentNullException(nameof(bindings));
            if (this.Document.Components == null)
                this.Document.Components = new();
            if (this.Document.Components.OperationBindings == null)
                this.Document.Components.OperationBindings = new();
            this.Document.Components.OperationBindings.Add(name, bindings);
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddMessageBinding(string name, MessageBindingCollection bindings)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (bindings == null)
                throw new ArgumentNullException(nameof(bindings));
            if (this.Document.Components == null)
                this.Document.Components = new();
            if (this.Document.Components.MessageBindings == null)
                this.Document.Components.MessageBindings = new();
            this.Document.Components.MessageBindings.Add(name, bindings);
            return this;
        }

        /// <inheritdoc/>
        public virtual AsyncApiDocument Build()
        {
            IEnumerable<ValidationResult> validationResults = this.Validators.Select(v => v.Validate(this.Document));
            if (!validationResults.All(r => r.IsValid))
                throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
            return this.Document;
        }

    }

}
