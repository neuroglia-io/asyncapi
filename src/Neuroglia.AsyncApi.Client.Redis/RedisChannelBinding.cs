/*
 * Copyright © 2021 Neuroglia SPRL. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using Microsoft.Extensions.Logging;
using Neuroglia.AsyncApi.Client.Services;
using Neuroglia.AsyncApi.Models;
using Neuroglia.Serialization;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Client
{

    /// <summary>
    /// Represents the NATS implementation of the <see cref="IChannelBinding"/> interface
    /// </summary>
    public class RedisChannelBinding
        : ChannelBindingBase
    {

        private const int DefaultRedisPort = 6379;

        /// <summary>
        /// Initializes a new <see cref="RedisChannelBinding"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="serializers">The service used to provide <see cref="ISerializer"/>s</param>
        /// <param name="channel">The <see cref="IChannel"/> the <see cref="ChannelBindingBase"/> belongs to</param>
        /// <param name="servers">An <see cref="IEnumerable{T}"/> containing the mappings of the <see cref="ServerDefinition"/>s available to the <see cref="IChannelBinding"/></param>
        public RedisChannelBinding(ILoggerFactory loggerFactory, ISerializerProvider serializers, IChannel channel, IEnumerable<KeyValuePair<string, ServerDefinition>> servers)
            : base(loggerFactory, serializers, channel, servers)
        {
            KeyValuePair<string, ServerDefinition> server = servers.First(); //todo
            ConfigurationOptions options = new();
            Uri serverUri = server.Value.InterpolateUrlVariables();
            options.EndPoints.Add(serverUri.Host, serverUri.Port == -1 ? DefaultRedisPort : serverUri.Port);
            this.ConnectionMultiplexer = StackExchange.Redis.ConnectionMultiplexer.Connect(options);
        }

        /// <summary>
        /// Gets the <see cref="RedisChannelBinding"/>'s <see cref="IConnectionMultiplexer"/>
        /// </summary>
        protected IConnectionMultiplexer ConnectionMultiplexer { get; }

        /// <summary>
        /// Gets the <see cref="RedisChannelBinding"/>'s <see cref="ISubscriber"/>
        /// </summary>
        protected ISubscriber Subscriber => this.ConnectionMultiplexer.GetSubscriber();

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="RedisChannelBinding"/> has subscribed to the channel's topic and is consuming messages
        /// </summary>
        protected bool ConsumingMessages { get; private set; }

        /// <inheritdoc/>
        public override async Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
        {
            this.ValidateMessage(message);
            this.InjectCorrelationKeyInto(message);
            if (message.Headers.Any())
                this.Logger.LogWarning("The Redis channel binding does not support headers.");
            await this.Subscriber.PublishAsync(this.ComputeChannelKeyFor(message), await this.SerializeAsync(message.Payload, cancellationToken), CommandFlags.FireAndForget);
        }

        /// <inheritdoc/>
        public override async Task<IDisposable> SubscribeAsync(IObserver<IMessage> observer, CancellationToken cancellationToken = default)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));
            if (!this.ConsumingMessages)
            {
                await this.Subscriber.SubscribeAsync(this.Channel.Key, this.OnMessageAsync);
                this.ConsumingMessages = true;
            }
            return await base.SubscribeAsync(observer, cancellationToken);
        }

        /// <summary>
        /// Handles messages consumed by the <see cref="Subscriber"/>
        /// </summary>
        /// <param name="channel">The channel that has consumed the message</param>
        /// <param name="value">The consumed message</param>
        protected virtual async void OnMessageAsync(RedisChannel channel, RedisValue value)
        {
            try
            {
                Message message = new()
                {
                    ChannelKey = channel,
                    Timestamp = DateTime.UtcNow,
                    Payload = await this.DeserializeAsync(value)
                };
                message.CorrelationKey = this.ExtractCorrelationKeyFrom(message);
                this.OnMessageSubject.OnNext(message);
            }
            catch (Exception ex)
            {
                this.Logger.LogError($"An error occured while consuming an inbound message on channel with key {{channel}}.{Environment.NewLine}{{ex}}", this.Channel.Key, ex.ToString());
            }
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing)
                return;
            this.ConnectionMultiplexer.Dispose();
        }

    }

}
