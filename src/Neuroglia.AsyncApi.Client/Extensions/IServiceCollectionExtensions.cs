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

using Microsoft.Extensions.DependencyInjection;

namespace Neuroglia.AsyncApi.Client;

/// <summary>
/// Defines extensions for <see cref="IServiceCollection"/>s
/// </summary>
public static class IServiceCollectionExtensions
{

    /// <summary>
    /// Adds and configures the services used to interact with applications described by <see cref="IAsyncApiDocument"/>s
    /// </summary>
    /// <param name="services">The extended <see cref="IServiceCollection"/></param>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the <see cref="IAsyncApiClient"/></param>
    /// <returns>The configured <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddAsyncApiClient(this IServiceCollection services, Action<IAsyncApiClientOptionsBuilder>? setup = null)
    {
        var builder = new AsyncApiClientConfigurationBuilder(services);
        setup?.Invoke(builder);
        services.AddLogging();
        services.AddXmlSerializer();
        services.AddTransient<IRuntimeExpressionEvaluator, RuntimeExpressionEvaluator>();
        services.AddTransient<ISchemaHandlerProvider, SchemaHandlerProvider>();
        services.AddTransient<ISchemaHandler, AvroSchemaHandler>();
        services.AddTransient<ISchemaHandler, JsonSchemaHandler>();
        services.AddTransient<ISchemaHandler, XmlSchemaHandler>();
        services.AddTransient<IBindingHandlerProvider, ProtocolHandlerProvider>();
        services.AddTransient<IAsyncApiClientFactory, AsyncApiClientFactory>();
        return services;
    }

}

