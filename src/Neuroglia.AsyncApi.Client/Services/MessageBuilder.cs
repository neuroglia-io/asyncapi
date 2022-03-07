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
    /// Represents the default implementation of the <see cref="IMessageBuilder"/> interface
    /// </summary>
    public class MessageBuilder
        : IMessageBuilder
    {

        /// <summary>
        /// Gets the <see cref="Client.Message"/> to configure
        /// </summary>
        protected Message Message { get; } = new();

        /// <inheritdoc/>
        public virtual IMessageBuilder WithCorrelationKey(object correlationKey)
        {
            this.Message.CorrelationKey = correlationKey;
            return this;
        }

        /// <inheritdoc/>
        public virtual IMessageBuilder WithHeader(string key, object value)
        {
            this.Message.Headers.Add(key, value);
            return this;
        }

        /// <inheritdoc/>
        public virtual IMessageBuilder WithHeaders(IDictionary<string, object> headers)
        {
            foreach(KeyValuePair<string, object> header in headers)
            {
                this.Message.Headers.Add(header.Key, header.Value);
            }
            return this;
        }

        /// <inheritdoc/>
        public virtual IMessageBuilder WithPayload(object payload)
        {
            this.Message.Payload = payload;
            return this;
        }

        /// <inheritdoc/>
        public virtual IMessageBuilder WithTimestamp(DateTime timestamp)
        {
            this.Message.Timestamp = timestamp;
            return this;
        }

        /// <inheritdoc/>
        public virtual IMessage Build()
        {
            return this.Message;
        }

    }
}
