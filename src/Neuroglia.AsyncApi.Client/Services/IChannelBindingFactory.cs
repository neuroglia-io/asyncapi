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
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Client.Services
{

    /// <summary>
    /// Defines the fundamentals of a service used to create <see cref="IChannelBinding"/>s
    /// </summary>
    public interface IChannelBindingFactory
    {

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="IChannelBindingFactory"/> can create bindings for the specified protocol
        /// </summary>
        /// <param name="protocol">The protocol for which to create a new <see cref="IChannelBinding"/></param>
        /// <returns>A boolean indicating whether or not the <see cref="IChannelBindingFactory"/> can create bindings for the specified protocol</returns>
        bool SupportsProtocol(string protocol);

        /// <summary>
        /// Creates a new <see cref="IChannelBinding"/> for the specified <see cref="IChannel"/>
        /// </summary>
        /// <param name="channel">The <see cref="IChannel"/> to create the <see cref="IChannelBinding"/> for</param>
        /// <param name="servers">An <see cref="IEnumerable{T}"/> containing the mappings of the <see cref="ServerDefinition"/>s supported by the <see cref="IChannelBinding"/> to create</param>
        /// <returns>A new <see cref="IChannelBinding"/></returns>
        IChannelBinding CreateBinding(IChannel channel, IEnumerable<KeyValuePair<string, ServerDefinition>> servers);

    }

}
