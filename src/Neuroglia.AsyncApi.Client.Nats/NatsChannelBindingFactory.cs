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
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Client.Services;
using Neuroglia.AsyncApi.Models;
using Neuroglia.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neuroglia.AsyncApi.Client
{

    /// <summary>
    /// Represents the NATS implementation of the <see cref="IChannelBindingFactory"/> interface
    /// </summary>
    public class NatsChannelBindingFactory
        : IChannelBindingFactory
    {

        /// <summary>
        /// Initializes a new <see cref="NatsChannelBindingFactory"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="ISerializerProvider"/></param>
        public NatsChannelBindingFactory(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected virtual IServiceProvider ServiceProvider { get; }

        /// <inheritdoc/>
        public virtual bool SupportsProtocol(string protocol)
        {
            if (string.IsNullOrWhiteSpace(protocol))
                throw new ArgumentNullException(nameof(protocol));
            return protocol.Equals(AsyncApiProtocols.Nats, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc/>
        public virtual IChannelBinding CreateBinding(IChannel channel, IEnumerable<KeyValuePair<string, ServerDefinition>> servers)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));
            if (servers == null)
                throw new ArgumentNullException(nameof(servers));
            if (!servers.Any())
                throw new ArgumentOutOfRangeException(nameof(servers));
            return ActivatorUtilities.CreateInstance<NatsChannelBinding>(this.ServiceProvider, channel, servers);
        }

    }

}
