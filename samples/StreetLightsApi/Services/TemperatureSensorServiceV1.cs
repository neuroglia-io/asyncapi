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

using Neuroglia.AsyncApi.v2;

namespace StreetLightsApi.Server.Services;

[AsyncApiV2("Temperature Sensor API", "1.0.0", Description = "The Temperature Sensor API allows you to get remotely notified about temperature changes captured by sensors.", LicenseName = "Apache 2.0", LicenseUrl = "https://www.apache.org/licenses/LICENSE-2.0")]
public class TemperatureSensorServiceV1(ILogger<TemperatureSensorServiceV1> logger, IJsonSerializer serializer)
      : BackgroundService
{

    protected ILogger Logger { get; } = logger;

    protected IJsonSerializer Serializer { get; } = serializer;

    protected IMqttClient MqttClient { get; private set; } = null!;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this.MqttClient = new MqttFactory().CreateMqttClient();
        var options = new MqttClientOptions() { ChannelOptions = new MqttClientTcpOptions() { Server = "test.mosquitto.org" } };
        await this.MqttClient.ConnectAsync(options, stoppingToken);
        stoppingToken.Register(async () => await this.MqttClient.DisconnectAsync());
        this.MqttClient.ApplicationMessageReceivedAsync += (async message =>
        {
            var degrees = this.Serializer.Deserialize<decimal>(Encoding.UTF8.GetString(message.ApplicationMessage.PayloadSegment));
            await this.OnTemperatureChanged(degrees);
            await message.AcknowledgeAsync(stoppingToken);
        });
        await this.MqttClient.SubscribeAsync("OnTemperatureChanged", cancellationToken: stoppingToken).ConfigureAwait(false);
    }

    [TagV2("temperature", "A tag for temperature-related operations"), TagV2("sensor", "A tag for sensor-related operations")]
    [ChannelV2("temperature/changed"), SubscribeOperationV2(OperationId = "OnTemperatureChanged", Summary = "Inform about temperature changes captured by sensors")]
    protected Task OnTemperatureChanged([Range(-100, 100)]decimal degrees)
    {
        this.Logger.LogInformation("Temperature is {degrees}° Celsius", degrees);
        return Task.CompletedTask;
    }

}
