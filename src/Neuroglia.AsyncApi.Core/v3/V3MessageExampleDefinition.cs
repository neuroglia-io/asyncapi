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
/// Represents an example of a Message Object.
/// </summary>
[DataContract]
public record V3MessageExampleDefinition
{

    /// <summary>
    /// Gets/sets the message's headers. MUST validate against the Message Object's headers field.
    /// </summary>
    [DataMember(Order = 1, Name = "headers"), JsonPropertyOrder(1), JsonPropertyName("headers"), YamlMember(Order = 1, Alias = "headers")]
    public virtual EquatableDictionary<string, object>? Headers { get; set; }

    /// <summary>
    /// Gets/sets the message's payload. MUST validate against the Message Object's payload field.
    /// </summary>
    [DataMember(Order = 2, Name = "payload"), JsonPropertyOrder(2), JsonPropertyName("payload"), YamlMember(Order = 2, Alias = "payload")]
    public virtual EquatableDictionary<string, object>? Payload { get; set; }

    /// <summary>
    /// Gets/sets a machine-friendly name.
    /// </summary>
    [DataMember(Order = 3, Name = "name"), JsonPropertyOrder(3), JsonPropertyName("name"), YamlMember(Order = 3, Alias = "name")]
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets/sets a short summary of what the example is about.
    /// </summary>
    [DataMember(Order = 4, Name = "summary"), JsonPropertyOrder(4), JsonPropertyName("summary"), YamlMember(Order = 4, Alias = "summary")]
    public virtual string? Summary { get; set; }

}