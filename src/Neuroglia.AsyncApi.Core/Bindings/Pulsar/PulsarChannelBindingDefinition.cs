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

using Neuroglia.AsyncApi.Bindings;

namespace Neuroglia.AsyncApi.Bindings.Pulsar;

/// <summary>
/// Represents the object used to configure a Pulsar channel binding
/// </summary>
[DataContract]
public record PulsarChannelBindingDefinition
    : PulsarBindingDefinition, IChannelBindingDefinition
{

    /// <summary>
    /// Gets/sets the namespace the channel is associated with.
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "namespace"), JsonPropertyOrder(1), JsonPropertyName("namespace"), YamlMember(Order = 1, Alias = "namespace")]
    public virtual string Namespace { get; set; } = null!;

    /// <summary>
    /// Gets/sets the persistence of the topic in Pulsar.
    /// </summary>
    [DataMember(Order = 2, Name = "persistence"), JsonPropertyOrder(2), JsonPropertyName("persistence"), YamlMember(Order = 2, Alias = "persistence")]
    public virtual PulsarTopicTypes Persistence { get; set; }

    /// <summary>
    /// Gets/sets the topic compaction threshold given in Megabytes.
    /// </summary>
    [DataMember(Order = 3, Name = "compaction"), JsonPropertyOrder(3), JsonPropertyName("compaction"), YamlMember(Order = 3, Alias = "compaction")]
    public virtual int? Compaction { get; set; }

    /// <summary>
    /// Gets/sets the list of clusters the topic is replicated to.
    /// </summary>
    [DataMember(Order = 4, Name = "geo-replication"), JsonPropertyOrder(4), JsonPropertyName("geo-replication"), YamlMember(Order = 4, Alias = "geo-replication")]
    public virtual EquatableList<string>? GeoReplication { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the channel's retention policy.
    /// </summary>
    [DataMember(Order = 5, Name = "retention"), JsonPropertyOrder(5), JsonPropertyName("retention"), YamlMember(Order = 5, Alias = "retention")]
    public virtual PulsarRetentionPolicyDefinition? Retention { get; set; }

    /// <summary>
    /// Gets/sets the message time-to-live in seconds.
    /// </summary>
    [DataMember(Order = 6, Name = "ttl"), JsonPropertyOrder(6), JsonPropertyName("ttl"), YamlMember(Order = 6, Alias = "ttl")]
    public virtual int? Ttl { get; set; }

    /// <summary>
    /// Gets/sets the list of clusters the topic is replicated to.
    /// </summary>
    [DataMember(Order = 7, Name = "deduplication"), JsonPropertyOrder(7), JsonPropertyName("deduplication"), YamlMember(Order = 7, Alias = "deduplication")]
    public virtual bool? Deduplication { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding. If omitted, "latest" MUST be assumed.
    /// </summary>
    [DataMember(Order = 8, Name = "bindingVersion"), JsonPropertyOrder(8), JsonPropertyName("bindingVersion"), YamlMember(Order = 8, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
