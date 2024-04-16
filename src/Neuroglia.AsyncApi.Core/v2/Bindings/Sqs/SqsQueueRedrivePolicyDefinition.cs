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

using Neuroglia.AsyncApi.v2.Bindings.Sns;

namespace Neuroglia.AsyncApi.v2.Bindings.Sqs;

/// <summary>
/// Represents an object used to configure the redrive policy of an SQS queue
/// </summary>
[DataContract]
public record SqsQueueRedrivePolicyDefinition
{

    /// <summary>
    /// Gets/sets the SQS queue to use as a dead letter queue (DLQ).
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "deadLetterQueue"), JsonPropertyOrder(1), JsonPropertyName("deadLetterQueue"), YamlMember(Order = 1, Alias = "deadLetterQueue")]
    public virtual SnsIdentifier DeadLetterQueue { get; set; } = null!;

    /// <summary>
    /// Gets/sets the number of times a message is delivered to the source queue before being moved to the dead-letter queue. Default is 10.
    /// </summary>
    [DataMember(Order = 2, Name = "maxReceiveCount"), JsonPropertyOrder(2), JsonPropertyName("maxReceiveCount"), YamlMember(Order = 2, Alias = "maxReceiveCount")]
    public virtual int MaxReceiveCount { get; set; } = 10;

}
