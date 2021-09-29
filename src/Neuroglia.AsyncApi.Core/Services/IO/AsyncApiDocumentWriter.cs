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
using Microsoft.Extensions.Logging;
using Neuroglia.AsyncApi.Models;
using Neuroglia.Serialization;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Services.IO
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IAsyncApiDocumentWriter"/> interface
    /// </summary>
    public class AsyncApiDocumentWriter
        : IAsyncApiDocumentWriter
    {

        /// <summary>
        /// Initializes a new <see cref="AsyncApiDocumentWriter"/>
        /// </summary>
        /// <param name="logger">The service used to perform logging</param>
        /// <param name="jsonSerializer">The service used to serialize and deserialize JSON</param>
        /// <param name="yamlSerializer">The service used to serialize and deserialize YAML</param>
        public AsyncApiDocumentWriter(ILogger<AsyncApiDocumentReader> logger, IJsonSerializer jsonSerializer, IYamlSerializer yamlSerializer)
        {
            this.Logger = logger;
            this.JsonSerializer = jsonSerializer;
            this.YamlSerializer = yamlSerializer;
        }

        /// <summary>
        /// Gets the service used to perform logging
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the service used to serialize and deserialize JSON
        /// </summary>
        protected IJsonSerializer JsonSerializer { get; }

        /// <summary>
        /// Gets the service used to serialize and deserialize YAML
        /// </summary>
        protected IYamlSerializer YamlSerializer { get; }

        /// <summary>
        /// Gets the <see cref="System.Net.Http.HttpClient"/> used to retrieve external definitions
        /// </summary>
        protected HttpClient HttpClient { get; }

        /// <inheritdoc/>
        public virtual async Task WriteAsync(AsyncApiDocument document, Stream stream, AsyncApiDocumentFormat format = AsyncApiDocumentFormat.Yaml, CancellationToken cancellationToken = default)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            switch (format)
            {
                case AsyncApiDocumentFormat.Json:
                    await this.JsonSerializer.SerializeAsync(document, stream, cancellationToken);
                    break;
                case AsyncApiDocumentFormat.Yaml:
                    await this.YamlSerializer.SerializeAsync(document, stream, cancellationToken);
                    break;
                default:
                    throw new NotSupportedException($"The specified async api document format '{format}' is not supported");
            }
        }

        /// <inheritdoc/>
        public virtual void Write(AsyncApiDocument document, Stream stream, AsyncApiDocumentFormat format = AsyncApiDocumentFormat.Yaml)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            switch (format)
            {
                case AsyncApiDocumentFormat.Json:
                    this.JsonSerializer.Serialize(document, stream);
                    break;
                case AsyncApiDocumentFormat.Yaml:
                    this.YamlSerializer.Serialize(document, stream);
                    break;
                default:
                    throw new NotSupportedException($"The specified async api document format '{format}' is not supported");
            }
        }

    }

}
