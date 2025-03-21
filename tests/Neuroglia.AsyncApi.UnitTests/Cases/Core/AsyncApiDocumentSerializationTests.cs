﻿// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
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

using Neuroglia.AsyncApi.v3;

namespace Neuroglia.AsyncApi.UnitTests.Cases.Core;

public class AsyncApiDocumentSerializationTests
{

    [Fact]
    public void SerializeAndDeserialize_AsyncApiDocument_v2_Should_Work()
    {
        //arrange
        var document = AsyncApiDocumentFactory.CreateV2();

        //act
        var json = JsonSerializer.Default.SerializeToText(document);
        var jsonDeserialized = JsonSerializer.Default.Deserialize<V2AsyncApiDocument>(json);

        var yaml = YamlSerializer.Default.SerializeToText(document);
        var yamlDeserialized = YamlSerializer.Default.Deserialize<V2AsyncApiDocument>(yaml);

        //assert
        jsonDeserialized.Should().BeEquivalentTo(document);
        yamlDeserialized.Should().BeEquivalentTo(document);
    }

    [Fact]
    public void SerializeAndDeserialize_AsyncApiDocument_v3_Should_Work()
    {
        //arrange
        var document = AsyncApiDocumentFactory.CreateV3();

        //act
        var json = JsonSerializer.Default.SerializeToText(document);
        var jsonDeserialized = JsonSerializer.Default.Deserialize<V3AsyncApiDocument>(json);

        var yaml = YamlSerializer.Default.SerializeToText(document);
        var yamlDeserialized = YamlSerializer.Default.Deserialize<V3AsyncApiDocument>(yaml);

        //assert
        jsonDeserialized.Should().BeEquivalentTo(document);
        yamlDeserialized.Should().BeEquivalentTo(document);
    }

}