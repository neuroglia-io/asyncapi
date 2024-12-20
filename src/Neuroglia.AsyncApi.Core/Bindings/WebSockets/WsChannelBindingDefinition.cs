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
using Neuroglia.AsyncApi.Bindings;
using Neuroglia.AsyncApi.Bindings.Http;

namespace Neuroglia.AsyncApi.Bindings.WebSockets;

/// <summary>
/// Represents the object used to configure an WebSocket channel binding
/// </summary>
[DataContract]
public record WsChannelBindingDefinition
    : WsBindingDefinition, IChannelBindingDefinition
{

    /// <summary>
    /// Gets/sets the <see cref="WsChannelBindingDefinition"/>'s operation type
    /// </summary>
    [DataMember(Order = 1, Name = "type"), JsonPropertyOrder(1), JsonPropertyName("type"), YamlMember(Order = 1, Alias = "type")]
    public virtual HttpBindingOperationType Type { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="JsonSchema"/> containing the definitions for each query parameter. This schema MUST be of type object and have a properties key.
    /// </summary>
    [DataMember(Order = 2, Name = "query"), JsonPropertyOrder(2), JsonPropertyName("query"), YamlMember(Order = 2, Alias = "query")]
    public virtual JsonSchema? Query { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="JsonSchema"/> containing the definitions for HTTP-specific headers. This schema MUST be of type object and have a properties key.
    /// </summary>
    [DataMember(Order = 3, Name = "headers"), JsonPropertyOrder(3), JsonPropertyName("headers"), YamlMember(Order = 3, Alias = "headers")]
    public virtual JsonSchema? Headers { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding. Defaults to 'latest'.
    /// </summary>
    [DataMember(Order = 4, Name = "bindingVersion"), JsonPropertyOrder(4), JsonPropertyName("bindingVersion"), YamlMember(Order = 4, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "latest";

}
