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

using Neuroglia.AsyncApi.Bindings.Http;
using Neuroglia.AsyncApi.Client;
using Neuroglia.AsyncApi.Client.Services;
using Neuroglia.AsyncApi.FluentBuilders;

namespace Neuroglia.AsyncApi.UnitTests.Cases.Client;

public class ClientTests
    : IDisposable
{

    public ClientTests()
    {
        var services = new ServiceCollection();
        services.AddAsyncApi();
        services.AddAsyncApiClient();
        ServiceProvider = services.BuildServiceProvider();
    }

    ServiceProvider ServiceProvider;

    IAsyncApiDocumentBuilder DocumentBuilder => ServiceProvider.GetRequiredService<IAsyncApiDocumentBuilder>();

    IAsyncApiClientFactory ClientFactory => ServiceProvider.GetRequiredService<IAsyncApiClientFactory>();

    [Fact]
    public async Task Send_HTTP_Message_Should_Work()
    {
        //arrange
        var serverId = "http-server";
        var channelId = "cloud-events";
        var operationId = "publishCloudEvent";
        var messageId = "cloudEvent";
        var stringSchema = new JsonSchemaBuilder().Type(SchemaValueType.String).Build();
        var objectSchema = new JsonSchemaBuilder().Type(SchemaValueType.Object).AdditionalProperties(true).Build();
        var document = DocumentBuilder
            .UsingAsyncApiV3()
            .WithTitle("Test HTTP API")
            .WithVersion("1.0.0")
            .WithServer(serverId, server => server
                .WithHost("https://httpbin.org")
                .WithProtocol(AsyncApiProtocol.Http)
                .WithBinding(new HttpServerBindingDefinition()))
            .WithChannel(channelId, channel => channel
                .WithAddress("/post")
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
                    .WithBinding(new HttpMessageBindingDefinition()))
                .WithBinding(new HttpChannelBindingDefinition()))
            .WithOperation(operationId, operation => operation
                .WithAction(v3.V3OperationAction.Receive)
                .WithChannel($"#/channels/{channelId}")
                .WithMessage($"#/channels/{channelId}/messages/{messageId}")
                .WithBinding(new HttpOperationBindingDefinition()
                {
                    Method = Bindings.Http.HttpMethod.POST
                }))
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
        await using var message = new AsyncApiOutboundMessage(operationId)
        {
            Payload = e
        };

        await using var result = await client.SendAsync(message);

        //assert
        result.IsSuccessful.Should().BeTrue();
    }

    void IDisposable.Dispose()
    {
        ServiceProvider.Dispose();
        GC.SuppressFinalize(this);
    }

}
