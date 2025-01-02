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

namespace Neuroglia.AsyncApi.Bindings.IbmMQ;

/// <summary>
/// Represents the attribute used to define an <see cref="IbmMQServerBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="version">The binding's version</param>
public class IbmMQServerBindingAttribute(string name, string version = "latest")
    : ServerBindingAttribute<IbmMQServerBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets/sets a logical group of IBM MQ server objects. This is necessary to specify multi-endpoint configurations used in high availability deployments. If omitted, the server object is not part of a group.
    /// </summary>
    public virtual string? GroupId { get; init; }

    /// <summary>
    /// Gets/sets the name of the IBM MQ queue manager to bind to in the CCDT file.
    /// </summary>
    public virtual string? CcdtQueueManagerName { get; init; }

    /// <summary>
    /// Gets/sets the recommended cipher specification used to establish a TLS connection between the client and the IBM MQ queue manager. More information on SSL/TLS cipher specifications supported by IBM MQ can be found on this page in the IBM MQ Knowledge Center.
    /// </summary>
    public virtual string? CipherSpec { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not multiple connections can be workload balanced. Where message ordering, or affinity to specific message resources is necessary, a single endpoint (multiEndpointServer = false) may be required.
    /// </summary>
    public virtual bool MultiEndpointServer { get; init; }

    /// <summary>
    /// Gets/sets the recommended value (in seconds) for the heartbeat sent to the queue manager during periods of inactivity. 
    /// A value of zero means that no heart beats are sent. 
    /// A value of 1 means that the client will use the value defined by the queue manager. 
    /// More information on heart beat interval can be found on this page in the IBM MQ Knowledge Center.
    /// </summary>
    public virtual int? HeartBeatInterval { get; init; }

    /// <inheritdoc/>
    public override IbmMQServerBindingDefinition Build() => new()
    {
        BindingVersion = this.Version,
        GroupId = this.GroupId,
        CcdtQueueManagerName = this.CcdtQueueManagerName,
        CipherSpec = this.CipherSpec,
        MultiEndpointServer = this.MultiEndpointServer,
        HeartBeatInterval = this.HeartBeatInterval
    };

}
