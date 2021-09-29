using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Neuroglia.AsyncApi.Models.Bindings.Mqtt;
using Neuroglia.AsyncApi.Services.FluentBuilders;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Neuroglia.AsyncApi.Sdk.UnitTests.Cases.Client
{

    public class MqttBindingTests
        : BindingTestsBase
    {

        public MqttBindingTests()
            : base(ServerSetup)
        {
            
        }

        IMqttServerOptions MqttServerOptions { get; set; }

        IMqttServer MqttServer { get; set; }

        protected override void Initialize()
        {
            this.MqttServerOptions = new MqttServerOptionsBuilder()
                .WithConnectionValidator(context =>
                {
                    context.ReasonCode = context.ClientId == ClientId ? MqttConnectReasonCode.Success : MqttConnectReasonCode.ClientIdentifierNotValid;
                })
                .Build();
            this.MqttServer = new MqttFactory().CreateMqttServer();
            base.Initialize();
        }

        [Fact]
        public override async Task SubscribeAndPublish()
        {
            await this.MqttServer.StartAsync(this.MqttServerOptions);
            await base.SubscribeAndPublish();
        }

        static void ServerSetup(IServerDefinitionBuilder server)
        {
            server.WithProtocol(AsyncApiProtocols.Mqtt)
                .WithUrl(new Uri("mqtt://localhost", UriKind.RelativeOrAbsolute))
                .UseBinding(new MqttServerBindingDefinition()
                {
                    ClientId = ClientId
                });
        }

    }

}
