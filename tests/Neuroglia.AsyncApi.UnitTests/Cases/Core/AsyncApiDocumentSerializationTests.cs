using Neuroglia.AsyncApi.Specification.v2;
using Neuroglia.Serialization.Json;
using Neuroglia.Serialization.Yaml;

namespace Neuroglia.AsyncApi.UnitTests.Cases.Core;

public class AsyncApiDocumentSerializationTests
{

    [Fact]
    public void SerializeAndDeserialize_AsyncApiDocument_Should_Work()
    {
        //arrange
        var document = AsyncApiDocumentFactory.Create();

        //act
        var json = JsonSerializer.Default.SerializeToText(document);
        var jsonDeserialized = JsonSerializer.Default.Deserialize<AsyncApiDocument>(json);

        var yaml = YamlSerializer.Default.SerializeToText(document);
        var yamlDeserialized = YamlSerializer.Default.Deserialize<AsyncApiDocument>(yaml);

        //assert
        jsonDeserialized.Should().Be(document);
        yamlDeserialized.Should().Be(document);
    }

}