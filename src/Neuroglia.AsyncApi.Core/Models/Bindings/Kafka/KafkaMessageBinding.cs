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
using Newtonsoft.Json.Schema;

namespace Neuroglia.AsyncApi.Models.Bindings.Kafka
{

    /// <summary>
    /// Represents the object used to configure a Kafka message binding
    /// </summary>
    public class KafkaMessageBinding
        : KafkaBindingDefinition, IMessageBindingDefinition
    {

        /// <summary>
        /// Gets/sets a <see cref="JSchema"/> that defines the message key.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("key")]
        [YamlDotNet.Serialization.YamlMember(Alias = "key")]
        [System.Text.Json.Serialization.JsonPropertyName("key")]
        public virtual JSchema Key { get; set; }

        /// <summary>
        /// Gets/sets the version of this binding. Defaults to 'latest'.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindingVersion")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindingVersion")]
        [System.Text.Json.Serialization.JsonPropertyName("bindingVersion")]
        public virtual string BindingVersion { get; set; } = "latest";

    }

}
