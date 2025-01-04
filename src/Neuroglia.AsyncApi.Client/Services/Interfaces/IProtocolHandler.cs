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
/// Defines the fundamentals of a service used to handle protocol specific operations
/// </summary>
public interface IProtocolHandler
{

    /// <summary>
    /// Determines whether or not the <see cref="IProtocolHandler"/> supports the specified protocol
    /// </summary>
    /// <param name="protocol">The protocol to check</param>
    /// <param name="protocolVersion">The version, if any, of the protocol to check</param>
    /// <returns>A boolean indicating whether or not the <see cref="IProtocolHandler"/> supports the specified protocol</returns>
    bool Supports(string protocol, string? protocolVersion);

    /// <summary>
    /// Handles the specified <see cref="AsyncApiOperation"/>
    /// </summary>
    /// <param name="operation">The <see cref="AsyncApiOperation"/> to handle</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>An object that describes the result of the operation</returns>
    Task<IAsyncApiOperationResult> HandleAsync(AsyncApiOperation operation, CancellationToken cancellationToken = default);

}
