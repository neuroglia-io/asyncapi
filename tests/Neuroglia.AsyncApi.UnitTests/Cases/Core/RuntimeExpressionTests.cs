using Neuroglia.AsyncApi.Specification.v2;

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