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

using Neuroglia.AsyncApi.Bindings.Solace;
using Neuroglia.AsyncApi.Client;
using Neuroglia.AsyncApi.Client.Bindings;
using Neuroglia.AsyncApi.Client.Bindings.Solace;
using Neuroglia.AsyncApi.UnitTests.Containers;
using Renci.SshNet;
using SolaceSystems.Solclient.Messaging;
using System.Buffers;
using static SolaceSystems.Solclient.Messaging.Helper;

namespace Neuroglia.AsyncApi.UnitTests.Cases.Client.Bindings;

public class SolaceBindingHandlerTests
    : BindingHandlerTestsBase
{

    public SolaceBindingHandlerTests()
        : base(builder => builder.AddSolaceBindingHandler(), ConfigureServices)
    {

    }

    [Fact(Skip = "Failed to run the Solace test container")]
    public async Task Publish_Should_Work()
    {
        //arrange
        var serverId = "solace-server";
        var channelId = "cloud-events";
        var operationId = "publishCloudEvent";
        var messageId = "cloudEvent";
        var stringSchema = new JsonSchemaBuilder().Type(SchemaValueType.String).Build();
        var objectSchema = new JsonSchemaBuilder().Type(SchemaValueType.Object).AdditionalProperties(true).Build();
        var document = DocumentBuilder
            .UsingAsyncApiV3()
            .WithTitle("Test Solace API")
            .WithVersion("1.0.0")
            .WithServer(serverId, server => server
                .WithHost($"solace://localhost:{ServiceProvider.GetRequiredKeyedService<DotNet.Testcontainers.Containers.IContainer>("solace").GetMappedPublicPort(SolaceContainerBuilder.PublicPort)}")
                .WithProtocol(AsyncApiProtocol.Solace)
                .WithBinding(new SolaceServerBindingDefinition()))
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
                    .WithBinding(new SolaceMessageBindingDefinition()))
                .WithBinding(new SolaceChannelBindingDefinition()))
            .WithOperation(operationId, operation => operation
                .WithAction(v3.V3OperationAction.Receive)
                .WithChannel($"#/channels/{channelId}")
                .WithMessage($"#/channels/{channelId}/messages/{messageId}")
                .WithBinding(new SolaceOperationBindingDefinition()))
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

    [Fact(Skip = "Failed to run the Solace test container")]
    public async Task Subscribe_Should_Work()
    {
        //arrange
        var serverId = "solace-server";
        var serverAddress = $"solace://localhost:{ServiceProvider.GetRequiredKeyedService<DotNet.Testcontainers.Containers.IContainer>("solace").GetMappedPublicPort(SolaceContainerBuilder.PublicPort)}";
        var channelId = "cloud-events";
        var channelAddress = "cloud-event";
        var operationId = "subscribeToCloudEvents";
        var messageId = "cloud-event";
        var stringSchema = new JsonSchemaBuilder().Type(SchemaValueType.String).Build();
        var objectSchema = new JsonSchemaBuilder().Type(SchemaValueType.Object).AdditionalProperties(true).Build();
        var document = DocumentBuilder
            .UsingAsyncApiV3()
            .WithTitle("Test Solace API")
            .WithVersion("1.0.0")
            .WithServer(serverId, server => server
                .WithHost(serverAddress)
                .WithProtocol(AsyncApiProtocol.Solace)
                .WithBinding(new SolaceServerBindingDefinition()))
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
                    .WithBinding(new SolaceMessageBindingDefinition()))
                .WithBinding(new SolaceChannelBindingDefinition()))
            .WithOperation(operationId, operation => operation
                .WithAction(v3.V3OperationAction.Send)
                .WithChannel($"#/channels/{channelId}")
                .WithMessage($"#/channels/{channelId}/messages/{messageId}")
                .WithBinding(new SolaceOperationBindingDefinition()))
            .Build();
        await using var client = ClientFactory.CreateFor(document);

        //act
        var parameters = new AsyncApiSubscribeOperationParameters(operationId);
        await using var result = await client.SubscribeAsync(parameters);
        var messageCount = 10;
        var messagesToSend = new List<Tuple<byte[], Dictionary<string, object>>>();
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
            var headers = new Dictionary<string, object>()
            {
                { "Header1", true },
                { "Header2", i },
                { "Header3", "Value3" }
            };
            messagesToSend.Add(new(payload, headers));
        }
        var messagesReceived = new List<IAsyncApiMessage>();
        var subscription = result.Messages?.Subscribe(messagesReceived.Add);
        var contextFactoryProperties = new ContextFactoryProperties();
        ContextFactory.Instance.Init(contextFactoryProperties);
        var contextProperties = new ContextProperties();
        using var context = ContextFactory.Instance.CreateContext(contextProperties, null);
        var sessionProperties = new SessionProperties()
        {
            Host = serverAddress
        };
        using var session = context.CreateSession(sessionProperties, null, null);
        session.Connect();
        using var queue = ContextFactory.Instance.CreateQueue(channelAddress);
        var endpointProperties = new EndpointProperties()
        {
            Permission = EndpointProperties.EndpointPermission.Consume,
            AccessType = EndpointProperties.EndpointAccessType.Exclusive
        };
        session.Provision(queue, endpointProperties, ProvisionFlag.IgnoreErrorIfEndpointAlreadyExists | ProvisionFlag.WaitForConfirm, null);
        foreach (var message in messagesToSend) SendMessage(session, queue, message.Item1, message.Item2);
        await Task.Delay(3500);
        subscription?.Dispose();

        //assert
        result.IsSuccessful.Should().BeTrue();
        result.Messages.Should().NotBeNull();
        messagesReceived.Should().NotBeEmpty();
    }

    static void SendMessage(ISession session, IQueue queue, byte[] payload, Dictionary<string, object> headers)
    {
        using var message = ContextFactory.Instance.CreateMessage();
        message.Destination = queue;
        message.HttpContentType = MediaTypeNames.Application.Json;
        message.BinaryAttachment = payload;
        message.UserData = JsonSerializer.Default.SerializeToByteArray(headers);
        session.Send(message);
    }

    static void ConfigureServices(IServiceCollection services)
    {
        services.AddKeyedSingleton("solace", SolaceContainerBuilder.Build());
        services.AddSingleton(provider => provider.GetRequiredKeyedService<DotNet.Testcontainers.Containers.IContainer>("solace"));
        services.AddHostedService<ContainerBootstrapper>();
    }

}