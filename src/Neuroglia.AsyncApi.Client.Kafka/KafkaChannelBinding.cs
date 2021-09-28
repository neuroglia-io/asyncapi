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
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Neuroglia.AsyncApi.Client.Services;
using Neuroglia.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Client
{

    /// <summary>
    /// Represents the Kafka implementation of the <see cref="IChannelBinding"/> interface
    /// </summary>
    public class KafkaChannelBinding
        : ChannelBindingBase
    {

        /// <summary>
        /// Initializes a new <see cref="ChannelBindingBase"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="serializers">The service used to provide <see cref="ISerializer"/>s</param>
        /// <param name="channel">The <see cref="IChannel"/> the <see cref="ChannelBindingBase"/> belongs to</param>
        public KafkaChannelBinding(ILoggerFactory loggerFactory, ISerializerProvider serializers, IChannel channel) 
            : base(loggerFactory, serializers, channel)
        {
            ClientConfig clientConfig = new()
            {
                BootstrapServers = "",
                ClientId = ""
            };
            if (this.Channel.Definition.DefinesSubscribeOperation)
            {
                ConsumerConfig consumerConfig = new(clientConfig);
                this.KafkaProducer = new ProducerBuilder<Null, byte[]>(consumerConfig)
                    .Build();
                this.ConsumeTask = Task.Run(() => this.ConsumeMessagesAsync(), this.CancellationTokenSource.Token);
            }
            if (this.Channel.Definition.DefinesPublishOperation)
            {
                ProducerConfig producerConfig = new(clientConfig);
                this.KafkaConsumer = new ConsumerBuilder<Null, byte[]>(producerConfig)
                    .Build();
            }
        }

        /// <summary>
        /// Gets the service used to produce Kafka messages
        /// </summary>
        protected IProducer<Null, byte[]> KafkaProducer { get; }

        /// <summary>
        /// Gets the service used to consume Kafka messages
        /// </summary>
        protected IConsumer<Null, byte[]> KafkaConsumer { get; }

        /// <summary>
        /// Gets the <see cref="Task"/> used to consume messages from the <see cref="KafkaConsumer"/>
        /// </summary>
        protected Task ConsumeTask { get; private set; }

        /// <inheritdoc/>
        public override async Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            cancellationToken = CancellationTokenSource.CreateLinkedTokenSource(this.CancellationTokenSource.Token, cancellationToken).Token;
            Message<Null, byte[]> kafkaMessage = new() { Value = await this.SerializeAsync(message.Payload, cancellationToken) };
            foreach(KeyValuePair<string, object> header in message.Headers)
            {
                kafkaMessage.Headers.Add(new Header(header.Key, await this.SerializeAsync(header.Value, cancellationToken)));
            }
            await this.KafkaProducer.ProduceAsync(this.ComputeChannelKeyFor(message), kafkaMessage, cancellationToken);
        }

        /// <summary>
        /// Polls and consumes messages from the <see cref="KafkaConsumer"/>
        /// </summary>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        protected virtual async Task ConsumeMessagesAsync()
        {
            while (!this.CancellationTokenSource.IsCancellationRequested)
            {
                ConsumeResult<Null, byte[]> consumeResult = this.KafkaConsumer.Consume(this.CancellationTokenSource.Token);
                Message message = new()
                {
                    ChannelKey = consumeResult.Topic,
                    Timestamp = consumeResult.Message.Timestamp.UtcDateTime,
                    Payload = await this.DeserializeAsync(consumeResult.Message.Value, this.CancellationTokenSource.Token)
                };
                if(consumeResult.Message.Headers != null)
                {
                    foreach (IHeader header in consumeResult.Message.Headers)
                    {
                        message.Headers.Add(header.Key, await this.DeserializeAsync(header.GetValueBytes(), this.CancellationTokenSource.Token));
                    }
                }
                if (this.Channel.Definition.Subscribe.Message.CorrelationId != null)
                {
                    JToken correlationKey = null;
                    string[] components = this.Channel.Definition.Subscribe.Message.CorrelationId.LocationExpression.Fragment.Split("/", StringSplitOptions.RemoveEmptyEntries);
                    switch (this.Channel.Definition.Subscribe.Message.CorrelationId.LocationExpression.Source)
                    {
                        case RuntimeExpressionSource.Header:
                            if (!message.Headers.TryGetValue(components.First(), out object value))
                                break;
                            correlationKey = (JToken)value;
                            for (int i = 1; i < components.Length; i++)
                            {
                                if (correlationKey is not JObject jobject)
                                    break;
                                JProperty property = jobject.Property(components[i]);
                                if (property == null)
                                    break;
                                correlationKey = property.Value;
                            }
                            break;
                        case RuntimeExpressionSource.Payload:
                            correlationKey = message.Payload as JToken;
                            for (int i = 0; i < components.Length; i++)
                            {
                                if (correlationKey is not JObject jobject)
                                    break;
                                JProperty property = jobject.Property(components[i]);
                                if (property == null)
                                    break;
                                correlationKey = property.Value;
                            }
                            break;
                        default:
                            throw new NotSupportedException($"The specified {nameof(RuntimeExpressionSource)} '{this.Channel.Definition.Subscribe.Message.CorrelationId.LocationExpression.Source}' is not supported");
                    }
                    message.CorrelationKey = correlationKey;
                }
            }
            this.KafkaConsumer.Close();
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing)
                return;
            this.KafkaConsumer.Dispose();
            this.KafkaProducer.Dispose();
            this.ConsumeTask.Dispose();
        }

    }

}
