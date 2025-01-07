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
using Neuroglia.AsyncApi.Bindings.Mqtt;
using Neuroglia.AsyncApi.Client;
using Neuroglia.AsyncApi.Client.Bindings;
using System.Text;

namespace Neuroglia.AsyncApi.UnitTests.Cases.Client.Bindings;

public class MqttBindingHandlerTests
    : BindingHandlerTestsBase
{
    public MqttBindingHandlerTests()
        : base(builder => builder.AddMqttBindingHandler())
    {

    }

    [Fact]
    public async Task Publish_Should_Work()
    {
        //arrange
        var serverId = "mqtt-server";
        var channelId = "cloud-events";
        var operationId = "publishCloudEvent";
        var messageId = "cloudEvent";
        var stringSchema = new JsonSchemaBuilder().Type(SchemaValueType.String).Build();
        var objectSchema = new JsonSchemaBuilder().Type(SchemaValueType.Object).AdditionalProperties(true).Build();
        var document = DocumentBuilder
            .UsingAsyncApiV3()
            .WithTitle("Test MQTT API")
            .WithVersion("1.0.0")
            .WithServer(serverId, server => server
                .WithHost("test.mosquitto.org")
                .WithProtocol(AsyncApiProtocol.Mqtt)
                .WithBinding(new MqttServerBindingDefinition()))
            .WithChannel(channelId, channel => channel
                .WithAddress("cloud-event")
                .WithServer($"#/servers/{serverId}")
                .WithMessage(messageId, message => message
                    .WithContentType(CloudEventContentType.Json)
                    .WithPayloadSchema(schemaDefinition => schemaDefinition
                        .WithJsonSchema(schema => schema
                            .Type(SchemaValueType.Object)
                            .Properties(new Dictionary<string, JsonSchema>()
                            {
                                { CloudEventAttributes.SpecVersion, stringSchema },
                                { CloudEventAttributes.Id, stringSchema },
                                { CloudEventAttributes.Time, stringSchema },
                                { CloudEventAttributes.Source, stringSchema },
                                { CloudEventAttributes.Type, stringSchema },
                                { CloudEventAttributes.Subject, stringSchema },
                                { CloudEventAttributes.DataSchema, stringSchema },
                                { CloudEventAttributes.DataContentType, stringSchema },
                                { CloudEventAttributes.Data, objectSchema },
                            })
                            .Required(CloudEventAttributes.GetRequiredAttributes())
                            .AdditionalProperties(true)))
                    .WithBinding(new MqttMessageBindingDefinition()))
                .WithBinding(new MqttChannelBindingDefinition()))
            .WithOperation(operationId, operation => operation
                .WithAction(v3.V3OperationAction.Receive)
                .WithChannel($"#/channels/{channelId}")
                .WithMessage($"#/channels/{channelId}/messages/{messageId}")
                .WithBinding(new MqttOperationBindingDefinition()))
            .Build();
        await using var client = ClientFactory.CreateFor(document);

        //act
        var e = new CloudEvent()
        {
            Id = Guid.NewGuid().ToString(),
            SpecVersion = CloudEventSpecVersion.V1.Version,
            Source = new("https://unit-tests.v3.asyncapi.neuroglia.io"),
            Type = "io.neuroglia.asyncapi.v3.test.v1",
            DataContentType = MediaTypeNames.Application.Json,
            Data = new
            {
                Greetings = "Hello, World!"
            }
        };
        var parameters = new AsyncApiPublishOperationParameters(operationId)
        {
            Payload = e
        };
        await using var result = await client.PublishAsync(parameters);

        //assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public async Task Subscribe_Should_Work()
    {
        //arrange
        var serverId = "mqtt-server";
        var serverAddress = "test.mosquitto.org";
        var channelId = "cloud-events";
        var channelAddress = "cloud-event";
        var operationId = "subscribeToCloudEvents";
        var messageId = "cloud-event";
        var stringSchema = new JsonSchemaBuilder().Type(SchemaValueType.String).Build();
        var objectSchema = new JsonSchemaBuilder().Type(SchemaValueType.Object).AdditionalProperties(true).Build();
        var document = DocumentBuilder
            .UsingAsyncApiV3()
            .WithTitle("Test MQTT API")
            .WithVersion("1.0.0")
            .WithServer(serverId, server => server
                .WithHost(serverAddress)
                .WithProtocol(AsyncApiProtocol.Mqtt)
                .WithBinding(new MqttServerBindingDefinition()))
            .WithChannel(channelId, channel => channel
                .WithAddress(channelAddress)
                .WithServer($"#/servers/{serverId}")
                .WithMessage(messageId, message => message
                    .WithContentType(CloudEventContentType.Json)
                    .WithPayloadSchema(schemaDefinition => schemaDefinition
                        .WithJsonSchema(schema => schema
                            .Type(SchemaValueType.Object)
                            .Properties(new Dictionary<string, JsonSchema>()
                            {
                                { CloudEventAttributes.SpecVersion, stringSchema },
                                { CloudEventAttributes.Id, stringSchema },
                                { CloudEventAttributes.Time, stringSchema },
                                { CloudEventAttributes.Source, stringSchema },
                                { CloudEventAttributes.Type, stringSchema },
                                { CloudEventAttributes.Subject, stringSchema },
                                { CloudEventAttributes.DataSchema, stringSchema },
                                { CloudEventAttributes.DataContentType, stringSchema },
                                { CloudEventAttributes.Data, objectSchema },
                            })
                            .Required(CloudEventAttributes.GetRequiredAttributes())
                            .AdditionalProperties(true)))
                    .WithBinding(new MqttMessageBindingDefinition()))
                .WithBinding(new MqttChannelBindingDefinition()))
            .WithOperation(operationId, operation => operation
                .WithAction(v3.V3OperationAction.Send)
                .WithChannel($"#/channels/{channelId}")
                .WithMessage($"#/channels/{channelId}/messages/{messageId}")
                .WithBinding(new MqttOperationBindingDefinition()))
            .Build();
        await using var client = ClientFactory.CreateFor(document);

        //act
        var parameters = new AsyncApiSubscribeOperationParameters(operationId);
        await using var result = await client.SubscribeAsync(parameters);
        var messageCount = 10;
        var messagesToSend = new List<MqttApplicationMessage>();
        for (var i = 0; i < messageCount; i++)
        {
            var e = new CloudEvent()
            {
                Id = Guid.NewGuid().ToString(),
                SpecVersion = CloudEventSpecVersion.V1.Version,
                Source = new("https://unit-tests.v3.asyncapi.neuroglia.io"),
                Subject = i.ToString(),
                Type = "io.neuroglia.asyncapi.v3.test.v1",
                DataContentType = MediaTypeNames.Application.Json,
                Data = new
                {
                    Greetings = "Hello, World!"
                }
            };
            var json = JsonSerializer.Default.SerializeToText(e);
            var payload = Encoding.UTF8.GetBytes(json);
            var headers = new Dictionary<string, object>()
            {
                { "HeaderValue1",  69 },
                { "HeaderValue2",  "Lorem Ipsum" },
                { "HeaderValue3",  true }
            };
            var messageBuilder = new MqttApplicationMessageBuilder()
                .WithTopic(channelAddress)
                .WithContentType(MediaTypeNames.Application.Json)
                .WithPayload(payload)
                .WithPayloadFormatIndicator(MQTTnet.Protocol.MqttPayloadFormatIndicator.CharacterData);
            foreach (var header in headers) messageBuilder.WithUserProperty(header.Key, header.Value.ToString());
            var message = messageBuilder.Build();
            messagesToSend.Add(message);
        }
        var messagesReceived = new List<IAsyncApiMessage>();
        var subscription = result.Messages?.Subscribe(messagesReceived.Add);
        var mqttClientFactory = new MqttClientFactory();
        using var mqttClient = mqttClientFactory.CreateMqttClient();
        var options = new MqttClientOptionsBuilder().WithTcpServer(serverAddress).Build();
        await mqttClient.ConnectAsync(options);
        foreach (var message in messagesToSend) await mqttClient.PublishAsync(message);
        await Task.Delay(3500);
        subscription?.Dispose();

        //assert
        result.IsSuccessful.Should().BeTrue();
        result.Messages.Should().NotBeNull();
        messagesReceived.Should().NotBeEmpty();
    }

}