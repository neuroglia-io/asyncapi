using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Sdk.Models;
using Neuroglia.AsyncApi.Sdk.Models.Bindings.Http;
using Neuroglia.AsyncApi.Sdk.Services.FluentBuilders;
using Neuroglia.AsyncApi.Sdk.UnitTests.Data;
using System;

namespace Neuroglia.AsyncApi.Sdk.UnitTests.Factories
{

    internal static class AsyncApiDocumentFactory
    {

        static AsyncApiDocumentFactory()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAsyncApi();
            ServiceProvider = services.BuildServiceProvider();
        }

        static IServiceProvider ServiceProvider { get; }

        static IAsyncApiDocumentBuilder Builder => ServiceProvider.GetRequiredService<IAsyncApiDocumentBuilder>();

        internal static AsyncApiDocument Create()
        {
            return Builder
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
                        .WithId("fake operation")
                        .WithDescription("fake operation description")
                        .UseMessage(message => message
                            .OfType<TestMessage>()
                            .WithName("fake pub operation name")
                            .WithTitle("fake pub operation title")
                            .WithHeaders<TestMessageHeaders>()
                            .UseBinding(new HttpMessageBinding()))))
                .Build();
        }

    }

}
