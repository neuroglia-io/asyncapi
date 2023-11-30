using Neuroglia.AsyncApi.Specification.v2;
using Neuroglia.Serialization;

namespace Neuroglia.AsyncApi.IO;

/// <summary>
/// Represents the default implementation of the <see cref="IAsyncApiDocumentWriter"/> interface
/// </summary>
/// <remarks>
/// Initializes a new <see cref="AsyncApiDocumentWriter"/>
/// </remarks>
/// <param name="jsonSerializer">The service used to serialize and deserialize JSON</param>
/// <param name="yamlSerializer">The service used to serialize and deserialize YAML</param>
public class AsyncApiDocumentWriter(IJsonSerializer jsonSerializer, IYamlSerializer yamlSerializer)
    : IAsyncApiDocumentWriter
{

    /// <summary>
    /// Gets the service used to serialize and deserialize JSON
    /// </summary>
    protected IJsonSerializer JsonSerializer { get; } = jsonSerializer;

    /// <summary>
    /// Gets the service used to serialize and deserialize YAML
    /// </summary>
    protected IYamlSerializer YamlSerializer { get; } = yamlSerializer;

    /// <inheritdoc/>
    public virtual async Task WriteAsync(AsyncApiDocument document, Stream stream, AsyncApiDocumentFormat format = AsyncApiDocumentFormat.Yaml, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(stream);

        switch (format)
        {
            case AsyncApiDocumentFormat.Json:
                if (this.JsonSerializer is IAsyncSerializer asyncJsonSerializer) await asyncJsonSerializer.SerializeAsync(stream, document, typeof(AsyncApiDocument), cancellationToken).ConfigureAwait(false);
                else this.JsonSerializer.Serialize(document, stream);
                break;
            case AsyncApiDocumentFormat.Yaml:
                if (this.YamlSerializer is IAsyncSerializer asyncYamlSerializer) await asyncYamlSerializer.SerializeAsync(stream, document, typeof(AsyncApiDocument), cancellationToken).ConfigureAwait(false);
                else this.YamlSerializer.Serialize(document, stream);
                break;
            default:
                throw new NotSupportedException($"The specified async api document format '{format}' is not supported");
        }
    }

}
