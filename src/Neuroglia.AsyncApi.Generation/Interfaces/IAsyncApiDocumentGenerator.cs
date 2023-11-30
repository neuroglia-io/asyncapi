namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Defines the fundamentals of a service used to generate <see cref="AsyncApiDocument"/>s in a code-first fashion
/// </summary>
public interface IAsyncApiDocumentGenerator
{

    /// <summary>
    /// Generates code-first <see cref="AsyncApiDocument"/>s for types reflected in the specified assemblies
    /// </summary>
    /// <param name="markupTypes">An <see cref="IEnumerable{T}"/> containing the mark up types belonging to the assemblies to scan</param>
    /// <param name="options">The <see cref="AsyncApiDocumentGenerationOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing the generated <see cref="AsyncApiDocument"/>s</returns>
    Task<IEnumerable<AsyncApiDocument>> GenerateAsync(IEnumerable<Type> markupTypes, AsyncApiDocumentGenerationOptions options, CancellationToken cancellationToken = default);

}
