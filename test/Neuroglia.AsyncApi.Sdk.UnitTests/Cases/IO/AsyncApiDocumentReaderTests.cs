using Neuroglia.AsyncApi.Sdk.Models;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Neuroglia.AsyncApi.Sdk.UnitTests.Cases.IO
{

    public class AsyncApiDocumentReaderTests
    {

        [Fact]
        public async Task Read_Json_ShouldWork()
        {
            var document = JsonConvert.DeserializeObject<AsyncApiDocument>(await File.ReadAllTextAsync(Path.Combine("Resources", "streetlights.json")));
        }

    }

}
