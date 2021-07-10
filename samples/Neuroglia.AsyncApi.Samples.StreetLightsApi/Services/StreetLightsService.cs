using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Neuroglia.AsyncApi.Samples.StreetLightsApi.Messages;
using Neuroglia.Serialization;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Samples.StreetLightsApi.Services
{

    [AsyncApi("Streetlights API", "1.0.0", Description = "The Smartylighting Streetlights API allows you to remotely manage the city lights.", LicenseName = "Apache 2.0", LicenseUrl = "https://www.apache.org/licenses/LICENSE-2.0")]
    public class StreetLightsService
        : BackgroundService
    {

        public StreetLightsService(ILogger<StreetLightsService> logger, IJsonSerializer serializer)
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
                LightMeasuredEvent e = await this.Serializer.DeserializeAsync<LightMeasuredEvent>(Encoding.UTF8.GetString(message.ApplicationMessage.Payload));
                await this.OnLightMeasured(e);
                await message.AcknowledgeAsync(stoppingToken);
            });
            await this.MqttClient.SubscribeAsync("onLightMeasured");
            await this.PublishLightMeasured(new() { Id = 415, Lumens = 5, SentAt = DateTime.UtcNow });
        }

        [Channel("light/measured"), PublishOperation(OperationId = "onLightMeasured", Summary = "Inform about environmental lighting conditions for a particular streetlight")]
        public async Task PublishLightMeasured(LightMeasuredEvent e)
        {
            MqttApplicationMessage message = new()
            {
                Topic = "onLightMeasured",
                ContentType = "application/json",
                Payload = Encoding.UTF8.GetBytes(await this.Serializer.SerializeAsync(e))
            };
            await this.MqttClient.PublishAsync(message);
        }

        [Channel("light/measured"), SubscribeOperation(OperationId = "lightMeasuredEvent", Summary = "Inform about environmental lighting conditions for a particular streetlight")]
        protected async Task OnLightMeasured(LightMeasuredEvent e)
        {
            this.Logger.LogInformation($"Event received:{Environment.NewLine}{await this.Serializer.SerializeAsync(e)}");
        }

    }

}
