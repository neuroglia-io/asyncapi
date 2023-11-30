namespace Neuroglia.AsyncApi.Specification.v2;

/// <summary>
/// Represents a parsed <see href="https://www.asyncapi.com/docs/specifications/v2.1.0#runtimeExpression">Async API runtime expression</see>
/// </summary>
public class RuntimeExpression
{

    /// <summary>
    /// Gets the default expression component for <see cref="RuntimeExpression"/>s
    /// </summary>
    public const string DefaultExpression = "$message";

    /// <summary>
    /// Initializes a new <see cref="RuntimeExpression"/>
    /// </summary>
    public RuntimeExpression()
    {
        Expression = DefaultExpression;
        Source = RuntimeExpressionSource.Header;
    }

    /// <summary>
    /// Initializes a new <see cref="RuntimeExpression"/>
    /// </summary>
    /// <param name="expression">The <see cref="RuntimeExpression"/>'s expression component (ex: 'message')</param>
    /// <param name="source">The <see cref="RuntimeExpression"/>'s source component (ex: 'header')</param>
    /// <param name="fragment">The <see cref="RuntimeExpression"/>'s fragment component (ex: '#/property')</param>
    public RuntimeExpression(string expression, RuntimeExpressionSource source, string fragment)
    {
        if (string.IsNullOrWhiteSpace(expression)) throw new ArgumentNullException(nameof(expression));
        if (string.IsNullOrWhiteSpace(fragment)) throw new ArgumentNullException(nameof(fragment));
        Expression = expression;
        Source = source;
        Fragment = fragment;
    }

    /// <summary>
    /// Gets/sets the <see cref="RuntimeExpression"/>'s expression component (ex: 'message')
    /// </summary>
    public virtual string Expression { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="RuntimeExpression"/>'s source component (ex: 'header')
    /// </summary>
    public virtual RuntimeExpressionSource Source { get; set; }

    /// <summary>
    /// Gets/sets the <see cref="RuntimeExpression"/>'s fragment component (ex: '#/property')
    /// </summary>
    public virtual string Fragment { get; set; } = null!;

    /// <inheritdoc/>
    public override string ToString() => $"{Expression}.{EnumHelper.Stringify(Source)}#{Fragment}";

    /// <summary>
    /// Parses the specified input
    /// </summary>
    /// <param name="input">The input to parse</param>
    /// <returns>A new <see cref="RuntimeExpression"/></returns>
    public static RuntimeExpression Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));
        if (!input.Trim().StartsWith(DefaultExpression)) throw new ArgumentException($"A valid Async API runtime expression should start with '{DefaultExpression}'", nameof(input));
        var substring = input.Trim()[(DefaultExpression.Length + 1)..];
        var source = substring.Split('#', StringSplitOptions.RemoveEmptyEntries)[0];
        var fragment = substring[(source.Length + 1)..];
        return new(DefaultExpression, EnumHelper.Parse<RuntimeExpressionSource>(source), fragment);
    }

    /// <summary>
    /// Attempts to parse the specifed input
    /// </summary>
    /// <param name="input">The input to parse</param>
    /// <param name="expression">The parsed <see cref="RuntimeExpression"/></param>
    /// <returns>A boolean indicating whether or not the specified input could be parsed</returns>
    public static bool TryParse(string input, out RuntimeExpression? expression)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));
        expression = null;
        try
        {
            expression = Parse(input);
            return true;
        }
        catch
        {
            return false;
        }
    }

}