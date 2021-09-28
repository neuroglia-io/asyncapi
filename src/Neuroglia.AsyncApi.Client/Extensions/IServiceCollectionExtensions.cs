using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Client.Services;

namespace Neuroglia.AsyncApi
{

    /// <summary>
    /// Defines extensions for <see cref="IServiceCollection"/>s
    /// </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary>
        /// Adds and configures a new <see cref="IAsyncApiClient"/>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddAsyncApiClient(this IServiceCollection services)
        {
            services.AddAsyncApi();
            return services;
        }

    }

}
