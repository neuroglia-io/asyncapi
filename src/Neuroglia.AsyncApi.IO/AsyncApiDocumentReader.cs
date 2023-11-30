using Neuroglia.AsyncApi.Specification.v2;
using Neuroglia.Serialization;

namespace Neuroglia.AsyncApi.IO;

/// <summary>
/// Represents the default implementation of the <see cref="IAsyncApiDocumentReader"/> interface
/// </summary>
/// <remarks>
/// Initializes a new <see cref="AsyncApiDocumentReader"/>
/// </remarks>
/// <param name="jsonSerializer">The service used to serialize and deserialize JSON</param>
/// <param name="yamlSerializer">The service used to serialize and deserialize YAML</param>
public class AsyncApiDocumentReader(IJsonSerializer jsonSerializer, IYamlSerializer yamlSerializer)
        : IAsyncApiDocumentReader
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
    public virtual async Task<AsyncApiDocument?> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(stream);
        var offset = stream.Position;
        using var reader = new StreamReader (stream);
        string input = reader.ReadToEnd();
        stream.Position = offset;
        var serializer = this.ResolveDocumentFormat(input) == AsyncApiDocumentFormat.Json ? (ITextSerializer)this.JsonSerializer : this.YamlSerializer;
        return serializer is IAsyncSerializer asyncSerializer ? await asyncSerializer.DeserializeAsync<AsyncApiDocument>(stream, cancellationToken) : serializer.Deserialize<AsyncApiDocument>(stream);
    }

    /// <summary>
    /// Resolves the <see cref="AsyncApiDocumentFormat"/> of the specified raw <see cref="AsyncApiDocument"/>
    /// </summary>
    /// <param name="input">The raw <see cref="AsyncApiDocument"/> to get the format of</param>
    /// <returns>The <see cref="AsyncApiDocumentFormat"/> of the specified raw <see cref="AsyncApiDocument"/></returns>
    protected virtual AsyncApiDocumentFormat ResolveDocumentFormat(string input) => input.TrimStart().StartsWith('{') && input.TrimEnd().EndsWith('}') ? AsyncApiDocumentFormat.Json : AsyncApiDocumentFormat.Yaml;

}
