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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Sdk.Models
{
    /// <summary>
    /// Represents an object used to define a Async API operation message trait
    /// </summary>
    public class MessageTraitDefinition
    {

        /// <summary>
        /// Gets/sets a <see cref="JSchema"/> of the application headers. Schema MUST be of type "object". It MUST NOT define the protocol headers.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("headers")]
        [YamlDotNet.Serialization.YamlMember(Alias = "headers")]
        [System.Text.Json.Serialization.JsonPropertyName("headers")]
        public virtual JSchema Headers { get; set; }

        /// <summary>
        /// Gets/sets the definition of the correlation ID used for message tracing or matching.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("correlationId")]
        [YamlDotNet.Serialization.YamlMember(Alias = "correlationId")]
        [System.Text.Json.Serialization.JsonPropertyName("correlationId")]
        public virtual CorrelationIdDefinition CorrelationId { get; set; }

        /// <summary>
        /// Gets/sets a string containing the name of the schema format used to define the message payload. 
        /// If omitted, implementations should parse the payload as a <see cref="JSchema"/>.
        /// When the payload is defined using a $ref to a remote file, it is RECOMMENDED the schema format includes the file encoding type to allow implementations to parse the file correctly. E.g., adding +yaml if content type is application/vnd.apache.avro results in application/vnd.apache.avro+yaml.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("schemaFormat")]
        [YamlDotNet.Serialization.YamlMember(Alias = "schemaFormat")]
        [System.Text.Json.Serialization.JsonPropertyName("schemaFormat")]
        public virtual string SchemaFormat { get; set; }

        /// <summary>
        /// Gets/sets the content type to use when encoding/decoding a message's payload. The value MUST be a specific media type (e.g. application/json). When omitted, the value MUST be the one specified on the <see cref="AsyncApiDocument.DefaultContentType"/> property.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("contentType")]
        [YamlDotNet.Serialization.YamlMember(Alias = "contentType")]
        [System.Text.Json.Serialization.JsonPropertyName("contentType")]
        public virtual string ContentType { get; set; }

        /// <summary>
        /// Gets/sets a machine-friendly name for the message.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("name")]
        [YamlDotNet.Serialization.YamlMember(Alias = "name")]
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets a human-friendly title for the message.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("title")]
        [YamlDotNet.Serialization.YamlMember(Alias = "title")]
        [System.Text.Json.Serialization.JsonPropertyName("title")]
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets/sets a short summary of what the message is about.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("summary")]
        [YamlDotNet.Serialization.YamlMember(Alias = "summary")]
        [System.Text.Json.Serialization.JsonPropertyName("summary")]
        public virtual string Summary { get; set; }

        /// <summary>
        /// Gets/sets an optional description of the message. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("description")]
        [YamlDotNet.Serialization.YamlMember(Alias = "description")]
        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public virtual string Description { get; set; }

        /// <summary>
        /// gets/sets a <see cref="List{T}"/> of tags for API documentation control. Tags can be used for logical grouping of operations.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("tags")]
        [YamlDotNet.Serialization.YamlMember(Alias = "tags")]
        [System.Text.Json.Serialization.JsonPropertyName("tags")]
        public virtual List<TagDefinition> Tags { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing additional external documentation for this message.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("externalDocs")]
        [YamlDotNet.Serialization.YamlMember(Alias = "externalDocs")]
        [System.Text.Json.Serialization.JsonPropertyName("externalDocs")]
        public virtual List<ExternalDocumentationDefinition> ExternalDocs { get; set; }

        /// <summary>
        /// Gets/sets an object used to configure the <see cref="MessageTraitDefinition"/>'s <see cref="IOperationBinding"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindings")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindings")]
        [System.Text.Json.Serialization.JsonPropertyName("bindings")]
        public virtual MessageBindingCollection Bindings { get; set; }

        /// <summary>
        /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> where keys MUST be either headers and/or payload. 
        /// Values MUST contain examples that validate against the headers or payload fields, respectively. 
        /// Example MAY also have the name and summary additional keys to provide respectively a machine-friendly name and a short summary of what the example is about.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("examples")]
        [YamlDotNet.Serialization.YamlMember(Alias = "examples")]
        [System.Text.Json.Serialization.JsonPropertyName("examples")]
        public virtual Dictionary<string, JObject> Examples { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}
