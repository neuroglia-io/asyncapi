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
using Microsoft.Extensions.Logging;
using Neuroglia.AsyncApi.Models;
using Neuroglia.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Mime;
using System.Reactive.Subjects;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Client.Services
{

    /// <summary>
    /// Represents the base class for all implementations of the <see cref="IChannelBinding"/> interface
    /// </summary>
    public abstract class ChannelBindingBase
        : IChannelBinding
    {

        /// <summary>
        /// Initializes a new <see cref="ChannelBindingBase"/>
        /// </summary>
        /// <param name="loggerFactory">The service used to create <see cref="ILogger"/>s</param>
        /// <param name="serializers">The service used to provide <see cref="ISerializer"/>s</param>
        /// <param name="channel">The <see cref="IChannel"/> the <see cref="ChannelBindingBase"/> belongs to</param>
        /// <param name="servers">An <see cref="IEnumerable{T}"/> containing the mappings of the <see cref="ServerDefinition"/>s available to the <see cref="IChannelBinding"/></param>
        protected ChannelBindingBase(ILoggerFactory loggerFactory, ISerializerProvider serializers, IChannel channel, IEnumerable<KeyValuePair<string, ServerDefinition>> servers)
        {
            this.Logger = loggerFactory.CreateLogger(this.GetType());
            this.Serializers = serializers;
            this.Channel = channel;
            this.Servers = servers;
            this.OnMessageSubject = new();
            this.CancellationTokenSource = new();
        }

        /// <summary>
        /// Gets the service used to perform logging
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the service used to provide <see cref="ISerializer"/>s
        /// </summary>
        protected ISerializerProvider Serializers { get; }

        /// <summary>
        /// Gets the <see cref="IChannel"/> the <see cref="ChannelBindingBase"/> belongs to
        /// </summary>
        protected IChannel Channel { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the mappings of the <see cref="ServerDefinition"/>s available to the <see cref="IChannelBinding"/>
        /// </summary>
        protected IEnumerable<KeyValuePair<string, ServerDefinition>> Servers { get; }

        /// <summary>
        /// Gets the <see cref="Subject{T}"/> used to observe <see cref="IMessage"/>s produced or consumed by the <see cref="ChannelBindingBase"/>
        /// </summary>
        protected Subject<IMessage> OnMessageSubject { get; }

        /// <summary>
        /// Gets the <see cref="ChannelBindingBase"/>'s <see cref="System.Threading.CancellationTokenSource"/>
        /// </summary>
        protected CancellationTokenSource CancellationTokenSource { get; }

        /// <inheritdoc/>
        public abstract Task PublishAsync(IMessage message, CancellationToken cancellationToken = default);

        /// <inheritdoc/>
        public virtual async Task<IDisposable> SubscribeAsync(IObserver<IMessage> observer, CancellationToken cancellationToken = default)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));
            return await Task.FromResult(this.OnMessageSubject.Subscribe(observer));
        }

        /// <summary>
        /// Serializes the specified object
        /// </summary>
        /// <param name="graph">The object to serialize</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The serialized object</returns>
        protected virtual async Task<byte[]> SerializeAsync(object graph, CancellationToken cancellationToken = default)
        {
            if (graph == null)
                return Array.Empty<byte>();
            string contentType = this.Channel.Definition.Publish.Message.ContentType;
            if (string.IsNullOrWhiteSpace(contentType))
                contentType = this.Channel.DefaultContentType;
            if (string.IsNullOrWhiteSpace(contentType))
            {
                contentType = MediaTypeNames.Application.Json;
                this.Logger.LogWarning($"Failed to determine the content type for messages published on the channel with key '{{channelKey}}'. Defaulting to '{contentType}'", this.Channel.Key);
            }   
            ISerializer serializer = this.Serializers.GetSerializerFor(contentType);
            if (serializer == null)
                throw new NullReferenceException($"Failed to find a serializer for the specified content type '{contentType}'");
            return await serializer.SerializeAsync(graph, cancellationToken);
        }

        /// <summary>
        /// Deserializes the specified data
        /// </summary>
        /// <param name="data">The data to deserialize</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The deserialized data</returns>
        protected virtual async Task<object> DeserializeAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            if (data == null)
                return null;
            string contentType = this.Channel.Definition.Subscribe.Message.ContentType;
            if (string.IsNullOrWhiteSpace(contentType))
                contentType = this.Channel.DefaultContentType;
            if (string.IsNullOrWhiteSpace(contentType))
            {
                contentType = MediaTypeNames.Application.Json;
                this.Logger.LogWarning($"Failed to determine the content type for messages published on the channel with key '{{channelKey}}'. Defaulting to '{contentType}'", this.Channel.Key);
            }
            ISerializer serializer = this.Serializers.GetSerializerFor(contentType);
            if (serializer == null)
                throw new NullReferenceException($"Failed to find a serializer for the specified content type '{contentType}'");
            return await serializer.DeserializeAsync<JToken>(data, cancellationToken);
        }

        /// <summary>
        /// Computes the channel key for the specified <see cref="IMessage"/>
        /// </summary>
        /// <param name="message">The <see cref="IMessage"/> to compute the channel key for</param>
        /// <returns>The channel key computed for the specified <see cref="IMessage"/></returns>
        protected virtual string ComputeChannelKeyFor(IMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            string channelKey = this.Channel.Key;
            if (this.Channel.Definition.Parameters == null
                || !this.Channel.Definition.Parameters.Any())
                return channelKey;
            foreach (KeyValuePair<string, ParameterDefinition> parameterDefinition in this.Channel.Definition.Parameters)
            {
                string[] components = parameterDefinition.Value.LocationExpression.Fragment.Split("/", StringSplitOptions.RemoveEmptyEntries);
                JToken parameter = null;
                switch (parameterDefinition.Value.LocationExpression.Source)
                {
                    case RuntimeExpressionSource.Header:
                        if (!message.Headers.TryGetValue(components.First(), out object value))
                            break;
                        parameter = (JToken)value;
                        for (int i = 1; i < components.Length; i++)
                        {
                            if (parameter is not JObject jobject)
                                break;
                            JProperty property = jobject.Property(components[i]);
                            if (property == null)
                                break;
                            parameter = property.Value;
                        }
                        break;
                    case RuntimeExpressionSource.Payload:
                        parameter = message.Payload as JToken;
                        for (int i = 0; i < components.Length; i++)
                        {
                            if (parameter is not JObject jobject)
                                break;
                            JProperty property = jobject.Property(components[i]);
                            if (property == null)
                                break;
                            parameter = property.Value;
                        }
                        break;
                    default:
                        throw new NotSupportedException($"The specified {nameof(RuntimeExpressionSource)} '{this.Channel.Definition.Subscribe.Message.CorrelationId.LocationExpression.Source}' is not supported");
                }
                if (parameter == null)
                    continue;
                channelKey = channelKey.Replace($"{{{parameterDefinition.Key}}}", parameter.ToString());
            }
            return channelKey;
        }

        /// <summary>
        /// Injects the correlation key into the specified published <see cref="IMessage"/>
        /// </summary>
        /// <param name="message">The published <see cref="IMessage"/> to inject the correlation key into</param>
        protected virtual void InjectCorrelationKeyInto(IMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (message.CorrelationKey == null
                || this.Channel.Definition.Publish.Message.CorrelationId == null)
                return;
            string[] components = this.Channel.Definition.Publish.Message.CorrelationId.LocationExpression.Fragment.Split("/", StringSplitOptions.RemoveEmptyEntries);
            object destination = null;
            switch (this.Channel.Definition.Publish.Message.CorrelationId.LocationExpression.Source)
            {
                case RuntimeExpressionSource.Header:
                    if (!message.Headers.TryGetValue(components.First(), out destination))
                    {
                        string headerKey = components.First();
                        object headerValue = message.CorrelationKey;
                        if (components.Length > 1)
                        {
                            for(int i = 0; i < components.Length - 1; i++)
                            {
                                string property = components.Reverse().ElementAt(i);
                                ExpandoObject expando = new();
                                expando.TryAdd(property, headerValue);
                                headerValue = expando;
                            }
                        }
                        message.Headers.Add(headerKey, headerValue);
                    }
                    break;
                case RuntimeExpressionSource.Payload:
                    destination = message.Payload;
                    break;
                default:
                    throw new NotSupportedException($"The specified {nameof(RuntimeExpressionSource)} '{this.Channel.Definition.Subscribe.Message.CorrelationId.LocationExpression.Source}' is not supported");
            }
            if(destination != null)
            {
                for (int i = 0; i < components.Length; i++)
                {
                    if (destination == null)
                        break;
                    PropertyInfo property = destination.GetType().GetProperty(components[i], BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
                    if (property == null)
                        break;
                    if (i == components.Length - 1)
                        property.SetValue(destination, message.CorrelationKey);
                    else
                        destination = property.GetValue(destination);
                }
            }
        }

        /// <summary>
        /// Extracts the correlation key for the specified consumed <see cref="IMessage"/>
        /// </summary>
        /// <param name="message">The consumed <see cref="IMessage"/> to extract the correlation key from</param>
        /// <returns>The extracted correlation key</returns>
        protected virtual JToken ExtractCorrelationKeyFrom(IMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            JToken correlationKey = null;
            int index = 0;
            if (this.Channel.Definition.Subscribe.Message.CorrelationId != null)
            {
                string[] components = this.Channel.Definition.Subscribe.Message.CorrelationId.LocationExpression.Fragment.Split("/", StringSplitOptions.RemoveEmptyEntries);
                switch (this.Channel.Definition.Subscribe.Message.CorrelationId.LocationExpression.Source)
                {
                    case RuntimeExpressionSource.Header:
                        if (!message.Headers.TryGetValue(components.First(), out object value))
                            break;
                        correlationKey = (JToken)value;
                        index = 1;
                        break;
                    case RuntimeExpressionSource.Payload:
                        correlationKey = message.Payload as JToken;
                        break;
                    default:
                        throw new NotSupportedException($"The specified {nameof(RuntimeExpressionSource)} '{this.Channel.Definition.Subscribe.Message.CorrelationId.LocationExpression.Source}' is not supported");
                }
                if(correlationKey != null)
                {
                    for (int i = index; i < components.Length; i++)
                    {
                        if (correlationKey == null 
                            || correlationKey is not JObject jobject)
                            break;
                        JProperty property = jobject.Property(components[i]);
                        if (property == null)
                            break;
                        correlationKey = property.Value;
                    }
                }
            }
            return correlationKey;
        }

        /// <summary>
        /// Validates the specified <see cref="IMessage"/>
        /// </summary>
        /// <param name="message">The <see cref="IMessage"/> to validate</param>
        protected virtual void ValidateMessage(IMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            JToken payload = JToken.FromObject(message.Payload);
            JSchema schema = this.Channel.Definition.Publish.Message.Payload.ToObject<JSchema>();
            if (!payload.IsValid(schema, out IList<string> errors))
                throw new FormatException($"The specified message is invalid:{Environment.NewLine}{string.Join(Environment.NewLine, errors)}");
        }

        private bool _Disposed;
        /// <summary>
        /// Disposes of the <see cref="ChannelBindingBase"/>
        /// </summary>
        /// <param name="disposing">A boolean indicating whether or not the <see cref="ChannelBindingBase"/> is being disposed of</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                {
                    this.OnMessageSubject.OnCompleted();
                    this.OnMessageSubject.Dispose();
                    this.CancellationTokenSource.Cancel();
                    this.CancellationTokenSource.Dispose();
                }
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
