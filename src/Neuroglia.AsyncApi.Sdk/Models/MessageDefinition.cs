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
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Sdk.Models
{
    /// <summary>
    /// Represents an object used to define a Async API operation message
    /// </summary>
    public class MessageDefinition
        : MessageTraitDefinition
    {

        /// <summary>
        /// Gets/sets the definition of the message payload. It can be of any type but defaults to Schema object. It must match the <see cref="MessageTraitDefinition.SchemaFormat"/>, including encoding type - e.g Avro should be inlined as either a YAML or JSON object NOT a string to be parsed as YAML or JSON.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("payload")]
        [YamlDotNet.Serialization.YamlMember(Alias = "payload")]
        [System.Text.Json.Serialization.JsonPropertyName("payload")]
        public virtual JObject Payload { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> of traits to apply to the message object. Traits MUST be merged into the message object using the JSON Merge Patch algorithm in the same order they are defined here.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("traits")]
        [YamlDotNet.Serialization.YamlMember(Alias = "traits")]
        [System.Text.Json.Serialization.JsonPropertyName("traits")]
        public virtual List<OperationTraitDefinition> Traits { get; set; }

    }

}
