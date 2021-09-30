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
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Logging;
using Neuroglia.AsyncApi.Client.Services;
using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Models.Bindings.Kafka;
using Neuroglia.Serialization;
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
        /// <param name="servers">An <see cref="IEnumerable{T}"/> containing the mappings of the <see cref="ServerDefinition"/>s available to the <see cref="IChannelBinding"/></param>
        public KafkaChannelBinding(ILoggerFactory loggerFactory, ISerializerProvider serializers, IChannel channel, IEnumerable<KeyValuePair<string, ServerDefinition>> servers) 
            : base(loggerFactory, serializers, channel, servers)
        {
            ClientConfig clientConfig = new()
            {
                BootstrapServers = this.Servers.First().Value.InterpolateUrlVariables().ToString(),
            };
            this.KafkaAdmin = new AdminClientBuilder(clientConfig)
                .SetErrorHandler((client, e) =>
                {

                })
                .Build();
            if (this.Channel.Definition.DefinesSubscribeOperation)
            {
                ConsumerConfig consumerConfig = new(clientConfig) { AllowAutoCreateTopics = true, AutoOffsetReset = AutoOffsetReset.Latest };
                KafkaOperationBindingDefinition operationBinding = this.Channel.Definition.Subscribe.Bindings?
                    .OfType<KafkaOperationBindingDefinition>()
                    .FirstOrDefault();
                if(operationBinding != null)
                {
                    clientConfig.ClientId = operationBinding.ClientId?.Default?.ToString();
                    if (string.IsNullOrWhiteSpace(clientConfig.ClientId))
                        clientConfig.ClientId = operationBinding.ClientId?.Enum?.FirstOrDefault()?.ToString();
                    consumerConfig.GroupId = operationBinding.GroupId?.Default?.ToString();
                    if (string.IsNullOrWhiteSpace(consumerConfig.GroupId))
                        consumerConfig.ClientId = operationBinding.GroupId?.Enum?.FirstOrDefault()?.ToString();
                }
                this.KafkaConsumer = new ConsumerBuilder<Null, byte[]>(consumerConfig)
                    .Build();
            }
            if (this.Channel.Definition.DefinesPublishOperation)
            {
                ProducerConfig producerConfig = new(clientConfig);
                KafkaOperationBindingDefinition operationBinding = this.Channel.Definition.Publish.Bindings?
                   .OfType<KafkaOperationBindingDefinition>()
                   .FirstOrDefault();
                if (operationBinding != null)
                {
                    clientConfig.ClientId = operationBinding.ClientId?.Default?.ToString();
                    if (string.IsNullOrWhiteSpace(clientConfig.ClientId))
                        clientConfig.ClientId = operationBinding.ClientId?.Enum?.FirstOrDefault()?.ToString();
                }
                this.KafkaProducer = new ProducerBuilder<Null, byte[]>(producerConfig)
                    .Build();
            }
        }

        /// <summary>
        /// Gets the service used to administer Kafka
        /// </summary>
        protected IAdminClient KafkaAdmin { get; }

        /// <summary>
        /// Gets the service used to produce Kafka messages
        /// </summary>
        protected IProducer<Null, byte[]> KafkaProducer { get; }

        /// <summary>
        /// Gets the service used to consume Kafka messages
        /// </summary>
        protected IConsumer<Null, byte[]> KafkaConsumer { get; }

        /// <summary>
        /// Gets the Kafka topic
        /// </summary>
        protected virtual string KafkaTopic => this.Channel.Key.Slugify();

        /// <summary>
        /// Gets the <see cref="Task"/> used to consume messages from the <see cref="KafkaConsumer"/>
        /// </summary>
        protected Task ConsumeTask { get; private set; }

        /// <inheritdoc/>
        protected override string ComputeChannelKeyFor(IMessage message)
        {
            return base.ComputeChannelKeyFor(message).Slugify();
        }

        /// <inheritdoc/>
        public override async Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
        {
            this.ValidateMessage(message);
            this.InjectCorrelationKeyInto(message);
            cancellationToken = CancellationTokenSource.CreateLinkedTokenSource(this.CancellationTokenSource.Token, cancellationToken).Token;
            Message<Null, byte[]> kafkaMessage = new() { Value = await this.SerializeAsync(message.Payload, cancellationToken) };
            kafkaMessage.Headers = new();
            foreach(KeyValuePair<string, object> header in message.Headers)
            {
                kafkaMessage.Headers.Add(new Header(header.Key, await this.SerializeAsync(header.Value, cancellationToken)));
            }
            await this.KafkaProducer.ProduceAsync(this.ComputeChannelKeyFor(message).Slugify(), kafkaMessage, cancellationToken);
        }

        /// <inheritdoc/>
        public override async Task<IDisposable> SubscribeAsync(IObserver<IMessage> observer, CancellationToken cancellationToken = default)
        {
            if (this.ConsumeTask == null)
            {
                try
                {
                    await this.KafkaAdmin.CreateTopicsAsync(new TopicSpecification[] { new() { Name = this.KafkaTopic } });
                }
                catch (CreateTopicsException ex)
                when (ex.Results.First().Error.Code == ErrorCode.TopicAlreadyExists){ }
                catch (Exception ex)
                {
                    this.Logger.LogError($"An error occured attempting to create the channel's topic '{{topic}}':{Environment.NewLine}{{ex}}", this.KafkaTopic, ex.ToString());
                    throw;
                }
                this.KafkaConsumer.Subscribe(this.KafkaTopic);
                this.ConsumeTask = Task.Factory.StartNew(this.ConsumeMessagesAsync, this.CancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
            return await base.SubscribeAsync(observer, cancellationToken);
        }

        /// <summary>
        /// Polls and consumes messages from the <see cref="KafkaConsumer"/>
        /// </summary>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        protected virtual async Task ConsumeMessagesAsync()
        {
            while (!this.CancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    ConsumeResult<Null, byte[]> consumeResult = this.KafkaConsumer.Consume(this.CancellationTokenSource.Token);
                    Message message = new()
                    {
                        ChannelKey = consumeResult.Topic,
                        Timestamp = consumeResult.Message.Timestamp.UtcDateTime,
                        Payload = await this.DeserializeAsync(consumeResult.Message.Value, this.CancellationTokenSource.Token)
                    };
                    if (consumeResult.Message.Headers != null)
                    {
                        foreach (IHeader header in consumeResult.Message.Headers)
                        {
                            message.Headers.Add(header.Key, await this.DeserializeAsync(header.GetValueBytes(), this.CancellationTokenSource.Token));
                        }
                    }
                    message.CorrelationKey = this.ExtractCorrelationKeyFrom(message);
                    this.OnMessageSubject.OnNext(message);
                }
                catch (Exception ex)
                {
                    this.Logger.LogError($"An error occured while consuming an inbound message on channel with key {{channel}}.{Environment.NewLine}{{ex}}", this.Channel.Key, ex.ToString());
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
            this.KafkaAdmin.Dispose();
            this.KafkaConsumer.Dispose();
            this.KafkaProducer.Dispose();
            this.ConsumeTask.Dispose();
        }

    }

}
