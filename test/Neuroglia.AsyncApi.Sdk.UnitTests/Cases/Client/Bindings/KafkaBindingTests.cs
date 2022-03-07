using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using DotNet.Testcontainers.Containers.WaitStrategies;
using Neuroglia.AsyncApi.Models.Bindings.Kafka;
using Neuroglia.AsyncApi.Services.FluentBuilders;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Neuroglia.AsyncApi.Sdk.UnitTests.Cases.Client
{

    public class KafkaBindingTests
        : BindingTestsBase
    {

        const int ContainerPort = 9092;

        public KafkaBindingTests()
            : base(ServerSetup)
        {
  
        }

        TestcontainersContainer Container { get; set; }

        protected override void Initialize()
        {
            var containerBuilder = new TestcontainersBuilder<TestcontainersContainer>()
                .WithImage("bashj79/kafka-kraft")
                .WithName("kafka")
                .WithExposedPort(ContainerPort)
                .WithPortBinding(ContainerPort, ContainerPort)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(ContainerPort));
            this.Container = containerBuilder.Build();
            this.Container.StartAsync().GetAwaiter().GetResult();
            base.Initialize();
        }

        protected override void ConfigureSubscribeOperation(IOperationDefinitionBuilder operation)
        {
            base.ConfigureSubscribeOperation(operation);
            operation.UseBinding(new KafkaOperationBindingDefinition()
            {
                GroupId = new() { Default = JToken.FromObject(Guid.NewGuid().ToString()) }
            });
        }

        [Fact(Skip = "For some reason, Kafka tests only works if setting a break point in the channel's publish method. In other words, it seems Kafka needs to 'wait' some time before after subscribing before attempting to publish")]
        public override Task SubscribeAndPublish()
        {
            return base.SubscribeAndPublish();
        }

        protected override async ValueTask DisposeAsync(bool disposing)
        {
            if(disposing)
                await this.Container.DisposeAsync();
        }

        static void ServerSetup(IServerDefinitionBuilder server)
        {
            server.WithProtocol(AsyncApiProtocols.Kafka)
                .WithUrl(new Uri($"localhost:{ContainerPort}", UriKind.RelativeOrAbsolute))
                .UseBinding(new KafkaServerBindingDefinition());
        }

    }

}
