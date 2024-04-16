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
/// Represents the object used to configure a Google Pub/Sub channel binding
/// </summary>
[DataContract]
public record GooglePubSubChannelBindingDefinition
    : GooglePubSubBindingDefinition, IChannelBindingDefinition
{

    /// <summary>
    /// Gets/sets the binding's version
    /// </summary>
    [DataMember(Order = 1, Name = "bindingVersion"), JsonPropertyOrder(1), JsonPropertyName("bindingVersion"), YamlMember(Order = 1, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

    /// <summary>
    /// Gets/sets a list of labels used to categorize Cloud Resources like Cloud Pub/Sub Topics
    /// </summary>
    [DataMember(Order = 2, Name = "labels"), JsonPropertyOrder(2), JsonPropertyName("labels"), YamlMember(Order = 2, Alias = "labels")]
    public virtual EquatableDictionary<string, string>? Labels { get; set; }

    /// <summary>
    /// Gets/sets the minimum duration to retain a message after it is published to the topic (Must be a valid Duration.)
    /// </summary>
    [DataMember(Order = 3, Name = "messageRetentionDuration"), JsonPropertyOrder(3), JsonPropertyName("messageRetentionDuration"), YamlMember(Order = 3, Alias = "messageRetentionDuration")]
    public virtual string? MessageRetentionDuration { get; set; }

    /// <summary>
    /// Gets/sets the policy constraining the set of Google Cloud Platform regions where messages published to the topic may be stored
    /// </summary>
    [DataMember(Order = 4, Name = "messageStoragePolicy"), JsonPropertyOrder(4), JsonPropertyName("messageStoragePolicy"), YamlMember(Order = 4, Alias = "messageStoragePolicy")]
    public virtual GooglePubSubMessageStoragePolicyDefinition? MessageStoragePolicy { get; set; }

    /// <summary>
    /// Gets/sets the settings for validating messages published against a schema
    /// </summary>
    [DataMember(Order = 5, Name = "schemaSettings"), JsonPropertyOrder(5), JsonPropertyName("schemaSettings"), YamlMember(Order = 5, Alias = "schemaSettings")]
    public virtual GooglePubSubSchemaSettings? SchemaSettings { get; set; }

}
