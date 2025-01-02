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
/// Represents the attribute used to define an <see cref="SqsChannelBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="queueName">The name of the queue. When an SNS Operation Binding Object references an SQS queue by name, the identifier should be the one in this field.</param>
/// <param name="version">The binding's version</param>
public class SqsChannelBindingAttribute(string name, string queueName, string version = "latest")
    : ChannelBindingAttribute<SqsChannelBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets/sets the name of the queue. When an SNS Operation Binding Object references an SQS queue by name, the identifier should be the one in this field.
    /// </summary>
    public virtual string QueueName { get; } = queueName;

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the queue is a FIFO queue.
    /// </summary>
    public virtual bool? QueueFifo { get; init; }

    /// <summary>
    /// Gets/sets the queue's deduplication scope
    /// </summary>
    public virtual SqsDeduplicationScope? QueueDeduplicationScope { get; init; }

    /// <summary>
    /// Gets/sets a value indicating whether the FIFO queue throughput quota applies to the entire queue or per message group. Valid values are perQueue and perMessageGroupId. The perMessageGroupId value is allowed only when the value for DeduplicationScope is messageGroup. Setting both these values as such will enable high throughput on a FIFO queue. As above, this property applies only to high throughput for FIFO queues.
    /// </summary>
    public virtual SqsFifoThroughputLimit? QueueFifoThroughputLimit { get; init; }

    /// <summary>
    /// Gets/sets the number of seconds to delay before a message sent to the queue can be received. Used to create a delay queue. Range is 0 to 15 minutes. Defaults to 0.
    /// </summary>
    public virtual int QueueDeliveryDelay { get; init; } = 0;

    /// <summary>
    /// Gets/sets the length of time, in seconds, that a consumer locks a message - hiding it from reads - before it is unlocked and can be read again. Range from 0 to 12 hours (43200 seconds). Defaults to 30 seconds.
    /// </summary>
    public virtual int? QueueVisibilityTimeout { get; init; }

    /// <summary>
    /// Gets/sets a value that determines if the queue uses short polling or long polling. Set to zero (the default) the queue reads available messages and returns immediately. Set to a non-zero integer, long polling waits the specified number of seconds for messages to arrive before returning.
    /// </summary>
    public virtual int? QueueReceiveMessageWaitTime { get; init; }

    /// <summary>
    /// Gets/sets a value that determines how long to retain a message on the queue in seconds, unless deleted. The range is 60 (1 minute) to 1,209,600 (14 days). The default is 345,600 (4 days). long to retain a message on the queue in seconds, unless deleted. The range is 60 (1 minute) to 1,209,600 (14 days). The default is 345,600 (4 days).
    /// </summary>
    public virtual int? QueueMessageRetentionPeriod { get; init; }

    /// <summary>
    /// Gets/sets a key/value mapping of the queue's AWS tags, if any. Values must follow the '{key}::{value}' format
    /// </summary>
    public virtual string[]? QueueTags { get; init; }

    /// <summary>
    /// Gets/sets the name of the queue. When an SNS Operation Binding Object references an SQS queue by name, the identifier should be the one in this field.
    /// </summary>
    public virtual string? DeadLetterQueueName { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the queue is a FIFO queue.
    /// </summary>
    public virtual bool? DeadLetterQueueFifo { get; init; }

    /// <summary>
    /// Gets/sets the queue's deduplication scope
    /// </summary>
    public virtual SqsDeduplicationScope? DeadLetterQueueDeduplicationScope { get; init; }

    /// <summary>
    /// Gets/sets a value indicating whether the FIFO queue throughput quota applies to the entire queue or per message group. Valid values are perQueue and perMessageGroupId. The perMessageGroupId value is allowed only when the value for DeduplicationScope is messageGroup. Setting both these values as such will enable high throughput on a FIFO queue. As above, this property applies only to high throughput for FIFO queues.
    /// </summary>
    public virtual SqsFifoThroughputLimit? DeadLetterQueueFifoThroughputLimit { get; init; }

    /// <summary>
    /// Gets/sets the number of seconds to delay before a message sent to the queue can be received. Used to create a delay queue. Range is 0 to 15 minutes. Defaults to 0.
    /// </summary>
    public virtual int DeadLetterQueueDeliveryDelay { get; init; } = 0;

    /// <summary>
    /// Gets/sets the length of time, in seconds, that a consumer locks a message - hiding it from reads - before it is unlocked and can be read again. Range from 0 to 12 hours (43200 seconds). Defaults to 30 seconds.
    /// </summary>
    public virtual int? DeadLetterQueueVisibilityTimeout { get; init; }

    /// <summary>
    /// Gets/sets a value that determines if the queue uses short polling or long polling. Set to zero (the default) the queue reads available messages and returns immediately. Set to a non-zero integer, long polling waits the specified number of seconds for messages to arrive before returning.
    /// </summary>
    public virtual int? DeadLetterQueueReceiveMessageWaitTime { get; init; }

    /// <summary>
    /// Gets/sets a value that determines how long to retain a message on the queue in seconds, unless deleted. The range is 60 (1 minute) to 1,209,600 (14 days). The default is 345,600 (4 days). long to retain a message on the queue in seconds, unless deleted. The range is 60 (1 minute) to 1,209,600 (14 days). The default is 345,600 (4 days).
    /// </summary>
    public virtual int? DeadLetterQueueMessageRetentionPeriod { get; init; }

    /// <summary>
    /// Gets/sets a key/value mapping of the queue's AWS tags, if any. Values must follow the '{key}::{value}' format
    /// </summary>
    public virtual string[]? DeadLetterQueueTags { get; init; }

    /// <inheritdoc/>
    public override SqsChannelBindingDefinition Build()
    {
        var binding = new SqsChannelBindingDefinition()
        {
            BindingVersion = Version,
            Queue = new()
            {
                Name = QueueName,
                FifoQueue = QueueFifo ?? false,
                DeduplicationScope = QueueDeduplicationScope,
                FifoThroughputLimit = QueueFifoThroughputLimit,
                DeliveryDelay = QueueDeliveryDelay,
                VisibilityTimeout = QueueVisibilityTimeout,
                ReceiveMessageWaitTime = QueueReceiveMessageWaitTime,
                MessageRetentionPeriod = QueueMessageRetentionPeriod,
                Tags = QueueTags == null ? null : new(QueueTags.Select(t =>
                {
                    var components = t.Split("::", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    return new { Key = components[0], Value = components[1] };
                }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value))
            }
        };
        if (!string.IsNullOrWhiteSpace(DeadLetterQueueName)) binding.DeadLetterQueue = new()
        {
            Name = DeadLetterQueueName,
            FifoQueue = DeadLetterQueueFifo ?? false,
            DeduplicationScope = DeadLetterQueueDeduplicationScope,
            FifoThroughputLimit = DeadLetterQueueFifoThroughputLimit,
            DeliveryDelay = DeadLetterQueueDeliveryDelay,
            VisibilityTimeout = DeadLetterQueueVisibilityTimeout,
            ReceiveMessageWaitTime = DeadLetterQueueReceiveMessageWaitTime,
            MessageRetentionPeriod = DeadLetterQueueMessageRetentionPeriod,
            Tags = DeadLetterQueueTags == null ? null : new(DeadLetterQueueTags.Select(t =>
            {
                var components = t.Split("::", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                return new { Key = components[0], Value = components[1] };
            }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value))
        };
        return binding;
    }

}
