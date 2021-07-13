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
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Services
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IAsyncApiMessageBus"/>
    /// </summary>
    public class AsyncApiMessageBus
        : IAsyncApiMessageBus
    {

        private bool _Disposed;

        /// <summary>
        /// Initializes a new <see cref="AsyncApiMessageBus"/>
        /// </summary>
        /// <param name="logger">The service used to perform logging</param>
        /// <param name="channelFactory">The service used to create <see cref="IAsyncApiChannel"/>s</param>
        /// <param name="asyncApiDocument">The <see cref="Models.AsyncApiDocument"/> used to configure the <see cref="AsyncApiMessageBus"/></param>
        public AsyncApiMessageBus(ILogger<AsyncApiMessageBus> logger, IAsyncApiChannelFactory channelFactory, AsyncApiDocument asyncApiDocument)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.ChannelFactory = channelFactory ?? throw new ArgumentNullException(nameof(channelFactory));
            this.AsyncApiDocument = asyncApiDocument ?? throw new ArgumentNullException(nameof(asyncApiDocument));
        }

        /// <summary>
        /// Gets the service used to perform logging
        /// </summary>
        protected virtual ILogger Logger { get; }

        /// <summary>
        /// Gets the service used to create <see cref="IAsyncApiChannel"/>s
        /// </summary>
        protected virtual IAsyncApiChannelFactory ChannelFactory { get; }

        /// <summary>
        /// Gets the <see cref="Models.AsyncApiDocument"/> used to configure the <see cref="AsyncApiMessageBus"/>
        /// </summary>
        protected virtual AsyncApiDocument AsyncApiDocument { get; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="AsyncApiMessageBus"/> has been initialized
        /// </summary>
        protected virtual bool Initialized { get; private set; }

        /// <summary>
        /// Gets a <see cref="Dictionary{TKey, TValue}"/> containing operation id to channel name mappings
        /// </summary>
        protected virtual Dictionary<string, string> OperationChannelMapping { get; private set; }

        /// <summary>
        /// Gets a <see cref="Dictionary{TKey, TValue}"/> containing the <see cref="AsyncApiMessageBus"/>'s <see cref="IAsyncApiChannel"/>s
        /// </summary>
        protected virtual Dictionary<string, IAsyncApiChannel> Channels { get; private set; }

        /// <summary>
        /// Initializes the <see cref="AsyncApiMessageBus"/>
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        public virtual async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            this.OperationChannelMapping = new Dictionary<string, string>();
            this.Channels = new Dictionary<string, IAsyncApiChannel>();
            foreach (KeyValuePair<string, Channel> channelKvp in this.AsyncApiDocument.Channels)
            {
                foreach(Operation operation in channelKvp.Value.Operations)
                {
                    this.OperationChannelMapping.Add(operation.OperationId, channelKvp.Key);
                    this.Channels.Add(channelKvp.Key, this.ChannelFactory.CreateChannelFor(channelKvp.Value));
                }
            }
            await Task.CompletedTask;
        }

        /// <inheritdoc/>
        public virtual async Task PublishAsync(string operationId, object message, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(operationId))
                throw new ArgumentNullException(nameof(operationId));
            if (!this.Initialized)
                await this.InitializeAsync(cancellationToken);
            if (!this.OperationChannelMapping.TryGetValue(operationId, out string channelName))
                throw new MissingMethodException($"Failed to find an operation with the specified id '{operationId}' in AsyncAPI '{this.AsyncApiDocument.Info.Title}:{this.AsyncApiDocument.Info.Version}'");
            if (this.Channels.TryGetValue(channelName, out IAsyncApiChannel channel))
                throw new NullReferenceException($"Failed to find a channel with the specified name '{channelName}'. The {nameof(AsyncApiMessageBus)} might have been unproperly initialized");
    
            await channel.PublishAsync(operationId, message, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<IDisposable> SubscribeAsync(string operationId, Func<IObservable<IAsyncApiMessage>, IDisposable> subscriptionFactory, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(operationId))
                throw new ArgumentNullException(nameof(operationId));
            if (!this.Initialized)
                await this.InitializeAsync(cancellationToken);
            if (!this.OperationChannelMapping.TryGetValue(operationId, out string channelName))
                throw new MissingMethodException($"Failed to find an operation with the specified id '{operationId}' in AsyncAPI '{this.AsyncApiDocument.Info.Title}:{this.AsyncApiDocument.Info.Version}'");
            if (this.Channels.TryGetValue(channelName, out IAsyncApiChannel channel))
                throw new NullReferenceException($"Failed to find a channel with the specified name '{channelName}'. The {nameof(AsyncApiMessageBus)} might have been unproperly initialized");
            return await channel.SubscribeAsync(operationId, subscriptionFactory, cancellationToken);
        }

        /// <summary>
        /// Disposes of the <see cref="AsyncApiMessageBus"/>
        /// </summary>
        /// <param name="disposing">A boolean indicating whether or not the <see cref="AsyncApiMessageBus"/> has been disposed of</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                {
                    
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
