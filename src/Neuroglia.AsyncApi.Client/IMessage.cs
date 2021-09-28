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
using System;
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Client
{
    /// <summary>
    /// Defines the fundamentals of an Async API message
    /// </summary>
    public interface IMessage
    {

        /// <summary>
        /// Gets the <see cref="IMessage"/>'s timestamp
        /// </summary>
        DateTime Timestamp { get; }

        /// <summary>
        /// Gets the key of the <see cref="IChannel"/> the <see cref="IMessage"/> is bound to
        /// </summary>
        string ChannelKey { get; }

        /// <summary>
        /// Gets the <see cref="IMessage"/>'s correlation key, if any
        /// </summary>
        object CorrelationKey { get; }

        /// <summary>
        /// Gets an <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="IMessage"/>'s headers
        /// </summary>
        IDictionary<string, object> Headers { get; }

        /// <summary>
        /// Gets the <see cref="IMessage"/>'s payload
        /// </summary>
        object Payload { get; }

    }

}
