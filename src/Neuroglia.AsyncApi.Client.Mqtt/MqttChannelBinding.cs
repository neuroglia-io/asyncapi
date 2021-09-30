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
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using Neuroglia.AsyncApi.Client.Services;
using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Models.Bindings.Mqtt;
using Neuroglia.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Client
{

    /// <summary>
    /// Represents the MQTT implementation of the <see cref="IChannelBinding"/> interface
    /// </summary>
    public class MqttChannelBinding
        : ChannelBindingBase
    {

        /// <summary>
        /// Initializes a new <see cref="MqttChannelBinding"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="serializers">The service used to provide <see cref="ISerializer"/>s</param>
        /// <param name="channel">The <see cref="IChannel"/> the <see cref="ChannelBindingBase"/> belongs to</param>
        /// <param name="servers">An <see cref="IEnumerable{T}"/> containing the mappings of the <see cref="ServerDefinition"/>s available to the <see cref="IChannelBinding"/></param>
        public MqttChannelBinding(ILoggerFactory loggerFactory, ISerializerProvider serializers, IChannel channel, IEnumerable<KeyValuePair<string, ServerDefinition>> servers)
            : base(loggerFactory, serializers, channel, servers)
        {
            KeyValuePair<string, ServerDefinition> server = servers.First();
            MqttClientOptionsBuilder optionsBuilder = new();
            optionsBuilder.WithTcpServer(server.Value.InterpolateUrlVariables().Host);
            if(server.Value.Protocol == AsyncApiProtocols.MqttV5
                || (!string.IsNullOrWhiteSpace(server.Value.ProtocolVersion) && server.Value.ProtocolVersion.StartsWith("5")))
                optionsBuilder.WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V500);
            MqttServerBindingDefinition serverBindingDefinition = server.Value.Bindings?.OfType<MqttServerBindingDefinition>().FirstOrDefault();
            if(serverBindingDefinition != null)
            {
                if (!string.IsNullOrWhiteSpace(serverBindingDefinition.ClientId))
                    optionsBuilder.WithClientId(serverBindingDefinition.ClientId);
                optionsBuilder.WithCleanSession(serverBindingDefinition.CleanSession);
                if (!serverBindingDefinition.KeepAlive)
                    optionsBuilder.WithNoKeepAlive();
                if(serverBindingDefinition.LastWill != null)
                    optionsBuilder.WithWillMessage(new()
                    {
                        Topic = serverBindingDefinition.LastWill.Topic,
                        QualityOfServiceLevel = (MqttQualityOfServiceLevel)(int)serverBindingDefinition.LastWill.QoS,
                        Payload = Encoding.UTF8.GetBytes(serverBindingDefinition.LastWill.Message),
                        Retain = serverBindingDefinition.LastWill.Retain
                    });
            }
            this.MqttClientOptions = optionsBuilder.Build();
            this.MqttClient = new MqttFactory()
                .CreateMqttClient();
            this.MqttClient.UseApplicationMessageReceivedHandler(OnMessageAsync);
        }

        /// <summary>
        /// Gets the options used to configure the <see cref="MqttClient"/>
        /// </summary>
        protected IMqttClientOptions MqttClientOptions { get; }

        /// <summary>
        /// Gets the service used to interact with the remote MQTT server
        /// </summary>
        protected IMqttClient MqttClient { get; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="MqttChannelBinding"/> has subscribed to the channel's topic and is consuming messages
        /// </summary>
        protected bool ConsumingMessages { get; private set; }

        /// <inheritdoc/>
        public override async Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
        {
            this.ValidateMessage(message);
            await this.EnsureConnectedAsync(cancellationToken);
            string contentType = this.Channel.Definition.Publish.Message.ContentType;
            if (string.IsNullOrWhiteSpace(contentType))
                contentType = this.Channel.Document.DefaultContentType;
            MqttApplicationMessage mqttMessage = new()
            {
                Topic = this.ComputeChannelKeyFor(message),
                ContentType = contentType,
                Payload = await this.SerializeAsync(message.Payload, cancellationToken)
            };
            MqttOperationBindingDefinition operationBindingDefinition = this.Channel.Definition.Publish.Bindings?
                .OfType<MqttOperationBindingDefinition>()
                .FirstOrDefault();
            if(operationBindingDefinition != null)
            {
                mqttMessage.QualityOfServiceLevel = (MqttQualityOfServiceLevel)(int)operationBindingDefinition.QoS;
                mqttMessage.Retain = operationBindingDefinition.Retain;
            }
            mqttMessage.UserProperties = new();
            foreach(KeyValuePair<string, object> header in message.Headers)
            {
                mqttMessage.UserProperties.Add(new(header.Key, header.Value is string str ? str : Encoding.UTF8.GetString(await SerializeAsync(header.Value, cancellationToken))));
            }
            if (message.CorrelationKey != null)
                mqttMessage.CorrelationData = await this.SerializeAsync(message.CorrelationKey, cancellationToken);
            await this.MqttClient.PublishAsync(mqttMessage, cancellationToken);
        }

        /// <inheritdoc/>
        public override async Task<IDisposable> SubscribeAsync(IObserver<IMessage> observer, CancellationToken cancellationToken = default)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));
            await this.EnsureConnectedAsync(cancellationToken);
            if (!this.ConsumingMessages)
            {
                MqttClientSubscribeOptions subscribeOptions = new() { TopicFilters = new() { new MqttTopicFilter() { Topic = this.Channel.Key } } };
                await this.MqttClient.SubscribeAsync(subscribeOptions, cancellationToken);
                this.ConsumingMessages = true;
            }
            return this.OnMessageSubject.Subscribe(observer);
        }

        /// <summary>
        /// Ensures that the <see cref="MqttClient"/> is connected and ready
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>An awaitable <see cref="Task"/></returns>
        protected virtual async Task EnsureConnectedAsync(CancellationToken cancellationToken = default)
        {
            if (this.MqttClient.IsConnected)
                return;
            await this.MqttClient.ConnectAsync(this.MqttClientOptions, cancellationToken);
        }

        /// <summary>
        /// Handles messages consumed by the <see cref="MqttClient"/>
        /// </summary>
        /// <param name="e">The <see cref="MqttApplicationMessageReceivedEventArgs"/> to handle</param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        protected virtual async Task OnMessageAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            try
            {
                Message message = new()
                {
                    ChannelKey = e.ApplicationMessage.Topic,
                    Payload = await this.DeserializeAsync(e.ApplicationMessage.Payload, this.CancellationTokenSource.Token)
                };
                if (e.ApplicationMessage.UserProperties != null)
                {
                    foreach (MqttUserProperty userProperty in e.ApplicationMessage.UserProperties)
                    {
                        message.Headers.Add(userProperty.Name, JToken.FromObject(userProperty.Value));
                    }
                }
                message.CorrelationKey = await this.DeserializeAsync(e.ApplicationMessage.CorrelationData);
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
            this.MqttClient.Dispose();
        }

    }

}
