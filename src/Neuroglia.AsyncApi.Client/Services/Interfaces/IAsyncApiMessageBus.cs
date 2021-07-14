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

namespace Neuroglia.AsyncApi.Services
{
    /// <summary>
    /// Defines the fundamentals of a service used to interact with a remote message broker described by an <see cref="AsyncApiDocument"/>
    /// </summary>
    public interface IAsyncApiMessageBus
        : IDisposable
    {

        /// <summary>
        /// Publishes a new message
        /// </summary>
        /// <param name="operationId">The id of the <see cref="Operation"/> of type <see cref="OperationType.Publish"/> to invoke</param>
        /// <param name="message">The message to publish</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        Task PublishAsync(string operationId, object message, CancellationToken cancellationToken = default);

        /// <summary>
        /// Subscribes to the specified operation
        /// </summary>
        /// <param name="operationId">The id of the <see cref="Operation"/> of type <see cref="OperationType.Subscribe"/> to invoke</param>
        /// <param name="subscriptionFactory">A <see cref="Func{T, TResult}"/> used to create the <see cref="IObservable{T}"/>'s subscription</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="IDisposable"/> used to unsubscribe from the observable sequence</returns>
        Task<IDisposable> SubscribeAsync(string operationId, Func<IObservable<IAsyncApiMessage>, IDisposable> subscriptionFactory, CancellationToken cancellationToken = default);

    }

}
