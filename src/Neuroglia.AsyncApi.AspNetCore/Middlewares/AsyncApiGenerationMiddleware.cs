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
using System.Net;
using System.Net.Mime;
using System.Text;
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
            byte[] contents;
            string documentName;
            switch (components.Length)
            {
                case 1:
                    contents = this.RenderAsyncApiDocumentList();
                    context.Response.ContentType = MediaTypeNames.Text.Html;
                    break;
                case 2:
                    documentName = HttpUtility.UrlDecode(components[^1]);
                    IGrouping<string, AsyncApiDocument> versions = this.DocumentProvider.GroupBy(d => d.Info.Title).FirstOrDefault(d => d.Key.Replace(" ", "").Equals(documentName, StringComparison.InvariantCultureIgnoreCase));
                    if(versions == null)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        context.Response.Body.Close();
                        return;
                    }
                    contents = this.RenderAsyncApiDocumentVersionList(versions);
                    context.Response.ContentType = MediaTypeNames.Text.Html;
                    break;
                case 3:
                    documentName = HttpUtility.UrlDecode(components[^2]);
                    string documentVersion = HttpUtility.UrlDecode(components[^1]);
                    AsyncApiDocumentFormat format = context.Request.ContentType switch
                    {
                        "application/yaml" => AsyncApiDocumentFormat.Yaml,
                        "application/json" => AsyncApiDocumentFormat.Json,
                        _ => AsyncApiDocumentFormat.Yaml
                    };
                    contents = await this.DocumentProvider.ReadDocumentContentsAsync(documentName, documentVersion, format);
                    context.Response.ContentType = context.Request.ContentType;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Response.Body.Close();
                    return;
            }

            await context.Response.BodyWriter.WriteAsync(contents);
            context.Response.Body.Close();
        }

        /// <summary>
        /// Renders the Async API document list
        /// </summary>
        /// <returns>The encoded html of the Async API document list</returns>
        protected virtual byte[] RenderAsyncApiDocumentList()
        {
            string html = $@"<ul>
{string.Join(Environment.NewLine, this.DocumentProvider.ToList().GroupBy(d => d.Info.Title).Select(dg => @$"    <li>
        {dg.Key}
        <ul>
            {string.Join(Environment.NewLine, dg.Select(d => @$"
            <li>
                <a href=""{this.Options.PathPrefix}/{d.Info.Title.ToLower().Replace(" ", "")}/{d.Info.Version}"">{d.Info.Version}</a>
            </li>"))}
        </ul>
    </li>"))}
</ul>
";
            return Encoding.UTF8.GetBytes(html);
        }

        /// <summary>
        /// Renders the Async API document version list for the specified <see cref="AsyncApiDocument"/>s 
        /// </summary>
        /// <param name="versions">The <see cref="IGrouping{TKey, TElement}"/> of <see cref="AsyncApiDocument"/> versions to render the list for</param>
        /// <returns>The encoded html of the Async API document version list</returns>
        protected virtual byte[] RenderAsyncApiDocumentVersionList(IGrouping<string, AsyncApiDocument> versions)
        {
            string html = $@"
{versions.Key}
<ul>
    {string.Join(Environment.NewLine, versions.Select(v => @$"
    <li>
        <a href=""{this.Options.PathPrefix}/{v.Info.Title.ToLower().Replace(" ", "")}/{v.Info.Version}"">{v.Info.Version}</a>
    </li>"))}
</ul>
";
            return Encoding.UTF8.GetBytes(html);
        }

    }

}
