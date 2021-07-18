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

namespace Neuroglia.AsyncApi.Services.FluentBuilders
{
    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="Variable"/>s
    /// </summary>
    public interface IVariableBuilder
    {

        /// <summary>
        /// Configures the <see cref="Variable"/> to build to use the specified enum values
        /// </summary>
        /// <param name="values">An array of values to be used if the substitution options are from a limited set.</param>
        /// <returns>The configured <see cref="IVariableBuilder"/></returns>
        IVariableBuilder WithEnumValues(params string[] values);

        /// <summary>
        /// Configures the <see cref="Variable"/> to build to use the specified default value
        /// </summary>
        /// <param name="value">The value to use by default for substitution, and to send, if an alternate value is not supplied.</param>
        /// <returns>The configured <see cref="IVariableBuilder"/></returns>
        IVariableBuilder WithDefaultValue(string value);

        /// <summary>
        /// Configures the <see cref="Variable"/> to use the specified description
        /// </summary>
        /// <param name="description">The description to use</param>
        /// <returns>The configured <see cref="IVariableBuilder"/></returns>
        IVariableBuilder WithDescription(string description);

        /// <summary>
        /// Adds the specified example to the <see cref="Variable"/> to build
        /// </summary>
        /// <param name="example">The example to add</param>
        /// <returns>The configured <see cref="IVariableBuilder"/></returns>
        IVariableBuilder AddExample(string example);

        /// <summary>
        /// Builds a new <see cref="Variable"/>
        /// </summary>
        /// <returns>A new <see cref="Variable"/></returns>
        Variable Build();

    }

}
