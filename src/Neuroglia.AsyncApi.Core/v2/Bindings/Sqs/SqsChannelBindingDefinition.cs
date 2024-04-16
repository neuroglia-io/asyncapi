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

namespace Neuroglia.AsyncApi.v2.Bindings.Sqs;

/// <summary>
/// Represents the object used to configure a SQS channel binding
/// </summary>
[DataContract]
public record SqsChannelBindingDefinition
    : SqsBindingDefinition, IChannelBindingDefinition
{

    /// <summary>
    /// Gets/sets a definition of the queue that will be used as the channel.
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "queue"), JsonPropertyOrder(1), JsonPropertyName("queue"), YamlMember(Order = 1, Alias = "queue")]
    public virtual SqsQueueDefinition Queue { get; set; } = null!;

    /// <summary>
    /// Gets/sets a definition of the queue, if any, that will be used for un-processable messages.
    /// </summary>
    [Required]
    [DataMember(Order = 2, Name = "deadLetterQueue"), JsonPropertyOrder(2), JsonPropertyName("deadLetterQueue"), YamlMember(Order = 2, Alias = "deadLetterQueue")]
    public virtual SqsQueueDefinition? DeadLetterQueue { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding.
    /// </summary>
    [DataMember(Order = 3, Name = "bindingVersion"), JsonPropertyOrder(3), JsonPropertyName("bindingVersion"), YamlMember(Order = 3, Alias = "bindingVersion")]
    public virtual string? BindingVersion { get; set; } = "latest";

}
