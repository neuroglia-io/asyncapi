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

namespace Neuroglia.AsyncApi.Bindings.Sqs;

/// <summary>
/// Represents the object used to configure an SQS queue
/// </summary>
[DataContract]
public record SqsQueueDefinition
{

    /// <summary>
    /// Gets/sets the name of the queue. When an SNS Operation Binding Object references an SQS queue by name, the identifier should be the one in this field.
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name"), YamlMember(Order = 1, Alias = "name")]
    public virtual string Name { get; set; } = null!;

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the queue is a FIFO queue.
    /// </summary>
    [DataMember(Order = 2, Name = "fifoQueue"), JsonPropertyOrder(2), JsonPropertyName("fifoQueue"), YamlMember(Order = 2, Alias = "fifoQueue")]
    public virtual bool FifoQueue { get; set; }

    /// <summary>
    /// Gets/sets the queue's deduplication scope
    /// </summary>
    [DataMember(Order = 3, Name = "deduplicationScope"), JsonPropertyOrder(3), JsonPropertyName("deduplicationScope"), YamlMember(Order = 3, Alias = "deduplicationScope")]
    public virtual SqsDeduplicationScope? DeduplicationScope { get; set; }

    /// <summary>
    /// Gets/sets a value indicating whether the FIFO queue throughput quota applies to the entire queue or per message group. Valid values are perQueue and perMessageGroupId. The perMessageGroupId value is allowed only when the value for DeduplicationScope is messageGroup. Setting both these values as such will enable high throughput on a FIFO queue. As above, this property applies only to high throughput for FIFO queues.
    /// </summary>
    [DataMember(Order = 4, Name = "fifoThroughputLimit"), JsonPropertyOrder(4), JsonPropertyName("fifoThroughputLimit"), YamlMember(Order = 4, Alias = "fifoThroughputLimit")]
    public virtual SqsFifoThroughputLimit? FifoThroughputLimit { get; set; }

    /// <summary>
    /// Gets/sets the number of seconds to delay before a message sent to the queue can be received. Used to create a delay queue. Range is 0 to 15 minutes. Defaults to 0.
    /// </summary>
    [DataMember(Order = 5, Name = "deliveryDelay"), JsonPropertyOrder(5), JsonPropertyName("deliveryDelay"), YamlMember(Order = 5, Alias = "deliveryDelay")]
    public virtual int DeliveryDelay { get; set; } = 0;

    /// <summary>
    /// Gets/sets the length of time, in seconds, that a consumer locks a message - hiding it from reads - before it is unlocked and can be read again. Range from 0 to 12 hours (43200 seconds). Defaults to 30 seconds.
    /// </summary>
    [DataMember(Order = 6, Name = "visibilityTimeout"), JsonPropertyOrder(6), JsonPropertyName("visibilityTimeout"), YamlMember(Order = 6, Alias = "visibilityTimeout")]
    public virtual int? VisibilityTimeout { get; set; }

    /// <summary>
    /// Gets/sets a value that determines if the queue uses short polling or long polling. Set to zero (the default) the queue reads available messages and returns immediately. Set to a non-zero integer, long polling waits the specified number of seconds for messages to arrive before returning.
    /// </summary>
    [DataMember(Order = 7, Name = "receiveMessageWaitTime"), JsonPropertyOrder(7), JsonPropertyName("receiveMessageWaitTime"), YamlMember(Order = 7, Alias = "receiveMessageWaitTime")]
    public virtual int? ReceiveMessageWaitTime { get; set; }

    /// <summary>
    /// Gets/sets a value that determines how long to retain a message on the queue in seconds, unless deleted. The range is 60 (1 minute) to 1,209,600 (14 days). The default is 345,600 (4 days). long to retain a message on the queue in seconds, unless deleted. The range is 60 (1 minute) to 1,209,600 (14 days). The default is 345,600 (4 days).
    /// </summary>
    [DataMember(Order = 8, Name = "messageRetentionPeriod"), JsonPropertyOrder(8), JsonPropertyName("messageRetentionPeriod"), YamlMember(Order = 8, Alias = "messageRetentionPeriod")]
    public virtual int? MessageRetentionPeriod { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the queue's redrive policy
    /// </summary>
    [DataMember(Order = 9, Name = "redrivePolicy"), JsonPropertyOrder(9), JsonPropertyName("redrivePolicy"), YamlMember(Order = 9, Alias = "redrivePolicy")]
    public virtual SqsQueueRedrivePolicyDefinition? RedrivePolicy { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the queue's security policy
    /// </summary>
    [DataMember(Order = 10, Name = "policy"), JsonPropertyOrder(10), JsonPropertyName("policy"), YamlMember(Order = 10, Alias = "policy")]
    public virtual SqsQueueSecurityPolicyDefinition? Policy { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping of the queue's AWS tags, if any
    /// </summary>
    [DataMember(Order = 11, Name = "tags"), JsonPropertyOrder(11), JsonPropertyName("tags"), YamlMember(Order = 11, Alias = "tags")]
    public virtual EquatableDictionary<string, string>? Tags { get; set; }

}
