namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="RuntimeExpression"/>s
/// </summary>
public interface IRuntimeExpressionBuilder
{

    /// <summary>
    /// Configures the <see cref="RuntimeExpressionSource"/> of the <see cref="RuntimeExpression"/> to build
    /// </summary>
    /// <param name="source">The <see cref="RuntimeExpressionSource"/> of the <see cref="RuntimeExpression"/> to build</param>
    /// <returns>A new <see cref="IRuntimeExpressionFragmentBuilder"/>, used to configure the <see cref="RuntimeExpression"/>'s fragment</returns>
    IRuntimeExpressionFragmentBuilder In(RuntimeExpressionSource source);

    /// <summary>
    /// Builds the configured runtime expression
    /// </summary>
    /// <returns>A new runtime expression</returns>
    RuntimeExpression Build();

}
