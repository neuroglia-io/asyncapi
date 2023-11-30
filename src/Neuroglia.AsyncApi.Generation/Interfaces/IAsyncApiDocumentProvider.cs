namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Defines the fundamentals of a service used to manage <see cref="AsyncApiDocument"/>s
/// </summary>
public interface IAsyncApiDocumentProvider
    : IEnumerable<AsyncApiDocument>
{

    /// <summary>
    /// Gets the <see cref="AsyncApiDocument"/> with the specified title and version
    /// </summary>
    /// <param name="title">The title of the <see cref="AsyncApiDocument"/> to get</param>
    /// <param name="version">The version of the <see cref="AsyncApiDocument"/> to get</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The <see cref="AsyncApiDocument"/> with the specified title and version, if any</returns>
    Task<AsyncApiDocument?> GetDocumentAsync(string title, string version, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the <see cref="AsyncApiDocument"/> with the specified id
    /// </summary>
    /// <param name="title">The title of the <see cref="AsyncApiDocument"/> to get</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The <see cref="AsyncApiDocument"/> with the specified id, if any</returns>
    Task<AsyncApiDocument?> GetDocumentAsync(string id, CancellationToken cancellationToken = default);

}
