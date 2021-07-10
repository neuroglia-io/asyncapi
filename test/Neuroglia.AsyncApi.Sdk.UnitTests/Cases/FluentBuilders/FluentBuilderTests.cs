using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Models.Bindings.Http;
using Neuroglia.AsyncApi.Services.FluentBuilders;
using Neuroglia.AsyncApi.UnitTests.Data;
using System;
using Xunit;

namespace Neuroglia.AsyncApi.UnitTests.Cases.FluentBuilders
{

    public class FluentBuilderTests
    {

        public FluentBuilderTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAsyncApi();
            this.Services = services.BuildServiceProvider();
        }

        IServiceProvider Services { get; }

        IAsyncApiDocumentBuilder Builder => this.Services.GetRequiredService<IAsyncApiDocumentBuilder>();

        [Fact]
        public void Build()
        {
            //act
            var document = this.Builder
                .WithId("fake-id")
                .WithTitle("Fake Title")
                .WithVersion("0.1.0")
                .WithDescription("Fake Server Description")
                .WithContact("info", new Uri("https://fake.url"), "fake@email.com")
                .UseServer("fake server", server => server
                    .WithUrl(new Uri("https://fake.server.com"))
                    .WithProtocol(AsyncApiProtocols.Http, "2.0")
                    .WithDescription("Fake Description")
                    .UseBinding(new HttpServerBinding()))
                .UseChannel("fake channel", channel => channel
                    .WithDescription("Fake Channel Description")
                    .UseBinding(new HttpChannelBinding())
                    .DefineSubscribeOperation(operation => operation
                        .WithOperationId("fake operation")
                        .WithDescription("fake operation description")
                        .UseMessage(message => message
                            .OfType<TestMessage>()
                            .WithName("fake pub operation name")
                            .WithTitle("fake pub operation title")
                            .WithHeaders<TestMessageHeaders>()
                            .UseBinding(new HttpMessageBinding()))))
                .Build();

            //assert
            document.Should().NotBeNull();
        }

    }

}
