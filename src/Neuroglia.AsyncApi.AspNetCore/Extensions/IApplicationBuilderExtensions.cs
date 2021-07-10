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
using Microsoft.AspNetCore.Builder;
using Neuroglia.AsyncApi.Middlewares;

namespace Neuroglia.AsyncApi
{
    /// <summary>
    /// Defines extensions for <see cref="IApplicationBuilder"/>s
    /// </summary>
    public static class IApplicationBuilderExtensions
    {

        /// <summary>
        /// Configures the <see cref="IApplicationBuilder"/> to use the <see cref="AsyncApiGenerationMiddleware"/>
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to configure</param>
        /// <returns>The configured <see cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseAsyncApiGeneration(this IApplicationBuilder app)
        {
            app.UseMiddleware<AsyncApiGenerationMiddleware>();
            return app;
        }

    }

}
