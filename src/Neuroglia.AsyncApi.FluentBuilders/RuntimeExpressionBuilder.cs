using Neuroglia.AsyncApi.FluentBuilders;
using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IRuntimeExpressionBuilder"/> interface
/// </summary>
public class RuntimeExpressionBuilder
    : IRuntimeExpressionBuilder, IRuntimeExpressionFragmentBuilder
{

    /// <summary>
    /// Gets the <see cref="RuntimeExpression"/> to configure
    /// </summary>
    protected RuntimeExpression Expression { get; } = new();

    /// <inheritdoc/>
    public virtual IRuntimeExpressionFragmentBuilder In(RuntimeExpressionSource source)
    {
        Expression.Source = source;
        return this;
    }

    /// <inheritdoc/>
    public virtual void At(string path) => Expression.Fragment = path;

    /// <inheritdoc/>
    public virtual RuntimeExpression Build() => Expression;

}
