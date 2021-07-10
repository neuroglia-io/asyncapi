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
using Neuroglia.AsyncApi.Services.FluentBuilders;
using System;

namespace Neuroglia.AsyncApi.Configuration
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IAsyncApiGenerationOptionsBuilder"/> interface
    /// </summary>
    public class AsyncApiGenerationOptionsBuilder
        : IAsyncApiGenerationOptionsBuilder
    {

        /// <summary>
        /// Gets the <see cref="AsyncApiGenerationOptions"/> to configure
        /// </summary>
        protected AsyncApiGenerationOptions Options { get; } = new();

        /// <inheritdoc/>
        public virtual IAsyncApiGenerationOptionsBuilder WithMarkupType<TMarkup>()
        {
            return this.WithMarkupType(typeof(TMarkup));
        }

        /// <inheritdoc/>
        public virtual IAsyncApiGenerationOptionsBuilder WithMarkupType(Type markupType)
        {
            if (markupType == null)
                throw new ArgumentNullException(nameof(markupType));
            this.Options.MarkupTypes.Add(markupType);
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiGenerationOptionsBuilder UsePathPrefix(string prefix)
        {
            if(string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentNullException(nameof(prefix));
            this.Options.PathPrefix = prefix;
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiGenerationOptionsBuilder UseDefaultConfiguration(Action<IAsyncApiDocumentBuilder> configurationAction)
        {
            this.Options.DefaultDocumentConfiguration = configurationAction;
            return this;
        }

        /// <inheritdoc/>
        public virtual AsyncApiGenerationOptions Build()
        {
            return this.Options;
        }

    }

}
