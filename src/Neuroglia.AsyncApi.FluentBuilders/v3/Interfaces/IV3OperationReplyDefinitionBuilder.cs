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

namespace Neuroglia.AsyncApi.FluentBuilders.v3;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="V3ReplyDefinition"/>s
/// </summary>
public interface IV3OperationReplyDefinitionBuilder
    : IReferenceableComponentDefinitionBuilder<V3ReplyDefinition>
{

    /// <summary>
    /// Configures the <see cref="V3ReplyDefinition"/> to use the specified <see cref="V3ReplyAddressDefinition"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V3ReplyAddressDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV3OperationReplyDefinitionBuilder"/></returns>
    IV3OperationReplyDefinitionBuilder WithAddress(Action<IV3OperationReplyAddressDefinitionBuilder> setup);

    /// <summary>
    /// Configures the <see cref="V3ReplyDefinition"/> to use the specified <see cref="V3ChannelDefinition"/>
    /// </summary>
    /// <param name="channel">A reference to the <see cref="V3ChannelDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV3OperationReplyDefinitionBuilder"/></returns>
    IV3OperationReplyDefinitionBuilder WithChannel(string channel);

    /// <summary>
    /// Configures the <see cref="V3ReplyDefinition"/> to use the specified <see cref="V3MessageDefinition"/>
    /// </summary>
    /// <param name="message">A reference to the <see cref="V3MessageDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV3OperationReplyDefinitionBuilder"/></returns>
    IV3OperationReplyDefinitionBuilder WithMessage(string message);

    /// <summary>
    /// Builds the configured <see cref="V3ReplyDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="V3ReplyDefinition"/></returns>
    V3ReplyDefinition Build();

}
