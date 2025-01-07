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

using Neuroglia.AsyncApi.Bindings.WebSockets;
using Neuroglia.AsyncApi.Client;
using Neuroglia.AsyncApi.Client.Bindings;
using Neuroglia.AsyncApi.UnitTests.Containers;
using System.Net.WebSockets;
using System.Threading;
using System;

namespace Neuroglia.AsyncApi.UnitTests.Cases.Client.Bindings;

public class WebSocketBindingHandlerTests
    : BindingHandlerTestsBase
{
    public WebSocketBindingHandlerTests()
        : base(builder => builder.AddWebSocketBindingHandler(), ConfigureServices)
    {

    }

    [Fact]
    public async Task Publish_Should_Work()
    {
        //arrange
        var serverId = "websocket-server";
        var channelId = "cloud-events";
        var operationId = "publishCloudEvent";
        var messageId = "cloudEvent";
        var stringSchema = new JsonSchemaBuilder().Type(SchemaValueType.String).Build();
        var objectSchema = new JsonSchemaBuilder().Type(SchemaValueType.Object).AdditionalProperties(true).Build();
        var document = DocumentBuilder
            .UsingAsyncApiV3()
            .WithTitle("Test WebSocket API")
            .WithVersion("1.0.0")
            .WithServer(serverId, server => server
                .WithHost($"ws://localhost:{ServiceProvider.GetRequiredKeyedService<DotNet.Testcontainers.Containers.IContainer>("websocket").GetMappedPublicPort(WebSocketContainerBuilder.PublicPort)}")
                .WithPathName("/.ws/test")
                .WithProtocol(AsyncApiProtocol.Ws)
                .WithBinding(new WsServerBindingDefinition()))
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
                    .WithBinding(new WsMessageBindingDefinition()))
                .WithBinding(new WsChannelBindingDefinition()))
            .WithOperation(operationId, operation => operation
                .WithAction(v3.V3OperationAction.Receive)
                .WithChannel($"#/channels/{channelId}")
                .WithMessage($"#/channels/{channelId}/messages/{messageId}")
                .WithBinding(new WsOperationBindingDefinition()))
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
        var serverId = "websocket-server";
        var serverAddress = $"ws://localhost:{ServiceProvider.GetRequiredKeyedService<DotNet.Testcontainers.Containers.IContainer>("websocket").GetMappedPublicPort(WebSocketContainerBuilder.PublicPort)}";
        var serverPathName = "/.ws/test";
        var channelId = "cloud-events";
        var channelAddress = "cloud-event";
        var operationId = "subscribeToCloudEvents";
        var messageId = "cloud-event";
        var stringSchema = new JsonSchemaBuilder().Type(SchemaValueType.String).Build();
        var objectSchema = new JsonSchemaBuilder().Type(SchemaValueType.Object).AdditionalProperties(true).Build();
        var document = DocumentBuilder
            .UsingAsyncApiV3()
            .WithTitle("Test WebSocket API")
            .WithVersion("1.0.0")
            .WithServer(serverId, server => server
                .WithHost(serverAddress)
                .WithPathName(serverPathName)
                .WithProtocol(AsyncApiProtocol.Ws)
                .WithBinding(new WsServerBindingDefinition()))
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
                    .WithBinding(new WsMessageBindingDefinition()))
                .WithBinding(new WsChannelBindingDefinition()))
            .WithOperation(operationId, operation => operation
                .WithAction(v3.V3OperationAction.Send)
                .WithChannel($"#/channels/{channelId}")
                .WithMessage($"#/channels/{channelId}/messages/{messageId}")
                .WithBinding(new WsOperationBindingDefinition()))
            .Build();
        await using var client = ClientFactory.CreateFor(document);

        //act
        var parameters = new AsyncApiSubscribeOperationParameters(operationId);
        await using var result = await client.SubscribeAsync(parameters);
        var messageCount = 10;
        var messagesToSend = new List<byte[]>();
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
            var message = JsonSerializer.Default.SerializeToByteArray(e)!;
            messagesToSend.Add(message);
        }
        var messagesReceived = new List<IAsyncApiMessage>();
        var subscription = result.Messages?.Subscribe(messagesReceived.Add);
        using var webSocket = new ClientWebSocket();
        await webSocket.ConnectAsync(new($"{serverAddress}{serverPathName}"), CancellationToken.None);
        foreach (var message in messagesToSend) await SendAsync(webSocket, message);
        await Task.Delay(3500);
        subscription?.Dispose();

        //assert
        result.IsSuccessful.Should().BeTrue();
        result.Messages.Should().NotBeNull();
        messagesReceived.Should().NotBeEmpty();
    }

    static async Task SendAsync(ClientWebSocket webSocket, byte[] buffer)
    {
        int totalLength = buffer.Length;
        int offset = 0;
        while (offset < totalLength)
        {
            var remaining = totalLength - offset;
            var currentSegmentSize = Math.Min(1024, remaining);
            var segment = new ArraySegment<byte>(buffer, offset, currentSegmentSize);
            var endOfMessage = (offset + currentSegmentSize) >= totalLength;
            await webSocket.SendAsync(segment, WebSocketMessageType.Binary, endOfMessage, CancellationToken.None);
            offset += currentSegmentSize;
        }
        await webSocket.SendAsync(buffer, WebSocketMessageType.Binary, true, CancellationToken.None);
    }

    static void ConfigureServices(IServiceCollection services)
    {
        services.AddKeyedSingleton("websocket", WebSocketContainerBuilder.Build());
        services.AddSingleton(provider => provider.GetRequiredKeyedService<DotNet.Testcontainers.Containers.IContainer>("websocket"));
        services.AddHostedService<ContainerBootstrapper>();
    }

}