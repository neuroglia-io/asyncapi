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
        /// <param name="bindingFactory">The service used to create <see cref="IChannelBinding"/>s</param>
        public Channel(string key, ChannelDefinition definition, AsyncApiDocument document, IChannelBindingFactory bindingFactory)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (definition == null)
                throw new ArgumentNullException(nameof(definition));
            if (document == null)
                throw new ArgumentNullException(nameof(document));
            this.Key = key;
            this.Document = document;
            this.Definition = definition;
            this.BindingFactory = bindingFactory;
            this.Initialize();
        }

        /// <inheritdoc/>
        public virtual string Key { get; }

        /// <inheritdoc/>
        public ChannelDefinition Definition { get; }

        /// <summary>
        /// Gets the <see cref="AsyncApiDocument"/> that defines the <see cref="IChannel"/>
        /// </summary>
        public virtual AsyncApiDocument Document { get; }

        /// <summary>
        /// Gets the service used to create <see cref="IChannelBinding"/>s
        /// </summary>
        protected IChannelBindingFactory BindingFactory { get; }

        private readonly List<IChannelBinding> _Bindings = new();
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing the <see cref="Channel"/>'s <see cref="IChannelBinding"/>s
        /// </summary>
        protected IReadOnlyCollection<IChannelBinding> Bindings => this._Bindings.AsReadOnly();

        /// <summary>
        /// Gets the <see cref="Channel"/>'s default content type
        /// </summary>
        public string DefaultContentType => this.Document.DefaultContentType;

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
                    || this.Definition.Bindings.Any())
                {
                    bindingDefinition = this.Definition.Bindings.FirstOrDefault(b => b.Protocols.Contains(serversPerProtocol.Key, StringComparer.OrdinalIgnoreCase));
                    if (bindingDefinition == null)
                        continue;
                }
                IChannelBinding binding = this.BindingFactory.CreateBindingFor(this, bindingDefinition, serversPerProtocol);
                this._Bindings.Add(binding);
            }
        }

        /// <inheritdoc/>
        public virtual IDisposable Subscribe(IObserver<IMessage> observer)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));
            if (!this.Definition.DefinesSubscribeOperation)
                throw new NotSupportedException($"The channel with key '{this.Key}' does not support operations of type 'SUB'");
            IChannelBinding binding = this.Bindings.FirstOrDefault(); //todo
            if (binding == null)
                throw new NullReferenceException($"Failed to find a binding for channel with key '{this.Key}'");
            return binding.Subscribe(observer);
        }

        /// <inheritdoc/>
        public virtual async Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!this.Definition.DefinesPublishOperation)
                throw new NotSupportedException($"The channel with key '{this.Key}' does not support operations of type 'PUB'");
            IChannelBinding binding = this.Bindings.FirstOrDefault(); //todo
            if (binding == null)
                throw new NullReferenceException($"Failed to find a binding for channel with key '{this.Key}'");
            await binding.PublishAsync(message, cancellationToken);
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
