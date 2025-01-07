# Neuroglia AsyncAPI

## Contents

- [Neuroglia AsyncAPI](#neuroglia-asyncapi)
  + [Contents](#contents)
  + [Summary](#summary)
  + [Status](#status)
  + [Installation](#installation)
    - [Core library](#core-library)
    - [Fluent validation library](#fluent-validation-library)
    - [Fluent builders library](#fluent-builders-library)
    - [Input/Output library](#inputoutput-library)
    - [Code-first generation library](#code-first-generation-library)
    - [Dependency inject extensions library](#dependency-inject-extensions-library)
    - [Cloud event extensions library](#cloud-event-extensions-library)
    - [AsyncAPI document serving library](#asyncapi-document-serving-library)
    - [AsyncAPI UI](#asyncapi-ui)
  + [Usage](#usage)
    - [Build AsyncAPI documents](#build-asyncapi-documents)
      + [Using AsyncAPI v2](#using-asyncapi-v2)
      + [Using AsyncAPI v3](#using-asyncapi-v3)
    - [Write AsyncAPI documents](#write-asyncapi-documents)
    - [Read AsyncAPI documents](#read-asyncapi-documents)
    - [Generate code-first AsyncAPI documents](#generate-code-first-asyncapi-documents)
      + [Using AsyncAPI v2](#using-asyncapi-v2-1)
      + [Using AsyncAPI v3](#using-asyncapi-v3-1)
    - [Generate documents explicitly](#generate-documents-explicitly)
    - [Generate documents implicitly](#generate-documents-implicitly)
    - [Use the AsyncAPI UI](#use-the-asyncapi-ui)
    - [Interact with AsyncAPI applications](#interact-with-asyncapi-applications)
  + [Samples](#samples)
    - [Streetlights API - Server](#streetlights-api---server)

## Summary
A .NET used to visualize and interact with [AsyncAPI](https://www.asyncapi.com/docs/reference/specification/v3.0.0) documents. The UI is built using Razor Pages and Boostrap

## Status

| Name | Description | Latest Release | Spec version |
|:------|:------------|:--------------:|:------------:|
| [Neuroglia.AsyncApi.Core](https://www.nuget.org/packages/Neuroglia.AsyncApi.Core) | Contains `AsyncAPI` models and core services such as fluent builders, validators, reader, writer and code-first generator | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.Validation](https://www.nuget.org/packages/Neuroglia.AsyncApi.Validation) | Contains services to validate `AsyncAPI` documents | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.IO](https://www.nuget.org/packages/Neuroglia.AsyncApi.IO) | Contains services to read and write `AsyncAPI` documents | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.Generation](https://www.nuget.org/packages/Neuroglia.AsyncApi.Generation) | Contains extensions and services for code-first generation of `AsyncAPI` documents | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.CloudEvents](https://www.nuget.org/packages/Neuroglia.AsyncApi.CloudEvents) | Contains fluent extensions to build Cloud Event messages | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.ServiceDependencyExtensions](https://www.nuget.org/packages/Neuroglia.AsyncApi.ServiceDependencyExtensions) | Contains extensions to configure and register `AsyncAPI` services using dependency injection | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.AspNetCore](https://www.nuget.org/packages/Neuroglia.AsyncApi.AspNetCore) | Contains an `ASP.NET` middleware used to serve `AsyncAPI` documents | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.AspNetCore.UI](https://www.nuget.org/packages/Neuroglia.AsyncApi.AspNetCore.UI) | Contains the Razor Pages based UI for exploring `AsyncAPI` documents | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.Client](https://www.nuget.org/packages/Neuroglia.AsyncApi.Client) | Contains client services to interact with `AsyncAPI` applications | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.Client.Bindings.All](https://www.nuget.org/packages/Neuroglia.AsyncApi.Client.Bindings.All) | References all binding handlers, for convenience | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.Client.Bindings.Amqp](https://www.nuget.org/packages/Neuroglia.AsyncApi.Client.Bindings.Amqp) | Contains the `AMQP` binding handler | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.Client.Bindings.Http](https://www.nuget.org/packages/Neuroglia.AsyncApi.Client.Bindings.Http) | Contains the `HTTP` binding handler | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.Client.Bindings.Kafka](https://www.nuget.org/packages/Neuroglia.AsyncApi.Client.Bindings.Kafka) | Contains the `Kafka` binding handler | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.Client.Bindings.Mqtt](https://www.nuget.org/packages/Neuroglia.AsyncApi.Client.Bindings.Mqtt) | Contains the `MQTT` binding handler | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.Client.Bindings.Nats](https://www.nuget.org/packages/Neuroglia.AsyncApi.Client.Bindings.Nats) | Contains the `NATS` binding handler | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.Client.Bindings.Pulsar](https://www.nuget.org/packages/Neuroglia.AsyncApi.Client.Bindings.Pulsar) | Contains the `Pulsar` binding handler | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.Client.Bindings.Redis](https://www.nuget.org/packages/Neuroglia.AsyncApi.Client.Bindings.Redis) | Contains the `Redis` binding handler | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.Client.Bindings.Solace](https://www.nuget.org/packages/Neuroglia.AsyncApi.Client.Bindings.Solace) | Contains the `Solace` binding handler | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.Client.Bindings.Stomp](https://www.nuget.org/packages/Neuroglia.AsyncApi.Client.Bindings.Stomp) | Contains the `Stomp` binding handler | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |
| [Neuroglia.AsyncApi.Client.Bindings.WebSocket](https://www.nuget.org/packages/Neuroglia.AsyncApi.Client.Bindings.WebSocket) | Contains the `WebSocket` binding handler | [3.0.1](https://github.com/neuroglia-io/asyncapi/releases/tag/v3.0.1) | [v3.0.0](https://www.asyncapi.com/docs/reference/specification/v3.0.0) |

## Installation

#### Core library
```bash
dotnet add package Neuroglia.AsyncApi.Core
```

#### Fluent validation library
```bash
dotnet add package Neuroglia.AsyncApi.Validation
```

#### Fluent builders library
```bash
dotnet add package Neuroglia.AsyncApi.FluentBuilders
```

#### Input/Output library
```bash
dotnet add package Neuroglia.AsyncApi.Validation
```

#### Code-first generation library
```bash
dotnet add package Neuroglia.AsyncApi.Generation
```

#### Dependency inject extensions library
```bash
dotnet add package Neuroglia.AsyncApi.DependencyInjectionExtensions
```

#### Cloud event extensions library
```bash
dotnet add package Neuroglia.AsyncApi.CloudEvents
```

#### AsyncAPI document serving library
```bash
dotnet add package Neuroglia.AsyncApi.AspNetCore
```
*Attention, please note that projects serving the UI MUST use the `Microsoft.NET.Sdk.Web`*

#### AsyncAPI UI
```bash
dotnet add package Neuroglia.AsyncApi.AspNetCore.UI
```
*Attention, please note that projects serving the UI MUST use the `Microsoft.NET.Sdk.Web`*

#### AsyncAPI client
```bash
dotnet add package Neuroglia.AsyncApi.Client
```

## Usage

### Building an AsyncAPI document

#### AsyncAPI v2

```csharp
var services = new ServiceCollection();
services.AddAsyncApi();
var serviceProvider = services.BuildServiceProvider();
var builder = serviceProvider.GetRequiredService<IAsyncApiDocumentBuilder>();
var document = builder
    .UsingAsyncApiV2()
    .WithTitle("Cloud Event API")
    .WithVersion("1.0.0")
    .WithServer("StreetLightsApi", server => server
        .WithUrl(new("https://streetlights.fake.com"))
        .WithProtocol(AsyncApiProtocol.Http, "2.0")
        .WithBinding(new HttpServerBindingDefinition())
        .WithSecurityRequirement("oauth2"))
    .WithChannel("/events", channel => channel
        .WithDescription("The endpoint used to publish and subscribe to cloud events")
        .WithBinding(new HttpChannelBindingDefinition())
        .WithSubscribeOperation(operation => operation
            .WithOperationId("ObserveCloudEvents")
            .WithDescription("Observes cloud events published by the StreetLightsApi")
            .WithBinding(new HttpOperationBindingDefinition() { Method = Neuroglia.AsyncApi.v2.Bindings.Http.HttpMethod.POST, Type = HttpBindingOperationType.Response })
            .WithMessages
            (
                message => message
                    .WithName("LightMeasuredEvent")
                    .WithDescription("The event fired whenever the luminosity of a light has been measured")
                    .WithContentType("application/cloudevents+json")
                    .WithTraitReference("cloud-event")
                    .WithPayloadSchema(lightMeasuredEventSchema)
                    .WithCorrelationId("$message.payload#/subject")
                    .WithTag(tag => tag
                        .WithName("light")),
                message => message
                    .WithName("MovementDetectedEvent")
                    .WithDescription("The event fired whenever a movement has been detected by a sensor")
                    .WithContentType("application/cloudevents+json")
                    .WithTraitReference("cloud-event")
                    .WithPayloadSchema(movementDetectedEventSchema)
                    .WithCorrelationId("$message.payload#/subject")
                    .WithTag(tag => tag
                        .WithName("movement"))
            )))
    .WithMessageTraitComponent("cloud-event", message => message
        .WithBinding(new HttpMessageBindingDefinition())
        .WithContentType("application/cloudevents+json"))
    .WithSecurityScheme("oauth2", scheme => scheme
        .WithType(SecuritySchemeType.OAuth2)
        .WithDescription("The security scheme used to authorize application requests")
        .WithAuthorizationScheme("Bearer")
        .WithOAuthFlows(oauth => oauth
            .WithClientCredentialsFlow(flow => flow
                .WithAuthorizationUrl(new("https://fake.idp.com/token"))
                .WithScope("api:read", "The scope used to read data")))))
    .Build();
```

#### AsyncAPI V3

```csharp
var services = new ServiceCollection();
services.AddAsyncApi();
var serviceProvider = services.BuildServiceProvider();
var builder = serviceProvider.GetRequiredService<IAsyncApiDocumentBuilder>();
var document = builder
    .UsingAsyncApiV3()
    .WithTitle("Cloud Event API")
    .WithVersion("1.0.0")
    .WithServer("StreetLightsApi", server => server
        .WithHost("https://streetlights.fake.com")
        .WithProtocol(AsyncApiProtocol.Http, "2.0")
        .WithBinding(new HttpServerBindingDefinition())
        .WithSecurityRequirement(security => security
            .Use("#/components/securitySchemes/oauth2")))
    .WithChannel("events", channel => channel
        .WithServer("#/servers/StreetLightsApi")
        .WithDescription("The endpoint used to publish and subscribe to cloud events")
        .WithBinding(new HttpChannelBindingDefinition()))
    .WithOperation("observeCloudEvents", operation => operation
        .WithAction(Neuroglia.AsyncApi.v3.V3OperationAction.Send)
        .WithChannel("#/channels/events")
        .WithTitle("ObserveCloudEvents")
        .WithDescription("Observes cloud events published by the StreetLightsApi")
        .WithBinding(new HttpOperationBindingDefinition() 
        { 
            Method = Neuroglia.AsyncApi.Bindings.Http.HttpMethod.POST, 
            Type = HttpBindingOperationType.Response 
        })
        .WithMessage("#/components/messages/lightMeasuredEvent"))
    .WithMessageComponent("lightMeasuredEvent", message => message
        .WithName("LightMeasuredEvent")
        .WithDescription("The event fired whenever the luminosity of a light has been measured")
        .WithContentType("application/cloudevents+json")
        .WithTrait(trait => trait
            .Use("#/components/messageTraits/cloud-event"))
        .WithPayloadSchema(schema => schema
            .WithFormat("application/schema+json")
            .WithSchema(lightMeasuredEventSchema))
        .WithCorrelationId(setup => setup
            .WithLocation("$message.payload#/subject"))
        .WithTag(tag => tag
            .WithName("light")))
    .WithMessageComponent("movementDetectedEvent", message => message
        .WithName("MovementDetectedEvent")
        .WithDescription("The event fired whenever a movement has been detected by a sensor")
        .WithContentType("application/cloudevents+json")
        .WithTrait(trait => trait
            .Use("#/components/messageTraits/cloud-event"))
        .WithPayloadSchema(schema => schema
            .WithFormat("application/schema+json")
            .WithSchema(movementDetectedEventSchema))
        .WithCorrelationId(setup => setup
            .WithLocation("$message.payload#/subject"))
        .WithTag(tag => tag
            .WithName("movement")))
    .WithMessageTraitComponent("cloud-event", message => message
        .WithBinding(new HttpMessageBindingDefinition())
        .WithContentType("application/cloudevents+json"))
    .WithSecuritySchemeComponent("oauth2", scheme => scheme
        .WithType(SecuritySchemeType.OAuth2)
        .WithDescription("The security scheme used to authorize application requests")
        .WithAuthorizationScheme("Bearer")
        .WithOAuthFlows(oauth => oauth
            .WithClientCredentialsFlow(flow => flow
                .WithAuthorizationUrl(new("https://fake.idp.com/token"))
                .WithScope("api:read", "The scope used to read data")))));
```

### Write AsyncAPI documents

```csharp
var writer = serviceProvider.GetRequiredService<IAsyncApiDocumentWriter>();
using MemoryStream stream = new();
await writer.WriteAsync(document, stream, AsyncApiDocumentFormat.Yaml, cancellationToken);
```

### Read AsyncAPI documents

```csharp
var reader = serviceProvider.GetRequiredService<IAsyncApiDocumentReader>();
var asyncApi = await reader.ReadAsync(stream, cancellationToken);
```

### Generate code-first AsyncAPI documents

#### Using AsyncAPI V2

```csharp
[AsyncApi("Streetlights API", "1.0.0", Description = "The Smartylighting Streetlights API allows you to remotely manage the city lights.", LicenseName = "Apache 2.0", LicenseUrl = "https://www.apache.org/licenses/LICENSE-2.0")]
public class StreetLightsService
    : BackgroundService
{

  ... //Omitted for brevity
  
  [Channel("light/measured"), PublishOperation(OperationId = "onLightMeasured", Summary = "Inform about environmental lighting conditions for a particular streetlight")]
  public async Task PublishLightMeasured(LightMeasuredEvent e)
  {
        ...
  }
  
  [Channel("light/measured"), SubscribeOperation(OperationId = "lightMeasuredEvent", Summary = "Inform about environmental lighting conditions for a particular streetlight")]
  protected async Task OnLightMeasured(LightMeasuredEvent e)
  {
        ...
  }
  
  ...

}
```

Note the usage of the following attributes:

- `AsyncApiV2`: Marks a class for code-first `AsyncAPI` document generation. Used to provide information about the API (licensing, contact, ...)
- `ChannelV2`: Marks a method or class for code-first `AsyncAPI` channel generation. Used to provide information about the channel marked methods belong to.
- `OperationV2`: Marks a method for code-first `AsyncAPI` operation generation. Use to provide information about the `AsyncAPI` operation.

#### Using AsyncAPI V3

```csharp
[AsyncApi("Streetlights API", "1.0.0", Description = "The **Smartylighting Streetlights API** allows you to remotely manage the city lights.", LicenseName = "Apache 2.0", LicenseUrl = "https://www.apache.org/licenses/LICENSE-2.0")]
[Server("http", "http://fake-http-server.com", AsyncApiProtocol.Http, PathName = "/{environment}", Description = "A sample **HTTP** server declared using attributes", Bindings = "#/components/serverBindings/http")]
[ServerVariable("http", "environment", Description = "The **environment** to use.", Enum = ["dev", "stg", "prod"])]
[HttpServerBinding("http")]
[Channel("lightingMeasuredMQTT", Address = "streets.{streetName}", Description = "This channel is used to exchange messages about lightning measurements.", Servers = ["#/servers/mosquitto"], Bindings = "#/components/channelBindings/mqtt")]
[MqttChannelBinding("mqtt")]
[ChannelParameter("lightingMeasured", "streetName", Description = "The name of the **street** the lights to get measurements for are located in")]
public class StreetLightsService
    : BackgroundService
{

    [Operation("sendLightMeasurement", V3OperationAction.Send, "#/channels/lightingMeasuredMQTT", Description = "Notifies remote **consumers** about environmental lighting conditions for a particular **streetlight**."), Neuroglia.AsyncApi.v3.Tag(Reference = "#/components/tags/measurement")]
    public async Task PublishLightMeasured(LightMeasuredEvent e, CancellationToken cancellationToken = default)
    {
        ...
    }

    [Operation("receiveLightMeasurement", V3OperationAction.Receive, "#/channels/lightingMeasuredMQTT"), Neuroglia.AsyncApi.v3.Tag(Reference = "#/components/tags/measurement")]
    protected Task OnLightMeasured(LightMeasuredEvent e)
    {
        ...
    }

}
```

#### Generate documents explicitly

```csharp
var generator = serviceProvider.GetRequiredService<IAsyncApiDocumentGenerator>();
var options = new AsyncApiDocumentGenerationOptions()
{
    DefaultV2Configuration = builder =>
    {
        //Setup V2 documents, by configuring servers, for example
    };
    DefaultV3Configuration = builder =>
    {
        //Setup V3 documents, by configuring servers, for example
    }
};
IEnumerable<AsyncApiDocument> documents = generator.GenerateAsync(typeof(StreetLightsService), options);
```

#### Generate documents implicitly

```csharp
services.AddAsyncApiGeneration(builder => 
    builder
        .WithMarkupType<StreetLightsService>()
        .UseDefaultV2Configuration(asyncApi =>
        {
            //Setup V2 documents, by configuring servers, for example
        })
        .UseDefaultV3Configuration(asyncApi =>
        {
            //Setup V3 documents, by configuring servers, for example
        }));
```

### Use the AsyncAPI UI

#### 1. Configure services

```csharp
...
builder.Services.AddAsyncApiUI();
...
```

#### 2. Map documents

```csharp
...
var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapAsyncApiDocuments();
app.MapRazorPages();
...
```

**Note**: Since Razor Pages are used to render the UI, make sure to configure its services and map the pages:

```csharp
...
builder.Services.AddRazorPages();
...
var app = builder.Build();
...
app.MapRazorPages();
...
```

You will also need to register an `IJsonSchemaResolver` and an `HttpClient`:
```csharp
services.AddSingleton<IJsonSchemaResolver, JsonSchemaResolver>();
services.AddHttpClient();
```
*For reference, please refer to the [sample](#streetlights-api---server).*

#### 3. Enjoy!

Launch your application, then navigate to `/asyncapi`. You should see something like this:

![AsyncAPI UI - Screenshot](/assets/img/ui.png)

### Interact with AsyncAPI applications

#### Supported bindings

- ✅ AMQP
- ✅ AMQP1
- ❌ AnypointMQ
- ❌ GooglePubSub
- ✅ HTTP/HTTPS
- ❌ IBMMQ
- ❌ JMS
- ✅ Kafka
- ❌ Mercure
- ✅ MQTT
- ✅ MQTT5
- ✅ NATS
- ✅ Pulsar
- ✅ Redis
- ❌ SNS
- ✅ Solace
- ❌ SQS
- ✅ Stomp
- ✅ WS/WSS

⚠️ **Warning:** Security requirements have not yet been implemented.

*We welcome community contributions to help implement and improve additional binding handlers!*

#### Configuring an AsyncAPI Client:

Configure an AsyncAPI client with all binding handlers:

```csharp
services.AddAsyncApiClient(options => options.AddAllBindingHandlers());
```

Configure an AsyncAPI client using specific binding handlers:

```csharp
services.AddAsyncApiClient(options => 
{
  options.AddHttpBindingHandler();
  options.AddWebSocketBindingHandler();
});
```

These configurations ensure that your application is set up to handle various communication protocols as defined in your AsyncAPI document.

#### Creating an AsyncAPI Client:

Once the services are configured, you can create an AsyncAPI client using the client factory:

```csharp
var clientFactory = ServiceProvider.GetRequiredService<IAsyncApiClientFactory>();
await using var client = clientFactory.CreateFor(asyncApiDocument);
```

Replace `asyncApiDocument` with your loaded AsyncAPI document instance.

#### Publishing a message:

To publish a message using the AsyncAPI client, construct the operation parameters with your payload and headers, then call PublishAsync:

```csharp
var parameters = new AsyncApiPublishOperationParameters("Greet")
{
  Payload = new 
  { 
    Greeting = "Hello, World!" 
  },
  Headers = new
  {
    SomeHeader = "SomeHeaderValue"
  }
};
await using var result = await client.PublishAsync(parameters);
```

This example publishes a message to the operation named "Greet" with a payload and headers. Adjust the operation name, payload, and headers as needed for your application.

#### Subscribing to messages:

To subscribe and react to messages from an AsyncAPI operation:

```csharp
await using var result = await client.SubscribeAsync(new AsyncApiSubscribeOperationParameters("Greet"));
result.Messages?.Subscribe(message => 
{
    Console.WriteLine(message.Payload?.Greeting);
});
```

This code subscribes to the "Greet" operation and prints the "Greeting" property of each received message's payload. Modify the operation name and message handling logic based on your specific use case.

## Samples

### [Streetlights API - Server](https://github.com/neuroglia-io/AsyncApi/tree/main/samples/StreetLightsApi/Server)

A simple `ASP.NET 9.0` REST API using a MQTT-powered message bus to send and receive information about environmental lighting conditions for a particular streetlight.

Clone the project in your favorite IDE, launch the app, and navigate to `https://localhost:44326/asyncapi/StreetLightsApi/1.0.0`. You should see something like the following:

```yaml
asyncapi: 2.6.0
info:
  title: Streetlights API
  version: 1.0.0
  description: The Smartylighting Streetlights API allows you to remotely manage the city lights.
  contact: {}
  license:
    name: Apache 2.0
    url: https://www.apache.org/licenses/LICENSE-2.0
servers:
  mosquitto:
    url: mqtt://test.mosquitto.org/
    protocol: mqtt
channels:
  light/measured:
    subscribe:
      message:
        payload:
          type: object
          properties:
            id:
              type: integer
            lumens:
              type: integer
            sentAt:
              type: string
              format: date-time
          required:
          - Id
          - Lumens
          - SentAt
        name: LightMeasured
        title: Light Measured
      operationId: lightMeasuredEvent
      summary: Inform about environmental lighting conditions for a particular streetlight
    publish:
      message:
        payload:
          type: object
          properties:
            id:
              type: integer
            lumens:
              type: integer
            sentAt:
              type: string
              format: date-time
          required:
          - Id
          - Lumens
          - SentAt
        name: LightMeasured
        title: Light Measured
      operationId: onLightMeasured
      summary: Inform about environmental lighting conditions for a particular streetlight
```