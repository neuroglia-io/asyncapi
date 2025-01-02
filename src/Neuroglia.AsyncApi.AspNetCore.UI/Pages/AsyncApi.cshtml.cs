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

using Microsoft.AspNetCore.Mvc.RazorPages;
using Neuroglia.AsyncApi.Generation;

namespace Neuroglia.AsyncApi.AspNetCore.UI.Pages;

/// <summary>
/// Represents the model of the page used to render an <see cref="IAsyncApiDocument"/>
/// </summary>
/// <param name="documents">The service used to access generated <see cref="IAsyncApiDocument"/>s</param>
public class AsyncApiDocumentModel(IAsyncApiDocumentProvider documents)
    : PageModel
{

    IAsyncApiDocumentProvider Documents { get; } = documents;

    /// <summary>
    /// Gets the requested <see cref="IAsyncApiDocument"/>'s title
    /// </summary>
    public string? RequestedTitle { get; private set; }

    /// <summary>
    /// Gets the requested <see cref="IAsyncApiDocument"/>'s version
    /// </summary>
    public string? RequestedVersion { get; private set; }

    /// <summary>
    /// Gets the current <see cref="IAsyncApiDocument"/>
    /// </summary>
    public IAsyncApiDocument? Document { get; private set; }

    /// <summary>
    /// Renders the <see cref="IAsyncApiDocument"/> with the specified title and version
    /// </summary>
    /// <param name="specVersion">The Async API version in which the <see cref="IAsyncApiDocument"/> to render has been written in</param>
    /// <param name="title">The title of the <see cref="IAsyncApiDocument"/> to render</param>
    /// <param name="version">The version of the <see cref="IAsyncApiDocument"/> to render</param>
    public void OnGet(string specVersion, string title, string version)
    {
        this.RequestedTitle = title;
        this.RequestedVersion = version;
        if (string.IsNullOrWhiteSpace(title))
        {
            this.Document = this.Documents.FirstOrDefault();
        }
        else
        {
            if (string.IsNullOrWhiteSpace(version))
                this.Document = this.Documents
                    .Where(d => d.AsyncApi == specVersion && d.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(d => d.Version)
                    .FirstOrDefault();
            else
                this.Document = this.Documents
                    .Where(d => d.AsyncApi == specVersion && d.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault(d => d.Version.Equals(version, StringComparison.OrdinalIgnoreCase));
        }
    }

}
