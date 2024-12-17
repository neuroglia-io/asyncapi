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

namespace Neuroglia.AsyncApi.Bindings.Solace;

/// <summary>
/// Represents an object used to configure a Solace queue
/// </summary>
[DataContract]
public record SolaceQueueDefinition
{

    /// <summary>
    /// Gets/sets the name of the queue.
    /// </summary>
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name"), YamlMember(Order = 1, Alias = "name")]
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets/sets a list of topics that the queue subscribes to. If none is given, the queue subscribes to the topic as represented by the channel name.
    /// </summary>
    [DataMember(Order = 2, Name = "topicSubscriptions"), JsonPropertyOrder(2), JsonPropertyName("topicSubscriptions"), YamlMember(Order = 2, Alias = "topicSubscriptions")]
    public virtual EquatableList<string>? TopicSubscriptions { get; set; }

    /// <summary>
    /// Gets/sets the queue's access type.
    /// </summary>
    [DataMember(Order = 3, Name = "accessType"), JsonPropertyOrder(3), JsonPropertyName("accessType"), YamlMember(Order = 3, Alias = "accessType")]
    public virtual SolaceQueueAccessType AccessType { get; set; }

    /// <summary>
    /// Gets/sets the maximum amount of message spool that the given queue may use.
    /// </summary>
    [DataMember(Order = 4, Name = "maxMsgSpoolSize"), JsonPropertyOrder(4), JsonPropertyName("maxMsgSpoolSize"), YamlMember(Order = 4, Alias = "maxMsgSpoolSize")]
    public virtual string? MaxMsgSpoolSize { get; set; }

    /// <summary>
    /// Gets/sets the maximum TTL to apply to messages to be spooled. This is documented here.
    /// </summary>
    [DataMember(Order = 5, Name = "maxTtl"), JsonPropertyOrder(5), JsonPropertyName("maxTtl"), YamlMember(Order = 5, Alias = "maxTtl")]
    public virtual string? MaxTtl { get; set; }

}
