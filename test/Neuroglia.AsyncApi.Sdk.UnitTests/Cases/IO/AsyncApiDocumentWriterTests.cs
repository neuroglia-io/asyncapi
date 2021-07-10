using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Sdk.Services.IO;
using Neuroglia.AsyncApi.Sdk.UnitTests.Factories;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Neuroglia.AsyncApi.Sdk.UnitTests.Cases.IO
{
    public class AsyncApiDocumentWriterTests
    {

        public AsyncApiDocumentWriterTests()
        {
            ServiceCollection services = new();
            services.AddAsyncApi();
            this.Writer = services.BuildServiceProvider().GetRequiredService<IAsyncApiDocumentWriter>();
        }

        IAsyncApiDocumentWriter Writer { get; }

        [Fact]
        public async Task Write_Json_ShouldWork()
        {
            //arrange
            var document = AsyncApiDocumentFactory.Create();
            using var stream = new MemoryStream();

            //act
            await this.Writer.WriteAsync(document, stream, AsyncApiDocumentFormat.Json);
            var json = Encoding.UTF8.GetString(stream.ToArray());

            //assert
            document.Should().NotBeNull();
        }

        [Fact]
        public async Task Write_Yaml_ShouldWork()
        {
            //arrange
            var document = AsyncApiDocumentFactory.Create();
            using var stream = new MemoryStream();

            //act
            await this.Writer.WriteAsync(document, stream, AsyncApiDocumentFormat.Yaml);
            var yaml = Encoding.UTF8.GetString(stream.ToArray());

            //assert
            document.Should().NotBeNull();
        }

    }

}
