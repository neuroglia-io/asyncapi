using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Services.FluentBuilders;
using System;

namespace Neuroglia.AsyncApi.Configuration
{

    /// <summary>
    /// Represents the options used to configure <see cref="AsyncApiDocument"/> generation
    /// </summary>
    public class AsyncApiDocumentGenerationOptions
    {

        /// <summary>
        /// Gets/sets an <see cref="Action{T}"/> used to configure the <see cref="AsyncApiDocument"/>s to configure
        /// </summary>
        public Action<IAsyncApiDocumentBuilder> DefaultConfiguration { get; set; }

    }

}
