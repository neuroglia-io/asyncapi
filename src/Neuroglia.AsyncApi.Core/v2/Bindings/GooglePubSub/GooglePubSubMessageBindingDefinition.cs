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

namespace Neuroglia.AsyncApi.v2.Bindings.GooglePubSub;

/// <summary>
/// Represents the object used to configure a Google Pub/Sub message binding
/// </summary>
[DataContract]
public record GooglePubSubMessageBindingDefinition
    : GooglePubSubBindingDefinition, IMessageBindingDefinition
{

    /// <summary>
    /// Gets/sets the binding's version
    /// </summary>
    [DataMember(Order = 1, Name = "bindingVersion"), JsonPropertyOrder(1), JsonPropertyName("bindingVersion"), YamlMember(Order = 1, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

    /// <summary>
    /// Gets/sets the attributes for this message (If this field is empty, the message must contain non-empty data. This can be used to filter messages on the subscription.)
    /// </summary>
    [DataMember(Order = 2, Name = "attributes"), JsonPropertyOrder(2), JsonPropertyName("attributes"), YamlMember(Order = 2, Alias = "attributes")]
    public virtual EquatableDictionary<string, object>? Attributes { get; set; }

    /// <summary>
    /// Gets/sets a value that identifies related messages for which publish order should be respected (For more information, see ordering messages.)
    /// </summary>
    [DataMember(Order = 3, Name = "orderingKey"), JsonPropertyOrder(3), JsonPropertyName("orderingKey"), YamlMember(Order = 3, Alias = "orderingKey")]
    public virtual string? OrderingKey { get; set; }

    /// <summary>
    /// Gets/sets the schema used to validate the payload of this message
    /// </summary>
    [DataMember(Order = 4, Name = "schema"), JsonPropertyOrder(4), JsonPropertyName("schema"), YamlMember(Order = 4, Alias = "schema")]
    public virtual GooglePubSubSchemaDefinition? Schema { get; set; }

}
