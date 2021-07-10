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
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Sdk.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IMessageBuilder"/> interface
    /// </summary>
    public class MessageBuilder
        : MessageTraitBuilder<IMessageBuilder, Message>, IMessageBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="MessageBuilder"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        /// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="MessageTrait"/>s</param>
        public MessageBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<Message>> validators) 
            : base(serviceProvider, validators)
        {

        }

        /// <inheritdoc/>
        public virtual IMessageBuilder OfType<TPayload>()
        {
            return this.OfType(typeof(TPayload));
        }

        /// <inheritdoc/>
        public virtual IMessageBuilder OfType(Type payloadType)
        {
            if (payloadType == null)
                throw new ArgumentNullException(nameof(payloadType));
            return this.WithPayloadSchema(new JSchemaGenerator().Generate(payloadType));
        }

        /// <inheritdoc/>
        public virtual IMessageBuilder WithPayloadSchema(JSchema payloadSchema)
        {
            if (payloadSchema == null)
                throw new ArgumentNullException(nameof(payloadSchema));
            this.Trait.Payload = JObject.FromObject(payloadSchema);
            return this;
        }

        /// <inheritdoc/>
        public virtual IMessageBuilder WithTrait(Action<IMessageTraitBuilder> setup)
        {
            if (setup == null)
                throw new ArgumentNullException(nameof(setup));
            if (this.Trait.Traits == null)
                this.Trait.Traits = new();
            IMessageTraitBuilder builder = ActivatorUtilities.CreateInstance<MessageTraitBuilder>(this.ServiceProvider);
            setup(builder);
            this.Trait.Traits.Add(builder.Build());
            return this;
        }

    }

}
