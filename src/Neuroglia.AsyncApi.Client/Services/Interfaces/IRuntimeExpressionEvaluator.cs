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

namespace Neuroglia.AsyncApi.Client.Services;

/// <summary>
/// Defines the fundamentals of a service used to evaluate AsyncAPI runtime expressions
/// </summary>
public interface IRuntimeExpressionEvaluator
{

    /// <summary>
    /// Evaluates the specified runtime expression
    /// </summary>
    /// <param name="expression">The runtime expression to evaluate</param>
    /// <param name="payload">The payload, if any, to evaluate the runtime expression against</param>
    /// <param name="headers">The headers, if any, to evaluate the runtime expression against</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The evaluation's result</returns>
    Task<string?> EvaluateAsync(string expression, object? payload, object? headers, CancellationToken cancellationToken = default);

}
