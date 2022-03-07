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
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Models.Bindings.Amqp
{
    /// <summary>
    /// Represents the base class for all AMQP implementations of the <see cref="IBindingDefinition"/> interface
    /// </summary>
    public abstract class AmqpBindingDefinition
        : IBindingDefinition
    {

        /// <inheritdoc/>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlDotNet.Serialization.YamlIgnore]
        public IEnumerable<string> Protocols
        {
            get
            {
                yield return AsyncApiProtocols.Amqp;
            }
        }

    }

}
