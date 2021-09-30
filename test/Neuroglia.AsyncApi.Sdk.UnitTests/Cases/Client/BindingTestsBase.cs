using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Client;
using Neuroglia.AsyncApi.Client.Services;
using Neuroglia.AsyncApi.Models;
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

    public abstract class BindingTestsBase
        : IDisposable, IAsyncDisposable
    {

        protected const string ChannelKey = "light/measured";
        protected static string ClientId = Guid.NewGuid().ToString();

        protected BindingTestsBase(Action<IServerDefinitionBuilder> serverSetup)
        {
            this.Initialize();
            var services = new ServiceCollection();
            services = new ServiceCollection();
            services.AddAsyncApiClient("test", builder =>
                builder.For(this.BuildDocument(serverSetup))
                    .AddAmqpBinding()
                    .AddKafkaBinding()
                    .AddMqttBinding());
            this.AsyncApiClient = services.BuildServiceProvider().GetRequiredService<IAsyncApiClient>();
        }

        protected IAsyncApiClient AsyncApiClient { get; }

        protected virtual void Initialize()
        {

        }

        [Fact]
        public virtual async Task SubscribeAndPublish()
        {
            //arrange
            var channelKey = ChannelKey;
            CancellationTokenSource cancellationTokenSource = null;
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
            subscription = await this.AsyncApiClient.SubscribeToAsync(channelKey, observer);
            await this.AsyncApiClient.PublishAsync(channelKey, producedMessage);
            cancellationTokenSource = new(5000);
            while (!cancellationTokenSource.IsCancellationRequested) { }

            //assert
            consumedMessage.Should().NotBeNull();
            consumedMessage.Payload.As<JObject>().ToObject<TestUser>().Should().BeEquivalentTo(producedMessage.Payload);

            consumedMessage.Headers.First().Key.Should().Be(producedMessage.Headers.First().Key);
            consumedMessage.Headers.First().Value.As<JToken>().ToString().Should().Be(producedMessage.Headers.First().Value as string);

            consumedMessage.Headers.Last().Key.Should().Be(producedMessage.Headers.Last().Key);
            consumedMessage.Headers.Last().Value.As<JToken>().ToString().Should().Be(producedMessage.Headers.Last().Value as string);

            consumedMessage.CorrelationKey.Should().NotBeNull();
            consumedMessage.CorrelationKey.As<JToken>().ToObject<Guid>().Should().Be((Guid)producedMessage.CorrelationKey);
        }

        private bool _Disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                {
                    
                }
                this._Disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual ValueTask DisposeAsync(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                {

                }
                this._Disposed = true;
            }
            return ValueTask.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            await this.DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual AsyncApiDocument BuildDocument(Action<IServerDefinitionBuilder> serverSetup)
        {
            var services = new ServiceCollection();
            services.AddAsyncApi();
            var documentBuilder = services.BuildServiceProvider().GetRequiredService<IAsyncApiDocumentBuilder>();
            return documentBuilder
                .WithId("Fake API")
                .WithVersion("1.0.0")
                .UseServer("Fake Server", server => serverSetup(server))
                .UseChannel(ChannelKey, ConfigureChannel)
                .Build();
        }

        protected virtual void ConfigureChannel(IChannelDefinitionBuilder channel)
        {
            channel
                .DefineSubscribeOperation(ConfigureSubscribeOperation)
                .DefinePublishOperation(ConfigurePublishOperation);
        }

        protected virtual void ConfigureSubscribeOperation(IOperationDefinitionBuilder operation)
        {
            operation
                .WithOperationId("Fake Subscribe")
                .UseMessage(msg => msg
                    .OfType<TestUser>());
        }

        protected virtual void ConfigurePublishOperation(IOperationDefinitionBuilder operation)
        {
            operation
                .WithOperationId("Fake Publish")
                .UseMessage(msg => msg
                    .OfType<TestUser>());
        }

    }

}
