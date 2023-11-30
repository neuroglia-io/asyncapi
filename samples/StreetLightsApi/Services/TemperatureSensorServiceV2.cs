using MQTTnet;
using MQTTnet.Client;
using Neuroglia.AsyncApi;
using Neuroglia.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StreetLightsApi.Server.Services;

[AsyncApi("Temperature Sensor API", "2.0.0", Description = "The Temperature Sensor API allows you to get remotely notified about temperature changes captured by sensors.", LicenseName = "Apache 2.0", LicenseUrl = "https://www.apache.org/licenses/LICENSE-2.0")]
public class TemperatureSensorServiceV2(ILogger<TemperatureSensorServiceV1> logger, IJsonSerializer serializer)
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
            await this.OnTemperatureChanged(degrees, DateTime.Now);
            await message.AcknowledgeAsync(stoppingToken);
        });
        await this.MqttClient.SubscribeAsync("OnTemperatureChanged", cancellationToken: stoppingToken).ConfigureAwait(false);
    }

    [Tag("temperature", "A tag for temeprature-related operations"), Tag("sensor", "A tag for sensor-related operations")]
    [Channel("temperature/changed"), SubscribeOperation(OperationId = "OnTemperatureChanged", Summary = "Inform about temperature changes captured by sensors")]
    protected async Task OnTemperatureChanged([Range(-100,100)]decimal degrees, DateTime timestamp)
    {
        this.Logger.LogInformation("{timestamp}: {degrees}°", timestamp, degrees);
        await Task.CompletedTask;
    }

}
