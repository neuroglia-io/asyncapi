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
/// Represents the context of an AsyncAPI publish operation
/// </summary>
/// <param name="Document">The <see cref="V3AsyncApiDocument"/> that defines the operation</param>
/// <param name="Host">The hostname or IP address used to send the message.</param>
/// <param name="Path">The path or endpoint used by the protocol.</param>
/// <param name="Channel">The channel key.</param>
/// <param name="Payload">The data to be sent as the message payload.</param>
/// <param name="Headers">The headers for the message.</param>
/// <param name="ContentType">The message's content type.</param>
/// <param name="CorrelationId">The message's correlation id, if any.</param>
/// <param name="ServerBinding">The definition specifying protocol-level settings at the server level.</param>
/// <param name="ChannelBinding">The definition specifying protocol-level settings at the channel level.</param>
/// <param name="OperationBinding">The definition specifying protocol-level settings at the operation level.</param>
/// <param name="MessageBinding">The definition specifying protocol-level settings at the message level.</param>
public record AsyncApiPublishOperationContext(V3AsyncApiDocument Document, string Host, string? Path, string? Channel, object? Payload, object? Headers, string ContentType, string? CorrelationId, IServerBindingDefinition? ServerBinding, IChannelBindingDefinition? ChannelBinding, IOperationBindingDefinition? OperationBinding, IMessageBindingDefinition? MessageBinding);
