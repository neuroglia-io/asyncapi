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
using Neuroglia.AsyncApi.Client.Services;
using Neuroglia.AsyncApi.Models;
using System;
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Client.Configuration
{
    /// <summary>
    /// Represents the options used to configure an <see cref="IAsyncApiClient"/>
    /// </summary>
    public class AsyncApiClientOptions
    {

        /// <summary>
        /// Gets/sets the <see cref="AsyncApiDocument"/> to build a new <see cref="IAsyncApiClient"/> for
        /// </summary>
        public virtual AsyncApiDocument Document { get; set; }

        /// <summary>
        /// Gets/sets an <see cref="Uri"/> referencing the <see cref="AsyncApiDocument"/> to build a new <see cref="IAsyncApiClient"/> for
        /// </summary>
        public virtual Uri DocumentUri { get; set; }

        /// <summary>
        /// Gets/sets the type of <see cref="IChannelFactory"/> to use
        /// </summary>
        public virtual Type ChannelFactoryType { get; set; } = typeof(ChannelFactory);

        /// <summary>
        /// Gets a <see cref="List{T}"/> containing the <see cref="IChannelBindingFactory"/> types to use
        /// </summary>
        public virtual List<Type> ChannelBindingFactoryTypes { get; set; } = new();

    }

}
