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

        const int KafkaContainerPort = 9092;

        public KafkaBindingTests()
            : base(ServerSetup)
        {
            var port = 9092;
            var containerBuilder = new TestcontainersBuilder<TestcontainersContainer>()
                .WithImage("bashj79/kafka-kraft")
                .WithName("kafka")
                .WithExposedPort(KafkaContainerPort)
                .WithPortBinding(KafkaContainerPort, KafkaContainerPort)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(port));
            this.Container = containerBuilder.Build();
        }

        TestcontainersContainer Container { get; }

        protected override void ConfigureSubscribeOperation(IOperationDefinitionBuilder operation)
        {
            base.ConfigureSubscribeOperation(operation);
            operation.UseBinding(new KafkaOperationBindingDefinition()
            {
                GroupId = new() { Default = JToken.FromObject(Guid.NewGuid().ToString()) }
            });
        }

        [Fact]
        public override async Task SubscribeAndPublish()
        {
            await this.Container.StartAsync();
            await base.SubscribeAndPublish();
        }

        protected override async ValueTask DisposeAsync(bool disposing)
        {
            await base.DisposeAsync();
            if(disposing)
                await this.Container.DisposeAsync();
        }

        static void ServerSetup(IServerDefinitionBuilder server)
        {
            server.WithProtocol(AsyncApiProtocols.Kafka)
                .WithUrl(new Uri($"localhost:{KafkaContainerPort}", UriKind.RelativeOrAbsolute))
                .UseBinding(new KafkaServerBindingDefinition()
                {
                    
                });
        }

    }

}
