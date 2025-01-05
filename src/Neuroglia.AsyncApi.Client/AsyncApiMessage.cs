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

namespace Neuroglia.AsyncApi.Client;

/// <summary>
/// Represents the default implementation of the <see cref="IAsyncApiMessage"/> interface
/// </summary>
/// <param name="contentType">The message's content type</param>
/// <param name="payload">The message's payload, if any</param>
/// <param name="headers">The message's headers, if any</param>
/// <param name="correlationId">The message's correlation id, if any</param>
public class AsyncApiMessage(string contentType, object? payload, object? headers, string? correlationId)
    : IAsyncApiMessage
{

    /// <inheritdoc/>
    public virtual string ContentType => contentType;

    /// <inheritdoc/>
    public virtual object? Payload => payload;

    /// <inheritdoc/>
    public virtual object? Headers => headers;

    /// <inheritdoc/>
    public virtual string? CorrelationId => correlationId;

}