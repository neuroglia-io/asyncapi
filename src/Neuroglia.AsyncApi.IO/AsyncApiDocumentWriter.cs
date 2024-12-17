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

using Neuroglia.AsyncApi.v2;
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
    public virtual async Task WriteAsync(V2AsyncApiDocument document, Stream stream, AsyncApiDocumentFormat format = AsyncApiDocumentFormat.Yaml, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(stream);

        switch (format)
        {
            case AsyncApiDocumentFormat.Json:
                if (this.JsonSerializer is IAsyncSerializer asyncJsonSerializer) await asyncJsonSerializer.SerializeAsync(stream, document, typeof(V2AsyncApiDocument), cancellationToken).ConfigureAwait(false);
                else this.JsonSerializer.Serialize(document, stream);
                break;
            case AsyncApiDocumentFormat.Yaml:
                if (this.YamlSerializer is IAsyncSerializer asyncYamlSerializer) await asyncYamlSerializer.SerializeAsync(stream, document, typeof(V2AsyncApiDocument), cancellationToken).ConfigureAwait(false);
                else this.YamlSerializer.Serialize(document, stream);
                break;
            default:
                throw new NotSupportedException($"The specified async api document format '{format}' is not supported");
        }
    }

}
