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
/// Represents an object used to describe the result of an HTTP subscribe operation
/// </summary>
/// <param name="statusCode">The <see cref="HttpStatusCode"/> returned by the operation</param>
/// <param name="headers">The headers returned by the operation</param>
/// <param name="messages">The <see cref="IObservable{T}"/> used to observe inbound <see cref="IAsyncApiMessage"/>s</param>
public class HttpSubscribeOperationResult(HttpStatusCode statusCode, HttpResponseHeaders headers, IObservable<IAsyncApiMessage>? messages)
    : AsyncApiSubscribeOperationResult
{

    /// <summary>
    /// Gets the <see cref="HttpStatusCode"/> returned by the operation
    /// </summary>
    public virtual HttpStatusCode StatusCode { get; } = statusCode;

    /// <summary>
    /// Gets the headers returned by the operation
    /// </summary>
    public virtual HttpResponseHeaders Headers { get; } = headers;

    /// <inheritdoc/>
    public override IObservable<IAsyncApiMessage>? Messages => messages;

    /// <inheritdoc/>
    public override bool IsSuccessful => (int)StatusCode >= 200 && (int)StatusCode < 300;

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (!disposing) return;
        if (Messages is IDisposable disposable) disposable.Dispose();
        base.Dispose(disposing);
    }

    /// <inheritdoc/>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (!disposing) return;
        switch (Messages)
        {
            case IAsyncDisposable asyncDisposable:
                await asyncDisposable.DisposeAsync().ConfigureAwait(false);
                break;
            case IDisposable disposable:
                disposable.Dispose();
                break;
        }
        await base.DisposeAsync(disposing).ConfigureAwait(false);
    }

}