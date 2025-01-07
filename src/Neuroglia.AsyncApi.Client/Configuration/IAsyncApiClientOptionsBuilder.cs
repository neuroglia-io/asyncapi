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
/// Defines the fundamentals of a service used to build the configuration of an <see cref="IAsyncApiClient"/>
/// </summary>
public interface IAsyncApiClientOptionsBuilder
{

    /// <summary>
    /// Gets the <see cref="IAsyncApiClient"/>'s <see cref="IServiceCollection"/>
    /// </summary>
    IServiceCollection Services { get; }

    /// <summary>
    /// Adds and configures the specified <see cref="IBindingHandler"/>
    /// </summary>
    /// <typeparam name="TBindingHandler">The <see cref="IBindingHandler"/> to add and configure</typeparam>
    /// <returns>The configured <see cref="IAsyncApiClientOptionsBuilder"/></returns>
    IAsyncApiClientOptionsBuilder AddBindingHandler<TBindingHandler>()
        where TBindingHandler : class, IBindingHandler;

    /// <summary>
    /// Adds and configures the specified <see cref="IBindingHandler"/>
    /// </summary>
    /// <typeparam name="TBindingHandler">The <see cref="IBindingHandler"/> to add and configure</typeparam>
    /// <typeparam name="TOptions">The type of options used to configure the <see cref="IBindingHandler"/> to add</typeparam>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="IBindingHandler"/></param>
    /// <returns>The configured <see cref="IAsyncApiClientOptionsBuilder"/></returns>
    IAsyncApiClientOptionsBuilder AddBindingHandler<TBindingHandler, TOptions>(Action<TOptions> setup)
        where TBindingHandler : class, IBindingHandler<TOptions>
        where TOptions : BindingHandlerOptions;

}
