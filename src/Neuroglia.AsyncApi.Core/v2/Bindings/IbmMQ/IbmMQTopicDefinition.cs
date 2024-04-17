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
/// Represents the object used to configure an IBMMQ topic
/// </summary>
[DataContract]
public record IbmMQTopicDefinition
{

    /// <summary>
    /// Gets/sets the value of the IBM MQ topic string to be used.
    /// </summary>
    [Required, MaxLength(48)]
    [DataMember(Order = 1, Name = "objectName"), JsonPropertyOrder(1), JsonPropertyName("objectName"), YamlMember(Order = 1, Alias = "objectName")]
    public virtual string ObjectName { get; set; } = null!;

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the subscription may be durable.
    /// </summary>
    [DataMember(Order = 2, Name = "durablePermitted"), JsonPropertyOrder(2), JsonPropertyName("durablePermitted"), YamlMember(Order = 2, Alias = "durablePermitted")]
    public virtual bool DurablePermitted { get; set; }

    /// <summary>
    /// Gets/sets the last message published will be made available to new subscriptions.
    /// </summary>
    [DataMember(Order = 3, Name = "lastMsgRetained"), JsonPropertyOrder(3), JsonPropertyName("lastMsgRetained"), YamlMember(Order = 3, Alias = "lastMsgRetained")]
    public virtual bool LastMsgRetained { get; set; }

}
