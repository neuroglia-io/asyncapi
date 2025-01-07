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
/// Represents an object used to describe the result of an MQTT subscribe operation
/// </summary>
/// <param name="connectResultCode">The operation's <see cref="MqttClientConnectResultCode"/></param>
/// <param name="results">The results for the operation's topic filters</param>
/// <param name="reason">The reason for the operation's failure, if any</param>
/// <param name="packetIdentifier">The operation's packet identifier</param>
/// <param name="messages">An <see cref="IObservable{T}"/>, if any, used to observe incoming <see cref="IAsyncApiMessage"/>s</param>
public class MqttSubscribeOperationResult(MqttClientConnectResultCode? connectResultCode, IEnumerable<MqttClientSubscribeResultItem>? results = null, string? reason = null, ushort? packetIdentifier = null, IObservable<IAsyncApiMessage>? messages = null)
    : AsyncApiSubscribeOperationResult
{

    /// <summary>
    /// Gets the operation's <see cref="MqttClientConnectResultCode"/>
    /// </summary>
    public virtual MqttClientConnectResultCode? ConnectResultCode { get; } = connectResultCode;

    /// <summary>
    /// Gets the results for the operation's topic filters
    /// </summary>
    public virtual IEnumerable<MqttClientSubscribeResultItem>? Results { get; } = results;

    /// <summary>
    /// Gets the reason for the operation's failure, if any
    /// </summary>
    public virtual string? Reason { get; } = reason;

    /// <summary>
    /// Gets the operation's packet identifier
    /// </summary>
    public virtual ushort? PacketIdentifier { get; } = packetIdentifier;

    /// <inheritdoc/>
    public override IObservable<IAsyncApiMessage>? Messages { get; } = messages;

    /// <inheritdoc/>
    public override bool IsSuccessful => ConnectResultCode.HasValue && ConnectResultCode == MqttClientConnectResultCode.Success && Results?.Any() == true && Results.All(r => r.ResultCode == MqttClientSubscribeResultCode.GrantedQoS0 || r.ResultCode == MqttClientSubscribeResultCode.GrantedQoS1 || r.ResultCode == MqttClientSubscribeResultCode.GrantedQoS2);

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (!disposing) return;
        if (Messages is IDisposable disposable) disposable.Dispose();
        base.Dispose(disposing);
    }

    /// <inheritdoc/>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        switch (Messages) 
        {
            case IAsyncDisposable asyncDisposable:
                await base.DisposeAsync(disposing).ConfigureAwait(false);
                break;
            case IDisposable disposable:
                disposable.Dispose();
                break;
        }
        await base.DisposeAsync(disposing).ConfigureAwait(false);
    }

}
