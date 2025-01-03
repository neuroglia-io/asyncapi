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
/// Defines the fundamentals of a service used to interact with an application described using an <see cref="IAsyncApiDocument"/>
/// </summary>
public interface IAsyncApiClient
    : IDisposable, IAsyncDisposable
{

    /// <summary>
    /// Sends the specified <see cref="AsyncApiOutboundMessage"/>
    /// </summary>
    /// <param name="message">The <see cref="AsyncApiOutboundMessage"/> to send</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>An object that describes the result of the operation</returns>
    Task<AsyncApiOperationResult> SendAsync(AsyncApiOutboundMessage message, CancellationToken cancellationToken = default);

}
