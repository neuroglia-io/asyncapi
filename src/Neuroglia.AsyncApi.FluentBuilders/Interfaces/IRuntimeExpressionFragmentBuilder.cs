namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="RuntimeExpression"/> fragments
/// </summary>
public interface IRuntimeExpressionFragmentBuilder
{

    /// <summary>
    /// Configures the source's path referenced by the <see cref="RuntimeExpression"/>'s fragment
    /// </summary>
    /// <param name="path">The source's path referenced by the <see cref="RuntimeExpression"/>'s fragment</param>
    void At(string path);

}
