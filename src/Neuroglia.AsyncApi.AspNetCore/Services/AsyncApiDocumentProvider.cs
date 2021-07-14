/*
 * Copyright © 2021 Neuroglia SPRL. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Neuroglia.AsyncApi.Configuration;
using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Services.Generators;
using Neuroglia.AsyncApi.Services.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Services
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IAsyncApiDocumentProvider"/> interface
    /// </summary>
    public class AsyncApiDocumentProvider
        : BackgroundService, IAsyncApiDocumentProvider
    {

        /// <summary>
        /// Initializes a new <see cref="AsyncApiDocumentProvider"/>
        /// </summary>
        /// <param name="options">The service used to access the current <see cref="AsyncApiGenerationOptions"/></param>
        /// <param name="generator">The service used to generate <see cref="AsyncApiDocument"/>s</param>
        /// <param name="writer">The service used to write <see cref="AsyncApiDocument"/>s</param>
        public AsyncApiDocumentProvider(IOptions<AsyncApiGenerationOptions> options, IAsyncApiDocumentGenerator generator, IAsyncApiDocumentWriter writer)
        {
            this.Options = options.Value;
            this.Generator = generator;
            this.Writer = writer;
        }

        /// <summary>
        /// Gets the current <see cref="AsyncApiGenerationOptions"/>
        /// </summary>
        protected AsyncApiGenerationOptions Options { get; }

        /// <summary>
        /// Gets the service used to generate <see cref="AsyncApiDocument"/>s
        /// </summary>
        protected IAsyncApiDocumentGenerator Generator { get; }

        /// <summary>
        /// Gets the service used to write <see cref="AsyncApiDocument"/>s
        /// </summary>
        protected IAsyncApiDocumentWriter Writer { get; }

        /// <summary>
        /// Gets a <see cref="List{T}"/> containing all available <see cref="AsyncApiDocument"/>s
        /// </summary>
        protected virtual List<AsyncApiDocument> Documents { get; private set; }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.Documents = (await this.Generator.GenerateAsync(this.Options.MarkupTypes, new AsyncApiDocumentGenerationOptions() { DefaultConfiguration = this.Options.DefaultDocumentConfiguration })).ToList();
        }

        /// <inheritdoc/>
        public virtual async Task<byte[]> ReadDocumentContentsAsync(string name, string version, AsyncApiDocumentFormat format)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            AsyncApiDocument document = this.Documents.FirstOrDefault(n => 
                (n.Info.Title.Equals(name, StringComparison.OrdinalIgnoreCase) || n.Info.Title.Replace(" ", "").Equals(name, StringComparison.OrdinalIgnoreCase))
                && n.Info.Version.Equals(version, StringComparison.OrdinalIgnoreCase));
            if (document == null)
                throw new NullReferenceException($"Failed to find an AsyncAPI document with the specified title '{name}' and version '{version}'");
            using MemoryStream stream = new();
            await this.Writer.WriteAsync(document, stream, format);
            await stream.FlushAsync();
            return stream.ToArray();
        }

        /// <inheritdoc/>
        public virtual IEnumerator<AsyncApiDocument> GetEnumerator()
        {
            return this.Documents.AsReadOnly().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }

}
