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
using Neuroglia.AsyncApi.Models.Bindings;
using System;
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Models
{

    /// <summary>
    /// Represents an object used to define an Async API channel
    /// </summary>
    public class ChannelDefinition
        : ReferenceableComponentDefinition
    {

        /// <summary>
        /// Gets/sets an optional description of this channel item. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("description")]
        [YamlDotNet.Serialization.YamlMember(Alias = "description")]
        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets/sets a definition of the SUBSCRIBE operation, which defines the messages produced by the application and sent to the channel.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("subscribe")]
        [YamlDotNet.Serialization.YamlMember(Alias = "subscribe")]
        [System.Text.Json.Serialization.JsonPropertyName("subscribe")]
        public virtual OperationDefinition Subscribe { get; set; }

        /// <summary>
        /// Gets/sets a definition of the PUBLISH operation, which defines the messages consumed by the application from the channel.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("publish")]
        [YamlDotNet.Serialization.YamlMember(Alias = "publish")]
        [System.Text.Json.Serialization.JsonPropertyName("publish")]
        public virtual OperationDefinition Publish { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> of the parameters included in the channel name. It SHOULD be present only when using channels with expressions (as defined by RFC 6570 section 2.2).
        /// </summary>
        [Newtonsoft.Json.JsonProperty("parameters")]
        [YamlDotNet.Serialization.YamlMember(Alias = "parameters")]
        [System.Text.Json.Serialization.JsonPropertyName("parameters")]
        public virtual Dictionary<string, ParameterDefinition> Parameters { get; set; }

        /// <summary>
        /// Gets/sets an object used to configure the <see cref="ChannelDefinition"/>'s <see cref="IChannelBinding"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindings")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindings")]
        [System.Text.Json.Serialization.JsonPropertyName("bindings")]
        public virtual ChannelBindingDefinitionCollection Bindings { get; set; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the <see cref="ChannelDefinition"/>'s <see cref="OperationDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlDotNet.Serialization.YamlIgnore]
        public virtual IEnumerable<OperationDefinition> Operations
        {
            get
            {
                yield return this.Subscribe;
                yield return this.Publish;
            }
        }

        /// <summary>
        /// Determines whether or not the <see cref="ChannelDefinition"/> defines an <see cref="OperationDefinition"/> with the specified id
        /// </summary>
        /// <param name="operationId">The id of the operation to check</param>
        /// <returns>A boolean indicating whether or not the <see cref="ChannelDefinition"/> defines an <see cref="OperationDefinition"/> with the specified id</returns>
        public virtual bool DefinesOperationWithId(string operationId)
        {
            if (string.IsNullOrWhiteSpace(operationId))
                throw new ArgumentNullException(nameof(operationId));
            return this.Publish?.OperationId == operationId || this.Subscribe?.OperationId == operationId;
        }

        /// <summary>
        /// Determines whether or not the <see cref="ChannelDefinition"/> defines an <see cref="OperationDefinition"/> of the specified type
        /// </summary>
        /// <param name="type">The type of the operation to check</param>
        /// <returns>A boolean indicating whether or not the <see cref="ChannelDefinition"/> defines an <see cref="OperationDefinition"/> of the specified type</returns>
        public virtual bool DefinesOperationOfType(OperationType type)
        {
            return type switch
            {
                OperationType.Publish => this.Publish != null,
                OperationType.Subscribe => this.Subscribe != null,
                _ => throw new NotSupportedException($"The specified operation type '{type}' is not supported"),
            };
        }

        /// <summary>
        /// Gets the <see cref="OperationDefinition"/> with the specified id
        /// </summary>
        /// <param name="operationId">The id of the <see cref="OperationDefinition"/> to get</param>
        /// <returns>The <see cref="OperationDefinition"/> with the specified id, if any</returns>
        public virtual OperationDefinition GetOperationById(string operationId)
        {
            if (string.IsNullOrWhiteSpace(operationId))
                throw new ArgumentNullException(nameof(operationId));
            if (this.Publish?.OperationId == operationId)
                return this.Publish;
            if (this.Subscribe?.OperationId == operationId)
                return this.Subscribe;
            return null;
        }

        /// <summary>
        /// Attempts to retrieve an <see cref="OperationDefinition"/> by id
        /// </summary>
        /// <param name="operationId">The id of the <see cref="OperationDefinition"/> to get</param>
        /// <param name="operation">The <see cref="OperationDefinition"/> with the specified id</param>
        /// <returns>A boolean indicating whether or not the <see cref="OperationDefinition"/> with the specified id could be found</returns>
        public virtual bool TryGetOperationById(string operationId, out OperationDefinition operation)
        {
            if (string.IsNullOrWhiteSpace(operationId))
                throw new ArgumentNullException(nameof(operationId));
            operation = this.GetOperationById(operationId);
            return operation != null;
        }

        /// <summary>
        /// Gets the <see cref="OperationDefinition"/> of the specified type
        /// </summary>
        /// <param name="type">The type of the <see cref="OperationDefinition"/> to get</param>
        /// <returns>The <see cref="OperationDefinition"/> of the specified type, if any</returns>
        public virtual OperationDefinition GetOperationByType(OperationType type)
        {
            return type switch
            {
                OperationType.Publish => this.Publish,
                OperationType.Subscribe => this.Subscribe,
                _ => throw new NotSupportedException($"The specified operation type '{type}' is not supported"),
            };
        }

        /// <summary>
        /// Attempts to retrieve an <see cref="OperationDefinition"/> by type
        /// </summary>
        /// <param name="type">The type of the <see cref="OperationDefinition"/> to get</param>
        /// <param name="operation">The <see cref="OperationDefinition"/> of the specified type</param>
        /// <returns>A boolean indicating whether or not the <see cref="OperationDefinition"/> of the specified type could be found</returns>
        public virtual bool TryGetOperationById(OperationType type, out OperationDefinition operation)
        {
            operation = this.GetOperationByType(type);
            return operation != null;
        }

    }

}
