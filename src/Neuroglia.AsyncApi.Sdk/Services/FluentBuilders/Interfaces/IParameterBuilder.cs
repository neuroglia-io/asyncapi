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
using Newtonsoft.Json.Schema;
using System;

namespace Neuroglia.AsyncApi.Services.FluentBuilders
{
    /// <summary>
    /// Defines the fundamentals of the service used to build <see cref="Parameter"/>s
    /// </summary>
    public interface IParameterBuilder
    {

        /// <summary>
        /// Configures the type of the <see cref="Parameter"/> to build
        /// </summary>
        /// <typeparam name="TParameter">The type of the <see cref="Parameter"/> to build</typeparam>
        /// <returns>The configured <see cref="IParameterBuilder"/></returns>
        IParameterBuilder OfType<TParameter>();

        /// <summary>
        /// Configures the type of the <see cref="Parameter"/> to build
        /// </summary>
        /// <param name="parameterType">The type of the <see cref="Parameter"/> to build</param>
        /// <returns>The configured <see cref="IParameterBuilder"/></returns>
        IParameterBuilder OfType(Type parameterType);

        /// <summary>
        /// Configures the <see cref="JSchema"/> of the <see cref="Parameter"/> to build
        /// </summary>
        /// <param name="schema">The <see cref="JSchema"/> of the <see cref="Parameter"/> to build</param>
        /// <returns>The configured <see cref="IParameterBuilder"/></returns>
        IParameterBuilder WithSchema(JSchema schema);

        /// <summary>
        /// Configures the <see cref="Parameter"/> to build to use the specified description
        /// </summary>
        /// <param name="description">The description to use</param>
        /// <returns>The configured <see cref="IParameterBuilder"/></returns>
        IParameterBuilder WithDescription(string description);

        /// <summary>
        /// Sets the location of the <see cref="Parameter"/> to build
        /// </summary>
        /// <param name="location">A runtime expression that specifies the location of the parameter value</param>
        /// <returns>The configured <see cref="IParameterBuilder"/></returns>
        IParameterBuilder AtLocation(string location);

        /// <summary>
        /// Builds a new <see cref="Parameter"/>
        /// </summary>
        /// <returns>A new <see cref="Parameter"/></returns>
        Parameter Build();

    }

}
