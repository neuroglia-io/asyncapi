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
using Neuroglia.AsyncApi.Sdk.Models;
using Neuroglia.Serialization;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Sdk.Services.IO
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IAsyncApiDocumentReader"/> interface
    /// </summary>
    public class AsyncApiDocumentReader
        : IAsyncApiDocumentReader
    {

        /// <summary>
        /// Initializes a new <see cref="AsyncApiDocumentReader"/>
        /// </summary>
        /// <param name="logger">The service used to perform logging</param>
        /// <param name="jsonSerializer">The service used to serialize and deserialize JSON</param>
        /// <param name="yamlSerializer">The service used to serialize and deserialize YAML</param>
        /// <param name="httpClientFactory">The service used to create <see cref="System.Net.Http.HttpClient"/>s</param>
        public AsyncApiDocumentReader(ILogger<AsyncApiDocumentReader> logger, IJsonSerializer jsonSerializer, IYamlSerializer yamlSerializer, IHttpClientFactory httpClientFactory)
        {
            this.Logger = logger;
            this.JsonSerializer = jsonSerializer;
            this.YamlSerializer = yamlSerializer;
            this.HttpClient = httpClientFactory.CreateClient();
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
        public virtual async Task<AsyncApiDocument> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            ISerializer serializer;
            long offset = stream.Position;
            using StreamReader reader = new(stream);
            string input = reader.ReadToEnd();
            stream.Position = offset;
            if (input.IsJson())
                serializer = this.JsonSerializer;
            else
                serializer = this.YamlSerializer;
            AsyncApiDocument document = await serializer.DeserializeAsync<AsyncApiDocument>(stream, cancellationToken);
            string output = Encoding.UTF8.GetString(await serializer.SerializeAsync(document, cancellationToken));
            return document;
        }

    }

}
