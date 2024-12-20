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

using Neuroglia.AsyncApi.Bindings;

namespace Neuroglia.AsyncApi.Bindings.Amqp;

/// <summary>
/// Represents the object used to configure an AMQP 0.9+ operation binding
/// </summary>
[DataContract]
public record AmqpOperationBindingDefinition
    : AmqpBindingDefinition, IOperationBindingDefinition
{

    /// <summary>
    /// Gets/sets the TTL (Time-To-Live) for the message. It MUST be greater than or equal to zero.
    /// </summary>
    [DataMember(Order = 1, Name = "expiration"), JsonPropertyOrder(1), JsonPropertyName("expiration"), YamlMember(Order = 1, Alias = "expiration")]
    public virtual int Expiration { get; set; }

    /// <summary>
    /// Gets/sets a string that identifies the user who has sent the message.
    /// </summary>
    [DataMember(Order = 2, Name = "userId"), JsonPropertyOrder(2), JsonPropertyName("userId"), YamlMember(Order = 2, Alias = "userId")]
    public virtual string? UserId { get; set; }

    /// <summary>
    /// Gets/sets the routing keys the message should be routed to at the time of publishing.
    /// </summary>
    [DataMember(Order = 3, Name = "cc"), JsonPropertyOrder(3), JsonPropertyName("cc"), YamlMember(Order = 3, Alias = "cc")]
    public virtual EquatableList<string>? Cc { get; set; }

    /// <summary>
    /// Gets/sets a priority for the message.
    /// </summary>
    [DataMember(Order = 4, Name = "priority"), JsonPropertyOrder(4), JsonPropertyName("priority"), YamlMember(Order = 4, Alias = "priority")]
    public virtual byte Priority { get; set; }

    /// <summary>
    /// Gets/sets the delivery mode of the message
    /// </summary>
    [DataMember(Order = 5, Name = "deliveryMode"), JsonPropertyOrder(5), JsonPropertyName("deliveryMode"), YamlMember(Order = 5, Alias = "deliveryMode")]
    public virtual AmqpDeliveryMode DeliveryMode { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the message is mandatory or not.
    /// </summary>
    [DataMember(Order = 6, Name = "mandatory"), JsonPropertyOrder(6), JsonPropertyName("mandatory"), YamlMember(Order = 6, Alias = "mandatory")]
    public virtual bool Mandatory { get; set; }

    /// <summary>
    /// Gets/sets the routing keys the message should be routed to at the time of publishing, undisclosed to consumers
    /// </summary>
    [DataMember(Order = 7, Name = "bcc"), JsonPropertyOrder(7), JsonPropertyName("bcc"), YamlMember(Order = 7, Alias = "bcc")]
    public virtual EquatableList<string>? Bcc { get; set; }

    /// <summary>
    /// Gets/sets the name of the queue where the consumer should send the response.
    /// </summary>
    [DataMember(Order = 8, Name = "replyTo"), JsonPropertyOrder(8), JsonPropertyName("replyTo"), YamlMember(Order = 8, Alias = "replyTo")]
    public virtual string? ReplyTo { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the message should include a timestamp or not.
    /// </summary>
    [DataMember(Order = 9, Name = "timestamp"), JsonPropertyOrder(9), JsonPropertyName("timestamp"), YamlMember(Order = 9, Alias = "timestamp")]
    public virtual bool Timestamp { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the consumer should ack the message or not.
    /// </summary>
    [DataMember(Order = 10, Name = "ack"), JsonPropertyOrder(10), JsonPropertyName("ack"), YamlMember(Order = 10, Alias = "ack")]
    public virtual bool Ack { get; set; }

}
