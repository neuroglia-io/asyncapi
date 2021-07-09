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
using Neuroglia.AsyncApi.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neuroglia.AsyncApi.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IChannelDefinitionBuilder"/>
    /// </summary>
    public class ChannelDefinitionBuilder
        : IChannelDefinitionBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="ChannelDefinitionBuilder"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        /// <param name="validators">The services used to validate <see cref="ChannelDefinition"/>s</param>
        public ChannelDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<ChannelDefinition>> validators)
        {
            this.ServiceProvider = serviceProvider;
            this.Validators = validators;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected virtual IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the services used to validate <see cref="ChannelDefinition"/>s
        /// </summary>
        protected virtual IEnumerable<IValidator<ChannelDefinition>> Validators { get; }

        /// <summary>
        /// Gets the <see cref="ChannelDefinition"/> to build
        /// </summary>
        protected ChannelDefinition Channel = new();

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
