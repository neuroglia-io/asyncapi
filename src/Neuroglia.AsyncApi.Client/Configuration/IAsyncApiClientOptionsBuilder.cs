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

namespace Neuroglia.AsyncApi.Client.Configuration
{

    /// <summary>
    /// Defines the fundamentals of the service used to build <see cref="AsyncApiClientOptions"/>s
    /// </summary>
    public interface IAsyncApiClientOptionsBuilder
    {

        /// <summary>
        /// Configures the <see cref="IAsyncApiClient"/> to use the specified <see cref="IChannelFactory"/>
        /// </summary>
        /// <typeparam name="TFactory">The type of <see cref="IChannelFactory"/> to use</typeparam>
        /// <returns>The configured <see cref="IAsyncApiClientOptionsBuilder"/></returns>
        IAsyncApiClientOptionsBuilder WithChannelFactory<TFactory>()
            where TFactory : class, IChannelFactory;

        /// <summary>
        /// Configures the <see cref="IAsyncApiClient"/> to use the specified <see cref="IChannelFactory"/>
        /// </summary>
        /// <typeparam name="TFactory">The type of <see cref="IChannelBindingFactory"/> to use</typeparam>
        /// <returns>The configured <see cref="IAsyncApiClientOptionsBuilder"/></returns>
        IAsyncApiClientOptionsBuilder WithChannelBindingFactory<TFactory>()
            where TFactory : class, IChannelBindingFactory;

        /// <summary>
        /// Builds the <see cref="AsyncApiClientOptions"/>
        /// </summary>
        /// <returns>New <see cref="AsyncApiClientOptions"/></returns>
        AsyncApiClientOptions Build();

    }

}
