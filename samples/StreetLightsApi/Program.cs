using Json.Schema;
using Neuroglia.AsyncApi;
using Neuroglia.AsyncApi.Specification.v2;
using Neuroglia.AsyncApi.Specification.v2.Bindings.Http;
using Neuroglia.AsyncApi.Specification.v2.Bindings.Mqtt;
using StreetLightsApi.Server.Services;

var builder = WebApplication.CreateBuilder(args);

using var httpClient = new HttpClient();
var cloudEventSchema = JsonSchema.FromText(await httpClient.GetStringAsync("https://raw.githubusercontent.com/cloudevents/spec/v1.0.1/spec.json").ConfigureAwait(false));

var jsonSchemaBuilder = new JsonSchemaBuilder();
jsonSchemaBuilder.Type(SchemaValueType.Object);
jsonSchemaBuilder.AllOf(cloudEventSchema);
jsonSchemaBuilder.Properties(("type", new JsonSchemaBuilder().Const("com.streetlights.light.measured.v1").Build()));
var lightMeasuredEventSchema = jsonSchemaBuilder.Build();

jsonSchemaBuilder = new JsonSchemaBuilder();
jsonSchemaBuilder.Type(SchemaValueType.Object);
jsonSchemaBuilder.AllOf(cloudEventSchema);
jsonSchemaBuilder.Properties(("type", new JsonSchemaBuilder().Const("com.streetlights.sensor.movement-detected.v2").Build()));
var movementDetectedEventSchema = jsonSchemaBuilder.Build();

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
builder.Services.AddAsyncApiUI();
builder.Services.AddAsyncApiGeneration(builder =>
    builder.WithMarkupType<StreetLightsService>()
        .UseDefaultConfiguration(asyncApi =>
        {
            asyncApi
                .WithTermsOfService(new Uri("https://www.websitepolicies.com/blog/sample-terms-service-template"))
                .WithServer("mosquitto", server => server
                    .WithUrl(new Uri("mqtt://test.mosquitto.org"))
                    .WithProtocol(AsyncApiProtocol.Mqtt)
                    .WithDescription("The Mosquitto test MQTT server")
                    .WithBinding(new MqttServerBindingDefinition()
                    {
                        ClientId = "StreetLightsAPI:1.0.0",
                        CleanSession = true
                    }));
        }));
builder.Services.AddAsyncApiDocument(document => document
    .WithTitle("Cloud Event API")
    .WithVersion("1.0.0")
    .WithServer("StreetLightsApi", server => server
        .WithUrl(new("https://streetlights.fake.com"))
        .WithProtocol(AsyncApiProtocol.Http, "2.0")
        .WithBinding(new HttpServerBindingDefinition()))
    .WithChannel("/events", channel => channel
        .WithDescription("The endpoint used to publish and subscribe to cloud events")
        .WithBinding(new HttpChannelBindingDefinition())
        .WithSubscribeOperation(operation => operation
            .WithOperationId("ObserveCloudEvents")
            .WithDescription("Observes cloud events published by the StreetLightsApi")
            .WithMessages
            (
                message => message
                    .WithName("LightMeasuredEvent")
                    .WithDescription("The event fired whenever the luminosity of a light has been measured")
                    .WithTraitReference("cloud-event")
                    .WithPayloadSchema(lightMeasuredEventSchema),
                message => message
                    .WithName("MovementDetectedEvent")
                    .WithDescription("The event fired whenever a movement has been detected by a sensor")
                    .WithTraitReference("cloud-event")
                    .WithPayloadSchema(movementDetectedEventSchema)
            )))
    .AddMessageTrait("cloud-event", message => message
        .WithBinding(new HttpMessageBindingDefinition())
        .WithContentType("application/cloudevents+json")));

var app = builder.Build();
app.UseRouteDebugger("/tools/route-debugger");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapAsyncApiDocuments();
app.MapRazorPages();

app.Run();
