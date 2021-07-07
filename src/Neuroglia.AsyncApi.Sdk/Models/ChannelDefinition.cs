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
using Neuroglia.AsyncApi.Sdk.Models.Bindings;
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Sdk.Models
{

    /// <summary>
    /// Represents an object used to define an Async API channel
    /// </summary>
    public class ChannelDefinition
        : ReferenceableComponent
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
        /// Gets/sets an object used to configure the <see cref="ChannelDefinition"/>'s <see cref="IChannelBindingDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindings")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindings")]
        [System.Text.Json.Serialization.JsonPropertyName("bindings")]
        public virtual MessageBindingDefinitionCollection Bindings { get; set; }

    }

}
