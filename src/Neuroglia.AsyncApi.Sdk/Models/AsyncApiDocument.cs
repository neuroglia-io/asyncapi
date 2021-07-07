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
using System.ComponentModel.DataAnnotations;

namespace Neuroglia.AsyncApi.Sdk.Models
{

    /// <summary>
    /// Represents an <see href="https://www.asyncapi.com">Async API</see> document
    /// </summary>
    public class AsyncApiDocument
    {

        /// <summary>
        /// Gets/sets the the AsyncAPI Specification version being used. It can be used by tooling Specifications and clients to interpret the version. 
        /// </summary>
        /// <remarks>
        /// The structure shall be major.minor.patch, where patch versions must be compatible with the existing major.minor tooling. 
        /// Typically patch versions will be introduced to address errors in the documentation, and tooling should typically be compatible with the corresponding major.minor (1.0.*). 
        /// Patch versions will correspond to patches of this document.
        /// </remarks>
        [Required]
        [Newtonsoft.Json.JsonProperty("asyncapi")]
        [YamlDotNet.Serialization.YamlMember(Alias = "asyncapi")]
        [System.Text.Json.Serialization.JsonPropertyName("asyncapi")]
        public virtual string AsyncApi { get; set; } = "2.1.0";

        /// <summary>
        /// Gets/sets the identifier of the application the AsyncAPI document is defining.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("id")]
        [YamlDotNet.Serialization.YamlMember(Alias = "id")]
        [System.Text.Json.Serialization.JsonPropertyName("id")]
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets/sets the object that provides metadata about the API. The metadata can be used by the clients if needed. 
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonProperty("info")]
        [YamlDotNet.Serialization.YamlMember(Alias = "info")]
        [System.Text.Json.Serialization.JsonPropertyName("info")]
        public virtual ApiDefinition Info { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="Dictionary{TKey, TValue}"/> containing name/configuration mappings for the application's servers
        /// </summary>
        [Newtonsoft.Json.JsonProperty("servers")]
        [YamlDotNet.Serialization.YamlMember(Alias = "servers")]
        [System.Text.Json.Serialization.JsonPropertyName("servers")]
        public virtual Dictionary<string, ServerDefinition> Servers { get; set; }

        /// <summary>
        /// Gets/sets the default content type to use when encoding/decoding a message's payload.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("defaultContentType")]
        [YamlDotNet.Serialization.YamlMember(Alias = "defaultContentType")]
        [System.Text.Json.Serialization.JsonPropertyName("defaultContentType")]
        public virtual string DefaultContentType { get; set; }

        /// <summary>
        /// Gets/sets a collection containing the available channels and messages for the API.
        /// </summary>
        [Required, MinLength(1)]
        [Newtonsoft.Json.JsonProperty("channels")]
        [YamlDotNet.Serialization.YamlMember(Alias = "channels")]
        [System.Text.Json.Serialization.JsonPropertyName("channels")]
        public virtual Dictionary<string, ChannelDefinition> Channels { get; set; }

        /// <summary>
        /// Gets/sets an object used to hold various schemas for the specification.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("components")]
        [YamlDotNet.Serialization.YamlMember(Alias = "components")]
        [System.Text.Json.Serialization.JsonPropertyName("components")]
        public virtual ComponentCollection Components { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> of tags used by the specification with additional metadata. Each tag name in the list MUST be unique.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("tags")]
        [YamlDotNet.Serialization.YamlMember(Alias = "tags")]
        [System.Text.Json.Serialization.JsonPropertyName("tags")]
        public virtual List<TagDefinition> Tags { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing additional external documentation.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("externalDocs")]
        [YamlDotNet.Serialization.YamlMember(Alias = "externalDocs")]
        [System.Text.Json.Serialization.JsonPropertyName("externalDocs")]
        public virtual List<ExternalDocumentationDefinition> ExternalDocs { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Info == null || string.IsNullOrWhiteSpace(this.Info.Title) ? this.Id : this.Info?.Title;
        }

    }

}
