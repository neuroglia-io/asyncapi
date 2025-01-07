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

using Neuroglia.AsyncApi.Bindings.Mqtt;
using Neuroglia.AsyncApi.v2;
using Neuroglia.AsyncApi.v3;
using System.Net;

namespace StreetLightsApi.Services;

/// <summary>
/// Represents the Smartylighting Streetlights API, which allows to remotely manage the city lights
/// </summary>
/// <param name="logger">The service used to perform logging</param>
/// <param name="serializer">The service used to serialize/deserialize data to/from JSON</param>
[Neuroglia.AsyncApi.v2.AsyncApi("Streetlights API", "1.0.0", Description = "The Smartylighting Streetlights API allows you to remotely manage the city lights.", LicenseName = "Apache 2.0", LicenseUrl = "https://www.apache.org/licenses/LICENSE-2.0")]
[Neuroglia.AsyncApi.v3.AsyncApi("Streetlights API", "1.0.0", Description = "The **Smartylighting Streetlights API** allows you to remotely manage the city lights.", LicenseName = "Apache 2.0", LicenseUrl = "https://www.apache.org/licenses/LICENSE-2.0")]
[Server("http", "http://fake-http-server.com", AsyncApiProtocol.Http, PathName = "/{environment}", Description = "A sample **HTTP** server declared using attributes", Bindings = "#/components/serverBindings/http")]
[ServerVariable("http", "environment", Description = "The **environment** to use.", Enum = ["dev", "stg", "prod"])]
[HttpServerBinding("http")]
[Neuroglia.AsyncApi.v3.Channel("lightingMeasuredMQTT", Address = "streets.{streetName}", Description = "This channel is used to exchange messages about lightning measurements.", Servers = ["#/servers/mosquitto"], Bindings = "#/components/channelBindings/mqtt")]
[MqttChannelBinding("mqtt")]
[ChannelParameter("lightingMeasured", "streetName", Description = "The name of the **street** the lights to get measurements for are located in")]
public class StreetLightsService(ILogger<StreetLightsService> logger, IJsonSerializer serializer)
    : BackgroundService
{

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; } = logger;

    /// <summary>
    /// Gets the service used to serialize/deserialize data to/from JSON
    /// </summary>
    protected IJsonSerializer Serializer { get; } = serializer;

    /// <summary>
    /// Gets the service used to interact with an MQTT server
    /// </summary>
    protected IMqttClient MqttClient { get; private set; } = null!;

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this.MqttClient = new MqttFactory().CreateMqttClient();
        var options = new MqttClientOptions() { ChannelOptions = new MqttClientTcpOptions() { RemoteEndpoint = new DnsEndPoint("test.mosquitto.org", 1883) } };
        await this.MqttClient.ConnectAsync(options, stoppingToken).ConfigureAwait(false);
        stoppingToken.Register(async () => await this.MqttClient.DisconnectAsync().ConfigureAwait(false));
        this.MqttClient.ApplicationMessageReceivedAsync += (async message =>
        {
            var e = this.Serializer.Deserialize<LightMeasuredEvent>(Encoding.UTF8.GetString(message.ApplicationMessage.PayloadSegment))!;
            await this.OnLightMeasured(e);
            await message.AcknowledgeAsync(stoppingToken);
        });
        await this.MqttClient.SubscribeAsync("onLightMeasured", cancellationToken: stoppingToken).ConfigureAwait(false);
        await this.PublishLightMeasured(new() { Id = Guid.NewGuid(), Lumens = 5, SentAt = DateTime.UtcNow }, stoppingToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Publishes the specified <see cref="LightMeasuredEvent"/>
    /// </summary>
    /// <param name="e">The <see cref="LightMeasuredEvent"/> to publish</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    [Neuroglia.AsyncApi.v2.Channel("light/measured"), PublishOperation(OperationId = "NotifyLightMeasured", Summary = "Notifies remote consumers about environmental lighting conditions for a particular streetlight."), Neuroglia.AsyncApi.v2.Tag("light", "A tag for light-related operations"), Neuroglia.AsyncApi.v2.Tag("measurement", "A tag for measurement-related operations")]
    [Neuroglia.AsyncApi.v3.Operation("sendLightMeasurement", V3OperationAction.Send, "#/channels/lightingMeasuredMQTT", Description = "Notifies remote **consumers** about environmental lighting conditions for a particular **streetlight**."), Neuroglia.AsyncApi.v3.Tag(Reference = "#/components/tags/measurement")]
    public async Task PublishLightMeasured(LightMeasuredEvent e, CancellationToken cancellationToken = default)
    {
        var message = new MqttApplicationMessage()
        {
            Topic = "onLightMeasured",
            ContentType = "application/json",
            PayloadSegment = Encoding.UTF8.GetBytes(this.Serializer.SerializeToText(e))
        };
        await this.MqttClient.PublishAsync(message, cancellationToken);
    }

    /// <summary>
    /// Handles the specified <see cref="LightMeasuredEvent"/>
    /// </summary>
    /// <param name="e">The <see cref="LightMeasuredEvent"/> to handle</param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    [Neuroglia.AsyncApi.v2.Channel("light/measured"), SubscribeOperation(OperationId = "OnLightMeasured", Summary = "Inform about environmental lighting conditions for a particular streetlight"), Neuroglia.AsyncApi.v2.Tag("light", "A tag for light-related operations"), Neuroglia.AsyncApi.v2.Tag("measurement", "A tag for measurement-related operations")]
    [Neuroglia.AsyncApi.v3.Operation("receiveLightMeasurement", V3OperationAction.Receive, "#/channels/lightingMeasuredMQTT"), Neuroglia.AsyncApi.v3.Tag(Reference = "#/components/tags/measurement")]
    protected Task OnLightMeasured(LightMeasuredEvent e)
    {
        this.Logger.LogInformation("Event received:\r\n{json}", this.Serializer.SerializeToText(e));
        return Task.CompletedTask;
    }

}
