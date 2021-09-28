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
namespace Neuroglia.AsyncApi
{

    /// <summary>
    /// Exposes constants about protocols officially supported by the Async API
    /// </summary>
    public static class AsyncApiProtocols
    {

        /// <summary>
        /// Gets the http Async API protocol
        /// </summary>
        public const string Http = "http";
        /// <summary>
        /// Gets the ws Async API protocol
        /// </summary>
        public const string Ws = "ws";
        /// <summary>
        /// Gets the kafka Async API protocol
        /// </summary>
        public const string Kafka = "kafka";
        /// <summary>
        /// Gets the amqp Async API protocol
        /// </summary>
        public const string Amqp = "amqp";
        /// <summary>
        /// Gets the amqp1 Async API protocol
        /// </summary>
        public const string AmqpV1 = "amqp1";
        /// <summary>
        /// Gets the mqtt Async API protocol
        /// </summary>
        public const string Mqtt = "mqtt";
        /// <summary>
        /// Gets the mqtt5 Async API protocol
        /// </summary>
        public const string MqttV5 = "mqtt5";
        /// <summary>
        /// Gets the nats Async API protocol
        /// </summary>
        public const string Nats = "nats";
        /// <summary>
        /// Gets the jms Async API protocol
        /// </summary>
        public const string Jms = "jms";
        /// <summary>
        /// Gets the sns Async API protocol
        /// </summary>
        public const string Sns = "sns";
        /// <summary>
        /// Gets the sqs Async API protocol
        /// </summary>
        public const string Sqs = "sqs";
        /// <summary>
        /// Gets the stomp Async API protocol
        /// </summary>
        public const string Stomp = "stomp";
        /// <summary>
        /// Gets the redis Async API protocol
        /// </summary>
        public const string Redis = "redis";
        /// <summary>
        /// Gets the mercure Async API protocol
        /// </summary>
        public const string Mercure = "mercure";
        /// <summary>
        /// Gets the ibmmq Async API protocol
        /// </summary>
        public const string Ibmmq = "ibmmq";

    }

}
