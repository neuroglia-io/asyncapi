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
/// Represents an object used to configure a Kafka topic
/// </summary>
[DataContract]
public record KafkaTopicConfiguration
{

    /// <summary>
    /// Gets/sets the cleanup policy, if any
    /// </summary>
    [DataMember(Order = 1, Name = "cleanup.policy"), JsonPropertyOrder(1), JsonPropertyName("cleanup.policy"), YamlMember(Order = 1, Alias = "cleanup.policy")]
    public virtual string[]? CleanupPolicy { get; set; }

    /// <summary>
    /// Gets/sets the retention duration in milliseconds, if any
    /// </summary>
    [DataMember(Order = 2, Name = "retention.ms"), JsonPropertyOrder(2), JsonPropertyName("retention.ms"), YamlMember(Order = 2, Alias = "retention.ms")]
    public virtual long? RetentionMilliseconds { get; set; }

    /// <summary>
    /// Gets/sets the retention bytes, if any
    /// </summary>
    [DataMember(Order = 3, Name = "retention.bytes"), JsonPropertyOrder(3), JsonPropertyName("retention.bytes"), YamlMember(Order = 3, Alias = "retention.bytes")]
    public virtual long? RetentionBytes { get; set; }

    /// <summary>
    /// Gets/sets the delete retention duration in milliseconds, if any
    /// </summary>
    [DataMember(Order = 4, Name = "delete.retention.ms"), JsonPropertyOrder(4), JsonPropertyName("delete.retention.ms"), YamlMember(Order = 4, Alias = "delete.retention.ms")]
    public virtual long? DeleteRetentionMilliseconds { get; set; }

    /// <summary>
    /// Gets/sets the maximum length in bytes, if any, for the topic's messages
    /// </summary>
    [DataMember(Order = 5, Name = "max.message.bytes"), JsonPropertyOrder(5), JsonPropertyName("max.message.bytes"), YamlMember(Order = 5, Alias = "max.message.bytes")]
    public virtual int? MaxMessageBytes { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to validate the key schema. This configuration is specific to Confluent
    /// </summary>
    [DataMember(Order = 6, Name = "confluent.key.schema.validation"), JsonPropertyOrder(6), JsonPropertyName("confluent.key.schema.validation"), YamlMember(Order = 6, Alias = "confluent.key.schema.validation")]
    public virtual bool ConfluentKeySchemaValidation { get; set; }

    /// <summary>
    /// Gets/sets the name of the schema lookup strategy for the message key. This configuration is specific to Confluent
    /// </summary>
    [DataMember(Order = 7, Name = "confluent.key.subject.name.strategy"), JsonPropertyOrder(7), JsonPropertyName("confluent.key.subject.name.strategy"), YamlMember(Order = 7, Alias = "confluent.key.subject.name.strategy")]
    public virtual bool ConfluentKeySubjectNameStrategy { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not whether the schema validation for the message value is enabled. This configuration is specific to Confluent
    /// </summary>
    [DataMember(Order = 8, Name = "confluent.value.schema.validation"), JsonPropertyOrder(8), JsonPropertyName("confluent.value.schema.validation"), YamlMember(Order = 8, Alias = "confluent.value.schema.validation")]
    public virtual bool ConfluentValueSchemaValidation { get; set; }

    /// <summary>
    /// Gets/sets the name of the schema lookup strategy for the message key. This configuration is specific to Confluent
    /// </summary>
    [DataMember(Order = 9, Name = "confluent.value.subject.name.strategy"), JsonPropertyOrder(9), JsonPropertyName("confluent.value.subject.name.strategy"), YamlMember(Order = 9, Alias = "confluent.value.subject.name.strategy")]
    public virtual bool ConfluentValueSubjectNameStrategy { get; set; }

}
