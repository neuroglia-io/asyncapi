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

namespace Neuroglia.AsyncApi.Sdk.Models.Bindings.Mqtt
{

    /// <summary>
    /// Represents the object used to configure an MQTT server binding
    /// </summary>
    public class MqttServerBinding
        : IServerBinding
    {

        /// <summary>
        /// Gets/sets the client identifier.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("clientId")]
        [YamlDotNet.Serialization.YamlMember(Alias = "clientId")]
        [System.Text.Json.Serialization.JsonPropertyName("clientId")]
        public virtual string ClientId { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether to create a persisten connection or not. When false, the connection will is persistent.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("cleanSession")]
        [YamlDotNet.Serialization.YamlMember(Alias = "cleanSession")]
        [System.Text.Json.Serialization.JsonPropertyName("cleanSession")]
        public virtual bool CleanSession { get; set; }

        /// <summary>
        /// Gets/sets the last Will and Testament configuration.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("lastWill")]
        [YamlDotNet.Serialization.YamlMember(Alias = "lastWill")]
        [System.Text.Json.Serialization.JsonPropertyName("lastWill")]
        public virtual MqttLastWill LastWill { get; set; }

        /// <summary>
        /// Gets/sets an interval in seconds of the longest period of time the broker and the client can endure without sending a message.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("keepAlive")]
        [YamlDotNet.Serialization.YamlMember(Alias = "keepAlive")]
        [System.Text.Json.Serialization.JsonPropertyName("keepAlive")]
        public virtual bool KeepAlive { get; set; }

        /// <summary>
        /// Gets/sets the version of this binding. Defaults to 'latest'.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindingVersion")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindingVersion")]
        [System.Text.Json.Serialization.JsonPropertyName("bindingVersion")]
        public virtual string BindingVersion { get; set; } = "latest";

    }

}
