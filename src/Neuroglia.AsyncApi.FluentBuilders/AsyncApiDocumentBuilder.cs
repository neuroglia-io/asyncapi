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

using Neuroglia.AsyncApi.FluentBuilders.v2;
using Neuroglia.AsyncApi.FluentBuilders.v3;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IAsyncApiDocumentBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
public class AsyncApiDocumentBuilder(IServiceProvider serviceProvider)
    : IAsyncApiDocumentBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets/sets the underlying <see cref="IVersionedApiDocumentBuilder"/> used to configure the <see cref="IAsyncApiDocument"/> to build
    /// </summary>
    protected IVersionedApiDocumentBuilder? Builder { get; set; }

    /// <inheritdoc/>
    public virtual IV2AsyncApiDocumentBuilder UsingAsyncApiV2()
    {
        var builder = this.ServiceProvider.GetRequiredService<IV2AsyncApiDocumentBuilder>();
        this.Builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual IV3AsyncApiDocumentBuilder UsingAsyncApiV3()
    {
        var builder = this.ServiceProvider.GetRequiredService<IV3AsyncApiDocumentBuilder>();
        this.Builder = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiDocument Build() => this.Builder?.Build() ?? throw new InvalidOperationException("You must configure the AsyncAPI specification version for the document before attempting to build it.");

}