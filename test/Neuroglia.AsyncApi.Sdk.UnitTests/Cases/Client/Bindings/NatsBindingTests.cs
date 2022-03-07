using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using DotNet.Testcontainers.Containers.WaitStrategies;
using Neuroglia.AsyncApi.Models.Bindings.Nats;
using Neuroglia.AsyncApi.Services.FluentBuilders;
using System;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Sdk.UnitTests.Cases.Client
{

    public class NatsBindingTests
        : BindingTestsBase
    {

        const int ContainerPort = 4222;

        public NatsBindingTests()
            : base(ServerSetup)
        {

        }

        TestcontainersContainer Container { get; set; }

        protected override void Initialize()
        {
            var containerBuilder = new TestcontainersBuilder<TestcontainersContainer>()
                .WithImage("bitnami/nats")
                .WithName("nats")
                .WithExposedPort(ContainerPort)
                .WithPortBinding(ContainerPort, ContainerPort)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(ContainerPort));
            this.Container = containerBuilder.Build();
            this.Container.StartAsync().GetAwaiter().GetResult();
            base.Initialize();
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
            server.WithProtocol(AsyncApiProtocols.Nats)
                .WithUrl(new Uri($"nats://localhost:{ContainerPort}", UriKind.RelativeOrAbsolute))
                .UseBinding(new NatsServerBindingDefinition());
        }

    }

}
