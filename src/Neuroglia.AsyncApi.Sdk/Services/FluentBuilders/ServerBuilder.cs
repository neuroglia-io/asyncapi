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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neuroglia.AsyncApi.Services.FluentBuilders
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IServerBuilder"/>
    /// </summary>
    public class ServerBuilder
        : IServerBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="ServerBuilder"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        /// <param name="validators">The services used to validate <see cref="Models.Server"/>s</param>
        public ServerBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<Server>> validators)
        {
            this.ServiceProvider = serviceProvider;
            this.Validators = validators;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected virtual IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the services used to validate <see cref="Models.Server"/>s
        /// </summary>
        protected virtual IEnumerable<IValidator<Server>> Validators { get; }

        /// <summary>
        /// Gets the <see cref="Models.Server"/> to configure
        /// </summary>
        protected virtual Server Server { get; } = new();

        /// <inheritdoc/>
        public virtual IServerBuilder WithUrl(Uri uri)
        {
            this.Server.Url = uri ?? throw new ArgumentNullException(nameof(uri));
            return this;
        }

        /// <inheritdoc/>
        public virtual IServerBuilder WithProtocol(string protocol, string version = null)
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
        public virtual IServerBuilder WithDescription(string description)
        {
            this.Server.Description = description;
            return this;
        }

        /// <inheritdoc/>
        public virtual IServerBuilder AddVariable(string name, Action<IVariableBuilder> setup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            IVariableBuilder builder = ActivatorUtilities.CreateInstance<VariableBuilder>(this.ServiceProvider);
            setup(builder);
            this.Server.Variables.Add(name, builder.Build());
            return this;
        }

        /// <inheritdoc/>
        public virtual IServerBuilder UseBinding(IServerBinding binding)
        {
            if (binding == null)
                throw new ArgumentNullException(nameof(binding));
            if (this.Server.Bindings == null)
                this.Server.Bindings = new();
            this.Server.Bindings.Add(binding);
            return this;
        }

        /// <inheritdoc/>
        public virtual IServerBuilder UseSecurityScheme(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            //TODO
            return this;
        }

        /// <inheritdoc/>
        public virtual Server Build()
        {
            IEnumerable<ValidationResult> validationResults = this.Validators.Select(v => v.Validate(this.Server));
            if (!validationResults.All(r => r.IsValid))
                throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
            return this.Server;
        }

    }

}
