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

namespace Neuroglia.AsyncApi.Bindings.Pulsar;

/// <summary>
/// Represents the attribute used to define an <see cref="PulsarChannelBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="namespace">The namespace the channel is associated with</param>
/// <param name="version">The binding's version</param>
public class PulsarChannelBindingAttribute(string name, string @namespace, string version = "latest")
    : ChannelBindingAttribute<PulsarChannelBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets/sets the namespace the channel is associated with.
    /// </summary>
    public virtual string Namespace { get; } = @namespace;

    /// <summary>
    /// Gets/sets the persistence of the topic in Pulsar.
    /// </summary>
    public virtual PulsarTopicTypes Persistence { get; init; }

    /// <summary>
    /// Gets/sets the topic compaction threshold given in Megabytes.
    /// </summary>
    public virtual int? Compaction { get; init; }

    /// <summary>
    /// Gets/sets the list of clusters the topic is replicated to.
    /// </summary>
    public virtual string[]? GeoReplication { get; init; }

    /// <summary>
    /// Gets/sets the retention time, in minutes.
    /// </summary>
    public virtual int? RetentionTime { get; init; }

    /// <summary>
    /// Gets/sets the retention size, in MB.
    /// </summary>
    public virtual int? RetentionSize { get; init; }

    /// <summary>
    /// Gets/sets the message time-to-live in seconds.
    /// </summary>
    public virtual int? Ttl { get; init; }

    /// <summary>
    /// Gets/sets the list of clusters the topic is replicated to.
    /// </summary>
    public virtual bool? Deduplication { get; init; }

    /// <inheritdoc/>
    public override PulsarChannelBindingDefinition Build()
    {
        var binding = new PulsarChannelBindingDefinition()
        {
            BindingVersion = Version,
            Namespace = Namespace,
            Persistence = Persistence,
            Compaction = Compaction,
            GeoReplication = GeoReplication == null ? null : new(GeoReplication),
            Ttl = Ttl,
            Deduplication = Deduplication
        };
        if (RetentionSize.HasValue || RetentionTime.HasValue) binding.Retention = new()
        {
            Time = RetentionTime ?? 0,
            Size = RetentionSize ?? 0
        };
        return binding;
    }

}
