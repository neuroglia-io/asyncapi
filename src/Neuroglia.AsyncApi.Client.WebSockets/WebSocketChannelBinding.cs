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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Client
{

    /// <summary>
    /// Represents the NATS implementation of the <see cref="IChannelBinding"/> interface
    /// </summary>
    public class WebSocketChannelBinding
        : ChannelBindingBase
    {

        /// <summary>
        /// Initializes a new <see cref="WebSocketChannelBinding"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="serializers">The service used to provide <see cref="ISerializer"/>s</param>
        /// <param name="channel">The <see cref="IChannel"/> the <see cref="ChannelBindingBase"/> belongs to</param>
        /// <param name="servers">An <see cref="IEnumerable{T}"/> containing the mappings of the <see cref="ServerDefinition"/>s available to the <see cref="IChannelBinding"/></param>
        public WebSocketChannelBinding(ILoggerFactory loggerFactory, ISerializerProvider serializers, IChannel channel, IEnumerable<KeyValuePair<string, ServerDefinition>> servers)
            : base(loggerFactory, serializers, channel, servers)
        {
            KeyValuePair<string, ServerDefinition> server = servers.First(); //todo
            this.ClientWebSocket = new ClientWebSocket();
        }

        /// <summary>
        /// Gets the service used to interact with the WebSocket server
        /// </summary>
        protected ClientWebSocket ClientWebSocket { get; }

        /// <summary>
        /// Gets the <see cref="Task"/> used to consume messages from the <see cref="ClientWebSocket"/>
        /// </summary>
        protected Task ConsumeTask { get; private set; }

        /// <inheritdoc/>
        public override async Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
        {
            await this.EnsureConnectedAsync(cancellationToken);
            this.ValidateMessage(message);
            this.InjectCorrelationKeyInto(message);
            if (message.Headers.Any())
                this.Logger.LogWarning("The WebSocket channel binding does not support headers.");
            await this.ClientWebSocket.SendAsync(await this.SerializeAsync(message.Payload, cancellationToken), WebSocketMessageType.Binary, true, cancellationToken);
        }

        /// <inheritdoc/>
        public override async Task<IDisposable> SubscribeAsync(IObserver<IMessage> observer, CancellationToken cancellationToken = default)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));
            await this.EnsureConnectedAsync(cancellationToken);
            if (this.ConsumeTask == null)
                this.ConsumeTask = Task.Factory.StartNew(this.ConsumeMessagesAsync, this.CancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            return await base.SubscribeAsync(observer, cancellationToken);
        }

        /// <summary>
        /// Ensures that the <see cref="ClientWebSocket"/> is connected and ready
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>An awaitable <see cref="Task"/></returns>
        protected virtual async Task EnsureConnectedAsync(CancellationToken cancellationToken = default)
        {
            if (this.ClientWebSocket.State == WebSocketState.Connecting
                || this.ClientWebSocket.State == WebSocketState.Open)
                return;
            await this.ClientWebSocket.ConnectAsync(this.Servers.First().Value.InterpolateUrlVariables(), cancellationToken);
        }

        /// <summary>
        /// Polls and consumes messages from the <see cref="ClientWebSocket"/>
        /// </summary>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        protected virtual async Task ConsumeMessagesAsync()
        {
            while (!this.CancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    using MemoryStream stream = new();
                    WebSocketReceiveResult result;
                    ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[2048]);
                    do
                    {
                        result = await this.ClientWebSocket.ReceiveAsync(buffer, this.CancellationTokenSource.Token);
                        await stream.WriteAsync(buffer.Array.AsMemory(buffer.Offset, result.Count), this.CancellationTokenSource.Token);
                    }
                    while (!result.EndOfMessage);
                    Message message = new()
                    {
                        ChannelKey = this.Channel.Key,
                        Timestamp = DateTime.UtcNow,
                        Payload = await this.DeserializeAsync(stream.ToArray(), this.CancellationTokenSource.Token)
                    };
                    message.CorrelationKey = this.ExtractCorrelationKeyFrom(message);
                    this.OnMessageSubject.OnNext(message);
                }
                catch (Exception ex)
                {
                    this.Logger.LogError($"An error occured while consuming an inbound message on channel with key {{channel}}.{Environment.NewLine}{{ex}}", this.Channel.Key, ex.ToString());
                }
            }
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing)
                return;
            this.ClientWebSocket.Dispose();
        }

    }

}
