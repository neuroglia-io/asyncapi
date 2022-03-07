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

namespace Neuroglia.AsyncApi.Client.Services
{

    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="IMessage"/>s
    /// </summary>
    public interface IMessageBuilder
    {

        /// <summary>
        /// Sets the specified timestamp
        /// </summary>
        /// <param name="timestamp">The <see cref="IMessage"/>'s timestamp</param>
        /// <returns>The configured <see cref="IMessageBuilder"/></returns>
        IMessageBuilder WithTimestamp(DateTime timestamp);

        /// <summary>
        /// Adds a header to the <see cref="IMessage"/> to build 
        /// </summary>
        /// <param name="key">The key of the header to add</param>
        /// <param name="value">The value of the header to add</param>
        /// <returns>The configured <see cref="IMessageBuilder"/></returns>
        IMessageBuilder WithHeader(string key, object value);

        /// <summary>
        /// Adds headers to the <see cref="IMessage"/> to build
        /// </summary>
        /// <param name="headers">An <see cref="IDictionary{TKey, TValue}"/> containing the headers to add</param>
        /// <returns>The configured <see cref="IMessageBuilder"/></returns>
        IMessageBuilder WithHeaders(IDictionary<string, object> headers);

        /// <summary>
        /// Sets the <see cref="IMessage"/>'s payload
        /// </summary>
        /// <param name="payload">The payload of the <see cref="IMessage"/> to build</param>
        /// <returns>The configured <see cref="IMessageBuilder"/></returns>
        IMessageBuilder WithPayload(object payload);

        /// <summary>
        /// Sets the <see cref="IMessage"/>'s correlation key
        /// </summary>
        /// <param name="correlationKey">The correlation key of the <see cref="IMessage"/> to build</param>
        /// <returns>The configured <see cref="IMessageBuilder"/></returns>
        IMessageBuilder WithCorrelationKey(object correlationKey);

        /// <summary>
        /// Builds the configured <see cref="IMessage"/>
        /// </summary>
        /// <returns>A new <see cref="IMessage"/></returns>
        IMessage Build();

    }
}
