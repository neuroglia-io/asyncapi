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

using System.Net;
using System.Net.Http.Headers;

namespace Neuroglia.AsyncApi.Client;

/// <summary>
/// Represents an object used to describe the result of an HTTP operation
/// </summary>
/// <param name="statusCode">The <see cref="HttpStatusCode"/> returned by the operation</param>
/// <param name="headers">The headers returned by the operation</param>
/// <param name="stream">The <see cref="Stream"/> that provides access to the payload, if any, returned by the operation </param>
public class HttpOperationResult(HttpStatusCode statusCode, HttpResponseHeaders headers, Stream stream)
    : AsyncApiOperationResult
{

    /// <summary>
    /// Gets the <see cref="HttpStatusCode"/> returned by the operation
    /// </summary>
    public HttpStatusCode StatusCode { get; } = statusCode;

    /// <summary>
    /// Gets the headers returned by the operation
    /// </summary>
    public HttpResponseHeaders Headers { get; } = headers;

    /// <inheritdoc/>
    public override bool IsSuccessful => (int)StatusCode >= 200 && (int)StatusCode < 300;

    /// <inheritdoc/>
    public override Stream? PayloadStream { get; } = stream;

}