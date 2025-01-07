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
/// Represents the parameters used to configure an AsyncAPI publish operation
/// </summary>
/// <param name="operation">The unique identifier of the operation to perform</param>
/// <param name="server">The identifier, if any, used to specify the target server for the operation to perform</param>
/// <param name="protocol">The protocol to use, if any, to perform the operation</param>
public class AsyncApiPublishOperationParameters(string operation, string? server = null, string? protocol = null)
    : AsyncApiOperationParameters(operation, server, protocol)
{

    /// <summary>
    /// Gets/sets the message's payload, if any
    /// </summary>
    public virtual object? Payload { get; init; }

    /// <summary>
    /// Gets/sets the message's headers, if any
    /// </summary>
    public virtual object? Headers { get; init; }

}
