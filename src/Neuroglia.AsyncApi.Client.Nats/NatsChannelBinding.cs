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
using NATS.Client;
using Neuroglia.AsyncApi.Client.Services;
using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Models.Bindings.Nats;
using Neuroglia.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Client
{

    /// <summary>
    /// Represents the NATS implementation of the <see cref="IChannelBinding"/> interface
    /// </summary>
    public class NatsChannelBinding
        : ChannelBindingBase
    {

        /// <summary>
        /// Initializes a new <see cref="NatsChannelBinding"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="serializers">The service used to provide <see cref="ISerializer"/>s</param>
        /// <param name="channel">The <see cref="IChannel"/> the <see cref="ChannelBindingBase"/> belongs to</param>
        /// <param name="servers">An <see cref="IEnumerable{T}"/> containing the mappings of the <see cref="ServerDefinition"/>s available to the <see cref="IChannelBinding"/></param>
        public NatsChannelBinding(ILoggerFactory loggerFactory, ISerializerProvider serializers, IChannel channel, IEnumerable<KeyValuePair<string, ServerDefinition>> servers)
            : base(loggerFactory, serializers, channel, servers)
        {
            KeyValuePair<string, ServerDefinition> server = servers.First(); //todo
            ConnectionFactory connectionFactory = new();
            Options options = ConnectionFactory.GetDefaultOptions();
            options.Url = server.Value.InterpolateUrlVariables().ToString();
            this.NatsConnection = connectionFactory.CreateConnection(options);
        }

        /// <summary>
        /// Gets the service used to interact with the NATS server
        /// </summary>
        protected IConnection NatsConnection { get; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="NatsChannelBinding"/> has subscribed to the channel's topic and is consuming messages
        /// </summary>
        protected bool ConsumingMessages { get; private set; }

        /// <inheritdoc/>
        public override async Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
        {
            this.ValidateMessage(message);
            this.InjectCorrelationKeyInto(message);
            Msg natsMessage = new(this.ComputeChannelKeyFor(message), await this.SerializeAsync(message.Payload, cancellationToken));
            natsMessage.Header = new();
            foreach(KeyValuePair<string, object> header in message.Headers)
            {
                natsMessage.Header.Add(header.Key, Encoding.UTF8.GetString(await this.SerializeAsync(header.Value, cancellationToken)));
            }
            this.NatsConnection.Publish(natsMessage);
        }

        /// <inheritdoc/>
        public override async Task<IDisposable> SubscribeAsync(IObserver<IMessage> observer, CancellationToken cancellationToken = default)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));
            if (!this.ConsumingMessages)
            {
                string subject = this.Channel.Key;
                NatsOperationBindingDefinition operationBindingDefinition = this.Channel.Definition.Subscribe.Bindings?
                    .OfType<NatsOperationBindingDefinition>()
                    .FirstOrDefault();
                EventHandler<MsgHandlerEventArgs> eventHandler = new(this.OnMessageAsync);
                if (operationBindingDefinition == null)
                    this.NatsConnection.SubscribeAsync(subject, eventHandler);
                else
                    this.NatsConnection.SubscribeAsync(subject, operationBindingDefinition.Queue, eventHandler);
                this.ConsumingMessages = true;
            }
            return await base.SubscribeAsync(observer, cancellationToken);
        }

        /// <summary>
        /// Handles messages consumed by the <see cref="NatsConnection"/>
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event args</param>
        protected virtual async void OnMessageAsync(object sender, MsgHandlerEventArgs e)
        {
            try
            {
                Message message = new()
                {
                    ChannelKey = e.Message.Subject,
                    Timestamp = DateTime.UtcNow,
                    Payload = await this.DeserializeAsync(e.Message.Data)
                };
                if (e.Message.HasHeaders)
                {
                    foreach (string headerKey in e.Message.Header.Keys.OfType<string>())
                    {
                        message.Headers.Add(headerKey, await this.DeserializeAsync(Encoding.UTF8.GetBytes(e.Message.Header[headerKey])));
                    }
                }
                message.CorrelationKey = this.ExtractCorrelationKeyFrom(message);
                this.OnMessageSubject.OnNext(message);
            }
            catch(Exception ex)
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
            this.NatsConnection.Dispose();
        }

    }

}
