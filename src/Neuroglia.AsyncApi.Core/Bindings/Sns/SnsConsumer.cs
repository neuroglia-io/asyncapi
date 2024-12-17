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
/// Represents an object used to configure an SNS consumer
/// </summary>
[DataContract]
public record SnsConsumer
{

    /// <summary>
    /// Gets/sets the protocol that this endpoint receives messages by. Can be http, https, email, email-json, sms, sqs, application, lambda or firehose
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "protocol"), JsonPropertyOrder(1), JsonPropertyName("protocol"), YamlMember(Order = 1, Alias = "protocol")]
    public virtual SnsProtocol Protocol { get; set; }

    /// <summary>
    /// Gets/sets the endpoint messages are delivered to.
    /// </summary>
    [Required]
    [DataMember(Order = 2, Name = "endpoint"), JsonPropertyOrder(2), JsonPropertyName("endpoint"), YamlMember(Order = 2, Alias = "endpoint")]
    public virtual SnsIdentifier Endpoint { get; set; } = null!;

    /// <summary>
    /// Gets/sets the policy used to configure the subset of messages to receive from the channel
    /// </summary>
    [DataMember(Order = 3, Name = "filterPolicy"), JsonPropertyOrder(3), JsonPropertyName("filterPolicy"), YamlMember(Order = 3, Alias = "filterPolicy")]
    public virtual object? FilterPolicy { get; set; }

    /// <summary>
    /// Gets/sets a value that determines whether the FilterPolicy applies to MessageAttributes (default) or MessageBody.
    /// </summary>
    [DataMember(Order = 4, Name = "filterPolicyScope"), JsonPropertyOrder(4), JsonPropertyName("filterPolicyScope"), YamlMember(Order = 4, Alias = "filterPolicyScope")]
    public virtual SnsConsumerFilterPolicyScope FilterPolicyScope { get; set; } = SnsConsumerFilterPolicyScope.MessageAttributes;

    /// <summary>
    /// Gets/sets a value used to configure whether or not to support raw message delivery. If true AWS SNS attributes are removed from the body, and for SQS, SNS message attributes are copied to SQS message attributes. If false the SNS attributes are included in the body.
    /// </summary>
    [DataMember(Order = 5, Name = "rawMessageDelivery"), JsonPropertyOrder(5), JsonPropertyName("rawMessageDelivery"), YamlMember(Order = 5, Alias = "rawMessageDelivery")]
    public virtual bool RawMessageDelivery { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the consumer's redrive policy. Prevents poison pill messages by moving un-processable messages to an SQS dead letter queue.
    /// </summary>
    [DataMember(Order = 6, Name = "redrivePolicy"), JsonPropertyOrder(6), JsonPropertyName("redrivePolicy"), YamlMember(Order = 6, Alias = "redrivePolicy")]
    public virtual SnsConsumerRedrivePolicy? RedrivePolicy { get; set; }

    /// <summary>
    /// Gets/sets the policy for retries to HTTP. The parameter is for that SNS Subscription and overrides any policy on the SNS Topic.
    /// </summary>
    [DataMember(Order = 7, Name = "deliveryPolicy"), JsonPropertyOrder(7), JsonPropertyName("deliveryPolicy"), YamlMember(Order = 7, Alias = "deliveryPolicy")]
    public virtual SnsConsumerDeliveryPolicy? DeliveryPolicy { get; set; }

    /// <summary>
    /// Gets/sets the display name to use with an SMS subscription
    /// </summary>
    [DataMember(Order = 8, Name = "displayName"), JsonPropertyOrder(8), JsonPropertyName("displayName"), YamlMember(Order = 8, Alias = "displayName")]
    public virtual string? DisplayName { get; set; }

}
