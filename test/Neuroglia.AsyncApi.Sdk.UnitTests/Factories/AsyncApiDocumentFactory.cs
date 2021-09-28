using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Models.Bindings.Http;
using Neuroglia.AsyncApi.Services.FluentBuilders;
using Neuroglia.AsyncApi.UnitTests.Data;
using System;

namespace Neuroglia.AsyncApi.UnitTests.Factories
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
                    .UseBinding(new HttpServerBindingDefinition()))
                .UseChannel("fake channel", channel => channel
                    .WithDescription("Fake Channel Description")
                    .UseBinding(new HttpChannelBindingDefinition())
                    .DefineSubscribeOperation(operation => operation
                        .WithOperationId("fake operation")
                        .WithDescription("fake operation description")
                        .UseMessage(message => message
                            .OfType<TestMessage>()
                            .WithName("fake pub operation name")
                            .WithTitle("fake pub operation title")
                            .WithHeaders<TestMessageHeaders>()
                            .UseBinding(new HttpMessageBindingDefinition()))))
                .Build();
        }

    }

}
