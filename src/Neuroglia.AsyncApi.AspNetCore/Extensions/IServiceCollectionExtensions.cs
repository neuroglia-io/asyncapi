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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Neuroglia.AsyncApi.Configuration;
using Neuroglia.AsyncApi.Services;
using System;

namespace Neuroglia.AsyncApi
{

    /// <summary>
    /// Defines extensions for <see cref="IServiceCollection"/>s
    /// </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary>
        /// Adds and configures code-first AsyncAPI document generation
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to configure the <see cref="AsyncApiGenerationOptions"/> to use</param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddAsyncApiGeneration(this IServiceCollection services, Action<IAsyncApiGenerationOptionsBuilder> setup)
        {
            services.AddAsyncApi();
            AsyncApiGenerationOptionsBuilder builder = new();
            setup(builder);
            services.TryAddSingleton(Options.Create(builder.Build()));
            services.TryAddSingleton<AsyncApiDocumentProvider>();
            services.TryAddSingleton<IAsyncApiDocumentProvider>(provider => provider.GetRequiredService<AsyncApiDocumentProvider>());
            services.TryAddSingleton<IHostedService>(provider => provider.GetRequiredService<AsyncApiDocumentProvider>());
            return services;
        }

    }

}
