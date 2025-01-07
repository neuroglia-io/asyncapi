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
/// Represents the attribute used to define an <see cref="MqttServerBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="version">The binding's version</param>
public class MqttServerBindingAttribute(string name, string version = "latest")
    : ServerBindingAttribute<MqttServerBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets/sets the client identifier.
    /// </summary>
    public virtual string? ClientId { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether to create a persistent connection or not. When false, the connection will is persistent.
    /// </summary>
    public virtual bool CleanSession { get; set; }

    /// <summary>
    /// Gets/sets the topic where the Last Will and Testament message will be sent.
    /// </summary>
    public virtual string? LastWillTopic { get; set; }

    /// <summary>
    /// Gets/sets an integer that defines how hard the broker/client will try to ensure that the Last Will and Testament message is received. Its value MUST be either 0, 1 or 2.
    /// </summary>
    public virtual MqttQualityOfServiceLevel? LastWillQoS { get; set; }

    /// <summary>
    /// Gets/sets the Last Will message
    /// </summary>
    public virtual string? LastWillMessage { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether the broker should retain the Last Will and Testament message or not.
    /// </summary>
    public virtual bool? LastWillRetain { get; set; }

    /// <summary>
    /// Gets/sets an interval in seconds of the longest period of time the broker and the client can endure without sending a message.
    /// </summary>
    public virtual int? KeepAlive { get; set; }

    /// <inheritdoc/>
    public override MqttServerBindingDefinition Build()
    {
        var binding = new MqttServerBindingDefinition()
        {
            BindingVersion = this.Version,
            ClientId = this.ClientId,
            CleanSession = this.CleanSession,
            KeepAlive = this.KeepAlive
        };
        if (!string.IsNullOrWhiteSpace(this.LastWillTopic) || LastWillQoS.HasValue || !string.IsNullOrWhiteSpace(this.LastWillMessage) || LastWillRetain.HasValue) binding.LastWill = new()
        {
            Topic = this.LastWillTopic,
            Message = this.LastWillMessage,
            QoS = LastWillQoS ?? MqttQualityOfServiceLevel.AtMostOne,
            Retain = LastWillRetain ?? false
        };
        return binding;
    }

}
