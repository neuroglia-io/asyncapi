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
using Neuroglia.AsyncApi.Sdk.Models;
using SemVersion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neuroglia.AsyncApi.Sdk.Services.FluentBuilders
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
        protected virtual AsyncApiDocument Document => new AsyncApiDocument();

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
        public virtual IAsyncApiDocumentBuilder TagWith(Action<ITagDefinitionBuilder> setup)
        {
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            if (this.Document.Tags == null)
                this.Document.Tags = new();
            ITagDefinitionBuilder builder = ActivatorUtilities.CreateInstance<TagDefinitionBuilder>(this.ServiceProvider);
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
        public virtual IAsyncApiDocumentBuilder AddServer(string name, Action<IServerDefinitionBuilder> setup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            if (this.Document.Servers == null)
                this.Document.Servers = new();
            IServerDefinitionBuilder builder = ActivatorUtilities.CreateInstance<ServerDefinitionBuilder>(this.ServiceProvider);
            setup(builder);
            this.Document.Servers.Add(name, builder.Build());
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiDocumentBuilder AddChannel(string name, Action<IChannelDefinitionBuilder> setup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            if (this.Document.Channels == null)
                this.Document.Channels = new();
            IChannelDefinitionBuilder builder = ActivatorUtilities.CreateInstance<ChannelDefinitionBuilder>(this.ServiceProvider);
            setup(builder);
            this.Document.Channels.Add(name, builder.Build());
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
