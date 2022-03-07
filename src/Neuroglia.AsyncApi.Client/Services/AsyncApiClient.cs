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
using Microsoft.Extensions.Options;
using Neuroglia.AsyncApi.Client.Configuration;
using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Services.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Client.Services
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IAsyncApiClient"/> interface
    /// </summary>
    public class AsyncApiClient
        : IAsyncApiClient
    {

        /// <summary>
        /// Initializes a new <see cref="AsyncApiClient"/>
        /// </summary>
        /// <param name="name">The <see cref="AsyncApiClient"/>'s name</param>
        /// <param name="logger">The service used to perform logging</param>
        /// <param name="documentReader">The service used to read <see cref="AsyncApiDocument"/>s</param>
        /// <param name="channelFactory">The service used to create <see cref="IChannel"/>s</param>
        /// <param name="httpClientFactory">The service used to create <see cref="System.Net.Http.HttpClient"/>s</param>
        /// <param name="options">The options used to configure the <see cref="AsyncApiClient"/></param>
        public AsyncApiClient(string name, ILogger<AsyncApiClient> logger, IAsyncApiDocumentReader documentReader, IChannelFactory channelFactory, IHttpClientFactory httpClientFactory, IOptionsSnapshot<AsyncApiClientOptions> options)
        {
            this.Name = name;
            this.Logger = logger;
            this.DocumentReader = documentReader;
            this.ChannelFactory = channelFactory;
            this.HttpClient = httpClientFactory.CreateClient(name);
            this.Options = options.Get(name);
        }

        /// <inheritdoc/>
        public string Name { get; }

        /// <summary>
        /// Gets the service used to perform logging
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the service used to read <see cref="AsyncApiDocument"/>s
        /// </summary>
        protected IAsyncApiDocumentReader DocumentReader { get; }

        /// <summary>
        /// Gets the service used to create <see cref="IChannel"/>s
        /// </summary>
        protected IChannelFactory ChannelFactory { get; }

        /// <summary>
        /// Gets the <see cref="System.Net.Http.HttpClient"/> used by the <see cref="AsyncApiClient"/> to request remote documents
        /// </summary>
        protected HttpClient HttpClient { get; }

        /// <summary>
        /// Gets the options used to configure the <see cref="AsyncApiClient"/>
        /// </summary>
        protected AsyncApiClientOptions Options { get; }

        /// <summary>
        /// Gets the <see cref="AsyncApiDocument"/> that describes the API the <see cref="AsyncApiClient"/> applies to
        /// </summary>
        public AsyncApiDocument Document { get; private set; }

        private readonly List<IChannel> _Channels = new();
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="AsyncApiClient"/>'s <see cref="IChannel"/>s
        /// </summary>
        protected IReadOnlyCollection<IChannel> Channels => this._Channels.AsReadOnly();

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="AsyncApiClient"/> has been initialized
        /// </summary>
        protected bool Initialized { get; private set; }

        /// <summary>
        /// Initializes the <see cref="AsyncApiClient"/>
        /// </summary>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        protected virtual async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            if (this.Initialized)
                return;
            if(this.Options.Document == null)
            {
                Stream stream;
                if (this.Options.DocumentUri.IsFile)
                {
                    if (!File.Exists(this.Options.DocumentUri.LocalPath))
                        throw new FileNotFoundException(this.Options.DocumentUri.LocalPath);
                    stream = File.OpenRead(this.Options.DocumentUri.LocalPath);
                }
                else
                {
                    using HttpResponseMessage response = await this.HttpClient.SendAsync(new(System.Net.Http.HttpMethod.Get, this.Options.DocumentUri), cancellationToken);
                    response.EnsureSuccessStatusCode();
                    stream = response.Content.ReadAsStream();
                }
                using (stream)
                    this.Document = this.DocumentReader.Read(stream);
            }
            else
            {
                this.Document = this.Options.Document;
            }
            foreach (KeyValuePair<string, ChannelDefinition> channelDefinition in this.Document.Channels)
            {
                IChannel channel = this.ChannelFactory.CreateChannel(channelDefinition.Key, channelDefinition.Value, this.Document);
                this._Channels.Add(channel);
            }
            this.Initialized = true;
        }

        /// <inheritdoc/>
        public virtual async Task PublishAsync(string channelKey, IMessage message, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(channelKey))
                throw new ArgumentNullException(nameof(channelKey));
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            await this.InitializeAsync(cancellationToken);
            IChannel channel = this.Channels.FirstOrDefault(c => c.Key.Equals(channelKey, StringComparison.OrdinalIgnoreCase));
            if (channel == null)
                throw new NullReferenceException($"Failed to find a channel with the specified key '{channelKey}'");
            if (!channel.Definition.DefinesPublishOperation)
                throw new NotSupportedException($"The channel with key '{channelKey}' does not support publish operations");
            await channel.PublishAsync(message, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task PublishAsync(string channelKey, Action<IMessageBuilder> messageSetup, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(channelKey))
                throw new ArgumentNullException(nameof(channelKey));
            if (messageSetup == null)
                throw new ArgumentNullException(nameof(messageSetup));
            IMessageBuilder messageBuilder = new MessageBuilder();
            messageSetup(messageBuilder);
            await this.PublishAsync(channelKey, messageBuilder.Build(), cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<IDisposable> SubscribeToAsync(string channelKey, IObserver<IMessage> observer, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(channelKey))
                throw new ArgumentNullException(nameof(channelKey));
            await this.InitializeAsync(cancellationToken);
            IChannel channel = this.Channels.FirstOrDefault(c => c.Key.Equals(channelKey, StringComparison.OrdinalIgnoreCase));
            if (channel == null)
                throw new NullReferenceException($"Failed to find a channel with the specified key '{channelKey}'");
            if (!channel.Definition.DefinesPublishOperation)
                throw new NotSupportedException($"The channel with key '{channelKey}' does not support subscribe operations");
            return await channel.SubscribeAsync(observer, cancellationToken);
        }

        private bool _Disposed;
        /// <summary>
        /// Disposes of the <see cref="AsyncApiClient"/>
        /// </summary>
        /// <param name="disposing">A boolean indicating whether or not the <see cref="AsyncApiClient"/> is being disposed of</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                {
                    this._Channels.ForEach(b => b.Dispose());
                    this.HttpClient.Dispose();
                }
                this._Disposed = true;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }

}
