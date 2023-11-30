namespace Neuroglia.AsyncApi;

/// <summary>
/// Defines extensions for <see cref="IServiceCollection"/>s
/// </summary>
public static class IAsyncApiUIServiceCollectionExtensions
{

    /// <summary>
    /// Adds and configures the services used by the AsyncAPI UI
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
    /// <param name="path">The path to the AsyncAPI UI</param>
    /// <returns>The configured <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddAsyncApiUI(this IServiceCollection services, string path = "AsyncApi")
    {
        services.AddRazorPages()
            .AddApplicationPart(typeof(IAsyncApiUIServiceCollectionExtensions).Assembly)
            .AddRazorPagesOptions(options =>
            {
                if(!path.Equals("AsyncApi", StringComparison.OrdinalIgnoreCase)) options.Conventions.AddPageRoute("/AsyncApi", path);
            });
        return services;
    }

}
