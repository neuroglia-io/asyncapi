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
/// Represents the attribute used to define an <see cref="AmqpChannelBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="channelType">The AMQP channel's type</param>
/// <param name="version">The channel's version</param>
public class AmqpChannelBindingAttribute(string name, AmqpChannelType channelType, string version = "latest")
    : ChannelBindingAttribute<AmqpChannelBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets the AMQP channel's type
    /// </summary>
    public AmqpChannelType ChannelType { get; } = channelType;

    /// <summary>
    /// Gets/sets the name of the exchange. It MUST NOT exceed 255 characters long.
    /// </summary>
    public string? ExchangeName { get; init; }

    /// <summary>
    /// Gets/sets the <see cref="AmqpExchangeDefinition"/>'s type
    /// </summary>
    public AmqpExchangeType? ExchangeType { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the exchange should survive broker restarts or not.
    /// </summary>
    public bool? ExchangeDurable { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the exchange should be deleted when the last queue is unbound from it.
    /// </summary>
    public bool? ExchangeAutoDelete { get; init; }

    /// <summary>
    /// Gets/sets the virtual host of the exchange. Defaults to '/'.
    /// </summary>
    public string? ExchangeVirtualHost { get; init; }

    /// <summary>
    /// Gets/sets the name of the queue. It MUST NOT exceed 255 characters long.
    /// </summary>
    public string? QueueName { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the queue should survive broker restarts or not.
    /// </summary>
    public bool? QueueDurable { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the queue should be used only by one connection or not.
    /// </summary>
    public bool? QueueExclusive { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the queue should be deleted when the last queue is unbound from it.
    /// </summary>
    public bool? QueueAutoDelete { get; init; }

    /// <summary>
    /// Gets/sets the virtual host of the queue. Defaults to '/'.
    /// </summary>
    public string? QueueVirtualHost { get; init; }

    /// <inheritdoc/>
    public override AmqpChannelBindingDefinition Build()
    {
        var binding = new AmqpChannelBindingDefinition()
        {
            BindingVersion = this.Version,
            Type = this.ChannelType
        };
        if (!string.IsNullOrWhiteSpace(ExchangeName) || ExchangeDurable.HasValue || ExchangeAutoDelete.HasValue || !string.IsNullOrWhiteSpace(ExchangeVirtualHost))
        {
            binding.Exchange = new()
            {
                Name = ExchangeName,
                Durable = ExchangeDurable ?? false,
                AutoDelete = ExchangeAutoDelete ?? false,
                VirtualHost = ExchangeVirtualHost ?? AmqpExchangeDefinition.DefaultVirtualHost
            };
        }
        if (!string.IsNullOrWhiteSpace(QueueName) || QueueDurable.HasValue || QueueExclusive.HasValue || QueueAutoDelete.HasValue || !string.IsNullOrWhiteSpace(QueueVirtualHost))
        {
            binding.Queue = new()
            {
                Name = QueueName,
                Durable = QueueDurable ?? false,
                Exclusive = QueueExclusive ?? false,
                AutoDelete = QueueAutoDelete ?? false,
                VirtualHost = ExchangeVirtualHost ?? AmqpExchangeDefinition.DefaultVirtualHost
            };
        }
        return binding;
    }

}
