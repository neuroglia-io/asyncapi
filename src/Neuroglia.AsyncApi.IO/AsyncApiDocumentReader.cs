// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using YamlDotNet.RepresentationModel;

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
    public virtual async Task<IAsyncApiDocument?> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(stream);
        using var reader = new StreamReader(stream, leaveOpen: true);
        var input = await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);
        var format = this.ResolveDocumentFormat(input);
        var version = this.ExtractDocumentSpecVersion(input, format);
        var serializer = format == AsyncApiDocumentFormat.Json ? (ITextSerializer)this.JsonSerializer : this.YamlSerializer;
        return version.Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0] switch
        {
            "2" => serializer.Deserialize<V2AsyncApiDocument>(input),
            "3" => serializer.Deserialize<V3AsyncApiDocument>(input),
            _ => throw new NotSupportedException($"The specified Async API version '{version}' is not supported")
        };
    }

    /// <summary>
    /// Resolves the <see cref="AsyncApiDocumentFormat"/> of the specified raw <see cref="V2AsyncApiDocument"/>
    /// </summary>
    /// <param name="input">The raw <see cref="V2AsyncApiDocument"/> to get the format of</param>
    /// <returns>The <see cref="AsyncApiDocumentFormat"/> of the specified raw <see cref="V2AsyncApiDocument"/></returns>
    protected virtual AsyncApiDocumentFormat ResolveDocumentFormat(string input) => input.TrimStart().StartsWith('{') && input.TrimEnd().EndsWith('}') ? AsyncApiDocumentFormat.Json : AsyncApiDocumentFormat.Yaml;

    /// <summary>
    /// Extracts the specified document's Async API specification version
    /// </summary>
    /// <param name="input">The serialized document to extract the version from</param>
    /// <param name="format">The format of the document to extract the version from</param>
    /// <returns>The specified document's Async API specification version</returns>
    protected virtual string ExtractDocumentSpecVersion(string input, AsyncApiDocumentFormat format)
    {
        if (format == AsyncApiDocumentFormat.Json)
        {
            using var jsonDoc = JsonDocument.Parse(input);
            if (jsonDoc.RootElement.TryGetProperty("asyncapi", out var versionElement)) return versionElement.GetString() ?? string.Empty;
        }
        else
        {
            var yaml = new YamlStream();
            yaml.Load(new StringReader(input));
            var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
            if (mapping.Children.TryGetValue(new YamlScalarNode("asyncapi"), out var versionNode)) return ((YamlScalarNode)versionNode).Value!;
        }
        throw new InvalidDataException("The AsyncAPI version property is missing or invalid.");
    }

}
