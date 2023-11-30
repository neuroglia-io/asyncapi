namespace Neuroglia.AsyncApi;

/// <summary>
/// Defines extensions for <see cref="IApplicationBuilder"/>s
/// </summary>
public static class IApplicationBuilderExtensions
{

    /// <summary>
    /// Configures the <see cref="IApplicationBuilder"/> to use an <see cref="AsyncApiDocumentServingMiddleware"/>
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> to configure</param>
    /// <param name="configuration">An <see cref="Action{T}"/> used to configure the <see cref="AsyncApiDocumentServingMiddleware"/></param>
    /// <returns>The configured <see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder MapAsyncApiDocuments(this IApplicationBuilder app)
    {
        app.UseMiddleware<AsyncApiDocumentServingMiddleware>();
        return app;
    }

}