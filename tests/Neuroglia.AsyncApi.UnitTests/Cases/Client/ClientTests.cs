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
        var environmentVariable = "environment";
        var document = DocumentBuilder
            .UsingAsyncApiV3()
            .WithTitle("Test HTTP API")
            .WithVersion("1.0.0")
            .WithServer(serverId, server => server
                .WithHost("http://fake-host.com")
                .WithPathName("/{broker}/{environment}")
                .WithProtocol(AsyncApiProtocol.Http)
                 .WithVariable("broker", variable => variable
                    .WithDefaultValue("test"))
                .WithVariable(environmentVariable, variable => variable
                    .WithEnumValues("development", "staging", "production"))
                .WithBinding(new HttpServerBindingDefinition()))
            .WithChannel(channelId, channel => channel
                .WithServer($"#/servers/{serverId}")
                .WithBinding(new HttpChannelBindingDefinition()))
            .WithOperation(operationId, operation => operation
                .WithAction(v3.V3OperationAction.Receive)
                .WithChannel($"#/channels/{channelId}")
                .WithBinding(new HttpOperationBindingDefinition()))
            .Build();
        await using var client = ClientFactory.CreateFor(document);

        //act
        await using var message = new AsyncApiOutboundMessage(operationId);
        message.ServerVariables[environmentVariable] = "development";

        await client.SendAsync(message);

        //assert

    }

    void IDisposable.Dispose()
    {
        ServiceProvider.Dispose();
        GC.SuppressFinalize(this);
    }

}
