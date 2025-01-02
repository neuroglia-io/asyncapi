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

using FluentValidation;
using Neuroglia.AsyncApi.v3;

namespace Neuroglia.AsyncApi.UnitTests.Cases.Validation.v3;

public class V3AsyncApiDocumentValidationTests
    : IDisposable
{

    public V3AsyncApiDocumentValidationTests()
    {
        var services = new ServiceCollection();
        services.AddAsyncApi();
        this.ServiceProvider = services.BuildServiceProvider();
    }

    ServiceProvider ServiceProvider { get; }

    IValidator<V3AsyncApiDocument> Validator  => this.ServiceProvider.GetRequiredService<IValidator<V3AsyncApiDocument>>();

    [Fact]
    public void Validate_Valid_Document_Should_Work()
    {
        //arrange
        var document = AsyncApiDocumentFactory.CreateV3();

        //act
        var validationResult = Validator.Validate(document);

        //assert
        validationResult.IsValid.Should().BeTrue();
    }

    void IDisposable.Dispose()
    {
        this.ServiceProvider.Dispose();
        GC.SuppressFinalize(this);
    }

}
