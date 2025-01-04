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

using Neuroglia.AsyncApi.Bindings;
using Neuroglia.AsyncApi.FluentBuilders.v3;
using Neuroglia.AsyncApi.v3;

namespace Neuroglia.AsyncApi.UnitTests.Cases.Core;

public class DereferenceTests
    : IDisposable
{

    public DereferenceTests()
    {
        var services = new ServiceCollection();
        services.AddAsyncApi();
        this.ServiceProvider = services.BuildServiceProvider();
    }

    protected ServiceProvider ServiceProvider { get; }

    protected IV3AsyncApiDocumentBuilder DocumentBuilder => this.ServiceProvider.GetRequiredService<IV3AsyncApiDocumentBuilder>();

    [Fact]
    public void Dereference_Server_Should_Work()
    {
        //arrange
        var componentName = "fake-server";
        var componentRef = $"#/servers/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithServer(componentName, component => component
                .WithHost("fake-host")
                .WithProtocol(AsyncApiProtocol.AmqpV1)
                .WithTitle("Fake Server"))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceServer(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ServerDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ServerDefinition>();
    }

    [Fact]
    public void Dereference_Channel_Should_Work()
    {
        //arrange
        var componentName = "fake-channel";
        var componentRef = $"#/channels/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithChannel(componentName, component => component
                .WithTitle("Fake Channel")
                .WithServer("#/components/servers/fake-server"))
            .WithServerComponent("fake-server", server => server
                .WithHost("fake-host")
                .WithProtocol(AsyncApiProtocol.AmqpV1))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceChannel(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ChannelDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ChannelDefinition>();
    }

    [Fact]
    public void Dereference_Operation_Should_Work()
    {
        //arrange
        var componentName = "fake-operation";
        var componentRef = $"#/operations/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithOperation(componentName, component => component
                .WithTitle("Fake Operation")
                .WithChannel("#/components/channels/fake-channel"))
            .WithServerComponent("fake-server", server => server
                .WithHost("fake-host")
                .WithProtocol(AsyncApiProtocol.AmqpV1))
            .WithChannelComponent("fake-channel", component => component
                .WithTitle("Fake Channel")
                .WithServer("#/components/servers/fake-server"))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceOperation(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3OperationDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3OperationDefinition>();
    }

    [Fact]
    public void Dereference_Server_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-server";
        var componentRef = $"#/components/servers/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithServerComponent(componentName, component => component
                .WithHost("fake-host")
                .WithProtocol(AsyncApiProtocol.AmqpV1)
                .WithTitle("Fake Server"))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceServer(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ServerDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ServerDefinition>();
    }

    [Fact]
    public void Dereference_Channel_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-channel";
        var componentRef = $"#/components/channels/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithChannelComponent(componentName, component => component
                .WithTitle("Fake Channel")
                .WithServer("#/components/servers/fake-server"))
            .WithServerComponent("fake-server", server => server
                .WithHost("fake-host")
                .WithProtocol(AsyncApiProtocol.AmqpV1))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceChannel(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ChannelDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ChannelDefinition>();
    }

    [Fact]
    public void Dereference_Operation_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-operation";
        var componentRef = $"#/components/operations/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithServerComponent("fake-server", server => server
                .WithHost("fake-host")
                .WithProtocol(AsyncApiProtocol.AmqpV1))
            .WithChannelComponent("fake-channel", component => component
                .WithTitle("Fake Channel")
                .WithServer("#/components/servers/fake-server"))
            .WithOperationComponent(componentName, component => component
                .WithTitle("Fake Operation")
                .WithChannel("#/components/channels/fake-channel"))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceOperation(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3OperationDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3OperationDefinition>();
    }

    [Fact]
    public void Dereference_Schema_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-schema";
        var componentRef = $"#/components/schemas/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithSchemaComponent(componentName, component => component 
                .WithFormat("Fake Format")
                .WithSchema(new { }))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceSchema(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3SchemaDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3SchemaDefinition>();
    }

    [Fact]
    public void Dereference_Message_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-message";
        var componentRef = $"#/components/messages/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithMessageComponent(componentName, component => component 
                .WithTitle("Fake Message"))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceMessage(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3MessageDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3MessageDefinition>();
    }

    [Fact]
    public void Dereference_SecurityScheme_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-security-scheme";
        var componentRef = $"#/components/securitySchemes/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithSecuritySchemeComponent(componentName, component => component 
                .WithDescription("Fake Security Scheme"))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceSecurityScheme(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3SecuritySchemeDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3SecuritySchemeDefinition>();
    }

    [Fact]
    public void Dereference_ServerVariable_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-server-variable";
        var componentRef = $"#/components/serverVariables/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithServerVariableComponent(componentName, component => component 
                .WithDescription("Fake Server Variable"))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceServerVariable(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ServerVariableDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ServerVariableDefinition>();
    }

    [Fact]
    public void Dereference_Parameter_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-parameter";
        var componentRef = $"#/components/parameters/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithParameterComponent(componentName, component => component 
                .WithDescription("Fake Parameter"))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceParameter(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ParameterDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ParameterDefinition>();
    }

    [Fact]
    public void Dereference_CorrelationId_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-correlationId";
        var componentRef = $"#/components/correlationIds/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithCorrelationIdComponent(componentName, component => component 
                .WithDescription("Fake Correlation Id")
                .WithLocation("$message.payload#/fake-location"))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceCorrelationId(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3CorrelationIdDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3CorrelationIdDefinition>();
    }

    [Fact]
    public void Dereference_Reply_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-reply";
        var componentRef = $"#/components/replies/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithServerComponent("fake-server", server => server
                .WithHost("fake-host")
                .WithProtocol(AsyncApiProtocol.AmqpV1))
            .WithChannelComponent("fake-channel", component => component
                .WithTitle("Fake Channel")
                .WithServer("#/components/servers/fake-server"))
            .WithReplyComponent(componentName, component => component 
                .WithChannel("#/components/channels/fake-channel"))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceReply(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ReplyDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ReplyDefinition>();
    }

    [Fact]
    public void Dereference_ReplyAddress_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-reply-address";
        var componentRef = $"#/components/replyAddresses/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithReplyAddressComponent(componentName, component => component 
                .WithDescription("Fake Reply Address")
                .WithLocation("$message.headers#/fake-location"))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceReplyAddress(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ReplyAddressDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ReplyAddressDefinition>();
    }

    [Fact]
    public void Dereference_ExternalDocs_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-external-docs";
        var componentRef = $"#/components/externalDocs/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithExternalDocumentationComponent(componentName, component => component
                .WithDescription("Fake External Docs")
                .WithUrl(new("https://fake-url.coml")))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceExternalDocumentation(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ExternalDocumentationDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3ExternalDocumentationDefinition>();
    }

    [Fact]
    public void Dereference_Tag_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-tag";
        var componentRef = $"#/components/tags/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithTagComponent(componentName, component => component
                .WithName("fake-tag")
                .WithDescription("Fake Tag"))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceTag(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3TagDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3TagDefinition>();
    }

    [Fact]
    public void Dereference_OperationTrait_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-operation-trait";
        var componentRef = $"#/components/operationTraits/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithOperationTraitComponent(componentName, component => component
                .WithDescription("Fake Operation Trait"))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceOperationTrait(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3OperationTraitDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3OperationTraitDefinition>();
    }

    [Fact]
    public void Dereference_MessageTrait_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-message-trait";
        var componentRef = $"#/components/messageTraits/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithMessageTraitComponent(componentName, component => component
                .WithDescription("Fake Message Trait"))
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceMessageTrait(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3MessageTraitDefinition>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<V3MessageTraitDefinition>();
    }

    [Fact]
    public void Dereference_ServerBinding_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-server-binding";
        var componentRef = $"#/components/serverBindings/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithServerBindingsComponent(componentName, new ServerBindingDefinitionCollection())
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceServerBinding(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<ServerBindingDefinitionCollection>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<ServerBindingDefinitionCollection>();
    }

    [Fact]
    public void Dereference_ChannelBinding_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-channel-binding";
        var componentRef = $"#/components/channelBindings/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithChannelBindingsComponent(componentName, new ChannelBindingDefinitionCollection())
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceChannelBinding(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<ChannelBindingDefinitionCollection>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<ChannelBindingDefinitionCollection>();
    }

    [Fact]
    public void Dereference_OperationBinding_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-operation-binding";
        var componentRef = $"#/components/operationBindings/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithOperationBindingsComponent(componentName, new OperationBindingDefinitionCollection())
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceOperationBinding(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<OperationBindingDefinitionCollection>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<OperationBindingDefinitionCollection>();
    }

    [Fact]
    public void Dereference_MessageBinding_Component_Should_Work()
    {
        //arrange
        var componentName = "fake-message-binding";
        var componentRef = $"#/components/messageBindings/{componentName}";
        var document = this.DocumentBuilder
            .WithTitle("fake")
            .WithVersion("1.0.0")
            .WithMessageBindingsComponent(componentName, new MessageBindingDefinitionCollection())
            .Build();

        //act
        var dereferenceGeneric = () => document.Dereference(componentRef);
        var dereference = () => document.DereferenceMessageBinding(componentRef);

        //assert
        dereferenceGeneric.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<MessageBindingDefinitionCollection>();
        dereference.Should().NotThrow().Which.Should().NotBeNull().And.BeOfType<MessageBindingDefinitionCollection>();
    }

    void IDisposable.Dispose()
    {
        this.ServiceProvider.Dispose();
        GC.SuppressFinalize(this);
    }

}
