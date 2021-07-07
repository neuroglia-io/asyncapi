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
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Sdk.Models
{
    /// <summary>
    /// Represents a set of reusable objects for different aspects of the AsyncAPI specification. 
    /// All objects defined within the components object will have no effect on the API unless they are explicitly referenced from properties outside the components object.
    /// </summary>
    public class ComponentCollection
    {

        /// <summary>
        /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> used to hold reusable <see cref="JSchema"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("schemas")]
        [YamlDotNet.Serialization.YamlMember(Alias = "schemas")]
        [System.Text.Json.Serialization.JsonPropertyName("schemas")]
        public virtual Dictionary<string, JSchema> Schemas { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> used to hold reusable <see cref="MessageDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("messages")]
        [YamlDotNet.Serialization.YamlMember(Alias = "messages")]
        [System.Text.Json.Serialization.JsonPropertyName("messages")]
        public virtual Dictionary<string, MessageDefinition> Messages { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> used to hold reusable <see cref="SecuritySchemeDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("securitySchemes")]
        [YamlDotNet.Serialization.YamlMember(Alias = "securitySchemes")]
        [System.Text.Json.Serialization.JsonPropertyName("securitySchemes")]
        public virtual Dictionary<string, SecuritySchemeDefinition> SecuritySchemes { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> used to hold reusable <see cref="ParameterDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("parameters")]
        [YamlDotNet.Serialization.YamlMember(Alias = "parameters")]
        [System.Text.Json.Serialization.JsonPropertyName("parameters")]
        public virtual Dictionary<string, ParameterDefinition> Parameters { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> used to hold reusable <see cref="CorrelationIdDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("correlationIds")]
        [YamlDotNet.Serialization.YamlMember(Alias = "correlationIds")]
        [System.Text.Json.Serialization.JsonPropertyName("correlationIds")]
        public virtual Dictionary<string, CorrelationIdDefinition> CorrelationIds { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> used to hold reusable <see cref="OperationTraitDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("operationTraits")]
        [YamlDotNet.Serialization.YamlMember(Alias = "operationTraits")]
        [System.Text.Json.Serialization.JsonPropertyName("operationTraits")]
        public virtual Dictionary<string, OperationTraitDefinition> OperationTraits { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> used to hold reusable <see cref="MessageTraitDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("messageTraits")]
        [YamlDotNet.Serialization.YamlMember(Alias = "messageTraits")]
        [System.Text.Json.Serialization.JsonPropertyName("messageTraits")]
        public virtual Dictionary<string, MessageTraitDefinition> MessageTraits { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> used to hold reusable <see cref="ServerBindingDefinitionCollection"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("serverBindings")]
        [YamlDotNet.Serialization.YamlMember(Alias = "serverBindings")]
        [System.Text.Json.Serialization.JsonPropertyName("serverBindings")]
        public virtual Dictionary<string, ServerBindingDefinitionCollection> ServerBindings { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> used to hold reusable <see cref="ChannelBindingDefinitionCollection"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("channelBindings")]
        [YamlDotNet.Serialization.YamlMember(Alias = "channelBindings")]
        [System.Text.Json.Serialization.JsonPropertyName("channelBindings")]
        public virtual Dictionary<string, ChannelBindingDefinitionCollection> ChannelBindings { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> used to hold reusable <see cref="OperationBindingDefinitionCollection"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("operationBindings")]
        [YamlDotNet.Serialization.YamlMember(Alias = "operationBindings")]
        [System.Text.Json.Serialization.JsonPropertyName("operationBindings")]
        public virtual Dictionary<string, OperationBindingDefinitionCollection> OperationBindings { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> used to hold reusable <see cref="MessageBindingDefinitionCollection"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("messageBindings")]
        [YamlDotNet.Serialization.YamlMember(Alias = "messageBindings")]
        [System.Text.Json.Serialization.JsonPropertyName("messageBindings")]
        public virtual Dictionary<string, MessageBindingDefinitionCollection> MessageBindings { get; set; }

    }

}
