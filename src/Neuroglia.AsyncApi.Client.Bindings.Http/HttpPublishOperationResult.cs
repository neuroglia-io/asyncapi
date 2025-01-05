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

namespace Neuroglia.AsyncApi.Client.Bindings.Http;

/// <summary>
/// Represents an object used to describe the result of an HTTP publish operation
/// </summary>
/// <param name="statusCode">The <see cref="HttpStatusCode"/> returned by the operation</param>
/// <param name="headers">The headers returned by the operation</param>
/// <param name="contentStream">The <see cref="Stream"/> that provides access to the response content</param>
public class HttpPublishOperationResult(HttpStatusCode statusCode, HttpResponseHeaders headers, Stream contentStream)
    : AsyncApiPublishOperationResult
{

    /// <summary>
    /// Gets the <see cref="HttpStatusCode"/> returned by the operation
    /// </summary>
    public virtual HttpStatusCode StatusCode { get; } = statusCode;

    /// <summary>
    /// Gets the headers returned by the operation
    /// </summary>
    public virtual HttpResponseHeaders Headers { get; } = headers;

    /// <summary>
    /// Gets the <see cref="Stream"/> that provides access to the response content
    /// </summary>
    public virtual Stream ContentStream { get; } = contentStream;

    /// <inheritdoc/>
    public override bool IsSuccessful => (int)StatusCode >= 200 && (int)StatusCode < 300;

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (!disposing) return;
        this.ContentStream.Dispose();
        base.Dispose(disposing);
    }

    /// <inheritdoc/>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (!disposing) return;
        await this.ContentStream.DisposeAsync().ConfigureAwait(false);
        await base.DisposeAsync(disposing).ConfigureAwait(false);
    }

}
