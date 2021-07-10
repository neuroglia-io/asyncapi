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
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Neuroglia.AsyncApi.Models
{

    /// <summary>
    /// Represents an object used to describe a message broker, a server or any other kind of computer program capable of sending and/or receiving data.
    /// </summary>
    /// <remarks>
    /// This object is used to capture details such as URIs, protocols and security configuration. 
    /// Variable substitution can be used so that some details, for example usernames and passwords, can be injected by code generation tools.
    /// </remarks>
    public class Server
    {

        /// <summary>
        /// Gets/sets the <see cref="Uri"/> pointing to the contact information.
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonProperty("url")]
        [YamlDotNet.Serialization.YamlMember(Alias = "url")]
        [System.Text.Json.Serialization.JsonPropertyName("url")]
        public virtual Uri Url { get; set; }

        /// <summary>
        /// Gets/sets the protocol this URL supports for connection. 
        /// Supported protocol include, but are not limited to: amqp, amqps, http, https, ibmmq, jms, kafka, kafka-secure, mqtt, secure-mqtt, stomp, stomps, ws, wss, mercure.
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonProperty("protocol")]
        [YamlDotNet.Serialization.YamlMember(Alias = "protocol")]
        [System.Text.Json.Serialization.JsonPropertyName("protocol")]
        public virtual string Protocol { get; set; }

        /// <summary>
        /// Gets/sets the version of the protocol used for connection. For instance: AMQP 0.9.1, HTTP 2.0, Kafka 1.0.0, etc.
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonProperty("protocolVersion")]
        [YamlDotNet.Serialization.YamlMember(Alias = "protocolVersion")]
        [System.Text.Json.Serialization.JsonPropertyName("protocolVersion")]
        public virtual string ProtocolVersion { get; set; }

        /// <summary>
        /// Gets/sets an optional string describing the host designated by the URL. <see href="https://spec.commonmark.org/">CommonMark</see> syntax MAY be used for rich text representation.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("description")]
        [YamlDotNet.Serialization.YamlMember(Alias = "description")]
        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets/sets an optional string describing the host designated by the URL. <see href="https://spec.commonmark.org/">CommonMark</see> syntax MAY be used for rich text representation.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("variables")]
        [YamlDotNet.Serialization.YamlMember(Alias = "variables")]
        [System.Text.Json.Serialization.JsonPropertyName("variables")]
        public virtual Dictionary<string, VariableDefinition> Variables { get; set; }

        /// <summary>
        /// Gets/sets an <see cref="IList{T}"/> of values that represent alternative security requirement objects that can be used. 
        /// Only one of the security requirement objects need to be satisfied to authorize a connection or operation. 
        /// </summary>
        [Newtonsoft.Json.JsonProperty("security")]
        [YamlDotNet.Serialization.YamlMember(Alias = "security")]
        [System.Text.Json.Serialization.JsonPropertyName("security")]
        public virtual Dictionary<string, JObject> Security { get; set; }

        /// <summary>
        /// Gets/sets an object used to configure the <see cref="Server"/>'s <see cref="IServerBinding"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindings")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindings")]
        [System.Text.Json.Serialization.JsonPropertyName("bindings")]
        public virtual ServerBindingCollection Bindings { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Url.ToString();
        }

    }

}
