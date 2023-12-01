using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.IO;

/// <summary>
/// Defines the fundamentals of a service used to read <see cref="AsyncApiDocument"/>s
/// </summary>
public interface IAsyncApiDocumentReader
{

    /// <summary>
    /// Reads a <see cref="AsyncApiDocument"/> from the specified <see cref="Stream"/>
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to read the <see cref="AsyncApiDocument"/> from</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="AsyncApiDocument"/></returns>
    Task<AsyncApiDocument?> ReadAsync(Stream stream, CancellationToken cancellationToken = default);

}
