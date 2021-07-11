using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Services.IO;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Neuroglia.AsyncApi.UnitTests.Cases.IO
{

    public class AsyncApiDocumentReaderTests
    {

        public AsyncApiDocumentReaderTests()
        {
            ServiceCollection services = new();
            services.AddAsyncApi();
            this.Reader = services.BuildServiceProvider().GetRequiredService<IAsyncApiDocumentReader>();
        }

        IAsyncApiDocumentReader Reader { get; }

        [Fact]
        public async Task Read_Json_ShouldWork()
        {
            //act
            var document = await this.Reader.ReadAsync(File.OpenRead(Path.Combine("Resources", "streetlights.json")));

            //assert
            document.Should().NotBeNull();
        }

        [Fact]
        public async Task Read_Yaml_ShouldWork()
        {
            //act
            var document = await this.Reader.ReadAsync(File.OpenRead(Path.Combine("Resources", "streetlights.yaml")));

            //assert
            document.Should().NotBeNull();
        }

    }

}
