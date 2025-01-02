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

using Neuroglia.AsyncApi.Bindings.Mqtt;
using Neuroglia.AsyncApi.v3;

namespace Neuroglia.AsyncApi.UnitTests.Services;

internal static class AsyncApiDocumentFactory
{

    public static V2AsyncApiDocument CreateV2()
    {
        return new V2AsyncApiDocument()
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
                    new V2ServerDefinition()
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
                    new V2ChannelDefinition()
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
                            Traits = [],
                            ExternalDocs = new()
                            {
                                Url = new("https://fake.contact.com"),
                                Description = "Fake Documentation Description"
                            },
                            Tags =
                            [
                                new V2TagDefinition()
                                {
                                    Name = "fake-tag",
                                    Description = "Fake Tag Description",
                                    ExternalDocs = new()
                                    {
                                        Url = new("https://fake.contact.com"),
                                        Description = "Fake Documentation Description"
                                    }
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
                        Parameters = []
                    }
                }
            },
            DefaultContentType = MediaTypeNames.Application.Json,
            ExternalDocs = new()
            {
                Url = new("https://fake.contact.com"),
                Description = "Fake Documentation Description"
            },
            Tags =
            [
                new V2TagDefinition()
                {
                    Name = "fake-tag",
                    Description = "Fake Tag Description",
                    ExternalDocs = new()
                    {
                        Url = new("https://fake.contact.com"),
                        Description = "Fake Documentation Description"
                    }
                }
            ]
        };
    }

    public static V3AsyncApiDocument CreateV3()
    {
        return new V3AsyncApiDocument()
        {
            AsyncApi = AsyncApiSpecVersion.V3,
            Id = "fake-document",
            Info = new()
            {
                Title = "Fake Async API",
                Version = AsyncApiSpecVersion.V3,
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
                    new()
                    {
                        Host = "test.mosquitto.org:{port}",
                        Protocol = AsyncApiProtocol.Mqtt,
                        Description = "Fake server",
                        Variables = new()
                        {
                            {
                                "port",
                                new()
                                {
                                    Description = "Fake server port description",
                                    Default = "1833",
                                    Enum =
                                    [
                                        "1833",
                                        "8883"
                                    ]
                                }
                            }
                        },
                        Security =
                        [
                            new()
                            {
                                Reference = "#/components/securitySchemes/apiKey"
                            },
                            new()
                            {
                                Type = SecuritySchemeType.OAuth2,
                                Description = "Fake OAUTH2 description",
                                Flows = new()
                                {
                                    Implicit = new()
                                    {
                                        AuthorizationUrl = new("https://fake.com"),
                                        Scopes = new()
                                        {
                                            {
                                                "streetlights:on",
                                                "Ability to switch lights on"
                                            },
                                              {
                                                "streetlights:off",
                                                "Ability to switch lights off"
                                            }
                                        }
                                    }
                                }
                            }
                        ],
                        Tags =
                        [
                            new()
                            {
                                Name = "fake",
                                Description = "Fake server tag"
                            }
                        ]
                    }
                }
            },
            Channels = new()
            {
                {
                    "lightingMeasured",
                    new()
                    {
                        Address = "smartylighting/streetlights/1/0/event/{streetlightId}/lighting/measured",
                        Messages = new()
                        {
                            {
                                "lightMeasured",
                                new()
                                {
                                    Reference = "#/components/messages/lightMeasured"
                                }
                            }
                        },
                        Description = "Fake channel description",
                        Parameters = new()
                        {
                            {
                                "streetlightId",
                                new()
                                {
                                    Reference = "#/components/parameters/streetlightId"
                                }
                            }
                        },
                        Servers = 
                        [
                            new() 
                            { 
                                Reference = "#/servers/fake-server"
                            }
                        ]
                    }
                }
            },
            Operations = new()
            {
                {
                    "fake-operation",
                    new()
                    {
                        Action = V3OperationAction.Receive,
                        Channel = new()
                        {
                            Reference = "#/channels/lightingMeasured"
                        },
                        Summary = "Fake operation summary",
                        Traits = 
                        [
                            new()
                            {
                                Reference = "#/components/operationTraits/mqtt"
                            }
                        ],
                        Messages =
                        [
                            new()
                            {
                                Reference = "#/components/messages/lightMeasured"
                            }
                        ]
                    }
                }
            },
            Components = new()
            {
                Messages = new()
                {
                    {
                        "lightMeasured",
                        new()
                        {
                            Name = "Fake message name",
                            Title = "Fake message title",
                            Summary = "Fake message summary",
                            ContentType = MediaTypeNames.Application.Json,
                            Traits =
                            [
                                new()
                                {
                                    Reference = "#/components/messageTraits/commonHeaders"
                                }
                            ],
                            Payload = new()
                            {
                                Reference = "#/components/schemas/lightMeasuredPayload"
                            }
                        }
                    }
                },
                Parameters = new()
                {
                    {
                        "streetlightId",
                        new()
                        {
                            Description = "Fake parameter description"
                        }
                    }
                },
                Schemas = new() 
                {
                    {
                        "lightMeasuredPayload",
                        new()
                        {
                            SchemaFormat = "JsonSchema",
                            Schema = new
                            {
                                Type = "object",
                                Properties = new
                                {
                                    Lumens = new
                                    {
                                        Type = "integer",
                                        Minimum = 0
                                    }.ToExpandoObject()!
                                }.ToExpandoObject()!
                            }.ToExpandoObject()!
                        }
                    }
                },
                MessageTraits = new()
                {
                    {
                        "commonHeaders",
                        new()
                        {
                            Headers = new()
                            {
                                SchemaFormat = "JsonSchema",
                                Schema = new
                                {
                                    Type = "object",
                                    Properties = new
                                    { 
                                        FakeHeader = new
                                        {
                                            Type = "integer",
                                            Minimum = 0,
                                            Maximum = 10
                                        }.ToExpandoObject()!
                                    }.ToExpandoObject()!
                                }.ToExpandoObject()!
                            }
                        }
                    }
                },
                OperationTraits = new()
                {
                    {
                        "mqtt",
                        new()
                        {
                            Title = "Fake operation trait title"
                        }
                    }
                },
                SecuritySchemes = new()
                {
                    {
                        "apiKey",
                        new()
                        {
                            Type = SecuritySchemeType.ApiKey,
                            In = ApiKeyLocation.User,
                            Description = "Fake ApiKey description"
                        }
                    }
                }
            }
        };
    }

}
