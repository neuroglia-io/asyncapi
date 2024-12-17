// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Neuroglia.AsyncApi.v3;

/// <summary>
/// Represents an object that represents a schema definition
/// </summary>
[DataContract]
public record V3SchemaDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets the string containing the name of the schema format that is used to define the information. 
    /// If schemaFormat is missing, it MUST default to application/vnd.aai.asyncapi+json;version={{asyncapi}} where {{asyncapi}} matches the AsyncAPI Version String. In such a case, this would make the Multi Format Schema Object equivalent to the Schema Object. When using Reference Object within the schema, the schemaFormat of the resource being referenced MUST match the schemaFormat of the schema that contains the initial reference. 
    /// </summary>
    [DataMember(Order = 1, Name = "schemaFormat"), JsonPropertyOrder(1), JsonPropertyName("schemaFormat"), YamlMember(Order = 1, Alias = "schemaFormat")]
    public virtual string? SchemaFormat { get; set; }

    /// <summary>
    /// Gets/sets the definition of the message payload. 
    /// It can be of any type but defaults to Schema Object. 
    /// It MUST match the schema format defined in schemaFormat, including the encoding type.
    /// </summary>
    [Required]
    [DataMember(Order = 2, Name = "schema"), JsonPropertyOrder(2), JsonPropertyName("schema"), YamlMember(Order = 2, Alias = "schema")]
    public virtual object Schema { get; set; } = null!;

}