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

using Json.More;
using Json.Schema;
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.CloudEvents;
using Neuroglia.AsyncApi.FluentBuilders;
using Neuroglia.AsyncApi.v2.Bindings.Http;
using Neuroglia.Eventing.CloudEvents;
using Neuroglia.Serialization.Json;
using System.Net.Mime;

namespace Neuroglia.AsyncApi.UnitTests.Cases.CloudEvents;

public class CloudEventExtensionsTests
   : IAsyncLifetime
{

    public CloudEventExtensionsTests()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IAsyncApiDocumentBuilder, AsyncApiDocumentBuilder>();
        this.Services = services.BuildServiceProvider();
    }

    ServiceProvider Services { get; }

    IAsyncApiDocumentBuilder Builder => this.Services.GetRequiredService<IAsyncApiDocumentBuilder>();

    [Fact]
    public void Build_Operation_With_CloudEventMessage_Should_Work()
    {
        //arrange
        var specVersion = CloudEventSpecVersion.V1.Version;
        var source = new Uri("https://fake.source.com");
        var type = "io.neuroglia.unit-tests.fake.event.v1";
        var subject = "fake-subject";
        var dataContentType = MediaTypeNames.Application.Json;
        var dataSchemaUri = new Uri("https://fake.source.com/fake/schema");
        var validCloudEvent = JsonSerializer.Default.SerializeToNode(new CloudEvent()
        {
            SpecVersion = specVersion,
            Id = Guid.NewGuid().ToString("N"),
            Source = source,
            Type = type,
            Subject = subject,
            DataContentType = dataContentType
        });
        var invalidSourceCloudEvent = JsonSerializer.Default.SerializeToNode(new CloudEvent()
        {
            SpecVersion = specVersion,
            Id = Guid.NewGuid().ToString("N"),
            Source = new Uri("http://invalid-source"),
            Type = type,
            Subject = subject,
            DataContentType = dataContentType
        });
        var invalidTypeCloudEvent = JsonSerializer.Default.SerializeToNode(new CloudEvent()
        {
            SpecVersion = specVersion,
            Id = Guid.NewGuid().ToString("N"),
            Source = source,
            Type = "invalid.type",
            Subject = subject,
            DataContentType = dataContentType
        });

        //act
        var document = this.Builder
           .WithServer("fake-server", server => server
               .WithBinding(new HttpServerBindingDefinition()))
           .WithChannel("fake-channel", channel => channel
               .WithBinding(new HttpChannelBindingDefinition())
               .WithPublishOperation(operation => operation
                   .WithCloudEventMessage(cloudEvent => cloudEvent
                       .WithSpecVersion(specVersion)
                       .WithSource(source)
                       .WithType(type)
                       .WithSubject(subject)
                       .WithDataContentType(dataContentType)
                       .WithDataSchemaUri(dataSchemaUri)
                       .WithDataOfType<FakeEvent>())))
           .Build();

        //assert
        document.Should().NotBeNull();
        document.Channels.Should().ContainSingle();

        var channel = document.Channels.Single();
        channel.Value.Publish.Should().NotBeNull();
        channel.Value.Publish!.Message.Should().NotBeNull();
        channel.Value.Publish.Message!.Payload.Should().NotBeNull();

        var schema = channel.Value.Publish.Message.Payload as JsonSchema;
        schema.Should().NotBeNull();

        var evaluationResults = schema!.Evaluate(validCloudEvent);
        evaluationResults.Should().NotBeNull();
        evaluationResults.IsValid.Should().BeTrue();

        evaluationResults = schema!.Evaluate(invalidSourceCloudEvent);
        evaluationResults.Should().NotBeNull();
        evaluationResults.IsValid.Should().BeFalse();

        evaluationResults = schema!.Evaluate(invalidTypeCloudEvent);
        evaluationResults.Should().NotBeNull();
        evaluationResults.IsValid.Should().BeFalse();
    }

    public Task InitializeAsync()
    {
        SchemaRegistry.Global.Fetch = uri =>
        {
            using var httpClient = new HttpClient();
            var content = httpClient.GetStringAsync(CloudEventMessageDefinitionBuilder.CloudEventSchemaUri).Result;
            return JsonSchema.FromText(content);
        };
        return Task.CompletedTask;
    }


    public async Task DisposeAsync()
    {
        await this.Services.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    class FakeEvent
    {

        public FakeEvent() { }

        public FakeEvent(string to, string message)
        {
            this.Id = Guid.NewGuid();
            this.CreatedAt = DateTimeOffset.Now;
            this.To = to;
            this.Message = message;
        }

        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? LastModified { get; set; }

        public string To { get; set; } = null!;

        public string Message { get; set; } = null!;

    }

}
