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
using System.Collections.Generic;

namespace Neuroglia.AsyncApi.Models
{
    /// <summary>
    /// Represents an object used to define a Async API operation trait
    /// </summary>
    public class OperationTraitDefinition
        : ReferenceableComponentDefinition
    {

        /// <summary>
        /// Gets/sets a unique string used to identify the operation. The id MUST be unique among all operations described in the API. The operationId value is case-sensitive. Tools and libraries MAY use the operationId to uniquely identify an operation, therefore, it is RECOMMENDED to follow common programming naming conventions.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("operationId")]
        [YamlDotNet.Serialization.YamlMember(Alias = "operationId")]
        [System.Text.Json.Serialization.JsonPropertyName("operationId")]
        public virtual string OperationId { get; set; }

        /// <summary>
        /// Gets/sets a short summary of what the operation is about.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("summary")]
        [YamlDotNet.Serialization.YamlMember(Alias = "summary")]
        [System.Text.Json.Serialization.JsonPropertyName("summary")]
        public virtual string Summary { get; set; }

        /// <summary>
        /// Gets/sets an optional description of the operation trait. <see href="https://spec.commonmark.org/">CommonMark</see> syntax can be used for rich text representation.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("description")]
        [YamlDotNet.Serialization.YamlMember(Alias = "description")]
        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> of tags for API documentation control. Tags can be used for logical grouping of operations.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("tags")]
        [YamlDotNet.Serialization.YamlMember(Alias = "tags")]
        [System.Text.Json.Serialization.JsonPropertyName("tags")]
        public virtual List<TagDefinition> Tags { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing additional external documentation for this operation.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("externalDocs")]
        [YamlDotNet.Serialization.YamlMember(Alias = "externalDocs")]
        [System.Text.Json.Serialization.JsonPropertyName("externalDocs")]
        public virtual List<ExternalDocumentationDefinition> ExternalDocs { get; set; }

        /// <summary>
        /// Gets/sets an object used to configure the <see cref="OperationTraitDefinition"/>'s <see cref="IOperationBinding"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindings")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindings")]
        [System.Text.Json.Serialization.JsonPropertyName("bindings")]
        public virtual OperationBindingDefinitionCollection Bindings { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.OperationId;
        }

    }

}
