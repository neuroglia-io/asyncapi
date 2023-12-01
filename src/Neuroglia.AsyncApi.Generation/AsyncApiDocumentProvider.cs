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

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Neuroglia.AsyncApi.Generation;
using Neuroglia.AsyncApi.v2;
using System.Collections;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents the default implementation of the <see cref="IAsyncApiDocumentProvider"/> interface
/// </summary>
/// <param name="generationOptions">The options used to configure how to generate code-first <see cref="AsyncApiDocument"/>s</param>
/// <param name="generator">The service used to generate code-first <see cref="AsyncApiDocument"/>s</param>
/// <param name="documents">An <see cref="IEnumerable{T}"/> containing all registered <see cref="AsyncApiDocument"/>s</param>
public class AsyncApiDocumentProvider(IOptions<AsyncApiGenerationOptions> generationOptions, IAsyncApiDocumentGenerator generator, IEnumerable<AsyncApiDocument> documents)
    : IHostedService, IAsyncApiDocumentProvider
{

    /// <summary>
    /// Gets the options used to configure how to generate code-first <see cref="AsyncApiDocument"/>s
    /// </summary>
    protected AsyncApiGenerationOptions GenerationOptions { get; } = generationOptions.Value;

    /// <summary>
    /// Gets the service used to generate code-first <see cref="AsyncApiDocument"/>s
    /// </summary>
    protected IAsyncApiDocumentGenerator Generator { get; } = generator;

    /// <summary>
    /// Gets a <see cref="List{T}"/> containing all known <see cref="AsyncApiDocument"/>s
    /// </summary>
    protected List<AsyncApiDocument>? Documents { get; private set; }

    /// <inheritdoc/>
    public virtual async Task StartAsync(CancellationToken cancellationToken)
    {
        this.Documents = documents.ToList();
        this.Documents.AddRange((await this.Generator.GenerateAsync(this.GenerationOptions.MarkupTypes, new AsyncApiDocumentGenerationOptions() { DefaultConfiguration = this.GenerationOptions.DefaultDocumentConfiguration }, cancellationToken).ConfigureAwait(false)).ToList());
    }

    /// <inheritdoc/>
    public virtual Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    /// <inheritdoc/>
    public virtual Task<AsyncApiDocument?> GetDocumentAsync(string title, string version, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(this.Documents!.FirstOrDefault(d => (d.Info.Title.Equals(title, StringComparison.OrdinalIgnoreCase) || d.Info.Title.ToKebabCase().Equals(title, StringComparison.OrdinalIgnoreCase)) && d.Info.Version.Equals(version, StringComparison.OrdinalIgnoreCase)));
    }

    /// <inheritdoc/>
    public virtual Task<AsyncApiDocument?> GetDocumentAsync(string id, CancellationToken cancellationToken = default) => Task.FromResult(this.Documents!.FirstOrDefault(d => !string.IsNullOrWhiteSpace(d.Id) && d.Id.Equals(id, StringComparison.OrdinalIgnoreCase)));

    /// <inheritdoc/>
    public virtual IEnumerator<AsyncApiDocument> GetEnumerator() => this.Documents?.GetEnumerator() ?? new List<AsyncApiDocument>().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

}
