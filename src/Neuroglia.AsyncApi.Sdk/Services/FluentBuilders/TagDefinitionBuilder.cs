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
    /// Represents the default implementation of the <see cref="ITagDefinitionBuilder"/>
    /// </summary>
    public class TagDefinitionBuilder
        : ITagDefinitionBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="TagDefinitionBuilder"/>
        /// </summary>
        /// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="TagDefinition"/>s</param>
        public TagDefinitionBuilder(IEnumerable<IValidator<TagDefinition>> validators)
        {
            this.Validators = validators;
        }

        /// <summary>
        /// Gets the services used to validate <see cref="TagDefinition"/>s
        /// </summary>
        protected virtual IEnumerable<IValidator<TagDefinition>> Validators { get; }

        /// <summary>
        /// Gets the <see cref="TagDefinition"/> to configure
        /// </summary>
        protected virtual TagDefinition Tag => new();

        /// <inheritdoc/>
        public virtual ITagDefinitionBuilder WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            this.Tag.Name = name;
            return this;
        }

        /// <inheritdoc/>
        public virtual ITagDefinitionBuilder WithDescription(string description)
        {
            this.Tag.Description = description;
            return this;
        }

        /// <inheritdoc/>
        public virtual ITagDefinitionBuilder DocumentWith(Uri uri, string description = null)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            if (this.Tag.ExternalDocs == null)
                this.Tag.ExternalDocs = new();
            this.Tag.ExternalDocs.Add(new() { Url = uri, Description = description });
            return this;
        }

        /// <inheritdoc/>
        public virtual TagDefinition Build()
        {
            IEnumerable<ValidationResult> validationResults = this.Validators.Select(v => v.Validate(this.Tag));
            if (!validationResults.All(r => r.IsValid))
                throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors));
            return this.Tag;
        }

    }

}
