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
/// Represents the object used to configure a Kafka channel binding
/// </summary>
[DataContract]
public record KafkaChannelBindingDefinition
    : KafkaBindingDefinition, IChannelBindingDefinition
{

    /// <summary>
    /// Gets/sets the topic name, if different from channel name.
    /// </summary>
    [DataMember(Order = 1, Name = "topic"), JsonPropertyOrder(1), JsonPropertyName("topic"), YamlMember(Order = 1, Alias = "topic")]
    public virtual string? Topic { get; set; }

    /// <summary>
    /// Gets/sets the number of partitions configured on this topic (useful to know how many parallel consumers you may run).
    /// </summary>
    [DataMember(Order = 2, Name = "partitions"), JsonPropertyOrder(2), JsonPropertyName("partitions"), YamlMember(Order = 2, Alias = "partitions")]
    public virtual uint? Partitions { get; set; }

    /// <summary>
    /// Gets/sets the number of replicas configured on this topic.
    /// </summary>
    [DataMember(Order = 3, Name = "replicas"), JsonPropertyOrder(3), JsonPropertyName("replicas"), YamlMember(Order = 3, Alias = "replicas")]
    public virtual uint? Replicas { get; set; }

    /// <summary>
    /// Gets/sets the topic configuration properties that are relevant for the API.
    /// </summary>
    [DataMember(Order = 4, Name = "topicConfiguration"), JsonPropertyOrder(4), JsonPropertyName("topicConfiguration"), YamlMember(Order = 4, Alias = "topicConfiguration")]
    public virtual KafkaTopicConfiguration? TopicConfiguration { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding. Defaults to 'latest'.
    /// </summary>
    [DataMember(Order = 5, Name = "bindingVersion"), JsonPropertyOrder(5), JsonPropertyName("bindingVersion"), YamlMember(Order = 5, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
