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
    /// Defines the fundamentals of a service used to build <see cref="Server"/>s
    /// </summary>
    public interface IServerBuilder
    {

        /// <summary>
        /// Configures the <see cref="Server"/> to use the specified url
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> of the <see cref="Server"/> to build</param>
        /// <returns>The configured <see cref="IServerBuilder"/></returns>
        IServerBuilder WithUrl(Uri uri);

        /// <summary>
        /// Configures the <see cref="Server"/> to use the specified protocol
        /// </summary>
        /// <param name="protocol">The protocol to use</param>
        /// <param name="version">The protocol version to use</param>
        /// <returns>The configured <see cref="IServerBuilder"/></returns>
        IServerBuilder WithProtocol(string protocol, string version = null);

        /// <summary>
        /// Configures the <see cref="Server"/> to use the specified description
        /// </summary>
        /// <param name="description">The description to use</param>
        /// <returns>The configured <see cref="IServerBuilder"/></returns>
        IServerBuilder WithDescription(string description);

        /// <summary>
        /// Adds the specified <see cref="VariableDefinition"/> to the <see cref="Server"/> to build
        /// </summary>
        /// <param name="name">The name of the <see cref="VariableDefinition"/> to add</param>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="VariableDefinition"/> to add</param>
        /// <returns>The configured <see cref="IServerBuilder"/></returns>
        IServerBuilder AddVariable(string name, Action<IVariableBuilder> setup);

        /// <summary>
        /// Configures the <see cref="Server"/> to build to use the specified <see cref="SecurityScheme"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="SecurityScheme"/> to add</param>
        /// <returns>The configured <see cref="IServerBuilder"/></returns>
        IServerBuilder UseSecurityScheme(string name);

        /// <summary>
        /// Adds the specified <see cref="IServerBinding"/> to the <see cref="Server"/> to build
        /// </summary>
        /// <param name="binding">The <see cref="IServerBinding"/> to add</param>
        /// <returns>The configured <see cref="IServerBuilder"/></returns>
        IServerBuilder UseBinding(IServerBinding binding);

        /// <summary>
        /// Builds a new <see cref="Server"/>
        /// </summary>
        /// <returns>A new <see cref="Server"/></returns>
        Server Build();

    }

}
