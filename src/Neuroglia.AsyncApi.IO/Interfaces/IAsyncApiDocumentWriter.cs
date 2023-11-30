using Neuroglia.AsyncApi.Specification.v2;

namespace Neuroglia.AsyncApi.IO;

/// <summary>
/// Defines the fundamentals of a service used to write <see cref="AsyncApiDocument"/>s
/// </summary>
public interface IAsyncApiDocumentWriter
{

    /// <summary>
    /// Writes the specified <see cref="AsyncApiDocument"/> to a <see cref="Stream"/>
    /// </summary>
    /// <param name="document">The <see cref="AsyncApiDocument"/> to write</param>
    /// <param name="stream">The <see cref="Stream"/> to read the <see cref="AsyncApiDocument"/> from</param>
    /// <param name="format">The format of the <see cref="AsyncApiDocument"/> to read. Defaults to '<see cref="AsyncApiDocumentFormat.Yaml"/>'</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="AsyncApiDocument"/></returns>
    Task WriteAsync(AsyncApiDocument document, Stream stream, AsyncApiDocumentFormat format = AsyncApiDocumentFormat.Yaml, CancellationToken cancellationToken = default);

}
