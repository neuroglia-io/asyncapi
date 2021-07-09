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
using Neuroglia.AsyncApi.Sdk.Models.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neuroglia.AsyncApi.Sdk.Services.FluentBuilders
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IServerDefinitionBuilder"/>
    /// </summary>
    public class ServerDefinitionBuilder
        : IServerDefinitionBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="ServerDefinitionBuilder"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        /// <param name="validators">The services used to validate <see cref="ServerDefinition"/>s</param>
        public ServerDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<ServerDefinition>> validators)
        {
            this.ServiceProvider = serviceProvider;
            this.Validators = validators;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected virtual IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the services used to validate <see cref="ServerDefinition"/>s
        /// </summary>
        protected virtual IEnumerable<IValidator<ServerDefinition>> Validators { get; }

        /// <summary>
        /// Gets the <see cref="ServerDefinition"/> to configure
        /// </summary>
        protected virtual ServerDefinition Server => new();

        /// <inheritdoc/>
        public virtual IServerDefinitionBuilder WithUrl(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            this.Server.Url = uri;
            return this;
        }

        /// <inheritdoc/>
        public virtual IServerDefinitionBuilder WithProtocol(string protocol, string version)
        {
            if (string.IsNullOrWhiteSpace(protocol))
                throw new ArgumentNullException(nameof(protocol));
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentNullException(nameof(version));
            this.Server.Protocol = protocol;
            this.Server.ProtocolVersion = version;
            return this;
        }

        /// <inheritdoc/>
        public virtual IServerDefinitionBuilder WithDescription(string description)
        {
            this.Server.Description = description;
            return this;
        }

        /// <inheritdoc/>
        public virtual IServerDefinitionBuilder AddVariable(string name, Action<IVariableDefinitionBuilder> setup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            IVariableDefinitionBuilder builder = ActivatorUtilities.CreateInstance<VariableDefinitionBuilder>(this.ServiceProvider);
            setup(builder);
            this.Server.Variables.Add(name, builder.Build());
            return this;
        }

        /// <inheritdoc/>
        public virtual IServerDefinitionBuilder AddBinding(IServerBindingDefinition binding)
        {
            if (binding == null)
                throw new ArgumentNullException(nameof(binding));
            if (this.Server.Bindings == null)
                this.Server.Bindings = new();
            this.Server.Bindings.Add(binding);
            return this;
        }

        /// <inheritdoc/>
        public virtual IServerDefinitionBuilder AddSecurity(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            //TODO
            return this;
        }

        /// <inheritdoc/>
        public virtual ServerDefinition Build()
        {
            IEnumerable<ValidationResult> validationResults = this.Validators.Select(v => v.Validate(this.Server));
            if (!validationResults.All(r => r.IsValid))
                throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
            return this.Server;
        }

    }

}
