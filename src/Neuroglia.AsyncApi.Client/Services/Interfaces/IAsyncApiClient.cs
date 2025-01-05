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
    /// Performs a publish operation
    /// </summary>
    /// <param name="parameters">The <see cref="AsyncApiPublishOperationParameters"/> of the operation to perform</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>An object that describes the result of the operation</returns>
    Task<IAsyncApiPublishOperationResult> PublishAsync(AsyncApiPublishOperationParameters parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Performs a subscribe operation
    /// </summary>
    /// <param name="parameters">The <see cref="AsyncApiSubscribeOperationParameters"/> of the operation to perform</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>An object that describes the result of the operation</returns>
    Task<IAsyncApiSubscribeOperationResult> SubscribeAsync(AsyncApiSubscribeOperationParameters parameters, CancellationToken cancellationToken = default);

}
