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

namespace Neuroglia.AsyncApi.Models
{
    /// <summary>
    /// Represents an object used to define a Async API operation
    /// </summary>
    public class Operation
        : OperationTrait
    {

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> of traits to apply to the operation object. Traits MUST be merged into the operation object using the JSON Merge Patch algorithm in the same order they are defined here.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("traits")]
        [YamlDotNet.Serialization.YamlMember(Alias = "traits")]
        [System.Text.Json.Serialization.JsonPropertyName("traits")]
        public virtual List<OperationTrait> Traits { get; set; }

        /// <summary>
        /// Gets/sets a definition of the message that will be published or received on this channel.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("message")]
        [YamlDotNet.Serialization.YamlMember(Alias = "message")]
        [System.Text.Json.Serialization.JsonPropertyName("message")]
        public virtual Message Message { get; set; }

    }

}
