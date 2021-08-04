using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Neuroglia.AsyncApi;
using Neuroglia.Serialization;
using StreetLightsApi.Server.Messages;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StreetLightsApi.Server.Services
{

    [AsyncApi("Movement Sensor API", "1.0.0", Description = "The Movement Sensor API allows you to get remotely notified about movements captured by sensors.", LicenseName = "Apache 2.0", LicenseUrl = "https://www.apache.org/licenses/LICENSE-2.0")]
    public class MovementSensorService
        : BackgroundService
    {

        public MovementSensorService(ILogger<MovementSensorService> logger, IJsonSerializer serializer)
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
                MovementDetectedEvent e = await this.Serializer.DeserializeAsync<MovementDetectedEvent>(Encoding.UTF8.GetString(message.ApplicationMessage.Payload));
                await this.OnMovementDetected(e);
                await message.AcknowledgeAsync(stoppingToken);
            });
            await this.MqttClient.SubscribeAsync("OnMovementDetected");
        }

        [Tag("movement", "A tag for movement-related operations"), Tag("sensor", "A tag for sensor-related operations")]
        [Channel("movement/detected"), SubscribeOperation(OperationId = "OnMovementDetected", Summary = "Inform about movement captured by sensors")]
        protected async Task OnMovementDetected(MovementDetectedEvent e)
        {
            this.Logger.LogInformation($"Movement detected by sensor with id '{e.SensorId}' at {e.SentAt}");
            await Task.CompletedTask;
        }

    }

}
