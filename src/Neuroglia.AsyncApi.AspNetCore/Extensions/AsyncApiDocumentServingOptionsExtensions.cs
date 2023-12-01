using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Defines extensions for <see cref="AsyncApiDocumentServingOptions"/>
/// </summary>
public static class AsyncApiDocumentServingOptionsExtensions
{

    /// <summary>
    /// Generates routes for the specified <see cref="AsyncApiDocument"/>
    /// </summary>
    /// <param name="options">The current <see cref="AsyncApiDocumentServingOptions"/></param>
    /// <param name="document">The <see cref="AsyncApiDocument"/> to generate the routes for</param>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the egenerated routes</returns>
    public static IEnumerable<string> GenerateRoutesFor(this AsyncApiDocumentServingOptions options, AsyncApiDocument document)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(document);
        if (string.IsNullOrWhiteSpace(options.PathTemplate)) throw new ArgumentNullException($"{nameof(AsyncApiDocumentServingOptions)}.{nameof(AsyncApiDocumentServingOptions.PathTemplate)}");

        yield return options.GenerateRouteFor(document, AsyncApiDocumentFormat.Json);
        yield return options.GenerateRouteFor(document, AsyncApiDocumentFormat.Yaml);
    }

    /// <summary>
    /// Generates route for the specified <see cref="AsyncApiDocument"/>
    /// </summary>
    /// <param name="options">The current <see cref="AsyncApiDocumentServingOptions"/></param>
    /// <param name="document">The <see cref="AsyncApiDocument"/> to generate the route for</param>
    /// <returns>The generated route</returns>
    public static string GenerateRouteFor(this AsyncApiDocumentServingOptions options, AsyncApiDocument document, AsyncApiDocumentFormat format)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(document);
        if (string.IsNullOrWhiteSpace(options.PathTemplate)) throw new ArgumentNullException($"{nameof(AsyncApiDocumentServingOptions)}.{nameof(AsyncApiDocumentServingOptions.PathTemplate)}");

        var result = StringFormatter.NamedFormat(options.PathTemplate, new
        {
            id = document.Id?.ToKebabCase(),
            title = document.Info.Title.ToKebabCase(),
            version = document.Info.Version
        });

        return result.Replace("[json|yaml]", format == AsyncApiDocumentFormat.Json ? "json" : "yaml");
    }

}