namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="AsyncApiGenerationOptions"/>
/// </summary>
public interface IAsyncApiGenerationOptionsBuilder
{

    /// <summary>
    /// Configures the <see cref="AsyncApiGenerationOptions"/> to use the specified type to markup an <see cref="Assembly"/> to scan for Async API definitions
    /// </summary>
    /// <param name="markupType">The type used to markup an <see cref="Assembly"/> to scan for Async API definitions</param>
    /// <returns>The configured <see cref="IAsyncApiGenerationOptionsBuilder"/></returns>
    IAsyncApiGenerationOptionsBuilder WithMarkupType(Type markupType);

    /// <summary>
    /// Configures the <see cref="AsyncApiGenerationOptions"/> to use the specified type to markup an <see cref="Assembly"/> to scan for Async API definitions
    /// </summary>
    /// <typeparam name="TMarkup">The type used to markup an <see cref="Assembly"/> to scan for Async API definitions</typeparam>
    /// <returns>The configured <see cref="IAsyncApiGenerationOptionsBuilder"/></returns>
    IAsyncApiGenerationOptionsBuilder WithMarkupType<TMarkup>();

    /// <summary>
    /// Configures the <see cref="AsyncApiGenerationOptions"/> to use the specified <see cref="Action{T}"/> to setup the default configuration for generated <see cref="AsyncApiDocument"/>s
    /// </summary>
    /// <param name="configurationAction">The <see cref="Action{T}"/> used to configure the <see cref="IAsyncApiDocumentBuilder"/> used to build <see cref="AsyncApiDocument"/>s</param>
    /// <returns>The configured <see cref="IAsyncApiGenerationOptionsBuilder"/></returns>
    IAsyncApiGenerationOptionsBuilder UseDefaultConfiguration(Action<IAsyncApiDocumentBuilder> configurationAction);

    /// <summary>
    /// Builds new <see cref="AsyncApiGenerationOptions"/>
    /// </summary>
    /// <returns>New <see cref="AsyncApiGenerationOptions"/></returns>
    AsyncApiGenerationOptions Build();

}
