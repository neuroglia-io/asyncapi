using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using DotNet.Testcontainers.Containers.WaitStrategies;
using Neuroglia.AsyncApi.Models.Bindings.Amqp;
using Neuroglia.AsyncApi.Models.Bindings.Kafka;
using Neuroglia.AsyncApi.Services.FluentBuilders;
using System;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Sdk.UnitTests.Cases.Client
{

    public class AmqpBindingTests
        : BindingTestsBase
    {

        const int ContainerPort = 5672;

        public AmqpBindingTests()
            : base(ServerSetup)
        {

        }

        TestcontainersContainer Container { get; set; }

        protected override void Initialize()
        {
            var containerBuilder = new TestcontainersBuilder<TestcontainersContainer>()
                .WithImage("rabbitmq:3")
                .WithName("rabbitmq")
                .WithExposedPort(ContainerPort)
                .WithPortBinding(ContainerPort, ContainerPort)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(ContainerPort));
            this.Container = containerBuilder.Build();
            this.Container.StartAsync().GetAwaiter().GetResult();
            base.Initialize();
        }

        protected override void ConfigureChannel(IChannelDefinitionBuilder channel)
        {
            base.ConfigureChannel(channel);
            channel.UseBinding(new AmqpChannelBindingDefinition()
            {
                Queue = new() { Name = ChannelKey },
                //Exchange = new() { Name = "test" }
            });
        }

        protected override async ValueTask DisposeAsync(bool disposing)
        {
            if (!disposing)
                return;
            await this.Container.StopAsync();
            await this.Container.DisposeAsync();
        }

        static void ServerSetup(IServerDefinitionBuilder server)
        {
            server.WithProtocol(AsyncApiProtocols.Amqp)
                .WithUrl(new Uri($"amqp://localhost:{ContainerPort}", UriKind.RelativeOrAbsolute))
                .UseBinding(new KafkaServerBindingDefinition());
        }

    }

}
