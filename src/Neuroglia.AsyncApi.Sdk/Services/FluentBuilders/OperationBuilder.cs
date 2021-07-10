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
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Sdk.Models;
using System;
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Represents the base class for all <see cref="IOperationBuilder"/> implementations
    /// </summary>
    public class OperationBuilder
        : OperationTraitBuilder<IOperationBuilder, Operation>, IOperationBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="OperationBuilder"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        /// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="OperationTrait"/>s</param>
        public OperationBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<OperationTrait>> validators)
            : base(serviceProvider, validators)
        {

        }

        /// <inheritdoc/>
        public virtual IOperationBuilder UseMessage(Action<IMessageBuilder> setup)
        {
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            IMessageBuilder builder = ActivatorUtilities.CreateInstance<MessageBuilder>(this.ServiceProvider);
            setup(builder);
            this.Trait.Message = builder.Build();
            return this;
        }

        /// <inheritdoc/>
        public virtual IOperationBuilder WithTrait(Action<IOperationTraitBuilder> setup)
        {
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            if (this.Trait.Traits == null)
                this.Trait.Traits = new();
            IOperationTraitBuilder builder = ActivatorUtilities.CreateInstance<OperationTraitBuilder>(this.ServiceProvider);
            setup(builder);
            this.Trait.Traits.Add(builder.Build());
            return this;
        }

    }

}
