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
/// Represents the object used to configure an IBMMQ queue
/// </summary>
[DataContract]
public record IbmMQQueueDefinition
{

    /// <summary>
    /// Gets/sets the name of the IBM MQ queue associated with the channel.
    /// </summary>
    [Required, MaxLength(48)]
    [DataMember(Order = 1, Name = "objectName"), JsonPropertyOrder(1), JsonPropertyName("objectName"), YamlMember(Order = 1, Alias = "objectName")]
    public virtual string ObjectName { get; set; } = null!;

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the queue is a cluster queue and therefore partitioned. If true, a binding option MAY be specified when accessing the queue. More information on binding options can be found on this page in the IBM MQ Knowledge Center.
    /// </summary>
    [DataMember(Order = 2, Name = "isPartitioned"), JsonPropertyOrder(2), JsonPropertyName("isPartitioned"), YamlMember(Order = 2, Alias = "isPartitioned")]
    public virtual bool IsPartitioned { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not it is recommended to open the queue exclusively.
    /// </summary>
    [DataMember(Order = 3, Name = "exclusive"), JsonPropertyOrder(3), JsonPropertyName("exclusive"), YamlMember(Order = 3, Alias = "exclusive")]
    public virtual bool Exclusive { get; set; }

}
