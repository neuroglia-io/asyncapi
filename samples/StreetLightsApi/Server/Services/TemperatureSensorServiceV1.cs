using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Neuroglia.AsyncApi;
using Neuroglia.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StreetLightsApi.Server.Services
{

    [AsyncApi("Temperature Sensor API", "1.0.0", Description = "The Temperature Sensor API allows you to get remotely notified about temperature changes captured by sensors.", LicenseName = "Apache 2.0", LicenseUrl = "https://www.apache.org/licenses/LICENSE-2.0")]
    public class TemperatureSensorServiceV1
       : BackgroundService
    {

        public TemperatureSensorServiceV1(ILogger<TemperatureSensorServiceV1> logger, IJsonSerializer serializer)
        {
            this.Logger = logger;
            this.Serializer = serializer;
        }

        protected ILogger Logger { get; }

        protected IJsonSerializer Serializer { get; }

        protected IMqttClient MqttClient { get; private set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.MqttClient = new MqttFactory().CreateMqttClient();
            MqttClientOptions options = new()
            {
                ChannelOptions = new MqttClientTcpOptions()
                {
                    Server = "test.mosquitto.org"
                }
            };
            await this.MqttClient.ConnectAsync(options, stoppingToken);
            stoppingToken.Register(async () => await this.MqttClient.DisconnectAsync());
            this.MqttClient.UseApplicationMessageReceivedHandler(async message =>
            {
                decimal degrees = await this.Serializer.DeserializeAsync<decimal>(Encoding.UTF8.GetString(message.ApplicationMessage.Payload));
                await this.OnTemperatureChanged(degrees);
                await message.AcknowledgeAsync(stoppingToken);
            });
            await this.MqttClient.SubscribeAsync("OnTemperatureChanged");
        }

        [Tag("temperature", "A tag for temeprature-related operations"), Tag("sensor", "A tag for sensor-related operations")]
        [Channel("temperature/changed"), SubscribeOperation(OperationId = "OnTemperatureChanged", Summary = "Inform about temperature changes captured by sensors")]
        protected async Task OnTemperatureChanged([Range(-100, 100)]decimal degrees)
        {
            this.Logger.LogInformation($"Temperature is {degrees}° Celsius");
            await Task.CompletedTask;
        }

    }

}
