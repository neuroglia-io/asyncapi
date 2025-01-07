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

using Neuroglia.AsyncApi.Bindings;

namespace Neuroglia.AsyncApi.Client;

/// <summary>
/// Represents the context of an AsyncAPI subscribe operation
/// </summary>
/// <param name="Document">The <see cref="V3AsyncApiDocument"/> that defines the operation</param>
/// <param name="Host">The hostname or IP address used to receive messages from</param>
/// <param name="Path">The path or endpoint used by the protocol.</param>
/// <param name="Channel">The channel key.</param>
/// <param name="DefaultContentType">The default content type of consumed messages.</param>
/// <param name="Messages">An <see cref="IEnumerable{T}"/> containing the definitions of all messages that can be consumed.</param>
/// <param name="ServerBinding">The definition specifying protocol-level settings at the server level.</param>
/// <param name="ChannelBinding">The definition specifying protocol-level settings at the channel level.</param>
/// <param name="OperationBinding">The definition specifying protocol-level settings at the operation level.</param>
public record AsyncApiSubscribeOperationContext(V3AsyncApiDocument Document, string Host, string? Path, string? Channel, string DefaultContentType, IEnumerable<V3MessageDefinition> Messages, IServerBindingDefinition? ServerBinding, IChannelBindingDefinition? ChannelBinding, IOperationBindingDefinition? OperationBinding);