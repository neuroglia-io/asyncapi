using Confluent.Kafka;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Publishing;
using MQTTnet.Protocol;
using Neuroglia.AsyncApi.Models;
using Neuroglia.AsyncApi.Models.Bindings;
using Neuroglia.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Client
{

    public interface IAsyncApiClientFactory
    {

        IAsyncApiClient Create(AsyncApiDocument document);

    }

    public interface IAsyncApiClient
    {

        Task PublishAsync(string channel, object payload, CancellationToken cancellationToken = default);

    }

    public interface IAsyncApiChannelFactory
    {

        IChannel Create(AsyncApiDocument document, string channel);

    }

    public interface IChannel
    {

        string Key { get; }

        ChannelDefinition Definition { get; }

    }

    public class ChannelBase
        : IChannel
    {

        public ChannelBase(ChannelDefinition definition)
        {
            this.Definition = definition;
        }

        public virtual string Key { get; }

        public virtual ChannelDefinition Definition { get; }

        public virtual async Task PublishAsync(object payload, string protocol, CancellationToken cancellationToken = default)
        {
            if (this.Definition.Publish == null)
                throw new NotSupportedException($"The channel '{this.Key}' does not support publishing");
            IChannelBinding bindingDefinition;
            if (string.IsNullOrWhiteSpace(protocol))
                bindingDefinition = this.Definition.Bindings?.FirstOrDefault();
            else
                if (!this.Definition.Bindings.TryGetBinding(protocol, out bindingDefinition))
                    throw new NullReferenceException($"Failed to find a binding definition for the specified protocol '{protocol}'");
            IChannelBindingBase binding = bindingDefinition == null ? this.Bindings.First() : this.Bindings.Get(bindingDefinition);
            await binding.PublishAsync(payload, cancellationToken);
        }

    }

    public class AsyncApiMessage
    {

        public virtual object CorrelationKey { get; protected set; }

        public virtual Dictionary<string, string> Headers { get; protected set; }

        public virtual object Payload { get; protected set; }

    }

    public interface IChannelBindingBase
        : IObservable<object>, IDisposable
    {

        Task PublishAsync(object payload, CancellationToken cancellationToken = default);

    }

    public abstract class ChannelBindingBase
        : IChannelBindingBase
    {

        protected virtual IChannel Channel { get; }

        protected virtual IEnumerable<ServerDefinition> Servers { get; }

        protected virtual ISerializerProvider SerializerProvider { get; }

        protected virtual Subject<object> MessageSubject { get; }

        public virtual async Task PublishAsync(object payload, CancellationToken cancellationToken = default)
        {
            byte[] serializedPayload = null;
            if (payload != null)
                if (payload.GetType() == typeof(byte[]))
                    serializedPayload = (byte[])payload;
                else
                    serializedPayload = await this.SerializerProvider.GetSerializerFor(this.Channel.Definition.Publish.Message.ContentType).SerializeAsync(payload, cancellationToken);
            await this.PublishAsync(serializedPayload, cancellationToken);
        }

        protected abstract Task PublishAsync(byte[] payload, CancellationToken cancellationToken = default);

        protected string ComputeChannelKey()
        {
            return this.Channel.Key;
        }

        public virtual IDisposable Subscribe(IObserver<object> observer)
        {
            return this.MessageSubject.Subscribe(observer);
        }

        private bool _Disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                {
                 
                }
                this._Disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }

    public class MqttChannelBinding
        : ChannelBindingBase
    {

        public MqttChannelBinding()
        {
            
        }

        protected virtual IMqttClient MqttClient { get; }

        protected override async Task PublishAsync(byte[] payload, CancellationToken cancellationToken = default)
        {
            MqttApplicationMessage message = new()
            {
                Topic = channel,
                ContentType = this.Channel.Definition.Publish.Message.ContentType,
                Payload = payload,
                QualityOfServiceLevel = (MqttQualityOfServiceLevel)(int)this.Channel.Definition.Publish.Bindings.Mqtt.QoS,
                Retain = this.Channel.Definition.Publish.Bindings.Mqtt.Retain
            };
            if(this.Channel.Definition.Publish.Message.CorrelationId != null
                && !string.IsNullOrWhiteSpace(this.Channel.Definition.Publish.Message.CorrelationId.Location))
                message.CorrelationData = ;
            MqttClientPublishResult result = await this.MqttClient.PublishAsync(message, cancellationToken);
            if(result.ReasonCode == MqttClientPublishReasonCode.Success)
            {

            }
            else
            {

            }
        }

        protected virtual Task OnMessageAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            this.MessageSubject.OnNext(e.ApplicationMessage);
            return Task.CompletedTask;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
                this.MqttClient.Dispose();
        }

    }

    public class KafkaChannelBinding
        : ChannelBindingBase
    {

        public KafkaChannelBinding()
        {
            ConsumerConfig consumerConfig = new();
            this.KafkaProducer = new ProducerBuilder<Null, byte[]>(consumerConfig)
                .Build();
            ProducerConfig producerConfig = new();
            this.KafkaConsumer = new ConsumerBuilder<Null, byte[]>(producerConfig)
                .Build();
            this.CancellationTokenSource = new();
            this.ConsumerTask = Task.Run(() => this.ConsumeMessages());
        }

        protected IProducer<Null, byte[]> KafkaProducer { get; }

        protected IConsumer<Null, byte[]> KafkaConsumer { get; }

        protected CancellationTokenSource CancellationTokenSource { get; }

        protected Task ConsumerTask { get; private set; }

        protected virtual void ConsumeMessages()
        {
            while (!this.CancellationTokenSource.IsCancellationRequested)
            {
                ConsumeResult<Null, byte[]> consumeResult = this.KafkaConsumer.Consume(this.CancellationTokenSource.Token);
                this.OnMessage(consumeResult);
            }
            this.KafkaConsumer.Close();
        }

        protected override async Task PublishAsync(byte[] payload, CancellationToken cancellationToken = default)
        {
            Message<Null, byte[]> message = new() { Value = payload };
            await this.KafkaProducer.ProduceAsync(channel, message, cancellationToken);
        }

        protected virtual void OnMessage(ConsumeResult<Null, byte[]> consumeResult)
        {

        }

        protected override void Dispose(bool disposing)
        {
                base.Dispose(disposing);
            if (!disposing)
                return;
            this.CancellationTokenSource.Cancel();
            this.CancellationTokenSource.Dispose();
            this.KafkaConsumer.Dispose();
            this.KafkaProducer.Dispose();
            this.ConsumerTask.Dispose();
        }

    }

}
