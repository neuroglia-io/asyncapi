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

using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Defines extensions for <see cref="AsyncApiDocumentServingOptions"/>
/// </summary>
public static class AsyncApiDocumentServingOptionsExtensions
{

    /// <summary>
    /// Generates routes for the specified <see cref="AsyncApiDocument"/>
    /// </summary>
    /// <param name="options">The current <see cref="AsyncApiDocumentServingOptions"/></param>
    /// <param name="document">The <see cref="AsyncApiDocument"/> to generate the routes for</param>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the egenerated routes</returns>
    public static IEnumerable<string> GenerateRoutesFor(this AsyncApiDocumentServingOptions options, AsyncApiDocument document)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(document);
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
        if (string.IsNullOrWhiteSpace(options.PathTemplate)) throw new ArgumentNullException($"{nameof(AsyncApiDocumentServingOptions)}.{nameof(AsyncApiDocumentServingOptions.PathTemplate)}");
#pragma warning restore CA2208 // Instantiate argument exceptions correctly

        yield return options.GenerateRouteFor(document, AsyncApiDocumentFormat.Json);
        yield return options.GenerateRouteFor(document, AsyncApiDocumentFormat.Yaml);
    }

    /// <summary>
    /// Generates route for the specified <see cref="AsyncApiDocument"/>
    /// </summary>
    /// <param name="options">The current <see cref="AsyncApiDocumentServingOptions"/></param>
    /// <param name="document">The <see cref="AsyncApiDocument"/> to generate the route for</param>
    /// <param name="format">The <see cref="AsyncApiDocumentFormat"/> to generate the route for</param>
    /// <returns>The generated route</returns>
    public static string GenerateRouteFor(this AsyncApiDocumentServingOptions options, AsyncApiDocument document, AsyncApiDocumentFormat format)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(document);
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
        if (string.IsNullOrWhiteSpace(options.PathTemplate)) throw new ArgumentNullException($"{nameof(AsyncApiDocumentServingOptions)}.{nameof(AsyncApiDocumentServingOptions.PathTemplate)}");
#pragma warning restore CA2208 // Instantiate argument exceptions correctly

        var result = StringFormatter.NamedFormat(options.PathTemplate, new
        {
            id = document.Id?.ToKebabCase(),
            title = document.Info.Title.ToKebabCase(),
            version = document.Info.Version
        });

        return result.Replace("[json|yaml]", format == AsyncApiDocumentFormat.Json ? "json" : "yaml");
    }

}