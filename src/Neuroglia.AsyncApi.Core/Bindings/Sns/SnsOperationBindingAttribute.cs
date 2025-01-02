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
/// Represents the attribute used to define an <see cref="SnsOperationBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="version">The binding's version</param>
public class SnsOperationBindingAttribute(string name, string version = "latest")
    : OperationBindingAttribute<SnsOperationBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets/sets the endpoint's URL
    /// </summary>
    public virtual Uri? TopicUrl { get; init; }

    /// <summary>
    /// Gets/sets the endpoint's email
    /// </summary>
    public virtual string? TopicEmail { get; init; }

    /// <summary>
    /// Gets/sets the endpoint's phone number
    /// </summary>
    public virtual string? TopicPhone { get; init; }

    /// <summary>
    /// Gets/sets the endpoint's arn
    /// </summary>
    public virtual string? TopicArn { get; init; }

    /// <summary>
    /// Gets/sets the endpoint's name
    /// </summary>
    public virtual string? TopicName { get; init; }

    /// <summary>
    /// Gets/sets the minimum delay for a retry in seconds
    /// </summary>
    public virtual int? DeliveryMinDelayTarget { get; init; }

    /// <summary>
    /// Gets/sets the minimum delay for a retry in seconds
    /// </summary>
    public virtual int? DeliveryMaxDelayTarget { get; init; }

    /// <summary>
    /// Gets/sets the total number of retries, including immediate, pre-backoff, backoff, and post-backoff retries
    /// </summary>
    public virtual int? DeliveryNumRetries { get; init; }

    /// <summary>
    /// Gets/sets the number of immediate retries (with no delay)
    /// </summary>
    public virtual int? DeliveryNumNoDelayRetries { get; init; }

    /// <summary>
    /// Gets/sets the number of immediate retries (with delay)
    /// </summary>
    public virtual int? DeliveryNumMinDelayRetries { get; init; }

    /// <summary>
    /// Gets/sets the number of post-backoff phase retries, with the maximum delay between retries
    /// </summary>
    public virtual int? DeliveryNumMaxDelayRetries { get; init; }

    /// <summary>
    /// Gets/sets the algorithm for backoff between retries
    /// </summary>
    public virtual SnsBackoffAlgorithm? DeliveryBackoffFunction { get; init; }

    /// <summary>
    /// Gets/sets the maximum number of deliveries per second, per subscription
    /// </summary>
    public virtual int? DeliveryMaxReceivesPerSecond { get; init; }

    /// <inheritdoc/>
    public override SnsOperationBindingDefinition Build()
    {
        var binding = new SnsOperationBindingDefinition()
        {
            BindingVersion = this.Version
        };
        if (TopicUrl != null || !string.IsNullOrWhiteSpace(TopicEmail) || !string.IsNullOrWhiteSpace(TopicPhone) || !string.IsNullOrWhiteSpace(TopicName)) binding.Topic = new()
        {
            Url = TopicUrl,
            Email = TopicEmail,
            Phone = TopicPhone,
            Arn = TopicArn,
            Name = TopicName
        };
        if (DeliveryBackoffFunction.HasValue || DeliveryMaxDelayTarget.HasValue || DeliveryMaxReceivesPerSecond.HasValue || DeliveryMinDelayTarget.HasValue || DeliveryNumMaxDelayRetries.HasValue || DeliveryNumMinDelayRetries.HasValue || DeliveryNumNoDelayRetries.HasValue || DeliveryNumRetries.HasValue) binding.DeliveryPolicy = new()
        {
            BackoffFunction = DeliveryBackoffFunction,
            MaxDelayTarget = DeliveryMaxDelayTarget,
            MaxReceivesPerSecond = DeliveryMaxReceivesPerSecond,
            MinDelayTarget = DeliveryMinDelayTarget,
            NumMaxDelayRetries = DeliveryNumMaxDelayRetries,
            NumMinDelayRetries = DeliveryNumMinDelayRetries,
            NumNoDelayRetries = DeliveryNumNoDelayRetries,
            NumRetries = DeliveryNumRetries
        };
        return binding;
    }

}
