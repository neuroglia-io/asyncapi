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

namespace Neuroglia.AsyncApi.Bindings.Kafka;

/// <summary>
/// Represents the object used to configure a Kafka server binding
/// </summary>
[DataContract]
public record KafkaServerBindingDefinition
    : KafkaBindingDefinition, IServerBindingDefinition
{

    /// <summary>
    /// Gets/sets the API URL for the Schema Registry used when producing Kafka messages (if a Schema Registry was used).
    /// </summary>
    [DataMember(Order = 1, Name = "schemaRegistryUrl"), JsonPropertyOrder(1), JsonPropertyName("schemaRegistryUrl"), YamlMember(Order = 1, Alias = "schemaRegistryUrl")]
    public virtual Uri? SchemaRegistryUrl { get; set; }

    /// <summary>
    /// Gets/sets the vendor of Schema Registry and Kafka serdes library that should be used (e.g. apicurio, confluent, ibm, or karapace)
    /// </summary>
    [DataMember(Order = 2, Name = "schemaRegistryVendor"), JsonPropertyOrder(2), JsonPropertyName("schemaRegistryVendor"), YamlMember(Order = 2, Alias = "schemaRegistryVendor")]
    public virtual string? SchemaRegistryVendor { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding. Defaults to 'latest'.
    /// </summary>
    [DataMember(Order = 3, Name = "bindingVersion"), JsonPropertyOrder(3), JsonPropertyName("bindingVersion"), YamlMember(Order = 3, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
