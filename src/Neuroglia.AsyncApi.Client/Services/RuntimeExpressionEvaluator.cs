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

using Json.Pointer;
using Neuroglia.Serialization;

namespace Neuroglia.AsyncApi.Client.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IRuntimeExpressionEvaluator"/> interface
/// </summary>
/// <param name="jsonSerializer">The service used to serialize/deserialize data to/from JSON</param>
public class RuntimeExpressionEvaluator(IJsonSerializer jsonSerializer)
    : IRuntimeExpressionEvaluator
{

    /// <summary>
    /// Gets the service used to serialize/deserialize data to/from JSON
    /// </summary>
    protected IJsonSerializer JsonSerializer { get; } = jsonSerializer;

    /// <inheritdoc/>
    public virtual Task<string?> EvaluateAsync(string expression, object? payload, object? headers, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(expression);
        var parsedExpression = RuntimeExpression.Parse(expression);
        var source = parsedExpression.Source switch
        {
            RuntimeExpressionSource.Payload => payload,
            RuntimeExpressionSource.Header => headers,
            _ => throw new NotSupportedException($"The specified runtime expression source '{parsedExpression.Source}' is not supported")
        };
        var jsonSource = JsonSerializer.SerializeToElement(source ?? new { });
        var jsonPointer = JsonPointer.Parse(parsedExpression.Fragment);
        if (!jsonSource.HasValue) return null;
        var jsonResult = jsonPointer.Evaluate(jsonSource.Value);
        return Task.FromResult(jsonResult.HasValue ? JsonSerializer.Deserialize<string?>(jsonResult.Value.ToString()) : null);
    }

}
