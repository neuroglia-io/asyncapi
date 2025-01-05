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

using MQTTnet;

namespace Neuroglia.AsyncApi.Client.Bindings.Mqtt;

/// <summary>
/// Represents an object used to describe the result of an MQTT publish operation
/// </summary>
/// <param name="connectResultCode">The operation's <see cref="MqttClientConnectResultCode"/></param>
/// <param name="publishReasonCode">The operation's <see cref="MqttClientPublishReasonCode"/></param>
/// <param name="reason">The reason for the operation's failure, if any</param>
/// <param name="packetIdentifier">The operation's packet identifier</param>
public class MqttPublishOperationResult(MqttClientConnectResultCode? connectResultCode, MqttClientPublishReasonCode? publishReasonCode = null, string? reason = null, ushort? packetIdentifier = null)
    : AsyncApiPublishOperationResult
{

    /// <summary>
    /// Gets the operation's <see cref="MqttClientConnectResultCode"/>
    /// </summary>
    public virtual MqttClientConnectResultCode? ConnectResultCode { get; } = connectResultCode;

    /// <summary>
    /// Gets the operation's <see cref="MqttClientPublishReasonCode"/>
    /// </summary>
    public virtual MqttClientPublishReasonCode? PublishReasonCode { get; } = publishReasonCode;

    /// <summary>
    /// Gets the reason for the operation's failure, if any
    /// </summary>
    public virtual string? Reason { get; } = reason;

    /// <summary>
    /// Gets the operation's packet identifier
    /// </summary>
    public virtual ushort? PacketIdentifier { get; } = packetIdentifier;

    /// <inheritdoc/>
    public override bool IsSuccessful => ConnectResultCode.HasValue && ConnectResultCode == MqttClientConnectResultCode.Success && PublishReasonCode.HasValue && PublishReasonCode == MqttClientPublishReasonCode.Success;

}
