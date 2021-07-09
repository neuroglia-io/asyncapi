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
using Neuroglia.AsyncApi.Sdk.Services.FluentBuilders;
using Neuroglia.AsyncApi.Sdk.Services.IO;
using Neuroglia.Serialization;

namespace Neuroglia.AsyncApi.Sdk.Extensions
{

    /// <summary>
    /// Defines extensions for <see cref="IServiceCollection"/>s
    /// </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary>
        /// Adds and configures Serverless Workflow services (<see cref="ISerializer"/>s, <see cref="IAsyncApiDocumentReader"/>, <see cref="IAsyncApiDocumentWriter"/>, ...)
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddServerlessWorkflow(this IServiceCollection services)
        {
            services.AddNewtonsoftJsonSerializer();
            services.AddYamlDotNetSerializer(
                serializer => serializer
                    .IncludeNonPublicProperties()
                    .WithTypeConverter(new JTokenSerializer())
                    .WithTypeConverter(new StringEnumSerializer())
                    .WithEmissionPhaseObjectGraphVisitor(args => new ChainedObjectGraphVisitor(args.InnerVisitor)),
                deserializer => { });
            services.AddHttpClient();
            services.AddSingleton<IAsyncApiDocumentReader, AsyncApiDocumentReader>();
            services.AddSingleton<IAsyncApiDocumentWriter, AsyncApiDocumentWriter>();
            //TODO: services.AddSingleton<IAsyncApiDocumentValidator, AsyncApiDocumentValidator>();
            services.AddTransient<IAsyncApiDocumentBuilder, AsyncApiDocumentBuilder>();
            //TODO: services.AddValidatorsFromAssemblyContaining<AsyncApiDocumentValidator>();
            return services;
        }

    }

}
