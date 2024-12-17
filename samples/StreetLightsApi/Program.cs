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

var builder = WebApplication.CreateBuilder(args);

using var httpClient = new HttpClient();
var cloudEventSchema = JsonSchema.FromText(await httpClient.GetStringAsync("https://raw.githubusercontent.com/cloudevents/spec/v1.0.1/spec.json").ConfigureAwait(false));
var jsonSchemaBuilder = new JsonSchemaBuilder();
jsonSchemaBuilder.Type(SchemaValueType.Object);
jsonSchemaBuilder.AllOf(cloudEventSchema);
jsonSchemaBuilder.Properties(("type", new JsonSchemaBuilder().Const("com.streetlights.light.measured.v1").Build()), ("datacontenttype", new JsonSchemaBuilder().Const(MediaTypeNames.Application.Json).Build()));
var lightMeasuredEventSchema = jsonSchemaBuilder.Build();
jsonSchemaBuilder = new JsonSchemaBuilder();
jsonSchemaBuilder.Type(SchemaValueType.Object);
jsonSchemaBuilder.AllOf(cloudEventSchema);
jsonSchemaBuilder.Properties(("type", new JsonSchemaBuilder().Const("com.streetlights.sensor.movement-detected.v2").Build()), ("datacontenttype", new JsonSchemaBuilder().Const(MediaTypeNames.Application.Json).Build()));
var movementDetectedEventSchema = jsonSchemaBuilder.Build();

builder.Services.AddHttpClient();
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
        .WithBinding(new HttpServerBindingDefinition())
        .WithSecurityRequirement("oauth2"))
    .WithChannel("/events", channel => channel
        .WithDescription("The endpoint used to publish and subscribe to cloud events")
        .WithBinding(new HttpChannelBindingDefinition())
        .WithSubscribeOperation(operation => operation
            .WithOperationId("ObserveCloudEvents")
            .WithDescription("Observes cloud events published by the StreetLightsApi")
            .WithBinding(new HttpOperationBindingDefinition() { Method = Neuroglia.AsyncApi.Bindings.Http.HttpMethod.POST, Type = HttpBindingOperationType.Response })
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
                .WithScope("api:read", "The scope used to read data")))));
builder.Services.AddSingleton<IJsonSchemaResolver, JsonSchemaResolver>();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapAsyncApiDocuments();
app.MapRazorPages();

app.Run();