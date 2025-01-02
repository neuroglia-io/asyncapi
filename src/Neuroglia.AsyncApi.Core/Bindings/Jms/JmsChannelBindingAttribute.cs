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

namespace Neuroglia.AsyncApi.Bindings.Jms;

/// <summary>
/// Represents the attribute used to define an <see cref="JmsChannelBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="version">The binding's version</param>
public class JmsChannelBindingAttribute(string name, string version = "latest")
    : ChannelBindingAttribute<JmsChannelBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets/sets the destination (queue) name for this channel. SHOULD only be specified if the channel name differs from the actual destination name, such as when the channel name is not a valid destination name according to the JMS Provider.
    /// </summary>
    public virtual string? Destination { get; init; }

    /// <summary>
    /// Gets/sets the type of destination, which MUST be either queue, or fifo-queue. SHOULD be specified to document the messaging model (point-to-point, or strict message ordering) supported by this channel.
    /// </summary>
    public virtual JmsDestinationType DestinationType { get; init; } = JmsDestinationType.Queue;

    /// <inheritdoc/>
    public override JmsChannelBindingDefinition Build() => new()
    {
        BindingVersion = Version,
        Destination = Destination,
        DestinationType = DestinationType
    };

}
