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
    /// Represents the default implementation of the <see cref="IChannelBuilder"/>
    /// </summary>
    public class ChannelBuilder
        : IChannelBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="ChannelBuilder"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        /// <param name="validators">The services used to validate <see cref="Models.ChannelDefinition"/>s</param>
        public ChannelBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<ChannelDefinition>> validators)
        {
            this.ServiceProvider = serviceProvider;
            this.Validators = validators;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected virtual IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the services used to validate <see cref="Models.ChannelDefinition"/>s
        /// </summary>
        protected virtual IEnumerable<IValidator<ChannelDefinition>> Validators { get; }

        /// <summary>
        /// Gets the <see cref="Models.ChannelDefinition"/> to build
        /// </summary>
        protected virtual ChannelDefinition Channel { get; } = new();

        /// <inheritdoc/>
        public virtual IChannelBuilder WithDescription(string description)
        {
            this.Channel.Description = description;
            return this;
        }

        /// <inheritdoc/>
        public virtual IChannelBuilder AddParameter(string name, Action<IParameterBuilder> setup)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if(setup == null)
                throw new ArgumentNullException(nameof(setup));
            if (this.Channel.Parameters == null)
                this.Channel.Parameters = new();
            IParameterBuilder builder = ActivatorUtilities.CreateInstance<ParameterBuilder>(this.ServiceProvider);
            setup(builder);
            this.Channel.Parameters.Add(name, builder.Build());
            return this;
        }

        /// <inheritdoc/>
        public virtual IChannelBuilder UseBinding(IChannelBindingDefinition binding)
        {
            if (binding == null)
                throw new ArgumentNullException(nameof(binding));
            if (this.Channel.Bindings == null)
                this.Channel.Bindings = new();
            this.Channel.Bindings.Add(binding);
            return this;
        }

        /// <inheritdoc/>
        public virtual IChannelBuilder DefineOperation(OperationType type, Action<IOperationBuilder> setup)
        {
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            IOperationBuilder builder = ActivatorUtilities.CreateInstance<OperationBuilder>(this.ServiceProvider);
            setup(builder);
            switch (type)
            {
                case OperationType.Publish:
                    this.Channel.Publish = builder.Build();
                    break;
                case OperationType.Subscribe:
                    this.Channel.Subscribe = builder.Build();
                    break;
                default:
                    throw new NotSupportedException($"The specified operation type '{type}' is not supported");
            }
            return this;
        }

        /// <inheritdoc/>
        public virtual IChannelBuilder DefineSubscribeOperation(Action<IOperationBuilder> setup)
        {
            return this.DefineOperation(OperationType.Subscribe, setup);
        }

        /// <inheritdoc/>
        public virtual IChannelBuilder DefinePublishOperation(Action<IOperationBuilder> setup)
        {
            return this.DefineOperation(OperationType.Publish, setup);
        }

        /// <inheritdoc/>
        public virtual ChannelDefinition Build()
        {
            IEnumerable<ValidationResult> validationResults = this.Validators.Select(v => v.Validate(this.Channel));
            if (!validationResults.All(r => r.IsValid))
                throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
            return this.Channel;
        }

    }

}
