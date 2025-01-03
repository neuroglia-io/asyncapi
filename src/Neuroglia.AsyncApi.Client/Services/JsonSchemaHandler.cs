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

using Json.Schema;
using Neuroglia.Serialization;
using System.Net;

namespace Neuroglia.AsyncApi.Client.Services;

/// <summary>
/// Represents the <see cref="ISchemaHandler"/> implementation used to handle JSON schemas
/// </summary>
/// <param name="serializer">The service used to serialize/deserialize data to/from JSON</param>
public class JsonSchemaHandler(IJsonSerializer serializer)
    : ISchemaHandler
{

    /// <summary>
    /// Gets the service used to serialize/deserialize data to/from JSON
    /// </summary>
    protected IJsonSerializer Serializer { get; } = serializer;

    /// <inheritdoc/>
    public virtual bool Supports(string format) => format.Equals(SchemaFormat.Json, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public virtual Task<IOperationResult> ValidateAsync(object graph, object schema, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(graph);
        ArgumentNullException.ThrowIfNull(schema);
        var json = string.Empty;
        json = this.Serializer.SerializeToText(schema);
        var jsonSchema = JsonSchema.FromText(json);
        var jsonDocument = this.Serializer.SerializeToDocument(graph)!;
        var options = new EvaluationOptions()
        {
            OutputFormat = OutputFormat.List
        };
        var results = jsonSchema.Evaluate(jsonDocument, options);
        var result = results.IsValid
            ? (IOperationResult)new OperationResult((int)HttpStatusCode.OK)
            : new OperationResult((int)HttpStatusCode.UnprocessableEntity, null, new Error()
            {
                Type = ErrorType.Validation,
                Title = ErrorTitle.Validation,
                Status = (int)HttpStatusCode.UnprocessableEntity,
                Errors = new(results.Details.Where(d => d.Errors != null).SelectMany(d => d.Errors!).GroupBy(e => e.Key).Select(e => new KeyValuePair<string, string[]>(e.Key, e.Select(e => e.Value).ToArray())))
            });
        return Task.FromResult(result);
    }

}