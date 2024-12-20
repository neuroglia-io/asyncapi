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

using Neuroglia.AsyncApi.IO;

namespace Neuroglia.AsyncApi.UnitTests.Cases.IO;

public class IOTests
{

    public IOTests()
    {
        var services = new ServiceCollection();
        services.AddAsyncApiIO();
        this.ServiceProvider = services.BuildServiceProvider();
    }

    protected ServiceProvider ServiceProvider { get; }

    protected IAsyncApiDocumentWriter DocumentWriter => this.ServiceProvider.GetRequiredService<IAsyncApiDocumentWriter>();

    protected IAsyncApiDocumentReader DocumentReader => this.ServiceProvider.GetRequiredService<IAsyncApiDocumentReader>();

    [Fact]
    public async Task Write_Then_Read_Json_Document_V2_Should_Work()
    {
        //arrange
        var documentToWrite = AsyncApiDocumentFactory.CreateV2();
        using var stream = new MemoryStream();

        //act
        await this.DocumentWriter.WriteAsync(documentToWrite, stream, AsyncApiDocumentFormat.Json);
        await stream.FlushAsync();
        stream.Position = 0;
        var readDocument = await this.DocumentReader.ReadAsync(stream);

        //assert
        readDocument.Should().BeEquivalentTo(documentToWrite);
    }

    [Fact]
    public async Task Write_Then_Read_Yaml_Document_V2_Should_Work()
    {
        //arrange
        var documentToWrite = AsyncApiDocumentFactory.CreateV2();
        using var stream = new MemoryStream();
        
        //act
        await this.DocumentWriter.WriteAsync(documentToWrite, stream, AsyncApiDocumentFormat.Yaml);
        await stream.FlushAsync();
        stream.Position = 0;
        var readDocument = await this.DocumentReader.ReadAsync(stream);

        //assert
        readDocument.Should().BeEquivalentTo(documentToWrite);
    }

    [Fact]
    public async Task Write_Then_Read_Json_Document_V3_Should_Work()
    {
        //arrange
        var documentToWrite = AsyncApiDocumentFactory.CreateV3();
        using var stream = new MemoryStream();

        //act
        await this.DocumentWriter.WriteAsync(documentToWrite, stream, AsyncApiDocumentFormat.Json);
        await stream.FlushAsync();
        stream.Position = 0;
        var readDocument = await this.DocumentReader.ReadAsync(stream);

        //assert
        readDocument.Should().BeEquivalentTo(documentToWrite);
    }

    [Fact]
    public async Task Write_Then_Read_Yaml_Document_V3_Should_Work()
    {
        //arrange
        var documentToWrite = AsyncApiDocumentFactory.CreateV3();
        using var stream = new MemoryStream();

        //act
        await this.DocumentWriter.WriteAsync(documentToWrite, stream, AsyncApiDocumentFormat.Yaml);
        await stream.FlushAsync();
        stream.Position = 0;
        var readDocument = await this.DocumentReader.ReadAsync(stream);

        //assert
        readDocument.Should().BeEquivalentTo(documentToWrite);
    }

}
