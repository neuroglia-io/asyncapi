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

namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Represents the default implementation of the <see cref="IAsyncApiDocumentGenerator"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="exampleGenerator">The service used to generate example values based on a JSON Schema</param>
public partial class AsyncApiDocumentGenerator(IServiceProvider serviceProvider, IJsonSchemaExampleGenerator exampleGenerator)
    : IAsyncApiDocumentGenerator
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the service used to generate example values based on a JSON Schema
    /// </summary>
    protected IJsonSchemaExampleGenerator ExampleGenerator { get; } = exampleGenerator;

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<IAsyncApiDocument>> GenerateAsync(IEnumerable<Type> markupTypes, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(markupTypes);
        var types = markupTypes
            .Select(t => t.Assembly)
            .Distinct()
            .SelectMany(t => t.GetTypes()).Where(t => t.GetCustomAttribute<v2.AsyncApiAttribute>() != null || t.GetCustomAttribute<v3.AsyncApiAttribute>() != null);
        var v2ApiTypes = types.Where(t => t.GetCustomAttribute<v2.AsyncApiAttribute>() != null);
        var v3ApiTypes = types.Where(t => t.GetCustomAttribute<v3.AsyncApiAttribute>() != null);
        var documents = new List<IAsyncApiDocument>(types.Count());
        foreach (var typeGroup in v2ApiTypes.GroupBy(t =>
        {
            var attribute = t.GetCustomAttribute<v2.AsyncApiAttribute>()!;
            return $"{attribute.Title}:{attribute.Version}";
        })) documents.Add(await this.GenerateV2DocumentForAsync(typeGroup, options, cancellationToken).ConfigureAwait(false));
        foreach(var typeGroup in v3ApiTypes.GroupBy(t =>
        {
            var attribute = t.GetCustomAttribute<v3.AsyncApiAttribute>()!;
            return $"{attribute.Title}:{attribute.Version}";
        })) documents.Add(await this.GenerateV3DocumentForAsync(typeGroup, options, cancellationToken).ConfigureAwait(false));
        return documents;
    }

}
