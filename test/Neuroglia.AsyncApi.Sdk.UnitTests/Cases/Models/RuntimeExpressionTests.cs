using FluentAssertions;
using Neuroglia.AsyncApi.Models;
using Xunit;

namespace Neuroglia.AsyncApi.Sdk.UnitTests.Cases.Models
{

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
            var succeeded = RuntimeExpression.TryParse(input, out RuntimeExpression runtimeExpression);

            //assert
            succeeded.Should().BeTrue();
            runtimeExpression.Should().NotBeNull();
            runtimeExpression.Expression.Should().Be(expression);
            runtimeExpression.Source.Should().Be(source);
            runtimeExpression.Fragment.Should().Be(fragment);
        }

    }

}
