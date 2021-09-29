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
    /// Represents the base class for all <see cref="IOperationTraitDefinitionBuilder{TBuilder, TTrait}"/> implementations
    /// </summary>
    /// <typeparam name="TBuilder">The type of <see cref="IOperationTraitDefinitionBuilder{TBuilder, TTrait}"/> to return for chaining purposes</typeparam>
    /// <typeparam name="TTrait">The type of <see cref="OperationTraitDefinition"/> to build</typeparam>
    public abstract class OperationTraitDefinitionBuilder<TBuilder, TTrait>
        : IOperationTraitDefinitionBuilder<TBuilder, TTrait>
        where TBuilder : IOperationTraitDefinitionBuilder<TBuilder, TTrait>
        where TTrait : OperationTraitDefinition, new()
    {

        /// <summary>
        /// Initializes a new <see cref="OperationTraitDefinitionBuilder{TBuilder, TTrait}"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        /// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="OperationTraitDefinition"/>s</param>
        protected OperationTraitDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<TTrait>> validators)
        {
            this.ServiceProvider = serviceProvider;
            this.Validators = validators;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="OperationTraitDefinition"/>s
        /// </summary>
        protected IEnumerable<IValidator<TTrait>> Validators { get; }

        /// <summary>
        /// Gets the <see cref="MessageTraitDefinition"/> to configure
        /// </summary>
        protected virtual TTrait Trait { get; } = new();

        /// <inheritdoc/>
        public virtual TBuilder DocumentWith(Uri uri, string description = null)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            if (this.Trait.ExternalDocs == null)
                this.Trait.ExternalDocs = new();
            this.Trait.ExternalDocs.Add(new ExternalDocumentationDefinition() { Url = uri, Description = description });
            return (TBuilder)(object)this;
        }

        /// <inheritdoc/>
        public virtual TBuilder TagWith(Action<ITagDefinitionBuilder> setup)
        {
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            if (this.Trait.Tags == null)
                this.Trait.Tags = new();
            ITagDefinitionBuilder builder = ActivatorUtilities.CreateInstance<TagDefinitionBuilder>(this.ServiceProvider);
            setup(builder);
            this.Trait.Tags.Add(builder.Build());
            return (TBuilder)(object)this;
        }

        /// <inheritdoc/>
        public virtual TBuilder UseBinding(IOperationBindingDefinition binding)
        {
            if (binding == null)
                throw new ArgumentNullException(nameof(binding));
            if (this.Trait.Bindings == null)
                this.Trait.Bindings = new();
            this.Trait.Bindings.Add(binding);
            return (TBuilder)(object)this;
        }

        /// <inheritdoc/>
        public virtual TBuilder WithDescription(string description)
        {
            this.Trait.Description = description;
            return (TBuilder)(object)this;
        }

        /// <inheritdoc/>
        public virtual TBuilder WithOperationId(string operationId)
        {
            this.Trait.OperationId = operationId;
            return (TBuilder)(object)this;
        }

        /// <inheritdoc/>
        public virtual TBuilder WithSummary(string summary)
        {
            this.Trait.Summary = summary;
            return (TBuilder)(object)this;
        }

        /// <inheritdoc/>
        public virtual TTrait Build()
        {
            IEnumerable<ValidationResult> validationResults = this.Validators.Select(v => v.Validate(this.Trait));
            if (!validationResults.All(r => r.IsValid))
                throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
            return this.Trait;
        }

    }

    /// <summary>
    /// Represents the base class for all <see cref="IOperationTraitBuilder"/> implementations
    /// </summary>
    public class OperationTraitBuilder
        : OperationTraitDefinitionBuilder<IOperationTraitBuilder, OperationTraitDefinition>, IOperationTraitBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="OperationTraitBuilder"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        /// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="OperationTraitDefinition"/>s</param>
        public OperationTraitBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<OperationTraitDefinition>> validators)
            : base(serviceProvider, validators)
        {

        }

    }

}
