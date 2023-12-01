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
/// Represents the model of the page used to render an <see cref="AsyncApiDocument"/>
/// </summary>
/// <remarks>
/// Initializes a new <see cref="AsyncApiDocumentModel"/>
/// </remarks>
/// <param name="documents">The service used to access generated <see cref="AsyncApiDocument"/>s</param>
public class AsyncApiDocumentModel(IAsyncApiDocumentProvider documents)
    : PageModel
{

    IAsyncApiDocumentProvider Documents { get; } = documents;

    /// <summary>
    /// Gets the requested <see cref="AsyncApiDocument"/>'s title
    /// </summary>
    public string? RequestedTitle { get; private set; }

    /// <summary>
    /// Gets the requested <see cref="AsyncApiDocument"/>'s version
    /// </summary>
    public string? RequestedVersion { get; private set; }

    /// <summary>
    /// Gets the current <see cref="AsyncApiDocument"/>
    /// </summary>
    public AsyncApiDocument? Document { get; private set; }

    /// <summary>
    /// Renders the <see cref="AsyncApiDocument"/> with the specified title and version
    /// </summary>
    /// <param name="title">The title of the <see cref="AsyncApiDocument"/> to render</param>
    /// <param name="version">The version of the <see cref="AsyncApiDocument"/> to render</param>
    public void OnGet(string title, string version)
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
                    .Where(d =>
                        d.Info.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(d => d.Info.Version)
                    .FirstOrDefault();
            else
                this.Document = this.Documents
                    .Where(d => 
                    d.Info.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault(d => d.Info.Version.Equals(version, StringComparison.OrdinalIgnoreCase));
        }
    }

}
