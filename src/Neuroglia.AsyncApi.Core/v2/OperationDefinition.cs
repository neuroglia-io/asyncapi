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

namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Represents an object used to define a Async API operation
/// </summary>
[DataContract]
public record OperationDefinition
    : OperationTraitDefinition
{

    /// <summary>
    /// Gets/sets a <see cref="List{T}"/> of traits to apply to the operation object. Traits MUST be merged into the operation object using the JSON Merge Patch algorithm in the same order they are defined here.
    /// </summary>
    [DataMember(Order = 1, Name = "traits"), JsonPropertyOrder(1), JsonPropertyName("traits"), YamlMember(Order = 1, Alias = "traits")]
    public virtual EquatableList<OperationTraitDefinition>? Traits { get; set; }

    /// <summary>
    /// Gets/sets a definition of the message(s) that will be published or received on this channel.
    /// </summary>
    [DataMember(Order = 2, Name = "message"), JsonPropertyOrder(2), JsonPropertyName("message"), YamlMember(Order = 2, Alias = "message")]
    public virtual OperationMessageDefinition? Message { get; set; }

}