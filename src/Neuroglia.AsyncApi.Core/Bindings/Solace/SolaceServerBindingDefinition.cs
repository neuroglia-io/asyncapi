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

using Neuroglia.AsyncApi.Bindings;

namespace Neuroglia.AsyncApi.Bindings.Solace;

/// <summary>
/// Represents the object used to configure a JMS server binding
/// </summary>
[DataContract]
public record SolaceServerBindingDefinition
    : SolaceBindingDefinition, IServerBindingDefinition
{

    /// <summary>
    /// Gets/sets the version of this binding.
    /// </summary>
    [DataMember(Order = 1, Name = "bindingVersion"), JsonPropertyOrder(1), JsonPropertyName("bindingVersion"), YamlMember(Order = 1, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "0.4.0";

    /// <summary>
    /// Gets/sets the Virtual Private Network name on the Solace broker.
    /// </summary>
    [Required]
    [DataMember(Order = 2, Name = "msgVpn"), JsonPropertyOrder(2), JsonPropertyName("msgVpn"), YamlMember(Order = 2, Alias = "msgVpn")]
    public virtual string MsgVpn { get; set; } = null!;

    /// <summary>
    /// Gets/sets a unique client name to use to register to the appliance. If specified, it must be a valid Topic name, and a maximum of 160 bytes in length when encoded as UTF-8.
    /// </summary>
    [DataMember(Order = 3, Name = "clientName"), JsonPropertyOrder(3), JsonPropertyName("clientName"), YamlMember(Order = 3, Alias = "clientName")]
    public virtual string? ClientName { get; set; }

}
