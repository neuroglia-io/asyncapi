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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Neuroglia.AsyncApi.Configuration;
using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Services;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;

namespace Neuroglia.AsyncApi.Middlewares
{

    /// <summary>
    /// Represents the middleware used to serve the generated <see cref="AsyncApiDocument"/>s
    /// </summary>
    public class AsyncApiGenerationMiddleware
    {

        /// <summary>
        /// Initializes a new <see cref="AsyncApiGenerationMiddleware"/>
        /// </summary>
        /// <param name="options">The service used to access the current <see cref="AsyncApiGenerationOptions"/></param>
        /// <param name="documentProvider">The service used to provide <see cref="AsyncApiDocument"/>s</param>
        /// <param name="next">The the next <see cref="RequestDelegate"/> in the pipeline</param>
        public AsyncApiGenerationMiddleware(IOptions<AsyncApiGenerationOptions> options, IAsyncApiDocumentProvider documentProvider, RequestDelegate next)
        {
            this.Options = options.Value;
            this.DocumentProvider = documentProvider;
            this.Next = next;
        }

        /// <summary>
        /// Gets the current <see cref="AsyncApiGenerationOptions"/>
        /// </summary>
        protected virtual AsyncApiGenerationOptions Options { get; }

        /// <summary>
        /// Gets the service used to provide <see cref="AsyncApiDocument"/>s
        /// </summary>
        protected virtual IAsyncApiDocumentProvider DocumentProvider { get; }

        /// <summary>
        /// Gets the next <see cref="RequestDelegate"/> in the pipeline
        /// </summary>
        protected virtual RequestDelegate Next { get; }

        /// <summary>
        /// Invokes the <see cref="AsyncApiGenerationMiddleware"/>
        /// </summary>
        /// <param name="context">The current <see cref="HttpContext"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        public virtual async Task InvokeAsync(HttpContext context)
        {
            if(context.Request.Method != HttpMethods.Get.ToString()
                || !context.Request.Path.ToString().StartsWith(this.Options.PathPrefix, StringComparison.OrdinalIgnoreCase))
            {
                await this.Next(context);
                return;
            }
            string[] components = context.Request.Path.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);
            string documentName = HttpUtility.UrlDecode(components[^2]);
            string documentVersion = HttpUtility.UrlDecode(components[^1]);
            AsyncApiDocumentFormat format = context.Request.ContentType switch
            {
                "application/yaml" => AsyncApiDocumentFormat.Yaml,
                "application/json" => AsyncApiDocumentFormat.Json,
                _ => AsyncApiDocumentFormat.Yaml
            };
            byte[] contents = await this.DocumentProvider.ReadDocumentContentsAsync(documentName, documentVersion, format);
            context.Response.ContentType = "";
            await context.Response.BodyWriter.WriteAsync(contents);
            context.Response.Body.Close();
        }

    }

}
