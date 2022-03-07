using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using DotNet.Testcontainers.Containers.WaitStrategies;
using Neuroglia.AsyncApi.Models.Bindings.Nats;
using Neuroglia.AsyncApi.Services.FluentBuilders;
using Neuroglia.AsyncApi.UnitTests.Data;
using System;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Sdk.UnitTests.Cases.Client
{
    public class RedisBindingTests
        : BindingTestsBase
    {

        const int ContainerPort = 6379;

        public RedisBindingTests()
            : base(ServerSetup)
        {

        }

        TestcontainersContainer Container { get; set; }

        protected override bool SupportsHeaders => false;

        protected override void Initialize()
        {
            var containerBuilder = new TestcontainersBuilder<TestcontainersContainer>()
                .WithImage("redis")
                .WithName("redis")
                .WithExposedPort(ContainerPort)
                .WithPortBinding(ContainerPort, ContainerPort)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(ContainerPort));
            this.Container = containerBuilder.Build();
            this.Container.StartAsync().GetAwaiter().GetResult();
            base.Initialize();
        }

        protected override void ConfigureCorrelationId(IRuntimeExpressionBuilder correlationId)
        {
            correlationId
                .In(RuntimeExpressionSource.Payload)
                .At(nameof(TestUser.Id));
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
            server.WithProtocol(AsyncApiProtocols.Redis)
                .WithUrl(new Uri($"redis://localhost:{ContainerPort}", UriKind.RelativeOrAbsolute))
                .UseBinding(new NatsServerBindingDefinition());
        }

    }

}
