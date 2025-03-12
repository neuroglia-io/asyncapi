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

using Microsoft.Extensions.DependencyInjection;
using MQTTnet;

namespace Neuroglia.AsyncApi.Client.Bindings.Mqtt;

/// <summary>
/// Represents the default MQTT implementation of the <see cref="IBindingHandler"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="options">The service used to access the current <see cref="MqttBindingHandlerOptions"/></param>
/// <param name="serializerProvider">The service used to provide <see cref="ISerializer"/>s</param>
public class MqttBindingHandler(IServiceProvider serviceProvider, ILogger<MqttBindingHandler> logger, IOptions<MqttBindingHandlerOptions> options, ISerializerProvider serializerProvider)
    : IBindingHandler<MqttBindingHandlerOptions>
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; } = logger;

    /// <summary>
    /// Gets the current <see cref="MqttBindingHandlerOptions"/>
    /// </summary>
    protected MqttBindingHandlerOptions Options { get; } = options.Value;

    /// <summary>
    /// Gets the service used to provide <see cref="ISerializer"/>s
    /// </summary>
    protected ISerializerProvider SerializerProvider { get; } = serializerProvider;

    /// <inheritdoc/>
    public virtual bool Supports(string protocol, string? protocolVersion) => protocol.Equals(AsyncApiProtocol.Mqtt, StringComparison.OrdinalIgnoreCase) || protocol.Equals(AsyncApiProtocol.MqttV5, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public virtual async Task<IAsyncApiPublishOperationResult> PublishAsync(AsyncApiPublishOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        var serverBinding = context.ChannelBinding as MqttServerBindingDefinition;
        var operationBinding = context.OperationBinding as MqttOperationBindingDefinition;
        var messageBinding = context.MessageBinding as MqttMessageBindingDefinition;
        var hostComponents = context.Host.Split(':');
        var host = hostComponents[0];
        var port = hostComponents.Length == 2 ? int.Parse(hostComponents[1]) : (int?)null;
        var clientOptionsBuilder = new MqttClientOptionsBuilder().WithTcpServer(host, port);
        if (!string.IsNullOrWhiteSpace(serverBinding?.ClientId)) clientOptionsBuilder.WithClientId(serverBinding.ClientId);
        if (serverBinding?.CleanSession == true) clientOptionsBuilder.WithCleanSession();
        if (serverBinding?.KeepAlive.HasValue == true) clientOptionsBuilder.WithKeepAlivePeriod(TimeSpan.FromSeconds(serverBinding.KeepAlive.Value));
        if (!string.IsNullOrWhiteSpace(serverBinding?.LastWill?.Topic)) clientOptionsBuilder.WithWillTopic(serverBinding.LastWill?.Topic);
        if (serverBinding?.LastWill?.QoS.HasValue == true) clientOptionsBuilder.WithWillQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)serverBinding.LastWill.QoS);
        if (serverBinding?.LastWill?.Retain == true) clientOptionsBuilder.WithWillRetain();
        var mqttClientFactory = new MqttClientFactory();
        using var mqttClient = mqttClientFactory.CreateMqttClient();
        var clientOptions = clientOptionsBuilder.Build();
        var connectResult = await mqttClient.ConnectAsync(clientOptions, cancellationToken).ConfigureAwait(false);
        if (connectResult == null || connectResult.ResultCode != MqttClientConnectResultCode.Success) return new MqttPublishOperationResult(connectResult?.ResultCode);
        var contentType = messageBinding?.ContentType ?? context.ContentType;
        using var stream = new MemoryStream();
        var serializer = SerializerProvider.GetSerializersFor(contentType).FirstOrDefault() ?? throw new NullReferenceException($"Failed to find a serializer for the specified content type '{contentType}'");
        serializer.Serialize(context.Payload ?? new { }, stream);
        await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
        stream.Position = 0;
        var messageBuilder = new MqttApplicationMessageBuilder()
            .WithTopic(context.Channel)
            .WithPayload(stream)
            .WithContentType(contentType)
            .WithPayloadFormatIndicator(messageBinding?.PayloadFormatIndicator == 0 ? MQTTnet.Protocol.MqttPayloadFormatIndicator.Unspecified : MQTTnet.Protocol.MqttPayloadFormatIndicator.CharacterData)
            .WithRetainFlag(operationBinding?.Retain ?? false);
        if (context.Headers != null) foreach(var header in context.Headers.ToDictionary()!) messageBuilder.WithUserProperty(header.Key, header.Value.ToString());
        //if (messageBinding?.CorrelationData != null) messageBuilder.WithCorrelationData(); //todo: implement
        if (!string.IsNullOrWhiteSpace(messageBinding?.ResponseTopic)) messageBuilder.WithResponseTopic(messageBinding.ResponseTopic);
        var message = messageBuilder.Build();
        var publishResult = await mqttClient.PublishAsync(message, cancellationToken).ConfigureAwait(false);
        await mqttClient.DisconnectAsync(new MqttClientDisconnectOptions(), cancellationToken).ConfigureAwait(false);
        return new MqttPublishOperationResult(connectResult.ResultCode, publishResult?.ReasonCode, publishResult?.ReasonString, publishResult?.PacketIdentifier);
    }

    /// <inheritdoc/>
    public virtual async Task<IAsyncApiSubscribeOperationResult> SubscribeAsync(AsyncApiSubscribeOperationContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        var serverBinding = context.ChannelBinding as MqttServerBindingDefinition;
        var operationBinding = context.OperationBinding as MqttOperationBindingDefinition;
        var mqttClientFactory = new MqttClientFactory();
        var mqttClient = mqttClientFactory.CreateMqttClient();
        var subscription = ActivatorUtilities.CreateInstance<MqttSubscription>(ServiceProvider, mqttClient, context.Document, context.Messages);
        var hostComponents = context.Host.Split(':');
        var host = hostComponents[0];
        var port = hostComponents.Length == 2 ? int.Parse(hostComponents[1]) : (int?)null;
        var clientOptionsBuilder = new MqttClientOptionsBuilder().WithTcpServer(host, port);
        if (!string.IsNullOrWhiteSpace(serverBinding?.ClientId)) clientOptionsBuilder.WithClientId(serverBinding.ClientId);
        if (serverBinding?.CleanSession == true) clientOptionsBuilder.WithCleanSession();
        if (serverBinding?.KeepAlive.HasValue == true) clientOptionsBuilder.WithKeepAlivePeriod(TimeSpan.FromSeconds(serverBinding.KeepAlive.Value));
        if (!string.IsNullOrWhiteSpace(serverBinding?.LastWill?.Topic)) clientOptionsBuilder.WithWillTopic(serverBinding.LastWill?.Topic);
        if (serverBinding?.LastWill?.QoS.HasValue == true) clientOptionsBuilder.WithWillQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)serverBinding.LastWill.QoS);
        if (serverBinding?.LastWill?.Retain == true) clientOptionsBuilder.WithWillRetain();
        var clientOptions = clientOptionsBuilder.Build();
        var connectResult = await mqttClient.ConnectAsync(clientOptions, cancellationToken).ConfigureAwait(false);
        if (connectResult == null || connectResult.ResultCode != MqttClientConnectResultCode.Success) return new MqttSubscribeOperationResult(connectResult?.ResultCode, reason: connectResult?.ReasonString);
        var subscribeOptions = new MqttClientSubscribeOptionsBuilder().WithTopicFilter(filter =>
            {
                filter.WithTopic(context.Channel);
                if (operationBinding?.QoS.HasValue == true) filter.WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)operationBinding.QoS);
            }).Build();
        var subscribeResult = await mqttClient.SubscribeAsync(subscribeOptions, cancellationToken).ConfigureAwait(false);
        return new MqttSubscribeOperationResult(connectResult.ResultCode, subscribeResult.Items, subscribeResult.ReasonString, subscribeResult.PacketIdentifier, subscription);
    }

}
