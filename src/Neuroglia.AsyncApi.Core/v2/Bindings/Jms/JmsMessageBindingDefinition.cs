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

using Json.Schema;

namespace Neuroglia.AsyncApi.v2.Bindings.Jms;

/// <summary>
/// Represents the object used to configure a JMS message binding
/// </summary>
[DataContract]
public record JmsMessageBindingDefinition
    : JmsBindingDefinition, IMessageBindingDefinition
{

    /// <summary>
    /// Gets/sets a schema object containing the definitions for Anypoint MQ-specific headers (so-called protocol headers). This schema MUST be of type object and have a properties key. Examples of Anypoint MQ protocol headers are messageId and messageGroupId.
    /// </summary>
    [DataMember(Order = 1, Name = "headers"), JsonPropertyOrder(1), JsonPropertyName("headers"), YamlMember(Order = 1, Alias = "headers")]
    public virtual JsonSchema? Headers { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding.
    /// </summary>
    [DataMember(Order = 2, Name = "bindingVersion"), JsonPropertyOrder(2), JsonPropertyName("bindingVersion"), YamlMember(Order = 2, Alias = "bindingVersion")]
    public virtual string? BindingVersion { get; set; } = "latest";

}