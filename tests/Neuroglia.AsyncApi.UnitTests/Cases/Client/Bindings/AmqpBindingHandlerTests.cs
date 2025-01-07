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

using Neuroglia.AsyncApi.Bindings.Amqp;
using Neuroglia.AsyncApi.Client;
using Neuroglia.AsyncApi.Client.Bindings;
using Neuroglia.AsyncApi.UnitTests.Containers;
using RabbitMQ.Client;
using System.Security.Authentication.ExtendedProtection;
using System.Threading;

namespace Neuroglia.AsyncApi.UnitTests.Cases.Client.Bindings;

public class AmqpBindingHandlerTests
    : BindingHandlerTestsBase
{
    public AmqpBindingHandlerTests()
        : base(builder => builder.AddAmqpBindingHandler(), ConfigureServices)
    {

    }

    [Fact]
    public async Task Publish_Should_Work()
    {
        //arrange
        var serverId = "amqp-server";
        var channelId = "cloud-events";
        var operationId = "publishCloudEvent";
        var messageId = "cloudEvent";
        var stringSchema = new JsonSchemaBuilder().Type(SchemaValueType.String).Build();
        var objectSchema = new JsonSchemaBuilder().Type(SchemaValueType.Object).AdditionalProperties(true).Build();
        var document = DocumentBuilder
            .UsingAsyncApiV3()
            .WithTitle("Test AMQP API")
            .WithVersion("1.0.0")
            .WithServer(serverId, server => server
                .WithHost($"localhost:{ServiceProvider.GetRequiredKeyedService<DotNet.Testcontainers.Containers.IContainer>("amqp").GetMappedPublicPort(AmqpContainerBuilder.PublicPort)}")
                .WithProtocol(AsyncApiProtocol.Amqp)
                .WithBinding(new AmqpServerBindingDefinition()))
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
                    .WithBinding(new AmqpMessageBindingDefinition()))
                .WithBinding(new AmqpChannelBindingDefinition()))
            .WithOperation(operationId, operation => operation
                .WithAction(v3.V3OperationAction.Receive)
                .WithChannel($"#/channels/{channelId}")
                .WithMessage($"#/channels/{channelId}/messages/{messageId}")
                .WithBinding(new AmqpOperationBindingDefinition()))
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
        var serverId = "amqp-server";
        var host = "localhost";
        var port = ServiceProvider.GetRequiredKeyedService<DotNet.Testcontainers.Containers.IContainer>("amqp").GetMappedPublicPort(AmqpContainerBuilder.PublicPort);
        var serverAddress = $"{host}:{port}";
        var channelId = "cloud-events";
        var channelAddress = "cloud-event";
        var operationId = "subscribeToCloudEvents";
        var messageId = "cloud-event";
        var stringSchema = new JsonSchemaBuilder().Type(SchemaValueType.String).Build();
        var objectSchema = new JsonSchemaBuilder().Type(SchemaValueType.Object).AdditionalProperties(true).Build();
        var document = DocumentBuilder
            .UsingAsyncApiV3()
            .WithTitle("Test AMQP API")
            .WithVersion("1.0.0")
            .WithServer(serverId, server => server
                .WithHost(serverAddress)
                .WithProtocol(AsyncApiProtocol.Amqp)
                .WithBinding(new AmqpServerBindingDefinition()))
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
                    .WithBinding(new AmqpMessageBindingDefinition()))
                .WithBinding(new AmqpChannelBindingDefinition()))
            .WithOperation(operationId, operation => operation
                .WithAction(v3.V3OperationAction.Send)
                .WithChannel($"#/channels/{channelId}")
                .WithMessage($"#/channels/{channelId}/messages/{messageId}")
                .WithBinding(new AmqpOperationBindingDefinition()))
            .Build();
        await using var client = ClientFactory.CreateFor(document);

        //act
        var parameters = new AsyncApiSubscribeOperationParameters(operationId);
        await using var result = await client.SubscribeAsync(parameters);
        var messageCount = 10;
        var messagesToSend = new List<Tuple<BasicProperties, byte[]>>();
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
            var payload = JsonSerializer.Default.SerializeToByteArray(e)!;
            var headers = new Dictionary<string, object?>()
            {
                { "HeaderValue1",  69 },
                { "HeaderValue2",  "Lorem Ipsum" },
                { "HeaderValue3",  true }
            };
            var properties = new BasicProperties()
            {
                ContentType = MediaTypeNames.Application.Json,
                Headers = headers
            };
            messagesToSend.Add(new(properties, payload));
        }
        var messagesReceived = new List<IAsyncApiMessage>();
        var subscription = result.Messages?.Subscribe(messagesReceived.Add);
        var connectionFactory = new ConnectionFactory
        {
            HostName = host,
            Port = port
        };
        using var connection = await connectionFactory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        foreach (var message in messagesToSend) await channel.BasicPublishAsync(string.Empty, channelAddress, false, message.Item1, message.Item2);
        await Task.Delay(3500);
        subscription?.Dispose();

        //assert
        result.IsSuccessful.Should().BeTrue();
        result.Messages.Should().NotBeNull();
        messagesReceived.Should().NotBeEmpty();
    }
    static void ConfigureServices(IServiceCollection services)
    {
        services.AddKeyedSingleton("amqp", AmqpContainerBuilder.Build());
        services.AddSingleton(provider => provider.GetRequiredKeyedService<DotNet.Testcontainers.Containers.IContainer>("amqp"));
        services.AddHostedService<ContainerBootstrapper>();
    }

}