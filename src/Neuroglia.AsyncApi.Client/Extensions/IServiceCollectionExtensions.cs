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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Neuroglia.AsyncApi.Client.Configuration;
using Neuroglia.AsyncApi.Client.Services;
using System;
using System.Linq;

namespace Neuroglia.AsyncApi
{

    /// <summary>
    /// Defines extensions for <see cref="IServiceCollection"/>s
    /// </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary>
        /// Adds and configures a new <see cref="IAsyncApiClientFactory"/>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to configure the <see cref="IAsyncApiClient"/> to add</param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddAsyncApiClientFactory(this IServiceCollection services, Action<IAsyncApiClientOptionsBuilder> setup)
        {
            IAsyncApiClientOptionsBuilder optionsBuilder = new AsyncApiClientOptionsBuilder();
            setup(optionsBuilder);
            AsyncApiClientOptions options = optionsBuilder.Build();

            services.AddAsyncApi();
            services.TryAddSingleton<AsyncApiClientFactory>();
            services.TryAddSingleton<IAsyncApiClientFactory>(provider => provider.GetRequiredService<AsyncApiClientFactory>());

            if (!services.Any(d => d.ServiceType == typeof(IChannelFactory) && d.ImplementationType == options.ChannelFactoryType))
                services.Add(new(typeof(IChannelFactory), options.ChannelFactoryType, ServiceLifetime.Singleton));
            foreach (Type channelBindingFactoryType in options.ChannelBindingFactoryTypes)
            {
                if (!services.Any(d => d.ServiceType == typeof(IChannelBindingFactory) && d.ImplementationType == channelBindingFactoryType))
                    services.Add(new(typeof(IChannelBindingFactory), channelBindingFactoryType, ServiceLifetime.Singleton));
            }
            return services;
        }

    }

}
