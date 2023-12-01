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

using Neuroglia.AsyncApi.v2;
using Neuroglia.AsyncApi.v2.Bindings.Mqtt;
using System.Net.Mime;

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
                            Traits = [],
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
                        Parameters = []
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
