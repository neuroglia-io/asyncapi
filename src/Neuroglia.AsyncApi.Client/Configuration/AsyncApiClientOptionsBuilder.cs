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
using Neuroglia.AsyncApi.Client.Services;
using System;
using System.Linq;

namespace Neuroglia.AsyncApi.Client.Configuration
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IAsyncApiClientOptionsBuilder"/> interface
    /// </summary>
    public class AsyncApiClientOptionsBuilder
        : IAsyncApiClientOptionsBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="AsyncApiClientOptionsBuilder"/>
        /// </summary>
        /// <param name="options">The <see cref="AsyncApiClientOptions"/> to configure</param>
        public AsyncApiClientOptionsBuilder(AsyncApiClientOptions options)
        {
            this.Options = options;
        }

        /// <summary>
        /// Initializes a new <see cref="AsyncApiClientOptionsBuilder"/>
        /// </summary>
        public AsyncApiClientOptionsBuilder()
            : this(new())
        {

        }

        /// <summary>
        /// Gets the <see cref="AsyncApiClientOptions"/> to configure
        /// </summary>
        protected AsyncApiClientOptions Options { get; }

        /// <inheritdoc/>
        public virtual IAsyncApiClientOptionsBuilder WithChannelFactory<TFactory>()
             where TFactory : class, IChannelFactory
        {
            this.Options.ChannelFactoryType = typeof(TFactory);
            return this;
        }

        /// <inheritdoc/>
        public virtual IAsyncApiClientOptionsBuilder WithChannelBindingFactory<TFactory>()
            where TFactory : class, IChannelBindingFactory
        {
            if(!this.Options.ChannelBindingFactoryTypes.Contains(typeof(TFactory)))
                this.Options.ChannelBindingFactoryTypes.Add(typeof(TFactory));
            return this;
        }

        /// <inheritdoc/>
        public virtual AsyncApiClientOptions Build()
        {
            if (this.Options.ChannelFactoryType == null)
                throw new NullReferenceException("The channel factory must be configured");
            if (!this.Options.ChannelBindingFactoryTypes.Any())
                throw new NullReferenceException("At least one channel binding factory type must be configured");
            return this.Options;
        }

    }

}
