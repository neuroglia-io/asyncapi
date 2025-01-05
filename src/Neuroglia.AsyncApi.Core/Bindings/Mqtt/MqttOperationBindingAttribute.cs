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

namespace Neuroglia.AsyncApi.Bindings.Mqtt;

/// <summary>
/// Represents the attribute used to define an <see cref="MqttOperationBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="version">The binding's version</param>
public class MqttOperationBindingAttribute(string name, string version = "latest")
    : OperationBindingAttribute<MqttOperationBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets/sets an integer that defines the Quality of Service (QoS) levels for the message flow between client and server. Its value MUST be either 0 (At most once delivery), 1 (At least once delivery), or 2 (Exactly once delivery).
    /// </summary>
    public virtual MqttQualityOfServiceLevel QoS { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the broker the broker should retain the message or not.
    /// </summary>
    public virtual bool Retain { get; init; }

    /// <inheritdoc/>
    public override MqttOperationBindingDefinition Build() => new()
    {
        BindingVersion = this.Version,
        QoS = this.QoS,
        Retain = this.Retain
    };

}
