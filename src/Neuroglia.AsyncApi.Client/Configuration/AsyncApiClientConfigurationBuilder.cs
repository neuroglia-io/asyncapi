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

namespace Neuroglia.AsyncApi.Client.Configuration;

/// <summary>
/// Represents the default implementation of the <see cref="IAsyncApiClientOptionsBuilder"/> interface
/// </summary>
/// <param name="services">The <see cref="IAsyncApiClient"/>'s <see cref="IServiceCollection"/></param>
public class AsyncApiClientConfigurationBuilder(IServiceCollection services)
    : IAsyncApiClientOptionsBuilder
{

    /// <inheritdoc/>
    public IServiceCollection Services => services;

    /// <inheritdoc/>
    public virtual IAsyncApiClientOptionsBuilder AddBindingHandler<TBindingHandler>()
        where TBindingHandler : class, IBindingHandler
    {
        this.Services.AddTransient<IBindingHandler, TBindingHandler>();
        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiClientOptionsBuilder AddBindingHandler<TBindingHandler, TOptions>(Action<TOptions> setup)
        where TBindingHandler : class, IBindingHandler<TOptions>
        where TOptions : BindingHandlerOptions
    {
        this.AddBindingHandler<TBindingHandler>();
        this.Services.Configure(setup);
        return this;
    }

}
