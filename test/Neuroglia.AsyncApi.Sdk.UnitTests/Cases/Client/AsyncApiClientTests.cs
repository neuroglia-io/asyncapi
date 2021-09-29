using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Neuroglia.AsyncApi.Client;
using Neuroglia.AsyncApi.Client.Services;
using Neuroglia.AsyncApi.Models.Bindings.Mqtt;
using Neuroglia.AsyncApi.Services.FluentBuilders;
using Neuroglia.AsyncApi.UnitTests.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Neuroglia.AsyncApi.Sdk.UnitTests.Cases.Client
{

    public class AsyncApiClientTests
    {

        const string ChannelKey = "light/measured";
        static string ClientId = Guid.NewGuid().ToString();

        public AsyncApiClientTests()
        {
            var services = new ServiceCollection();
            services.AddAsyncApi();
            var documentBuilder = services.BuildServiceProvider().GetRequiredService<IAsyncApiDocumentBuilder>();
            var document = documentBuilder
                .WithId("Fake API")
                .WithVersion("1.0.0")
                .UseServer("Fake Server", server => server
                    .WithProtocol(AsyncApiProtocols.Mqtt)
                    .WithUrl(new Uri("mqtt://localhost", UriKind.RelativeOrAbsolute))
                    .UseBinding(new MqttServerBindingDefinition() { ClientId = ClientId }))
                .UseChannel(ChannelKey, channel => channel
                    .DefineSubscribeOperation(operation => operation
                        .WithOperationId("Fake Subscribe")
                        .UseMessage(msg => msg
                            .OfType<TestUser>()))
                    .DefinePublishOperation(operation => operation
                        .WithOperationId("Fake Subscribe")
                        .UseMessage(msg => msg
                            .OfType<TestUser>())))
                .Build();
            services = new ServiceCollection();
            services.AddAsyncApiClient("test", builder => 
                builder.For(document)
                    .AddKafkaBinding()
                    .AddMqttBinding());
            this.AsyncApiClient = services.BuildServiceProvider().GetRequiredService<IAsyncApiClient>();
            this.MqttServerOptions = new MqttServerOptionsBuilder()
                .WithConnectionValidator(context =>
                {
                    context.ReasonCode = context.ClientId == ClientId ? MqttConnectReasonCode.Success : MqttConnectReasonCode.ClientIdentifierNotValid;
                })
                .Build();
            this.MqttServer = new MqttFactory().CreateMqttServer();
        }

        IMqttServerOptions MqttServerOptions { get; }

        IMqttServer MqttServer { get; }

        IAsyncApiClient AsyncApiClient { get; }

        [Fact]
        public async Task SubscribeAndPublish()
        {
            //arrange
            var channelKey = ChannelKey;
            var cancellationTokenSource = new CancellationTokenSource(5000);
            IMessage consumedMessage = null;
            IDisposable subscription = null;
            var observer = Observer.Create<IMessage>(message =>
            {
                consumedMessage = message;
                cancellationTokenSource.Cancel();
            });
            var producedMessage = new MessageBuilder()
                .WithPayload(new TestUser() { FirstName = "Fake First Name", LastName = "Fake Last Name" })
                .WithHeader("Fake Header 1", "Fake Value 1")
                .WithHeader("Fake Header 2", "Fake Value 2")
                .WithCorrelationKey(Guid.NewGuid())
                .Build();

            //act
            await this.MqttServer.StartAsync(this.MqttServerOptions);
            subscription = await this.AsyncApiClient.SubscribeToAsync(channelKey, observer);
            await this.AsyncApiClient.PublishAsync(channelKey, producedMessage);
            while (!cancellationTokenSource.IsCancellationRequested) { }

            //assert
            consumedMessage.Should().NotBeNull();
            consumedMessage.Payload.As<JObject>().ToObject<TestUser>().Should().BeEquivalentTo(producedMessage.Payload);
            consumedMessage.Headers.First().Key.Should().Be(producedMessage.Headers.First().Key);
            consumedMessage.Headers.First().Value.As<JToken>().ToString().Should().Be(producedMessage.Headers.First().Value as string);
            consumedMessage.Headers.Last().Key.Should().Be(producedMessage.Headers.Last().Key);
            consumedMessage.Headers.Last().Value.As<JToken>().ToString().Should().Be(producedMessage.Headers.Last().Value as string);
            consumedMessage.CorrelationKey.As<JToken>().ToObject<Guid>().Should().Be((Guid)producedMessage.CorrelationKey);
        }

    }

}
