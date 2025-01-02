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
using Neuroglia.AsyncApi.FluentBuilders.v2;
using Neuroglia.AsyncApi.FluentBuilders.v3;

namespace Neuroglia.AsyncApi.UnitTests.Cases.FluentBuilders;

public class FluentBuilderTests
    : IDisposable
{

    public FluentBuilderTests()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IV2AsyncApiDocumentBuilder, V2AsyncApiDocumentBuilder>();
        services.AddSingleton<IV3AsyncApiDocumentBuilder, V3AsyncApiDocumentBuilder>();
        this.Services = services.BuildServiceProvider();
    }

    ServiceProvider Services { get; }

    IV2AsyncApiDocumentBuilder V2Builder => this.Services.GetRequiredService<IV2AsyncApiDocumentBuilder>();

    IV3AsyncApiDocumentBuilder V3Builder => this.Services.GetRequiredService<IV3AsyncApiDocumentBuilder>();

    [Fact]
    public void Build_AsyncApiDocument_V2_Should_Work()
    {
        //arrange
        var specVersion = AsyncApiSpecVersion.V2;
        var id = "fake-id";
        var title = "Fake Title";
        var version = "1.0.0-alpha1";
        var description = "Fake Description";
        var contactName = "Fake Contact Name";
        var contactUri = new Uri("https://fake-contact-uri.com");
        var contactEmail = "fake@email.com";
        var licenseName = "Apache 2.0";
        var licenseUri = new Uri("https://fake-contact-uri.com");
        var termsOfServiceUri = new Uri("https://fake.com/terms-of-service");
        var defaultContentType = MediaTypeNames.Application.Json;
        var tagName = "fake-document-tag";
        var tagDescription = "Fake Document Tag Description";
        var tagDocumentationUri = new Uri("https://fake-uri.com");
        var tagDocumentationDescription = "Fake Document Tag Documentation Description";
        var serverName = "fake-server";
        var serverUrl = new Uri("https://fake-uri.com");
        var serverProtocol = AsyncApiProtocol.Http;
        var serverDescription = "fake-server-description";
        var serverVariableName = "fake-server-variable";
        var serverVariableDefaultValue = "fake-server-variable-default";
        var serverVariableEnumValues = new string[] { "value1", "value2", "value3" };
        var serverVariableDescription = "Fake Server Variable Description";
        var channelName = "fake-channel";
        var channelDescription = "Fake Channel Description";
        var channelParameterName = "fakeChannelParam";
        var channelParameterLocation = "/MQMD/CorrelId";
        var channelParameterDescription = "Fake Channel Param Description";
        var channelParameterSchema = new JsonSchemaBuilder().FromType<V2LicenseDefinition>();
        var publishOperationId = "fake-publish-operation";
        var publishOperationDescription = "Fake Publish Operation Description";
        var publishOperationSummary = "Fake Publish Operation Summary";
        var subscribeOperationId = "fake-subscribe-operation";
        var subscribeOperationDescription = "Fake Subscribe Operation Description";
        var subscribeOperationSummary = "Fake Subscribe Operation Summary";

        //act
        var document = this.V2Builder
            .WithSpecVersion(specVersion)
            .WithId(id)
            .WithTitle(title)
            .WithVersion(version)
            .WithDescription(description)
            .WithContact(contactName, contactUri, contactEmail)
            .WithLicense(licenseName, licenseUri)
            .WithTermsOfService(termsOfServiceUri)
            .WithDefaultContentType(defaultContentType)
            .WithServer(serverName, server => server
                .WithUrl(serverUrl)
                .WithProtocol(serverProtocol, "2")
                .WithDescription(serverDescription)
                .WithVariable(serverVariableName, variable => variable
                    .WithDefaultValue(serverVariableDefaultValue)
                    .WithEnumValues(serverVariableEnumValues)
                    .WithDescription(serverVariableDescription))
                .WithBinding(new HttpServerBindingDefinition()))
            .WithChannel(channelName, channel => channel
                .WithDescription(channelDescription)
                .WithBinding(new HttpChannelBindingDefinition())
                .WithParameter(channelParameterName, parameter => parameter
                    .WithLocation(channelParameterLocation)
                    .WithDescription(channelParameterDescription)
                    .WithSchema(channelParameterSchema))
                .WithPublishOperation(publish => publish
                    .WithOperationId(publishOperationId)
                    .WithDescription(publishOperationDescription)
                    .WithSummary(publishOperationSummary)
                    .WithBinding(new HttpOperationBindingDefinition())
                    .WithMessage(message => message
                        .WithPayloadOfType<V2LicenseDefinition>()))
                .WithSubscribeOperation(subscribe => subscribe
                    .WithOperationId(subscribeOperationId)
                    .WithDescription(subscribeOperationDescription)
                    .WithSummary(subscribeOperationSummary)
                    .WithBinding(new HttpOperationBindingDefinition())
                    .WithMessages
                    (
                        message1 => message1.WithPayloadOfType<V2LicenseDefinition>(),
                        message2 => message2.WithPayloadOfType<V2ContactDefinition>()
                    )))
            .WithTag(tag => tag
                .WithName(tagName)
                .WithDescription(tagDescription)
                .WithExternalDocumentation(tagDocumentationUri, tagDocumentationDescription))
            .WithExternalDocumentation(tagDocumentationUri, tagDocumentationDescription)
            .Build();

        //assert
        document.AsyncApi.Should().Be(specVersion);
        document.Id.Should().Be(id);
        document.Info.Title.Should().Be(title);
        document.Info.Version.Should().Be(version);
        document.Info.Description.Should().Be(description);
        document.Info.Contact.Should().NotBeNull();
        document.Info.Contact!.Name.Should().Be(contactName);
        document.Info.Contact!.Email.Should().Be(contactEmail);
        document.Info.Contact!.Url.Should().Be(contactUri);
        document.Info.License.Should().NotBeNull();
        document.Info.License!.Name.Should().Be(licenseName);
        document.Info.License.Url.Should().Be(licenseUri);
        document.Info.TermsOfService.Should().Be(termsOfServiceUri);
        document.DefaultContentType.Should().Be(defaultContentType);
        document.ExternalDocs.Should().NotBeNull();
        document.ExternalDocs!.Description.Should().Be(tagDocumentationDescription);
        document.ExternalDocs.Url.Should().Be(tagDocumentationUri);

        var server = document.Servers!.SingleOrDefault();
        server.Should().NotBeNull();
        server.Key.Should().Be(serverName);
        server.Value.Url.Should().Be(serverUrl);
        server.Value.Protocol.Should().Be(serverProtocol);
        server.Value.Description.Should().Be(serverDescription);
        server.Value.Bindings.Should().NotBeNull();
        server.Value.Bindings!.Http.Should().NotBeNull();

        var variable = server.Value.Variables!.SingleOrDefault();
        variable.Should().NotBeNull();
        variable.Key.Should().Be(serverVariableName);
        variable.Value.Default.Should().Be(serverVariableDefaultValue);
        variable.Value.Enum.Should().BeEquivalentTo(serverVariableEnumValues);
        variable.Value.Description.Should().Be(serverVariableDescription);

        var channel = document.Channels!.SingleOrDefault();
        channel.Should().NotBeNull();
        channel.Key.Should().Be(channelName);
        channel.Value.Publish.Should().NotBeNull();
        channel.Value.Publish!.OperationId.Should().Be(publishOperationId);
        channel.Value.Publish.Description.Should().Be(publishOperationDescription);
        channel.Value.Publish.Summary.Should().Be(publishOperationSummary);
        channel.Value.Publish.Message.Should().NotBeNull();
        channel.Value.DefinesPublishOperation.Should().BeTrue();
        channel.Value.Subscribe!.OperationId.Should().Be(subscribeOperationId);
        channel.Value.Subscribe.Description.Should().Be(subscribeOperationDescription);
        channel.Value.Subscribe.Summary.Should().Be(subscribeOperationSummary);
        channel.Value.Subscribe.Message.Should().NotBeNull();
        channel.Value.Subscribe.Message!.OneOf.Should().HaveCountGreaterThan(1);
        channel.Value.DefinesSubscribeOperation.Should().BeTrue();
        channel.Value.Bindings.Should().NotBeNull();
        channel.Value.Bindings!.Http.Should().NotBeNull();

        var parameter = channel.Value.Parameters!.SingleOrDefault();
        parameter.Should().NotBeNull();
        parameter.Key.Should().Be(channelParameterName);
        parameter.Value.Location.Should().Be(channelParameterLocation);
        parameter.Value.Description.Should().Be(channelParameterDescription);
        parameter.Value.Schema.Should().NotBeNull();

        var tag = document.Tags!.SingleOrDefault();
        tag.Should().NotBeNull();
        tag!.Name.Should().Be(tagName);
        tag.Description.Should().Be(tagDescription);
        tag.ExternalDocs.Should().NotBeNull();
        tag.ExternalDocs!.Description.Should().Be(tagDocumentationDescription);
        tag.ExternalDocs.Url.Should().Be(tagDocumentationUri);
    }

    [Fact]
    public void Build_AsyncApiDocument_V3_Should_Work()
    {
        //arrange
        var specVersion = AsyncApiSpecVersion.V2;
        var id = "fake-id";
        var title = "Fake Title";
        var version = "1.0.0-alpha1";
        var description = "Fake Description";
        var contactName = "Fake Contact Name";
        var contactUri = new Uri("https://fake-contact-uri.com");
        var contactEmail = "fake@email.com";
        var licenseName = "Apache 2.0";
        var licenseUri = new Uri("https://fake-contact-uri.com");
        var termsOfServiceUri = new Uri("https://fake.com/terms-of-service");
        var defaultContentType = MediaTypeNames.Application.Json;
        var tagName = "fake-document-tag";
        var tagDescription = "Fake Document Tag Description";
        var tagDocumentationUri = new Uri("https://fake-uri.com");
        var tagDocumentationDescription = "Fake Document Tag Documentation Description";
        var serverName = "fake-server";
        var serverHost = "fake-uri.com";
        var serverProtocol = AsyncApiProtocol.Http;
        var serverDescription = "fake-server-description";
        var serverVariableName = "fake-server-variable";
        var serverVariableDefaultValue = "fake-server-variable-default";
        var serverVariableEnumValues = new string[] { "value1", "value2", "value3" };
        var serverVariableDescription = "Fake Server Variable Description";
        var channelName = "fake-channel";
        var channelDescription = "Fake Channel Description";
        var channelParameterName = "fakeChannelParam";
        var channelParameterLocation = "/MQMD/CorrelId";
        var channelParameterDescription = "Fake Channel Param Description";
        var channelParameterSchema = new JsonSchemaBuilder().FromType<V2LicenseDefinition>();
        var sendMessageName = "fake-receive-message";
        var sendOperationName = "fake-send-operation";
        var sendOperationChannelRef = $"#/channels/{channelName}";
        var sendOperationMessageRef = $"#/components/messages/{sendMessageName}";
        var sendOperationDescription = "Fake Send Operation Description";
        var sendOperationSummary = "Fake Send Operation Summary";
        var receiveMessageName = "fake-receive-message";
        var receiveOperationName = "fake-receive-operation";
        var receiveOperationChannelRef = $"#/channels/{channelName}";
        var receiveOperationMessageRef = $"#/components/messages/{receiveMessageName}";
        var receiveOperationDescription = "Fake Receive Operation Description";
        var receiveOperationSummary = "Fake Receive Operation Summary";

        //act
        var document = this.V3Builder
            .WithSpecVersion(specVersion)
            .WithId(id)
            .WithTitle(title)
            .WithVersion(version)
            .WithDescription(description)
            .WithContact(contactName, contactUri, contactEmail)
            .WithLicense(licenseName, licenseUri)
            .WithTermsOfService(termsOfServiceUri)
            .WithDefaultContentType(defaultContentType)
            .WithServer(serverName, server => server
                .WithHost(serverHost)
                .WithProtocol(serverProtocol, "2")
                .WithDescription(serverDescription)
                .WithVariable(serverVariableName, variable => variable
                    .WithDefaultValue(serverVariableDefaultValue)
                    .WithEnumValues(serverVariableEnumValues)
                    .WithDescription(serverVariableDescription))
                .WithBinding(new HttpServerBindingDefinition()))
            .WithChannel(channelName, channel => channel
                .WithDescription(channelDescription)
                .WithBinding(new HttpChannelBindingDefinition())
                .WithParameter(channelParameterName, parameter => parameter
                    .WithLocation(channelParameterLocation)
                    .WithDescription(channelParameterDescription))
                .WithTag(tag => tag
                    .WithName(tagName)
                    .WithDescription(tagDescription)
                    .WithExternalDocumentation(doc => doc
                        .WithUrl(tagDocumentationUri)
                        .WithDescription(tagDocumentationDescription))))
            .WithOperation(sendOperationName, publish => publish
                .WithAction(v3.V3OperationAction.Send)
                .WithChannel(sendOperationChannelRef)
                .WithDescription(sendOperationDescription)
                .WithSummary(sendOperationSummary)
                .WithBinding(new HttpOperationBindingDefinition())
                .WithMessage(sendOperationMessageRef))
            .WithOperation(receiveOperationName, subscribe => subscribe
                .WithAction(v3.V3OperationAction.Receive)
                .WithChannel(receiveOperationChannelRef)
                .WithDescription(receiveOperationDescription)
                .WithSummary(receiveOperationSummary)
                .WithBinding(new HttpOperationBindingDefinition())
                .WithMessage(receiveOperationMessageRef))
            .WithExternalDocumentation(documentation => documentation
                .WithUrl(tagDocumentationUri)
                .WithDescription(tagDocumentationDescription))
            .WithTag(tag => tag
                .WithName(tagName)
                .WithDescription(tagDescription)
                .WithExternalDocumentation(doc => doc
                    .WithUrl(tagDocumentationUri)
                    .WithDescription(tagDocumentationDescription)))
            .Build();

        //assert
        document.AsyncApi.Should().Be(specVersion);
        document.Id.Should().Be(id);
        document.Info.Title.Should().Be(title);
        document.Info.Version.Should().Be(version);
        document.Info.Description.Should().Be(description);
        document.Info.Contact.Should().NotBeNull();
        document.Info.Contact!.Name.Should().Be(contactName);
        document.Info.Contact!.Email.Should().Be(contactEmail);
        document.Info.Contact!.Url.Should().Be(contactUri);
        document.Info.License.Should().NotBeNull();
        document.Info.License!.Name.Should().Be(licenseName);
        document.Info.License.Url.Should().Be(licenseUri);
        document.Info.TermsOfService.Should().Be(termsOfServiceUri);
        document.DefaultContentType.Should().Be(defaultContentType);
        document.Info.ExternalDocs.Should().NotBeNull();
        document.Info.ExternalDocs!.Description.Should().Be(tagDocumentationDescription);
        document.Info.ExternalDocs.Url.Should().Be(tagDocumentationUri);

        var server = document.Servers!.SingleOrDefault();
        server.Should().NotBeNull();
        server.Key.Should().Be(serverName);
        server.Value.Host.Should().Be(serverHost);
        server.Value.Protocol.Should().Be(serverProtocol);
        server.Value.Description.Should().Be(serverDescription);
        server.Value.Bindings.Should().NotBeNull();
        server.Value.Bindings!.Http.Should().NotBeNull();

        var variable = server.Value.Variables!.SingleOrDefault();
        variable.Should().NotBeNull();
        variable.Key.Should().Be(serverVariableName);
        variable.Value.Default.Should().Be(serverVariableDefaultValue);
        variable.Value.Enum.Should().BeEquivalentTo(serverVariableEnumValues);
        variable.Value.Description.Should().Be(serverVariableDescription);

        var channel = document.Channels!.SingleOrDefault();
        channel.Should().NotBeNull();
        channel.Key.Should().Be(channelName);
        channel.Value.Bindings.Should().NotBeNull();
        channel.Value.Bindings!.Http.Should().NotBeNull();

        var sendOperation = document.Operations?.FirstOrDefault(o => o.Key == sendOperationName).Value;
        sendOperation.Should().NotBeNull();
        sendOperation!.Description.Should().Be(sendOperationDescription);
        sendOperation.Summary.Should().Be(sendOperationSummary);
        sendOperation.Channel.Should().NotBeNull();
        sendOperation.Channel.Reference.Should().Be(sendOperationChannelRef);
        sendOperation.Messages.Should().NotBeNull();
        sendOperation.Messages.Should().Contain(m => m.Reference == sendOperationMessageRef);

        var receiveOperation = document.Operations?.FirstOrDefault(o => o.Key == receiveOperationName).Value;
        receiveOperation.Should().NotBeNull();
        receiveOperation!.Description.Should().Be(receiveOperationDescription);
        receiveOperation.Summary.Should().Be(receiveOperationSummary);
        receiveOperation.Channel.Should().NotBeNull();
        receiveOperation.Channel.Reference.Should().Be(receiveOperationChannelRef);
        receiveOperation.Messages.Should().NotBeNull();
        receiveOperation.Messages.Should().Contain(m => m.Reference == receiveOperationMessageRef);

        var parameter = channel.Value.Parameters!.SingleOrDefault();
        parameter.Should().NotBeNull();
        parameter.Key.Should().Be(channelParameterName);
        parameter.Value.Location.Should().Be(channelParameterLocation);
        parameter.Value.Description.Should().Be(channelParameterDescription);

        var tag = document.Info.Tags!.SingleOrDefault();
        tag.Should().NotBeNull();
        tag!.Name.Should().Be(tagName);
        tag.Description.Should().Be(tagDescription);
        tag.ExternalDocs.Should().NotBeNull();
        tag.ExternalDocs!.Description.Should().Be(tagDocumentationDescription);
        tag.ExternalDocs.Url.Should().Be(tagDocumentationUri);
    }

    void IDisposable.Dispose()
    {
        this.Services.Dispose();
        GC.SuppressFinalize(this);
    }

}
