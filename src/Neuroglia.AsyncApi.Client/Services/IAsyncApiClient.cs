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
using Neuroglia.AsyncApi.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Client.Services
{

    /// <summary>
    /// Defines the fundamentals of a service used to interact with remote message brokers described by an <see cref="AsyncApiDocument"/>
    /// </summary>
    public interface IAsyncApiClient
        : IDisposable
    {

        /// <summary>
        /// Gets the <see cref="IAsyncApiClient"/>'s name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the <see cref="AsyncApiDocument"/> that describes the API the <see cref="IAsyncApiClient"/> applies to
        /// </summary>
        AsyncApiDocument Document { get; }

        /// <summary>
        /// Publishes the specified <see cref="IMessage"/>
        /// </summary>
        /// <param name="channelKey">The key of the channel to publish the <see cref="IMessage"/> to</param>
        /// <param name="message">The <see cref="IMessage"/> to publish</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        Task PublishAsync(string channelKey, IMessage message, CancellationToken cancellationToken = default);

        /// <summary>
        /// Publishes the specified <see cref="IMessage"/>
        /// </summary>
        /// <param name="channelKey">The key of the channel to publish the <see cref="IMessage"/> to</param>
        /// <param name="messageSetup">An <see cref="Action{T}"/> used to setup the <see cref="IMessage"/> to publish</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        Task PublishAsync(string channelKey, Action<IMessageBuilder> messageSetup, CancellationToken cancellationToken = default);

        /// <summary>
        /// Subscribes to the specified channel
        /// </summary>
        /// <param name="channelKey">The key of the channel to subscribe to</param>
        /// <param name="observer">The <see cref="IObserver{T}"/> used to observed subscribed <see cref="IMessage"/>s</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="IDisposable"/> used to unsubscribe from the specified channel</returns>
        Task<IDisposable> SubscribeToAsync(string channelKey, IObserver<IMessage> observer, CancellationToken cancellationToken = default);

    }

}
