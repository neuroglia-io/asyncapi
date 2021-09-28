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
using System;
using System.Collections.Generic;
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
        /// <param name="logger">The service used to perform logging</param>
        /// <param name="channelFactory">The service used to create <see cref="IChannel"/>s</param>
        public AsyncApiClient(ILogger<AsyncApiClient> logger, IChannelFactory channelFactory)
        {
            this.Logger = logger;
            this.ChannelFactory = channelFactory;
            this.Initialize();
        }

        /// <summary>
        /// Gets the service used to perform logging
        /// </summary>
        protected virtual ILogger Logger { get; }

        /// <summary>
        /// Gets the service used to create <see cref="IChannel"/>s
        /// </summary>
        protected virtual IChannelFactory ChannelFactory { get; }

        private readonly List<IChannel> _Channels = new();
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="AsyncApiClient"/>'s <see cref="IChannel"/>s
        /// </summary>
        protected IReadOnlyCollection<IChannel> Channels => this._Channels.AsReadOnly();

        /// <summary>
        /// Initializes the <see cref="AsyncApiClient"/>
        /// </summary>
        protected virtual void Initialize()
        {

        }

        /// <inheritdoc/>
        public virtual async Task PublishAsync(string channel, object payload, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public virtual IDisposable SubscribeTo(string channel, IObserver<IMessage> observer)
        {
            throw new NotImplementedException();
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
                    this._Channels.ForEach(b => b.Dispose());
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
