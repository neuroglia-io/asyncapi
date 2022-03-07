/*
 * Copyright © 2021 Neuroglia SPRL. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Models.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Client.Services
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IChannel"/> interface
    /// </summary>
    public class Channel
        : IChannel
    {

        /// <summary>
        /// Initializes a new <see cref="Channel"/>
        /// </summary>
        /// <param name="key">The <see cref="IChannel"/>'s key</param>
        /// <param name="definition">The <see cref="IChannel"/>'s <see cref="ChannelDefinition"/></param>
        /// <param name="document">The <see cref="AsyncApiDocument"/> that defines the <see cref="IChannel"/></param>
        /// <param name="bindingFactories">An <see cref="IEnumerable{T}"/> containing all available <see cref="IChannelBindingFactory"/> services</param>
        public Channel(string key, ChannelDefinition definition, AsyncApiDocument document, IEnumerable<IChannelBindingFactory> bindingFactories)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            this.Key = key;
            this.Document = document ?? throw new ArgumentNullException(nameof(document));
            this.Definition = definition ?? throw new ArgumentNullException(nameof(definition));
            this.BindingFactories = bindingFactories;
            this.Initialize();
        }

        /// <inheritdoc/>
        public virtual string Key { get; }

        /// <inheritdoc/>
        public ChannelDefinition Definition { get; }

        /// <inheritdoc/>
        public virtual AsyncApiDocument Document { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing all available <see cref="IChannelBindingFactory"/> services
        /// </summary>
        protected IEnumerable<IChannelBindingFactory> BindingFactories { get; }

        private readonly List<IChannelBinding> _Bindings = new();
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="Channel"/>'s <see cref="IChannelBinding"/>s
        /// </summary>
        protected IReadOnlyCollection<IChannelBinding> Bindings => this._Bindings.AsReadOnly();

        /// <summary>
        /// Gets the <see cref="Channel"/>'s default content type
        /// </summary>
        public string DefaultContentType => string.IsNullOrWhiteSpace(this.Document.DefaultContentType) ? MediaTypeNames.Application.Json : this.Document.DefaultContentType;

        /// <summary>
        /// Initializes the <see cref="Channel"/>
        /// </summary>
        protected virtual void Initialize()
        {
            foreach (IGrouping<string, KeyValuePair<string, ServerDefinition>> serversPerProtocol in this.Document.Servers
                .GroupBy(s => s.Value.Protocol))
            {
                IChannelBindingDefinition bindingDefinition = null;
                if (this.Definition.Bindings != null
                    && this.Definition.Bindings.Any())
                {
                    bindingDefinition = this.Definition.Bindings.FirstOrDefault(b => b.Protocols.Contains(serversPerProtocol.Key, StringComparer.OrdinalIgnoreCase));
                    if (bindingDefinition == null)
                        continue;
                }
                IChannelBindingFactory bindingFactory = this.BindingFactories.FirstOrDefault(b => b.SupportsProtocol(serversPerProtocol.Key));
                if (bindingFactory == null)
                    continue;
                IChannelBinding binding = bindingFactory.CreateBinding(this, serversPerProtocol);
                this._Bindings.Add(binding);
            }
        }

        /// <inheritdoc/>
        public virtual async Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!this.Definition.DefinesPublishOperation)
                throw new NotSupportedException($"The channel with key '{this.Key}' does not support operations of type 'PUB'");
            IChannelBinding binding = this.Bindings.FirstOrDefault(); //todo: check prefered binding based on specified protocol
            if (binding == null)
                throw new NullReferenceException($"Failed to find a binding for channel with key '{this.Key}'");
            await binding.PublishAsync(message, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<IDisposable> SubscribeAsync(IObserver<IMessage> observer, CancellationToken cancellationToken = default)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));
            if (!this.Definition.DefinesSubscribeOperation)
                throw new NotSupportedException($"The channel with key '{this.Key}' does not support operations of type 'SUB'");
            IChannelBinding binding = this.Bindings.FirstOrDefault(); //todo: check prefered binding based on specified protocol
            if (binding == null)
                throw new NullReferenceException($"Failed to find a binding for channel with key '{this.Key}'");
            return await binding.SubscribeAsync(observer, cancellationToken);
        }

        private bool _Disposed;
        /// <summary>
        /// Disposes of the <see cref="Channel"/>
        /// </summary>
        /// <param name="disposing">A boolean indicating whether or not the <see cref="Channel"/> is being disposed of</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                    this._Bindings.ForEach(b => b.Dispose());
                this._Disposed = true;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }

}
