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
/// Represents an object used to describe a message broker, a server or any other kind of computer program capable of sending and/or receiving data.
/// </summary>
/// <remarks>
/// This object is used to capture details such as URIs, protocols and security configuration. 
/// Variable substitution can be used so that some details, for example usernames and passwords, can be injected by code generation tools.
/// </remarks>
[DataContract]
public record V2ServerDefinition
{

    /// <summary>
    /// Gets/sets the <see cref="Uri"/> pointing to the contact information.
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "url"), JsonPropertyOrder(1), JsonPropertyName("url"), YamlMember(Order = 1, Alias = "url")]
    public virtual Uri Url { get; set; } = null!;

    /// <summary>
    /// Gets/sets the protocol this URL supports for connection. 
    /// Supported protocol include, but are not limited to: amqp, amqps, http, https, ibmmq, jms, kafka, kafka-secure, mqtt, secure-mqtt, stomp, stomps, ws, wss, mercure.
    /// </summary>
    [Required]
    [DataMember(Order = 2, Name = "protocol"), JsonPropertyOrder(2), JsonPropertyName("protocol"), YamlMember(Order = 2, Alias = "protocol")]
    public virtual string Protocol { get; set; } = null!;

    /// <summary>
    /// Gets/sets the version of the protocol used for connection. For instance: AMQP 0.9.1, HTTP 2.0, Kafka 1.0.0, etc.
    /// </summary>
    [Required]
    [DataMember(Order = 3, Name = "protocolVersion"), JsonPropertyOrder(3), JsonPropertyName("protocolVersion"), YamlMember(Order = 3, Alias = "protocolVersion")]
    public virtual string ProtocolVersion { get; set; } = null!;

    /// <summary>
    /// Gets/sets an optional string describing the host designated by the URL. <see href="https://spec.commonmark.org/">CommonMark</see> syntax MAY be used for rich text representation.
    /// </summary>
    [DataMember(Order = 4, Name = "description"), JsonPropertyOrder(4), JsonPropertyName("description"), YamlMember(Order = 4, Alias = "description")]
    public virtual string? Description { get; set; }

    /// <summary>
    /// Gets/sets an optional string describing the host designated by the URL. <see href="https://spec.commonmark.org/">CommonMark</see> syntax MAY be used for rich text representation.
    /// </summary>
    [DataMember(Order = 5, Name = "variables"), JsonPropertyOrder(5), JsonPropertyName("variables"), YamlMember(Order = 5, Alias = "variables")]
    public virtual EquatableDictionary<string, ServerVariableDefinition>? Variables { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="IList{T}"/> of values that represent alternative security requirement objects that can be used. 
    /// Only one of the security requirement objects need to be satisfied to authorize a connection or operation. 
    /// </summary>
    [DataMember(Order = 6, Name = "security"), JsonPropertyOrder(6), JsonPropertyName("security"), YamlMember(Order = 6, Alias = "security")]
    public virtual EquatableDictionary<string, object>? Security { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the <see cref="V2ServerDefinition"/>'s <see cref="IServerBindingDefinition"/>s
    /// </summary>
    [DataMember(Order = 7, Name = "bindings"), JsonPropertyOrder(7), JsonPropertyName("bindings"), YamlMember(Order = 7, Alias = "bindings")]
    public virtual ServerBindingDefinitionCollection? Bindings { get; set; }

    /// <summary>
    /// Interpolates the defined server's url variables
    /// </summary>
    /// <returns>The interpolated server url</returns>
    public virtual Uri InterpolateUrlVariables()
    {
        if (Variables == null || Variables.Count == 0) return Url;
        var rawUrl = Url.ToString();
        foreach (var variable in Variables)
        {
            var value = variable.Value.Default;
            if (string.IsNullOrWhiteSpace(value)) value = variable.Value.Enum?.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(value)) continue;
            rawUrl = rawUrl.Replace($"{{{variable.Key}}}", value);
        }
        return new Uri(rawUrl, UriKind.RelativeOrAbsolute);
    }

    /// <inheritdoc/>
    public override string ToString() => this.Url.ToString();

}
