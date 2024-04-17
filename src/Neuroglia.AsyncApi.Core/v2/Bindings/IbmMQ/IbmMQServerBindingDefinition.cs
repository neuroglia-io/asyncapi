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

namespace Neuroglia.AsyncApi.v2.Bindings.IbmMQ;

/// <summary>
/// Represents the object used to configure an IBMMQ server binding
/// </summary>
[DataContract]
public record IbmMQServerBindingDefinition
    : IbmMQBindingDefinition, IServerBindingDefinition
{

    /// <summary>
    /// Gets/sets a logical group of IBM MQ server objects. This is necessary to specify multi-endpoint configurations used in high availability deployments. If omitted, the server object is not part of a group.
    /// </summary>
    [DataMember(Order = 1, Name = "groupId"), JsonPropertyOrder(1), JsonPropertyName("groupId"), YamlMember(Order = 1, Alias = "groupId")]
    public virtual string? GroupId { get; set; }

    /// <summary>
    /// Gets/sets the name of the IBM MQ queue manager to bind to in the CCDT file.
    /// </summary>
    [DataMember(Order = 2, Name = "ccdtQueueManagerName"), JsonPropertyOrder(2), JsonPropertyName("ccdtQueueManagerName"), YamlMember(Order = 2, Alias = "ccdtQueueManagerName")]
    public virtual string? CcdtQueueManagerName { get; set; }

    /// <summary>
    /// Gets/sets the recommended cipher specification used to establish a TLS connection between the client and the IBM MQ queue manager. More information on SSL/TLS cipher specifications supported by IBM MQ can be found on this page in the IBM MQ Knowledge Center.
    /// </summary>
    [DataMember(Order = 3, Name = "cipherSpec"), JsonPropertyOrder(3), JsonPropertyName("cipherSpec"), YamlMember(Order = 3, Alias = "cipherSpec")]
    public virtual string? CipherSpec { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not multiple connections can be workload balanced. Where message ordering, or affinity to specific message resources is necessary, a single endpoint (multiEndpointServer = false) may be required.
    /// </summary>
    [DataMember(Order = 4, Name = "multiEndpointServer"), JsonPropertyOrder(4), JsonPropertyName("multiEndpointServer"), YamlMember(Order = 4, Alias = "multiEndpointServer")]
    public virtual bool MultiEndpointServer { get; set; }

    /// <summary>
    /// Gets/sets the recommended value (in seconds) for the heartbeat sent to the queue manager during periods of inactivity. 
    /// A value of zero means that no heart beats are sent. 
    /// A value of 1 means that the client will use the value defined by the queue manager. 
    /// More information on heart beat interval can be found on this page in the IBM MQ Knowledge Center.
    /// </summary>
    [DataMember(Order = 5, Name = "heartBeatInterval"), JsonPropertyOrder(5), JsonPropertyName("heartBeatInterval"), YamlMember(Order = 5, Alias = "heartBeatInterval")]
    public virtual int? HeartBeatInterval { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding.
    /// </summary>
    [DataMember(Order = 6, Name = "bindingVersion"), JsonPropertyOrder(6), JsonPropertyName("bindingVersion"), YamlMember(Order = 6, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
