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
using Neuroglia.Serialization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Services
{
    /// <summary>
    /// Represents a base class for all <see cref="IAsyncApiChannel"/> implementations
    /// </summary>
    public abstract class AsyncApiChannelBase
        : IAsyncApiChannel
    {

        private bool _Disposed;

        /// <summary>
        /// Initializes a new <see cref="AsyncApiChannelBase"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
        protected AsyncApiChannelBase(ILoggerFactory loggerFactory, ISerializerProvider serializerProvider)
        {
            this.Logger = loggerFactory.CreateLogger(this.GetType());
            this.SerializerProvider = serializerProvider;
        }

        /// <summary>
        /// Gets the service used to perform logging
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the service used to provide <see cref="ISerializer"/>s
        /// </summary>
        protected ISerializerProvider SerializerProvider { get; }

        /// <inheritdoc/>
        public abstract Task PublishAsync(string operationId, object message, CancellationToken cancellationToken = default);

        /// <inheritdoc/>
        public abstract Task<IDisposable> SubscribeAsync(string operationId, Func<IObservable<IAsyncApiMessage>, IDisposable> subscriptionFactory, CancellationToken cancellationToken = default);

        /// <summary>
        /// Disposes of the <see cref="AsyncApiChannelBase"/>
        /// </summary>
        /// <param name="disposing">A boolean indicating whether or not to dispose of the <see cref="AsyncApiChannelBase"/></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                {
                    
                }
               this. _Disposed = true;
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
