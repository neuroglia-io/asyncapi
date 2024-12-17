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

namespace Neuroglia.AsyncApi.v3;

/// <summary>
/// Represents an object that describes a specific operation.
/// </summary>
[DataContract]
public record V3OperationDefinition
    : V3OperationTraitDefinition
{

    /// <summary>
    /// Gets/sets the operation's action
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "action"), JsonPropertyOrder(1), JsonPropertyName("action"), YamlMember(Order = 1, Alias = "action", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual V3OperationAction Action { get; set; }

    /// <summary>
    /// Gets/sets a $ref pointer to the definition of the channel in which this operation is performed.
    /// </summary>
    [Required]
    [DataMember(Order = 2, Name = "channel"), JsonPropertyOrder(2), JsonPropertyName("channel"), YamlMember(Order = 2, Alias = "channel", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual V3ReferenceDefinition Channel { get; set; } = null!;

    /// <summary>
    /// Gets/sets a list of traits to apply to the operation object. Traits MUST be merged using traits merge mechanism. The resulting object MUST be a valid Operation Object.
    /// </summary>
    [DataMember(Order = 3, Name = "traits"), JsonPropertyOrder(3), JsonPropertyName("traits"), YamlMember(Order = 3, Alias = "traits", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual EquatableList<V3OperationTraitDefinition>? Traits { get; set; }

    /// <summary>
    /// Gets/sets a list of $ref pointers pointing to the supported Message Objects that can be processed by this operation.
    /// </summary>
    [DataMember(Order = 4, Name = "messages"), JsonPropertyOrder(4), JsonPropertyName("messages"), YamlMember(Order = 4, Alias = "messages", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual EquatableList<V3ReferenceDefinition>? Messages { get; set; }

    /// <summary>
    /// Gets/sets the definition of the reply in a request-reply operation.
    /// </summary>
    [DataMember(Order = 5, Name = "reply"), JsonPropertyOrder(5), JsonPropertyName("reply"), YamlMember(Order = 5, Alias = "reply", ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual V3OperationReplyDefinition? Reply { get; set; }

}
