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
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neuroglia.AsyncApi.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IParameterBuilder"/> interface
    /// </summary>
    public class ParameterBuilder
        : IParameterBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="ParameterBuilder"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        /// <param name="validators">The services used to validate <see cref="Models.Parameter"/>s</param>
        public ParameterBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<Parameter>> validators)
        {
            this.ServiceProvider = serviceProvider;
            this.Validators = validators;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected virtual IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the services used to validate <see cref="Models.Parameter"/>s
        /// </summary>
        protected virtual IEnumerable<IValidator<Parameter>> Validators { get; }

        /// <summary>
        /// Gets the <see cref="Models.Parameter"/> to configure
        /// </summary>
        protected virtual Parameter Parameter { get; } = new();

        /// <inheritdoc/>
        public virtual IParameterBuilder OfType<TParameter>()
        {
            return this.OfType(typeof(TParameter));
        }

        /// <inheritdoc/>
        public virtual IParameterBuilder OfType(Type parameterType)
        {
            if (parameterType == null)
                throw new ArgumentNullException(nameof(parameterType));
            return this.WithSchema(new JSchemaGenerator().Generate(parameterType));
        }

        /// <inheritdoc/>
        public virtual IParameterBuilder WithSchema(JSchema schema)
        {
            this.Parameter.Schema = schema ?? throw new ArgumentNullException(nameof(schema));
            return this;
        }

        /// <inheritdoc/>
        public virtual IParameterBuilder WithDescription(string description)
        {
            this.Parameter.Description = description;
            return this;
        }

        /// <inheritdoc/>
        public virtual IParameterBuilder AtLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentNullException(nameof(location));
            this.Parameter.Location = location;
            return this;
        }

        /// <inheritdoc/>
        public virtual Parameter Build()
        {
            IEnumerable<ValidationResult> validationResults = this.Validators.Select(v => v.Validate(this.Parameter));
            if (!validationResults.All(r => r.IsValid))
                throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
            return this.Parameter;
        }

    }

}
