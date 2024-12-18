// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.Extensions.Options;
using Neuroglia.AsyncApi.Generation;
using Neuroglia.AsyncApi.IO;
using System.Net;
using System.Text;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents the middleware used to serve <see cref="IAsyncApiDocument"/>s
/// </summary>
/// <remarks>
/// Initializes a new <see cref="AsyncApiDocumentServingMiddleware"/>
/// </remarks>
/// <param name="options">The service used to access the current <see cref="AsyncApiDocumentServingOptions"/></param>
/// <param name="documentProvider">The service used to provide <see cref="IAsyncApiDocument"/>s</param>
/// <param name="documentWriter">The service used to write <see cref="IAsyncApiDocument"/>s</param>
/// <param name="next">The the next <see cref="RequestDelegate"/> in the pipeline</param>
public class AsyncApiDocumentServingMiddleware(IOptions<AsyncApiDocumentServingOptions> options, IAsyncApiDocumentProvider documentProvider, IAsyncApiDocumentWriter documentWriter, RequestDelegate next)
{

    /// <summary>
    /// Gets the current <see cref="AsyncApiDocumentServingOptions"/>
    /// </summary>
    protected virtual AsyncApiDocumentServingOptions Options { get; } = options.Value;

    /// <summary>
    /// Gets the service used to provide <see cref="V2AsyncApiDocument"/>s
    /// </summary>
    protected virtual IAsyncApiDocumentProvider DocumentProvider { get; } = documentProvider;

    /// <summary>
    /// Gets the service used to write <see cref="V2AsyncApiDocument"/>s
    /// </summary>
    protected virtual IAsyncApiDocumentWriter DocumentWriter { get; } = documentWriter;

    /// <summary>
    /// Gets the next <see cref="RequestDelegate"/> in the pipeline
    /// </summary>
    protected virtual RequestDelegate Next { get; } = next;

    /// <summary>
    /// Gets a route/<see cref="IAsyncApiDocument"/> mapping of the routes rendered based on the configured template for all available <see cref="IAsyncApiDocument"/>s 
    /// </summary>
    protected virtual Dictionary<string, IAsyncApiDocument> DocumentRoutes { get; } = documentProvider.ToDictionary(d => d, options.Value.GenerateRoutesFor).SelectMany(kvp => kvp.Value.Select(r => new KeyValuePair<string, IAsyncApiDocument>(r, kvp.Key))).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

    /// <summary>
    /// Invokes the <see cref="AsyncApiDocumentServingMiddleware"/>
    /// </summary>
    /// <param name="context">The current <see cref="HttpContext"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    public virtual async Task InvokeAsync(HttpContext context)
    {
        var extension = Path.GetExtension(context.Request.Path)?.Split('.', StringSplitOptions.RemoveEmptyEntries)?.LastOrDefault();
        if (context.Request.Method != HttpMethods.Get.ToString() || string.IsNullOrWhiteSpace(extension) || !this.DocumentRoutes.TryGetValue(context.Request.Path, out var document) || document == null) { await this.Next(context); return; }

        var format = string.IsNullOrWhiteSpace(extension)
            ? context.Request.ContentType switch
            {
                "application/yaml" => AsyncApiDocumentFormat.Yaml,
                "application/json" => AsyncApiDocumentFormat.Json,
                _ => AsyncApiDocumentFormat.Yaml
            }
            : extension == "json" ? AsyncApiDocumentFormat.Json : extension == "yaml" ? AsyncApiDocumentFormat.Yaml : throw new NotSupportedException($"The specified AsyncAPI extension '{extension}' is not supported");

        if (document == null) { context.Response.StatusCode = (int)HttpStatusCode.NotFound; return; }
        var stream = new MemoryStream();
        await this.DocumentWriter.WriteAsync(document, stream, format, context.RequestAborted).ConfigureAwait(false);

        var contents = stream.ToArray();
        stream.Dispose();
        context.Response.ContentType = context.Request.ContentType;
        await context.Response.BodyWriter.WriteAsync(contents, context.RequestAborted);
    }

    /// <summary>
    /// Renders the Async API document list
    /// </summary>
    /// <returns>The encoded html of the Async API document list</returns>
    protected virtual byte[] RenderAsyncApiDocumentList()
    {
        string html = $@"<ul>
{string.Join(Environment.NewLine, this.DocumentProvider.ToList().GroupBy(d => d.Title).Select(dg => @$"    <li>
        {dg.Key}
        <ul>
            {string.Join(Environment.NewLine, dg.Select(d => @$"
            <li>
                <a href=""{this.Options.PathTemplate}/{d.Title.ToLower().Replace(" ", "")}/{d.Version}"">{d.Version}</a>
            </li>"))}
        </ul>
    </li>"))}
</ul>
";
        return Encoding.UTF8.GetBytes(html);
    }

    /// <summary>
    /// Renders the Async API document version list for the specified <see cref="V2AsyncApiDocument"/>s 
    /// </summary>
    /// <param name="versions">The <see cref="IGrouping{TKey, TElement}"/> of <see cref="V2AsyncApiDocument"/> versions to render the list for</param>
    /// <returns>The encoded html of the Async API document version list</returns>
    protected virtual byte[] RenderAsyncApiDocumentVersionList(IGrouping<string, V2AsyncApiDocument> versions)
    {
        string html = $@"
{versions.Key}
<ul>
    {string.Join(Environment.NewLine, versions.Select(v => @$"
    <li>
        <a href=""{this.Options.PathTemplate}/{v.Info.Title.ToLower().Replace(" ", "")}/{v.Info.Version}"">{v.Info.Version}</a>
    </li>"))}
</ul>
";
        return Encoding.UTF8.GetBytes(html);
    }

}
