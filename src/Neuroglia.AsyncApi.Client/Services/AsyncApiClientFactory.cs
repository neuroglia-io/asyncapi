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
using Neuroglia.AsyncApi.v3;

namespace Neuroglia.AsyncApi.Client.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IAsyncApiClientFactory"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
public class AsyncApiClientFactory(IServiceProvider serviceProvider)
    : IAsyncApiClientFactory
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <inheritdoc/>
    public virtual IAsyncApiClient CreateFor(IAsyncApiDocument document)
    {
        return document switch
        {
            V3AsyncApiDocument v3Document => ActivatorUtilities.CreateInstance<V3AsyncApiClient>(ServiceProvider, v3Document),
            _ => throw new NotSupportedException($"The specified AsyncAPI document type '{document.GetType()}' is not supported")
        };
    }

}
