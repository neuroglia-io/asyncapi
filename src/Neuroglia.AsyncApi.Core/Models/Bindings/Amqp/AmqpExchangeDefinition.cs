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
namespace Neuroglia.AsyncApi.Models.Bindings.Amqp
{
    /// <summary>
    /// Represents the object used to configure an AMQP routing key based channel
    /// </summary>
    public class AmqpExchangeDefinition
    {

        /// <summary>
        /// Gets/sets the name of the exchange. It MUST NOT exceed 255 characters long.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name")]
        [YamlDotNet.Serialization.YamlMember(Alias = "name")]
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="AmqpExchangeDefinition"/>'s type
        /// </summary>
        [Newtonsoft.Json.JsonProperty("type")]
        [YamlDotNet.Serialization.YamlMember(Alias = "type")]
        [System.Text.Json.Serialization.JsonPropertyName("type")]
        public virtual AmqpExchangeType Type { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether the exchange should survive broker restarts or not.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("durable")]
        [YamlDotNet.Serialization.YamlMember(Alias = "durable")]
        [System.Text.Json.Serialization.JsonPropertyName("durable")]
        public virtual bool Durable { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether the exchange should be deleted when the last queue is unbound from it.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("autoDelete")]
        [YamlDotNet.Serialization.YamlMember(Alias = "autoDelete")]
        [System.Text.Json.Serialization.JsonPropertyName("autoDelete")]
        public virtual bool AutoDelete { get; set; }

        /// <summary>
        /// Gets/sets the virtual host of the exchange. Defaults to '/'.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("vhost")]
        [YamlDotNet.Serialization.YamlMember(Alias = "vhost")]
        [System.Text.Json.Serialization.JsonPropertyName("vhost")]
        public virtual string VirtualHost { get; set; } = "/";

    }

}
