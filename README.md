# Neuroglia AsyncAPI

## Contents

- [Summary](#summary)
- [Status](#status)
- [Installation](#installation)
- [Usage](#usage)
  - [Building an AsyncAPI Document](#building-an-asyncapi-document)
  - [Writing an AsyncAPI Document](#writing-an-asyncapi-document)
  - [Reading an AsyncAPI Document](#reading-an-asyncapi-document)
  - [Generating code-first AsyncAPI documents](#generating-code-first-asyncapi-documents)
  - [Using the AsyncAPI UI](#using-the-asyncapi-ui)
- [Samples](#samples)
  - [Streetlights API - Server](#streetlights-api---server)

## Summary
A .NET 5.0 library used to visualize and interact with [AsyncAPI](https://www.asyncapi.com/docs/specifications/v2.1.0) documents. The UI is built using Razor Pages and Boostrap 4.0

## Status

| Name | Description | Latest Release | Spec version |
| :---: | :---: | :---: | :---: |
| [Neuroglia.AsyncApi.Core](http://www.nuget.org/packages/Neuroglia.AsyncApi.Core) | Contains `AsyncAPI` models and core services such as fluent builders, validators, reader, writer and code-first generator | [2.1.0.1](https://github.com/neuroglia-io/asyncapi/releases/) | [v2.1.0](https://www.asyncapi.com/docs/specifications/v2.1.0) |
[Neuroglia.AsyncApi.AspNetCore](http://www.nuget.org/packages/Neuroglia.AsyncApi.AspNetCore) | Contains `ASP.NET 5.0` extensions, services for code-first generation and middleware for serving `AsyncAPI` documents | [2.1.0.1](https://github.com/neuroglia-io/asyncapi/releases/) | [v2.1.0](https://www.asyncapi.com/docs/specifications/v2.1.0) |
[Neuroglia.AsyncApi.AspNetCore.UI](http://www.nuget.org/packages/Neuroglia.AsyncApi.AspNetCore.UI) | Contains `ASP.NET 5.0` extensions, services for code-first generation and middleware for serving `AsyncAPI` documents | [2.1.0.1](https://github.com/neuroglia-io/asyncapi/releases/) | [v2.1.0](https://www.asyncapi.com/docs/specifications/v2.1.0) |
Neuroglia.AsyncApi.Client | Contains services to build clients at runtime based on `AsyncAPI` documents | WIP | [v2.1.0](https://www.asyncapi.com/docs/specifications/v2.1.0) |

## Installation

### Core library
```bash
dotnet add package Neuroglia.AsyncApi.Core --version 2.1.0.1
```

### Code-first generation library
```bash
dotnet add package Neuroglia.AsyncApi.AspNetCore --version 2.1.0.1
```

### AsyncAPI UI
```bash
dotnet add package Neuroglia.AsyncApi.AspNetCore.UI --version 2.1.0.1
```

### AsyncAPI client
```bash
dotnet add package Neuroglia.AsyncApi.Client --version 2.1.0.1
```

## Usage

### Building an AsyncAPI Document

```csharp
IServiceCollection services = new ServiceCollection();
services.AddAsyncApi();
var serviceProvider = services.BuildServiceProvider();
var builder = serviceProvider.GetRequiredService<IAsyncApiDocumentBuilder>();
var document = builder
    .UseAsyncApi("2.1.0")
    .WithTitle("Streetlights API")
    .WithVersion("1.0.0")
    .WithDescription("The Smartylighting Streetlights API allows you to remotely manage the city lights.")
    .WithLicense("Apache 2.0", new Uri("https://www.apache.org/licenses/LICENSE-2.0"))
    .UseServer("mosquitto", server => server
        .WithUrl(new Uri("mqtt://test.mosquitto.org"))
        .WithProtocol("mqtt"))
    .UseChannel("light/measured", channel => channel
        .DefinePublishOperation(operation => operation
            .WithSummary("Inform about environmental lighting conditions for a particular streetlight.")
            .WithOperationId("onLightMeasured")
            .UseMessage(message => message
                .WithName("LightMeasured")
                .OfType<LightMeasuredEvent>())))
    .Build();
```

### Writing an AsyncAPI Document 

```csharp
var writer = serviceProvider.GetRequiredService<IAsyncApiDocumentWriter>();
using MemoryStream stream = new();
await writer.WriteAsync(document, stream, AsyncApiDocumentFormat.Yaml, cancellationToken);
```

### Reading an AsyncAPI document

```csharp
var reader = serviceProvider.GetRequiredService<IAsyncApiDocumentReader>();
AsyncApiDocument asyncApi = await reader.ReadAsync(stream, cancellationToken);
```

### Generating code-first AsyncAPI documents

#### 1. Mark your services with adequate attributes

```csharp
[AsyncApi("Streetlights API", "1.0.0", Description = "The Smartylighting Streetlights API allows you to remotely manage the city lights.", LicenseName = "Apache 2.0", LicenseUrl = "https://www.apache.org/licenses/LICENSE-2.0")]
    public class StreetLightsService
        : BackgroundService
    {

        ... //Omitted for brevity

        [Channel("light/measured"), PublishOperation(OperationId = "onLightMeasured", Summary = "Inform about environmental lighting conditions for a particular streetlight")]
        public async Task PublishLightMeasured(LightMeasuredEvent e)
        {
            MqttApplicationMessage message = new()
            {
                Topic = "onLightMeasured",
                ContentType = "application/json",
                Payload = Encoding.UTF8.GetBytes(await this.Serializer.SerializeAsync(e))
            };
            await this.MqttClient.PublishAsync(message);
        }

        [Channel("light/measured"), SubscribeOperation(OperationId = "lightMeasuredEvent", Summary = "Inform about environmental lighting conditions for a particular streetlight")]
        protected async Task OnLightMeasured(LightMeasuredEvent e)
        {
            this.Logger.LogInformation($"Event received:{Environment.NewLine}{await this.Serializer.SerializeAsync(e)}");
        }

        ...

    }
```

Note the usage of the following attributes:

- `AsyncApi`: Marks a class for code-first `AsyncAPI` document generation. Used to provide information about the API (licensing, contact, ...)
- `Channel`: Marks a method or class for code-first `AsyncAPI` channel generation. Used to provide information about the channel marked methods belong to.
- `Operation`: Marks a method for code-first `AsyncAPI` operation generation. Use to provide information about the `AsyncAPI` operation.

#### 2.1. Generating documents manually

```csharp
var generator = serviceProvider.GetRequiredService<IAsyncApiDocumentGenerator>();
var options = new AsyncApiDocumentGenerationOptions()
{
    DefaultConfiguration = builder =>
    {
        //Setup the document by configuring servers, for example
    }
};
IEnumerable<AsyncApiDocument> documents = generator.GenerateAsync(typeof(StreetLightsService), options);
```

#### 2.2. Generating documents automatically and serve them using ASP

Go to your ASP project's `Startup.cs` file and add the following lines:
```csharp
//Startup.cs

public class Startup
{

    ...

    public void ConfigureServices(IServiceCollection services)
    {
        ...
        //Registers and configures the AsyncAPI code-first generation
        services.AddAsyncApiGeneration(builder => 
            builder.WithMarkupType<StreetLightsService>()
                .UseDefaultConfiguration(asyncApi =>
                {
                    asyncApi.UseServer("mosquitto", server => server
                        .WithUrl(new Uri("mqtt://test.mosquitto.org"))
                        .WithProtocol(AsyncApiProtocols.Mqtt));
                }));
        ...
    }

    public void Configure(IApplicationBuilder app)
    {
        ...
        //Adds the middleware used to serve AsyncAPI documents
        app.UseAsyncApiGeneration();
        ...
    }

}

```

### Using the AsyncAPI UI

Go to your ASP project's `Startup.cs` file and add the following line to your `ConfigureServices` method:

```csharp
services.AddAsyncApiUI();
```

Launch your ASP project, then navigate to `http://localhost:44236/asyncapi`. You should see something like this:

![AsyncAPI UI - Screenshot](/assets/img/ui.png)

## Samples

### [Streetlights API - Server](https://github.com/neuroglia-io/AsyncApi/tree/main/samples/StreetLightsApi/Server)

A simple `ASP.NET 5.0` REST API using a MQTT-powered message bus to send and receive information about environmental lighting conditions for a particular streetlight.

Clone the project in your favorite IDE, launch the app, and navigate to `https://localhost:44326/asyncapi/StreetLightsApi/1.0.0`. You should see something like the following:

```yaml
asyncapi: 2.1.0
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
