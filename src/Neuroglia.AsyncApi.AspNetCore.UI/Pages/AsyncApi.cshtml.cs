using Microsoft.AspNetCore.Mvc.RazorPages;
using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Services;
using System;
using System.Linq;

namespace Neuroglia.AsyncApi.AspNetCore.UI.Pages
{

    /// <summary>
    /// Represents the model of the page used to render an <see cref="AsyncApiDocument"/>
    /// </summary>
    public class AsyncApiModel 
        : PageModel
    {

        /// <summary>
        /// Initializes a new <see cref="AsyncApiModel"/>
        /// </summary>
        /// <param name="documents">The service used to access generated <see cref="AsyncApiDocument"/>s</param>
        public AsyncApiModel(IAsyncApiDocumentProvider documents)
        {
            this.Documents = documents;
        }

        IAsyncApiDocumentProvider Documents { get; }

        /// <summary>
        /// Gets the requested <see cref="AsyncApiDocument"/>'s title
        /// </summary>
        public string RequestedTitle { get; private set; }

        /// <summary>
        /// Gets the requested <see cref="AsyncApiDocument"/>'s version
        /// </summary>
        public string RequestedVersion { get; private set; }

        /// <summary>
        /// Gets the current <see cref="AsyncApiDocument"/>
        /// </summary>
        public AsyncApiDocument Document { get; private set; }

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
                            d.Info.Title.Equals(title, StringComparison.OrdinalIgnoreCase) 
                            || d.Info.Title.Replace(" ", "").Equals(title, StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(d => d.Info.Version)
                        .FirstOrDefault();
                else
                    this.Document = this.Documents
                        .Where(d => 
                        d.Info.Title.Equals(title, StringComparison.OrdinalIgnoreCase) 
                        || d.Info.Title.Replace(" ", "").Equals(title, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault(d => d.Info.Version.Equals(version, StringComparison.OrdinalIgnoreCase));
            }
        }

    }

}
