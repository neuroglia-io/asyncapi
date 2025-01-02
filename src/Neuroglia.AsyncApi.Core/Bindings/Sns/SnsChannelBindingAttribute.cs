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

namespace Neuroglia.AsyncApi.Bindings.Sns;

/// <summary>
/// Represents the attribute used to define an <see cref="SnsChannelBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="topicName">The name of the topic. Can be different from the channel name to allow flexibility around AWS resource naming limitations.</param>
/// <param name="version">The binding's version</param>
public class SnsChannelBindingAttribute(string name, string topicName, string version = "latest")
    : ChannelBindingAttribute<SnsChannelBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets/sets the name of the topic. Can be different from the channel name to allow flexibility around AWS resource naming limitations.
    /// </summary>
    public virtual string TopicName { get; } = topicName;

    /// <summary>
    /// Gets/sets the type of SNS Topic. Can be either standard or FIFO.
    /// </summary>
    public virtual SnsTopicOrderingType? OrderingType { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the de-duplication of messages should be turned on. Defaults to false.
    /// </summary>
    public virtual bool? OrderingContentBasedDeduplication { get; set; }

    /// <summary>
    /// Gets key-value pairs that represent AWS tags on the topic. Values must follow the '{key}::{value}' format
    /// </summary>
    public virtual string[]? Tags { get; set; }

    /// <inheritdoc/>
    public override SnsChannelBindingDefinition Build()
    {
        var binding = new SnsChannelBindingDefinition()
        {
            BindingVersion = Version,
            Name = TopicName,
            Tags = Tags == null ? null : new(Tags.Select(t =>
            {
                var components = t.Split("::", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                return new { Key = components[0], Value = components[1] };
            }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value))
        };
        if (OrderingType.HasValue) binding.Ordering = new()
        {
            Type = OrderingType.Value,
            ContentBasedDeduplication = OrderingContentBasedDeduplication ?? false
        };
        return binding;
    }

}
