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
/// Represents an object that
/// </summary>
[DataContract]
public record V3MessageDefinition
    : V3MessageTraitDefinition
{

    /// <summary>
    /// Gets/sets the definition of the message payload. 
    /// If this is a Schema Object, then the schemaFormat will be assumed to be "application/vnd.aai.asyncapi+json;version=asyncapi" where the version is equal to the AsyncAPI Version String.
    /// </summary>
    [DataMember(Order = 1, Name = "payload"), JsonPropertyOrder(1), JsonPropertyName("payload"), YamlMember(Order = 1, Alias = "payload")]
    public virtual V3SchemaDefinition? Payload { get; set; }

    /// <summary>
    /// Gets/sets a list of traits to apply to the message object. Traits MUST be merged using traits merge mechanism. The resulting object MUST be a valid Message Object.
    /// </summary>
    [DataMember(Order = 2, Name = "traits"), JsonPropertyOrder(2), JsonPropertyName("traits"), YamlMember(Order = 2, Alias = "traits")]
    public virtual EquatableList<V3MessageTraitDefinition>? Traits { get; set; }

}