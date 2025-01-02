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

namespace Neuroglia.AsyncApi.Bindings.Amqp;

/// <summary>
/// Represents the attribute used to define an <see cref="AmqpOperationBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="version">The channel's version</param>
public class AmqpOperationBindingAttribute(string name, string version = "latest")
    : OperationBindingAttribute<AmqpOperationBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets/sets the TTL (Time-To-Live) for the message. It MUST be greater than or equal to zero.
    /// </summary>
    public virtual int Expiration { get; init; }

    /// <summary>
    /// Gets/sets a string that identifies the user who has sent the message.
    /// </summary>
    public virtual string? UserId { get; init; }

    /// <summary>
    /// Gets/sets the routing keys the message should be routed to at the time of publishing.
    /// </summary>
    public virtual string[]? Cc { get; init; }

    /// <summary>
    /// Gets/sets a priority for the message.
    /// </summary>
    public virtual byte Priority { get; init; }

    /// <summary>
    /// Gets/sets the delivery mode of the message
    /// </summary>
    public virtual AmqpDeliveryMode DeliveryMode { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the message is mandatory or not.
    /// </summary>
    public virtual bool Mandatory { get; init; }

    /// <summary>
    /// Gets/sets the routing keys the message should be routed to at the time of publishing, undisclosed to consumers
    /// </summary>
    public virtual string[]? Bcc { get; init; }

    /// <summary>
    /// Gets/sets the name of the queue where the consumer should send the response.
    /// </summary>
    public virtual string? ReplyTo { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the message should include a timestamp or not.
    /// </summary>
    public virtual bool Timestamp { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the consumer should ack the message or not.
    /// </summary>
    public virtual bool Ack { get; init; }

    /// <inheritdoc/>
    public override AmqpOperationBindingDefinition Build() => new()
    {
        BindingVersion = Version,
        Expiration = Expiration,
        UserId = UserId,
        Cc = Cc == null ? null : new(Cc),
        Priority = Priority,
        DeliveryMode = DeliveryMode,
        Mandatory = Mandatory,
        Bcc = Bcc == null ? null : new(Bcc),
        ReplyTo = ReplyTo,
        Timestamp = Timestamp,
        Ack = Ack
    };

}
