// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Neuroglia.AsyncApi.v2;
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