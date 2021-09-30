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
using Neuroglia.AsyncApi.Models.Bindings.Amqp;
using Neuroglia.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Client
{

    /// <summary>
    /// Represents the Kafka implementation of the <see cref="IChannelBinding"/> interface
    /// </summary>
    public class AmqpChannelBinding
        : ChannelBindingBase
    {

        /// <summary>
        /// Initializes a new <see cref="ChannelBindingBase"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="serializers">The service used to provide <see cref="ISerializer"/>s</param>
        /// <param name="channel">The <see cref="IChannel"/> the <see cref="ChannelBindingBase"/> belongs to</param>
        /// <param name="servers">An <see cref="IEnumerable{T}"/> containing the mappings of the <see cref="ServerDefinition"/>s available to the <see cref="IChannelBinding"/></param>
        public AmqpChannelBinding(ILoggerFactory loggerFactory, ISerializerProvider serializers, IChannel channel, IEnumerable<KeyValuePair<string, ServerDefinition>> servers) 
            : base(loggerFactory, serializers, channel, servers)
        {
            ServerDefinition server = servers.First().Value;
            ConnectionFactory connectionFactory = new() { HostName = server.InterpolateUrlVariables().Host, DispatchConsumersAsync = true };
            this.RabbitMQConnection = connectionFactory.CreateConnection();
            this.RabbitMQChannel = this.RabbitMQConnection.CreateModel();
            this.RabbitMQChannel.BasicQos(0, 1, false);
            this.ServerBindingDefinition = server.Bindings?
                .OfType<AmqpServerBindingDefinition>()
                .FirstOrDefault();
            this.ChannelBindingDefinition = this.Channel.Definition.Bindings?
                .OfType<AmqpChannelBindingDefinition>()
                .FirstOrDefault();
            if (this.ChannelBindingDefinition == null)
            {
                this.RabbitMQChannel.QueueDeclare();
            }  
            else
            {
                if(this.ChannelBindingDefinition.Queue != null)
                    this.RabbitMQChannel.QueueDeclare(this.ChannelBindingDefinition.Queue.Name, this.ChannelBindingDefinition.Queue.Durable, this.ChannelBindingDefinition.Queue.Exclusive, this.ChannelBindingDefinition.Queue.AutoDelete);
                if (this.ChannelBindingDefinition.Exchange != null)
                    this.RabbitMQChannel.ExchangeDeclare(this.ChannelBindingDefinition.Exchange.Name, EnumHelper.Stringify(this.ChannelBindingDefinition.Exchange.Type), this.ChannelBindingDefinition.Exchange.Durable, this.ChannelBindingDefinition.Exchange.AutoDelete);
            }
        }

        /// <summary>
        /// Gets the service used to connect to the remote RabbitMQ server
        /// </summary>
        protected IConnection RabbitMQConnection { get; }

        /// <summary>
        /// Gets the service used to channel messages to the remote RabbitMQ server
        /// </summary>
        protected IModel RabbitMQChannel { get; }

        /// <summary>
        /// Gets the service used to consume messages from the remote RabbitMQ server
        /// </summary>
        protected IBasicConsumer RabbitMQConsumer { get; private set; }

        /// <summary>
        /// Gets the current <see cref="AmqpServerBindingDefinition"/>
        /// </summary>
        protected AmqpServerBindingDefinition ServerBindingDefinition { get; }

        /// <summary>
        /// Gets the current <see cref="AmqpChannelBindingDefinition"/>
        /// </summary>
        protected AmqpChannelBindingDefinition ChannelBindingDefinition { get; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="AmqpChannelBinding"/> has subscribed to the channel's topic and is consuming messages
        /// </summary>
        protected bool ConsumingMessages { get; private set; }

        /// <inheritdoc/>
        public override async Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            string exchange = string.Empty;
            string routingKey = this.ComputeChannelKeyFor(message);
            bool mandatory = false;
            if (this.ChannelBindingDefinition?.Exchange != null)
                exchange = this.ChannelBindingDefinition.Exchange.Name;
            AmqpOperationBindingDefinition operationBindingDefinition = this.Channel.Definition.Publish.Bindings?
                .OfType<AmqpOperationBindingDefinition>()
                .FirstOrDefault();
            AmqpMessageBindingDefinition messageBindingDefinition = this.Channel.Definition.Publish.Message.Bindings?
                .OfType<AmqpMessageBindingDefinition>()
                .FirstOrDefault();
            IBasicProperties properties = this.RabbitMQChannel.CreateBasicProperties();
            properties.ContentType = this.Channel.Definition.Publish.Message.ContentType;
            if (string.IsNullOrWhiteSpace(properties.ContentType))
                properties.ContentType = this.Channel.DefaultContentType;
            if (operationBindingDefinition != null)
            {
                mandatory = operationBindingDefinition.Mandatory;
                properties.Expiration = operationBindingDefinition.Expiration.ToString();
                properties.UserId = operationBindingDefinition.UserId;
                properties.Priority = operationBindingDefinition.Priority;
                properties.DeliveryMode = (byte)operationBindingDefinition.DeliveryMode;
                properties.ReplyTo = operationBindingDefinition.ReplyTo;
                if (operationBindingDefinition.Timestamp)
                    properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            } 
            if (messageBindingDefinition != null)
            {
                properties.ContentEncoding = messageBindingDefinition.ContentEncoding;
                properties.Type = messageBindingDefinition.MessageType;
            }
            properties.Headers = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> header in message.Headers)
            {
                properties.Headers.Add(header.Key, await this.SerializeAsync(header.Value, cancellationToken));
            }
            if (message.CorrelationKey != null)
                properties.CorrelationId = Encoding.UTF8.GetString(await this.SerializeAsync(message.CorrelationKey, cancellationToken));
            this.RabbitMQChannel.BasicPublish(exchange, routingKey, mandatory, properties, await this.SerializeAsync(message.Payload, cancellationToken));
        }

        /// <inheritdoc/>
        public override async Task<IDisposable> SubscribeAsync(IObserver<IMessage> observer, CancellationToken cancellationToken = default)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));
            if (!this.ConsumingMessages)
            {
                AmqpOperationBindingDefinition operationBindingDefinition = this.Channel.Definition.Subscribe.Bindings?
                    .OfType<AmqpOperationBindingDefinition>()
                    .FirstOrDefault();
                AmqpMessageBindingDefinition messageBindingDefinition = this.Channel.Definition.Subscribe.Message.Bindings?
                    .OfType<AmqpMessageBindingDefinition>()
                    .FirstOrDefault();
                string queue = string.Empty;
                bool autoAck = true;
                string consumerTag = string.Empty;
                bool noLocal = false;
                bool exclusive = true;
                if (this.ChannelBindingDefinition?.Queue != null)
                {
                    queue = this.ChannelBindingDefinition.Queue.Name;
                    exclusive = this.ChannelBindingDefinition.Queue.Exclusive;
                }
                if (operationBindingDefinition != null)
                    autoAck = !operationBindingDefinition.Ack;
                if (this.ChannelBindingDefinition.Exchange != null
                    && !string.IsNullOrWhiteSpace(this.ChannelBindingDefinition.Exchange.Name))
                    this.RabbitMQChannel.QueueBind(queue, this.ChannelBindingDefinition.Exchange.Name, this.Channel.Key);
                this.RabbitMQConsumer = new AsyncEventingBasicConsumer(this.RabbitMQChannel);
                ((AsyncEventingBasicConsumer)this.RabbitMQConsumer).Received += this.OnMessageAsync;
                this.RabbitMQChannel.BasicConsume(this.RabbitMQConsumer, queue, autoAck, consumerTag, noLocal, exclusive);
                this.RabbitMQChannel.DefaultConsumer = this.RabbitMQConsumer;
                this.ConsumingMessages = true;
            }
            return await base.SubscribeAsync(observer, cancellationToken);
        }

        /// <summary>
        /// Handles messages consumed by the <see cref="AmqpChannelBinding"/>
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event args</param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        protected virtual async Task OnMessageAsync(object sender, BasicDeliverEventArgs e)
        {
            Message message = new()
            {
                ChannelKey = e.RoutingKey,
                Payload = await this.DeserializeAsync(e.Body.ToArray())
            };
            if(e.BasicProperties != null)
            {
                message.Timestamp = UnixTimeStamp.ToDateTime(e.BasicProperties.Timestamp.UnixTime);
                if (e.BasicProperties.Headers != null)
                {
                    foreach (KeyValuePair<string, object> header in e.BasicProperties.Headers)
                    {
                        message.Headers.Add(header.Key, await this.DeserializeAsync(header.Value as byte[]));
                    }
                }
                if (!string.IsNullOrWhiteSpace(e.BasicProperties.CorrelationId))
                    message.CorrelationKey = await this.DeserializeAsync(Encoding.UTF8.GetBytes(e.BasicProperties.CorrelationId));
            }
            AmqpOperationBindingDefinition operationBindingDefinition = this.Channel.Definition.Subscribe.Bindings?
                 .OfType<AmqpOperationBindingDefinition>()
                 .FirstOrDefault();
            if (operationBindingDefinition != null
                && operationBindingDefinition.Ack)
                this.RabbitMQChannel.BasicAck(e.DeliveryTag, false);
            this.OnMessageSubject.OnNext(message);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing)
                return;
            this.RabbitMQChannel.Dispose();
            this.RabbitMQConnection.Dispose();
        }

    }

}
