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

namespace Neuroglia.AsyncApi.UnitTests.Cases.Core;

public class RuntimeExpressionTests
{

    [Fact]
    public void ParseRuntimeExpression()
    {
        //arrange
        var expression = "$message";
        var source = RuntimeExpressionSource.Header;
        var fragment = "/MQMD/CorrelId";
        var input = $"{expression}.{EnumHelper.Stringify(source)}#{fragment}";

        //act
        var succeeded = RuntimeExpression.TryParse(input, out var runtimeExpression);

        //assert
        succeeded.Should().BeTrue();
        runtimeExpression.Should().NotBeNull();
        runtimeExpression!.Expression.Should().Be(expression);
        runtimeExpression.Source.Should().Be(source);
        runtimeExpression.Fragment.Should().Be(fragment);
    }

}