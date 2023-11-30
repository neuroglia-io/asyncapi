namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Represents the default implementation of the <see cref="IAsyncApiGenerationOptionsBuilder"/> interface
/// </summary>
/// <remarks>
/// Initializes a new <see cref="AsyncApiGenerationOptionsBuilder"/>
/// </remarks>
/// <param name="options">The <see cref="AsyncApiGenerationOptions"/> to configure</param>
public class AsyncApiGenerationOptionsBuilder(AsyncApiGenerationOptions options)
        : IAsyncApiGenerationOptionsBuilder
{

    /// <summary>
    /// Initializes a new <see cref="AsyncApiGenerationOptionsBuilder"/>
    /// </summary>
    public AsyncApiGenerationOptionsBuilder() : this(new()) { }

    /// <summary>
    /// Gets the <see cref="AsyncApiGenerationOptions"/> to configure
    /// </summary>
    protected AsyncApiGenerationOptions Options { get; } = options;

    /// <inheritdoc/>
    public virtual IAsyncApiGenerationOptionsBuilder WithMarkupType(Type markupType)
    {
        ArgumentNullException.ThrowIfNull(markupType);

        this.Options.MarkupTypes.Add(markupType);

        return this;
    }

    /// <inheritdoc/>
    public virtual IAsyncApiGenerationOptionsBuilder WithMarkupType<TMarkup>() => this.WithMarkupType(typeof(TMarkup));

    /// <inheritdoc/>
    public virtual IAsyncApiGenerationOptionsBuilder UseDefaultConfiguration(Action<IAsyncApiDocumentBuilder> configurationAction)
    {
        this.Options.DefaultDocumentConfiguration = configurationAction;
        return this;
    }

    /// <inheritdoc/>
    public virtual AsyncApiGenerationOptions Build() => this.Options;

}
