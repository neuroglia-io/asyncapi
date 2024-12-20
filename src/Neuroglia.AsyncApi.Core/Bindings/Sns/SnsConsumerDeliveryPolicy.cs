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

namespace Neuroglia.AsyncApi.Bindings.Sns;

/// <summary>
/// Represents an object used to configure an SNS consumer's delivery policy
/// </summary>
[DataContract]
public record SnsConsumerDeliveryPolicy
{

    /// <summary>
    /// Gets/sets the minimum delay for a retry in seconds
    /// </summary>
    [DataMember(Order = 1, Name = "minDelayTarget"), JsonPropertyOrder(1), JsonPropertyName("minDelayTarget"), YamlMember(Order = 1, Alias = "minDelayTarget")]
    public virtual int? MinDelayTarget { get; set; }

    /// <summary>
    /// Gets/sets the minimum delay for a retry in seconds
    /// </summary>
    [DataMember(Order = 2, Name = "maxDelayTarget"), JsonPropertyOrder(2), JsonPropertyName("maxDelayTarget"), YamlMember(Order = 2, Alias = "maxDelayTarget")]
    public virtual int? MaxDelayTarget { get; set; }

    /// <summary>
    /// Gets/sets the total number of retries, including immediate, pre-backoff, backoff, and post-backoff retries
    /// </summary>
    [DataMember(Order = 3, Name = "numRetries"), JsonPropertyOrder(3), JsonPropertyName("numRetries"), YamlMember(Order = 3, Alias = "numRetries")]
    public virtual int? NumRetries { get; set; }

    /// <summary>
    /// Gets/sets the number of immediate retries (with no delay)
    /// </summary>
    [DataMember(Order = 4, Name = "numNoDelayRetries"), JsonPropertyOrder(4), JsonPropertyName("numNoDelayRetries"), YamlMember(Order = 4, Alias = "numNoDelayRetries")]
    public virtual int? NumNoDelayRetries { get; set; }

    /// <summary>
    /// Gets/sets the number of immediate retries (with delay)
    /// </summary>
    [DataMember(Order = 5, Name = "numMinDelayRetries"), JsonPropertyOrder(5), JsonPropertyName("numMinDelayRetries"), YamlMember(Order = 5, Alias = "numMinDelayRetries")]
    public virtual int? NumMinDelayRetries { get; set; }

    /// <summary>
    /// Gets/sets the number of post-backoff phase retries, with the maximum delay between retries
    /// </summary>
    [DataMember(Order = 6, Name = "numMaxDelayRetries"), JsonPropertyOrder(6), JsonPropertyName("numMaxDelayRetries"), YamlMember(Order = 6, Alias = "numMaxDelayRetries")]
    public virtual int? NumMaxDelayRetries { get; set; }

    /// <summary>
    /// Gets/sets the algorithm for backoff between retries
    /// </summary>
    [DataMember(Order = 7, Name = "backoffFunction"), JsonPropertyOrder(7), JsonPropertyName("backoffFunction"), YamlMember(Order = 7, Alias = "backoffFunction")]
    public virtual SnsBackoffAlgorithm? BackoffFunction { get; set; }

    /// <summary>
    /// Gets/sets the maximum number of deliveries per second, per subscription
    /// </summary>
    [DataMember(Order = 8, Name = "maxReceivesPerSecond"), JsonPropertyOrder(8), JsonPropertyName("maxReceivesPerSecond"), YamlMember(Order = 8, Alias = "maxReceivesPerSecond")]
    public virtual int? maxReceivesPerSecond { get; set; }

}
