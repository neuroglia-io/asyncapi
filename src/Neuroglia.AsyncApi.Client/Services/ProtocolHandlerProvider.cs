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

namespace Neuroglia.AsyncApi.Client.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IProtocolHandlerProvider"/> interface
/// </summary>
/// <param name="handlers">An <see cref="IEnumerable{T}"/> containing all registered <see cref="IProtocolHandler"/>s</param>
public class ProtocolHandlerProvider(IEnumerable<IProtocolHandler> handlers)
    : IProtocolHandlerProvider
{

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing all registered <see cref="IProtocolHandler"/>s
    /// </summary>
    protected IEnumerable<IProtocolHandler> Handlers { get; } = handlers;

    /// <inheritdoc/>
    public virtual IProtocolHandler GetHandlerFor(string protocol, string? protocolVersion = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(protocol);
        return this.Handlers.FirstOrDefault(h => h.Supports(protocol, protocolVersion)) ?? throw new NullReferenceException($"Failed to find an handler for the specified protocol '{(string.IsNullOrWhiteSpace(protocolVersion) ? protocol : $"{protocol} version {protocolVersion}")}'");
    }

}
