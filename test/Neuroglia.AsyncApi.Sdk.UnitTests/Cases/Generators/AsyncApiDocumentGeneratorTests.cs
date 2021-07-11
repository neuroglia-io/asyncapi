using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Sdk.UnitTests.Services;
using Neuroglia.AsyncApi.Services.Generators;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Neuroglia.AsyncApi.Sdk.UnitTests.Cases.Generators
{

    public class AsyncApiDocumentGeneratorTests
    {

        public AsyncApiDocumentGeneratorTests()
        {
            ServiceCollection services = new();
            services.AddAsyncApi();
            this.Generator = services.BuildServiceProvider().GetRequiredService<IAsyncApiDocumentGenerator>();
        }

        IAsyncApiDocumentGenerator Generator { get; }

        [Fact]
        public async Task Generate_ShouldWork()
        {
            //act
            var documents = await this.Generator.GenerateAsync(typeof(TestAsyncApiService));
            var document = documents.SingleOrDefault();

            //assert
            document.Should().NotBeNull();

        }

    }

}
