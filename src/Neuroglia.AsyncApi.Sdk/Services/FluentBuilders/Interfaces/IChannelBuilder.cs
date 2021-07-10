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
using Neuroglia.AsyncApi.Sdk.Models;
using Neuroglia.AsyncApi.Sdk.Models.Bindings;
using System;

namespace Neuroglia.AsyncApi.Sdk.Services.FluentBuilders
{

    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="Channel"/>s
    /// </summary>
    public interface IChannelBuilder
    {

        /// <summary>
        /// Configures the <see cref="Channel"/> to build to use the specified description
        /// </summary>
        /// <param name="description">The <see cref="Channel"/>'s description</param>
        /// <returns>The configured <see cref="IChannelBuilder"/></returns>
        IChannelBuilder WithDescription(string description);

        /// <summary>
        /// Adds a new <see cref="Parameter"/> to the <see cref="Channel"/> to build
        /// </summary>
        /// <param name="name">The name of the <see cref="Parameter"/> to add</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="Parameter"/> to add</param>
        /// <returns>The configured <see cref="IChannelBuilder"/></returns>
        IChannelBuilder AddParameter(string name, Action<IParameterBuilder> setup);

        /// <summary>
        /// Adds the specified <see cref="IChannelBinding"/> to the <see cref="Channel"/> to build
        /// </summary>
        /// <param name="binding">The <see cref="IChannelBinding"/> to add</param>
        /// <returns>The configured <see cref="IChannelBuilder"/></returns>
        IChannelBuilder UseBinding(IChannelBinding binding);

        /// <summary>
        /// Defines and configures the Subscribe operation of the <see cref="Channel"/> to build
        /// </summary>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the Subscribe <see cref="Operation"/></param>
        /// <returns>The configured <see cref="IChannelBuilder"/></returns>
        IChannelBuilder DefineSubscribeOperation(Action<IOperationBuilder> setup);

        /// <summary>
        /// Defines and configures the Publish operation of the <see cref="Channel"/> to build
        /// </summary>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the Publish <see cref="Operation"/></param>
        /// <returns>The configured <see cref="IChannelBuilder"/></returns>
        IChannelBuilder DefinePublishOperation(Action<IOperationBuilder> setup);

        /// <summary>
        /// Builds a new <see cref="Channel"/>
        /// </summary>
        /// <returns>A new <see cref="Channel"/></returns>
        Channel Build();

    }

}
