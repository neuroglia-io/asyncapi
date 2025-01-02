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
/// Represents the attribute used to define an <see cref="SolaceServerBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="msgVpn">The Virtual Private Network name on the Solace broker</param>
/// <param name="version">The binding's version</param>
public class SolaceServerBindingAttribute(string name, string msgVpn, string version = "latest")
    : ServerBindingAttribute<SolaceServerBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets/sets the Virtual Private Network name on the Solace broker.
    /// </summary>
    public virtual string MsgVpn { get; } = msgVpn;

    /// <summary>
    /// Gets/sets a unique client name to use to register to the appliance. If specified, it must be a valid Topic name, and a maximum of 160 bytes in length when encoded as UTF-8.
    /// </summary>
    public virtual string? ClientName { get; set; }

    /// <inheritdoc/>
    public override SolaceServerBindingDefinition Build() => new()
    {
        BindingVersion = this.Version,
        MsgVpn = this.MsgVpn,
        ClientName = this.ClientName
    };

}
