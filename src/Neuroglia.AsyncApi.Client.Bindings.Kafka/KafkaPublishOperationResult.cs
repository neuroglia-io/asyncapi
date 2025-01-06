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

using Confluent.Kafka;

namespace Neuroglia.AsyncApi.Client.Bindings.Kafka;

/// <summary>
/// Represents an object used to describe the result of a Kafka publish operation
/// </summary>
public class KafkaPublishOperationResult
    : AsyncApiPublishOperationResult
{

    /// <summary>
    /// Gets the persistence status of the published message
    /// </summary>
    public virtual PersistenceStatus? PersistenceStatus { get; init; }

    /// <summary>
    /// Gets the partition associated with the published message
    /// </summary>
    public virtual Partition? Partition { get; init; }

    /// <summary>
    /// Gets the partition offset associated with the published message
    /// </summary>
    public virtual Offset? Offset { get; init; }

    /// <summary>
    /// Gets the topic partition associated with the published message
    /// </summary>
    public virtual TopicPartition? TopicPartition { get; init; }

    /// <summary>
    /// Gets the topic partition offset associated with the published message
    /// </summary>
    public virtual TopicPartitionOffset? TopicPartitionOffset { get; init; }

    /// <summary>
    /// Gets/sets the <see cref="System.Exception"/>, if any, that occurred during publishing
    /// </summary>
    public virtual Exception? Exception { get; init; }

    /// <inheritdoc/>
    public override bool IsSuccessful => Exception == null;

}
