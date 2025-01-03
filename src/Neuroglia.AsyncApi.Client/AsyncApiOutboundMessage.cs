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

namespace Neuroglia.AsyncApi.Client;

/// <summary>
/// Represents a message sent to an AsyncAPI application
/// </summary>
/// <param name="operation">The unique operation identifier that this outbound message corresponds to</param>
/// <param name="server">The identifier, if any, used to specify the target server for this message</param>
/// <param name="protocol">The protocol to use, if any, to send the message</param>
public class AsyncApiOutboundMessage(string operation, string? server = null, string? protocol = null)
    : IDisposable, IAsyncDisposable
{

    bool _disposed;

    /// <summary>
    /// Gets the unique operation identifier that this outbound message corresponds to
    /// </summary>
    public virtual string Operation { get; } = operation;

    /// <summary>
    /// Gets the identifier, if any, used to specify the target server for this message.<para></para>
    /// If not set, the server will be determined by one of the following, in order:<para></para>
    /// 1) The first server that matches the specified protocol (if any).<para></para>
    /// 2) If a single server is defined at the channel level, that server will be used.<para></para>
    /// 3) If a single server is defined at the top level, that server will be used.<para></para>
    /// 4) The first server defined by the channel, if any.<para></para>
    /// 5) The first server defined at the top level.<para></para>
    /// </summary>
    public virtual string? Server { get; } = server;

    /// <summary>
    /// Gets/sets a key/value mapping of the variables, if any, to use for the targeted server
    /// </summary>
    public virtual IDictionary<string, string> ServerVariables { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Gets the protocol to use, if any, to send the message.<para></para>
    /// If set, the protocol is used to discriminate the server to send the message to.<para></para>
    /// If not set, the protocol is inferred by the binding of the target server.
    /// </summary>
    public virtual string? Protocol { get; } = protocol;

    /// <summary>
    /// Gets/sets the message's payload, if any
    /// </summary>
    public virtual object? Payload { get; init; }

    /// <summary>
    /// Gets/sets the message's headers, if any
    /// </summary>
    public virtual object? Headers { get; init; }

    /// <summary>
    /// Disposes of the <see cref="AsyncApiOutboundMessage"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="AsyncApiOutboundMessage"/> is being disposed of</param>
    protected virtual ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {

            }
            _disposed = true;
        }
        return ValueTask.CompletedTask;
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes of the <see cref="AsyncApiOutboundMessage"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="AsyncApiOutboundMessage"/> is being disposed of</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {

            }
            _disposed = true;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

}