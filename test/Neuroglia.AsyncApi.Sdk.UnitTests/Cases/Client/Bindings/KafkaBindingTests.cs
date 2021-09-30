﻿using DotNet.Testcontainers.Containers.Builders;
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
                .WithUrl(new Uri($"kafka://localhost:{ContainerPort}", UriKind.RelativeOrAbsolute))
                .UseBinding(new KafkaServerBindingDefinition());
        }

    }

}