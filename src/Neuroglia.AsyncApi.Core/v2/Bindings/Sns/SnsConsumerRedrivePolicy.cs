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

namespace Neuroglia.AsyncApi.v2.Bindings.Sns;

/// <summary>
/// Represents an object used to configure an SNS consumer's redrive policy
/// </summary>
[DataContract]
public record SnsConsumerRedrivePolicy
{

    /// <summary>
    /// Gets/sets the SQS queue to use as a dead letter queue (DLQ). 
    /// Note that you may have a Redrive Policy to put messages that cannot be delivered to an SQS queue, even if you use another protocol to consume messages from the queue, so it is defined at the level of the SNS Operation Binding Object in a Consumer Object (and is applied as part of an SNS Subscription). 
    /// The SQS Binding describes how to define an SQS Binding that supports defining the target SQS of the Redrive Policy.
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "deadLetterQueue"), JsonPropertyOrder(1), JsonPropertyName("deadLetterQueue"), YamlMember(Order = 1, Alias = "deadLetterQueue")]
    public virtual SnsIdentifier DeadLetterQueue { get; set; } = null!;

    /// <summary>
    /// Gets/sets the number of times a message is delivered to the source queue before being moved to the dead-letter queue. Defaults to 10.
    /// </summary>
    [DataMember(Order = 2, Name = "maxReceiveCount"), JsonPropertyOrder(2), JsonPropertyName("maxReceiveCount"), YamlMember(Order = 2, Alias = "maxReceiveCount")]
    public virtual int MaxReceiveCount { get; set; } = 10;

}
