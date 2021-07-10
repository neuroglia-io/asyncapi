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
using Microsoft.Extensions.DependencyInjection.Extensions;
using Neuroglia.AsyncApi.Services.FluentBuilders;
using Neuroglia.AsyncApi.Services.Generators;
using Neuroglia.AsyncApi.Services.IO;
using Neuroglia.AsyncApi.Services.Validation;
using Neuroglia.Serialization;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace Neuroglia.AsyncApi
{

    /// <summary>
    /// Defines extensions for <see cref="IServiceCollection"/>s
    /// </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary>
        /// Adds and configures Async API services (<see cref="Serialization.ISerializer"/>s, <see cref="IAsyncApiDocumentReader"/>, <see cref="IAsyncApiDocumentWriter"/>, ...)
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddAsyncApi(this IServiceCollection services)
        {
            services.AddNewtonsoftJsonSerializer(options =>
            {
                options.NullValueHandling = NullValueHandling.Ignore;
            });
            services.AddYamlDotNetSerializer(
                serializer => serializer
                    .IncludeNonPublicProperties()
                    .WithTypeConverter(new JTokenSerializer())
                    .WithTypeConverter(new StringEnumSerializer())
                    .WithTypeConverter(new UriTypeConverter())
                    .WithEmissionPhaseObjectGraphVisitor(args => new ChainedObjectGraphVisitor(args.InnerVisitor)),
                deserializer => deserializer
                    .WithTypeConverter(new UriTypeConverter()));
            services.AddHttpClient();
            services.TryAddSingleton<IAsyncApiDocumentReader, AsyncApiDocumentReader>();
            services.TryAddSingleton<IAsyncApiDocumentWriter, AsyncApiDocumentWriter>();
            services.TryAddTransient<IAsyncApiDocumentBuilder, AsyncApiDocumentBuilder>();
            services.TryAddTransient<IAsyncApiDocumentGenerator, AsyncApiDocumentGenerator>();
            services.AddValidatorsFromAssemblyContaining<AsyncApiDocumentValidator>(ServiceLifetime.Transient);
            return services;
        }

    }

}
