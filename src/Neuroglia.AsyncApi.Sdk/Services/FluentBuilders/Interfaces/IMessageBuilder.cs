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
    /// Defines the fundamentals of a service used to build <see cref="Message"/>s
    /// </summary>
    public interface IMessageBuilder
        : IMessageTraitBuilder<IMessageBuilder, Message>
    {

        /// <summary>
        /// Configures the <see cref="MessageTrait"/> to build to use the specified payload
        /// </summary>
        /// <typeparam name="TPayload">The type used to define the <see cref="MessageTrait"/>'s payload</typeparam>
        /// <returns>The configured <see cref="IMessageBuilder"/></returns>
        IMessageBuilder OfType<TPayload>();

        /// <summary>
        /// Configures the <see cref="MessageTrait"/> to build to use the specified payload
        /// </summary>
        /// <param name="payloadType">The type used to define the <see cref="MessageTrait"/>'s payload</param>
        /// <returns>The configured <see cref="IMessageBuilder"/></returns>
        IMessageBuilder OfType(Type payloadType);

        /// <summary>
        /// Configures the <see cref="MessageTrait"/> to build to use the specified payload
        /// </summary>
        /// <param name="payloadSchema">The <see cref="JSchema"/> used to define the <see cref="MessageTrait"/>'s payload</param>
        /// <returns>The configured <see cref="IMessageBuilder"/></returns>
        IMessageBuilder WithPayloadSchema(JSchema payloadSchema);

        /// <summary>
        /// Configures the <see cref="Message"/> to build to use the specified <see cref="MessageTrait"/>
        /// </summary>
        /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="MessageTrait"/> to use</param>
        /// <returns>The configured <see cref="IMessageBuilder"/></returns>
        IMessageBuilder WithTrait(Action<IMessageTraitBuilder> setup);

    }

}
