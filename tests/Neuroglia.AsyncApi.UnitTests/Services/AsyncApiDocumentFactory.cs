using System.Net.Mime;
using Neuroglia.AsyncApi.v2;
using Neuroglia.AsyncApi.v2;
using Neuroglia.AsyncApi.v2.Bindings.Mqtt;

namespace Neuroglia.AsyncApi.UnitTests.Services;

internal static class AsyncApiDocumentFactory
{

    public static AsyncApiDocument Create()
    {
        return new AsyncApiDocument()
        {
            AsyncApi = AsyncApiSpecVersion.V2,
            Id = "fake-document",
            Info = new()
            {
                Title = "Fake Async API",
                Description = "Fake Async API description",
                Contact = new()
                {
                    Name = "Fake Contact",
                    Email = "fake@contact.com",
                    Url = new("https://fake.contact.com")
                },
                License = new()
                {
                    Name = "Apache-2.0",
                    Url = new("https://spdx.org/licenses/Apache-2.0.html")
                }
            },
            Servers = new()
            {
                {
                    "fake-server",
                    new ServerDefinition()
                    {
                        Url = new("https://fake.contact.com"),
                        Description = "Fake AsyncAPI server",
                        Protocol = AsyncApiProtocol.Http,
                        ProtocolVersion = "latest",
                        Bindings = new()
                        {
                           Http = new(),
                           Ws = new(),
                           Kafka = new(),
                           Amqp = new(),
                           Amqp1 = new(),
                           Mqtt = new()
                           {
                                ClientId = Guid.NewGuid().ToString(),
                                CleanSession = true,
                                KeepAlive = true,
                                LastWill = new()
                                {
                                    Message = "Fake Message",
                                    Retain = true,
                                    Topic = "Fake Topic",
                                    QoS = MqttQoSLevel.ExactlyOne

                                },
                                BindingVersion = "latest"
                           },
                           Mqtt5 = new(),
                           Nats = new(),
                           Redis = new()
                        }
                    }
                }
            },
            Channels = new()
            {
                {
                    "fake-channel",
                    new ChannelDefinition()
                    {
                        Publish = new()
                        {
                            OperationId = "fake-operation-id",
                            Description = "Fake Operation Description",
                            Summary = "Fake Operation Summary",
                            Bindings = new()
                            {
                                //todo
                            },
                            Message = new()
                            {
                                //todo
                            },
                            Traits = new()
                            {
                                //todo
                            },
                            ExternalDocs =
                            [
                                new ExternalDocumentationDefinition()
                                {
                                    Url = new("https://fake.contact.com"),
                                    Description = "Fake Documentation Description"
                                }
                            ],
                            Tags =
                            [
                                new TagDefinition()
                                {
                                    Name = "fake-tag",
                                    Description = "Fake Tag Description",
                                    ExternalDocs =
                                    [
                                        new ExternalDocumentationDefinition()
                                        {
                                            Url = new("https://fake.contact.com"),
                                            Description = "Fake Documentation Description"
                                        }
                                    ]
                                }
                            ]
                        },
                        Subscribe = new()
                        {

                        },
                        Description = "Fake Channel Description",
                        Bindings = new()
                        {
                            //todo
                        },
                        Parameters = new EquatableDictionary<string, ParameterDefinition>()
                        {
                            //todo
                        }
                    }
                }
            },
            DefaultContentType = MediaTypeNames.Application.Json,
            ExternalDocs = 
            [
                new ExternalDocumentationDefinition()
                {
                    Url = new("https://fake.contact.com"),
                    Description = "Fake Documentation Description"
                }
            ],
            Tags =
            [
                new TagDefinition()
                {
                    Name = "fake-tag",
                    Description = "Fake Tag Description",
                    ExternalDocs =
                    [
                        new ExternalDocumentationDefinition()
                        {
                            Url = new("https://fake.contact.com"),
                            Description = "Fake Documentation Description"
                        }
                    ]
                }
            ]
        };
    }

}
