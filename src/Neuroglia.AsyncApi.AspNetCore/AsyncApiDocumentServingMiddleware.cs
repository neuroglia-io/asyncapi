using Microsoft.Extensions.Options;
using Neuroglia.AsyncApi.Generation;
using Neuroglia.AsyncApi.IO;
using System.Net;
using System.Text;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents the middleware used to serve <see cref="AsyncApiDocument"/>s
/// </summary>
/// <remarks>
/// Initializes a new <see cref="AsyncApiDocumentServingMiddleware"/>
/// </remarks>
/// <param name="options">The service used to access the current <see cref="AsyncApiDocumentServingOptions"/></param>
/// <param name="documentProvider">The service used to provide <see cref="AsyncApiDocument"/>s</param>
/// <param name="documentWriter">The service used to write <see cref="AsyncApiDocument"/>s</param>
/// <param name="next">The the next <see cref="RequestDelegate"/> in the pipeline</param>
public class AsyncApiDocumentServingMiddleware(IOptions<AsyncApiDocumentServingOptions> options, IAsyncApiDocumentProvider documentProvider, IAsyncApiDocumentWriter documentWriter, RequestDelegate next)
{

    /// <summary>
    /// Gets the current <see cref="AsyncApiDocumentServingOptions"/>
    /// </summary>
    protected virtual AsyncApiDocumentServingOptions Options { get; } = options.Value;

    /// <summary>
    /// Gets the service used to provide <see cref="AsyncApiDocument"/>s
    /// </summary>
    protected virtual IAsyncApiDocumentProvider DocumentProvider { get; } = documentProvider;

    protected virtual IAsyncApiDocumentWriter DocumentWriter { get; } = documentWriter;

    /// <summary>
    /// Gets the next <see cref="RequestDelegate"/> in the pipeline
    /// </summary>
    protected virtual RequestDelegate Next { get; } = next;

    /// <summary>
    /// Gets a route/<see cref="AsyncApiDocument"/> mapping of the routes rendered based on the configured template for all available <see cref="AsyncApiDocument"/>s 
    /// </summary>
    protected virtual Dictionary<string, AsyncApiDocument> DocumentRoutes { get; } = documentProvider.ToDictionary(d => d, options.Value.GenerateRoutesFor).SelectMany(kvp => kvp.Value.Select(r => new KeyValuePair<string, AsyncApiDocument>(r, kvp.Key))).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

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
{string.Join(Environment.NewLine, this.DocumentProvider.ToList().GroupBy(d => d.Info.Title).Select(dg => @$"    <li>
        {dg.Key}
        <ul>
            {string.Join(Environment.NewLine, dg.Select(d => @$"
            <li>
                <a href=""{this.Options.PathTemplate}/{d.Info.Title.ToLower().Replace(" ", "")}/{d.Info.Version}"">{d.Info.Version}</a>
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
        <a href=""{this.Options.PathTemplate}/{v.Info.Title.ToLower().Replace(" ", "")}/{v.Info.Version}"">{v.Info.Version}</a>
    </li>"))}
</ul>
";
        return Encoding.UTF8.GetBytes(html);
    }

}
