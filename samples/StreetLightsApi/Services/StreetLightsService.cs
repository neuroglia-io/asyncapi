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

using StreetLightsApi.Server.Messages;
using Neuroglia.Serialization;
using System.Text;
using Neuroglia.AsyncApi;
using MQTTnet;
using MQTTnet.Client;

namespace StreetLightsApi.Server.Services;

[AsyncApi("Streetlights API", "1.0.0", Description = "The Smartylighting Streetlights API allows you to remotely manage the city lights.", LicenseName = "Apache 2.0", LicenseUrl = "https://www.apache.org/licenses/LICENSE-2.0")]
public class StreetLightsService(ILogger<StreetLightsService> logger, IJsonSerializer serializer)
    : BackgroundService
{
    protected ILogger Logger { get; } = logger;

    protected IJsonSerializer Serializer { get; } = serializer;

    protected IMqttClient MqttClient { get; private set; } = null!;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this.MqttClient = new MqttFactory().CreateMqttClient();
        var options = new MqttClientOptions() { ChannelOptions = new MqttClientTcpOptions() { Server = "test.mosquitto.org" } };
        await this.MqttClient.ConnectAsync(options, stoppingToken).ConfigureAwait(false);
        stoppingToken.Register(async () => await this.MqttClient.DisconnectAsync().ConfigureAwait(false));
        this.MqttClient.ApplicationMessageReceivedAsync += (async message =>
        {
            var e = this.Serializer.Deserialize<LightMeasuredEvent>(Encoding.UTF8.GetString(message.ApplicationMessage.PayloadSegment))!;
            await this.OnLightMeasured(e);
            await message.AcknowledgeAsync(stoppingToken);
        });
        await this.MqttClient.SubscribeAsync("onLightMeasured", cancellationToken: stoppingToken).ConfigureAwait(false);
        await this.PublishLightMeasured(new() { Id = Guid.NewGuid(), Lumens = 5, SentAt = DateTime.UtcNow }).ConfigureAwait(false);
    }

    [Tag("light", "A tag for light-related operations"), Tag("measurement", "A tag for measurement-related operations")]
    [Channel("light/measured"), PublishOperation(OperationId = "NotifyLightMeasured", Summary = "Notifies remote consumers about environmental lighting conditions for a particular streetlight")]
    public async Task PublishLightMeasured(LightMeasuredEvent e)
    {
        var message = new MqttApplicationMessage()
        {
            Topic = "onLightMeasured",
            ContentType = "application/json",
            PayloadSegment = Encoding.UTF8.GetBytes(this.Serializer.SerializeToText(e))
        };
        await this.MqttClient.PublishAsync(message);
    }

    [Tag("light", "A tag for light-related operations"), Tag("measurement", "A tag for measurement-related operations")]
    [Channel("light/measured"), SubscribeOperation(OperationId = "OnLightMeasured", Summary = "Inform about environmental lighting conditions for a particular streetlight")]
    protected Task OnLightMeasured(LightMeasuredEvent e)
    {
        this.Logger.LogInformation("Event received:\r\n{json}", this.Serializer.SerializeToText(e));
        return Task.CompletedTask;
    }

}
