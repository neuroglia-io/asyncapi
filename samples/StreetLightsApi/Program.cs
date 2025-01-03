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

using Json.Schema.Generation;
using Neuroglia.AsyncApi.Bindings.Mqtt;
using StreetLightsApi.Messages;

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
        .UseDefaultV2DocumentConfiguration(asyncApi =>
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
        })
        .UseDefaultV3DocumentConfiguration(asyncApi =>
        {
            asyncApi
                .WithTermsOfService(new Uri("https://www.websitepolicies.com/blog/sample-terms-service-template"))
                .WithExternalDocumentation(doc => doc
                    .WithDescription("The exhaustive documentation of the described **API**, its purpose, usage scenarios, and detailed specifications. This documentation includes all relevant architectural diagrams, authentication flows, and code samples, offering a comprehensive reference for developers and integrators.")
                    .WithUrl(new("https://fakeurl.com")))
                .WithTag(tag => tag
                    .Use("#/components/tags/kubernetes"))
                .WithTag(tag => tag
                    .WithName("showcase")
                    .WithDescription("The present document is used to showcase features of the `Neuroglia.AsyncAPI` document generation."))
                .WithServer("mosquitto", server => server
                    .WithHost("mqtt://test.mosquitto.org")
                    .WithPathName("/{environment}")
                    .WithProtocol(AsyncApiProtocol.Mqtt)
                    .WithDescription("The **Mosquitto test MQTT server**. Use the `env` variable to point to either `production` or `staging`.")
                    .WithVariable("environment", variable => variable
                        .WithDescription("Environment to connect to.")
                        .WithEnumValues("production", "staging"))
                    .WithBinding(new MqttServerBindingDefinition()
                    {
                        ClientId = "StreetLightsAPI:1.0.0",
                        CleanSession = true
                    }))
                .WithSchemaComponent("addStreetLightRequest", schema => schema
                    .WithJsonSchema(jsonSchema => jsonSchema
                        .FromType<AddStreetLightRequest>(Neuroglia.AsyncApi.JsonSchemaGeneratorConfiguration.Default)))
                .WithServerComponent("http", server => server
                    .WithHost("https://test.com")
                    .WithPathName("/{environment}")
                    .WithProtocol(AsyncApiProtocol.Http)
                    .WithDescription("The **HTTP test server**. Use the `env` variable to point to either `production` or `staging`.")
                    .WithVariable("environment", variable => variable
                        .WithDescription("Environment to connect to.")
                        .WithEnumValues("production", "staging"))
                    .WithBindings(bindings => bindings
                        .Use("#/components/serverBindings/http")))
                .WithChannelComponent("lightingMeasuredHTTP", channel => channel
                    .WithDescription("This channel is used to exchange messages about lightning measurements.")
                    .WithServer("#/components/servers/http")
                    .WithBindings(bindings => bindings
                        .Use("#/components/channelBindings/http"))
                    .WithMessage("addStreetLightRequest", message => message
                        .Use("#/components/messages/addStreetLightRequest")))
                .WithOperationComponent("addStreetLight", operation => operation
                    .WithAction(Neuroglia.AsyncApi.v3.V3OperationAction.Receive)
                    .WithChannel("#/components/channels/lightingMeasuredHTTP")
                    .WithDescription("Adds a new **streetlight** to the API.")
                    .WithTrait(trait => trait
                        .Use("#/components/operationTraits/commonBindings"))
                    .WithMessage("#/channels/lightingMeasuredHTTP/messages/addStreetLightRequest")
                    .WithBindings(bindings => bindings
                        .Use("#/components/operationBindings/http")))
                .WithMessageComponent("addStreetLightRequest", message => message
                    .WithPayloadSchema(schema => schema
                        .WithJsonSchema(jsonSchema => jsonSchema
                            .FromType<AddStreetLightRequest>(Neuroglia.AsyncApi.JsonSchemaGeneratorConfiguration.Default)))
                    .WithTrait(trait => trait
                        .Use("#/components/messageTraits/commonHeaders"))
                    .WithBindings(bindings => bindings
                        .Use("#/components/messageBindings/http")))
                .WithMessageComponent("measureStreetLightLuminosityReply", message => message
                    .WithPayloadSchema(schema => schema
                        .WithJsonSchema(jsonSchema => jsonSchema
                            .FromType<MeasureStreetLightLuminosityReply>(Neuroglia.AsyncApi.JsonSchemaGeneratorConfiguration.Default)))
                    .WithBindings(bindings => bindings
                        .Use("#/components/messageBindings/http")))
                .WithSecuritySchemeComponent("oauth2", security => security
                    .WithType(SecuritySchemeType.OAuth2)
                    .WithDescription("The security scheme used to authenticate using **OAUTH2**.")
                    .WithScope("api")
                    .WithAuthorizationScheme("Bearer")
                    .WithOAuthFlows(flows => flows
                        .WithImplicitFlow(flow => flow
                            .WithTokenUrl(new Uri("https://fake.com/auth/streetlights/token")))))
                .WithServerVariableComponent("environment", variable => variable
                    .WithDescription("The variable used to define the **hosting environment**.")
                    .WithDefaultValue("dev")
                    .WithEnumValues("dev", "staging", "production")
                    .WithExample("dev")
                    .WithExample("staging")
                    .WithExample("production"))
                .WithParameterComponent("environment", parameter => parameter
                    .WithDescription("The variable used to define the **hosting environment**.")
                    .WithDefaultValue("dev")
                    .WithEnumValues("dev", "staging", "production")
                    .WithExample("dev")
                    .WithExample("staging")
                    .WithExample("production"))
                .WithCorrelationIdComponent("streetId", correlationId => correlationId
                    .WithDescription("The parameter used to correlate the **street** a **light** is located in.")
                    .WithLocation("$message.header#/MQMD/CorrelId"))
                .WithReplyComponent("measureStreetLightLuminosityReply", reply => reply
                    .WithAddress(address => address.Use("#/components/replyAddresses/measureStreetLightLuminosityReplyAddress"))
                    .WithMessage("#/components/messages/measureStreetLightLuminosityReply"))
                .WithReplyAddressComponent("measureStreetLightLuminosityReplyAddress", address => address
                    .WithDescription("Represents the **address** to reply to after a **measureStreetLightLuminosityRequest**.")
                    .WithLocation("$message.header#/replyTo"))
                .WithExternalDocumentationComponent("kubernetesTag", doc => doc
                    .WithDescription("The **external documentation** used to document the `kubernetes` **tag**.")
                    .WithUrl(new("https://fake.com/tags/kubernetes")))
                .WithTagComponent("kubernetes", tag => tag
                    .WithName("kubernetes")
                    .WithDescription("The **tag** used to mark a **server** deployed in `Kubernetes`.")
                    .WithExternalDocumentation(doc => doc.Use("#/components/externalDocs/kubernetesTag")))
                .WithServerBindingsComponent("http", bindings => bindings
                    .WithBinding(new HttpServerBindingDefinition()))
                .WithChannelBindingsComponent("http", bindings => bindings
                    .WithBinding(new HttpChannelBindingDefinition()))
                .WithOperationBindingsComponent("http", bindings => bindings
                    .WithBinding(new HttpOperationBindingDefinition()
                    {
                        Method = Neuroglia.AsyncApi.Bindings.Http.HttpMethod.POST,
                        Type = HttpBindingOperationType.Request
                    }))
                .WithMessageBindingsComponent("http", bindings => bindings
                    .WithBinding(new HttpMessageBindingDefinition()))
                .WithOperationTraitComponent("commonBindings", trait => trait
                    .WithDescription("The **operation trait** used to define the bindings common to all operations defined by the application.")
                    .WithBinding(new HttpOperationBindingDefinition()))
                .WithMessageTraitComponent("commonHeaders", trait => trait
                    .WithName("Common Headers")    
                    .WithDescription("The **message trait** used to define the common headers used by all the messages exchanged by the application.")
                        .WithHeadersSchema(schema => schema
                            .WithJsonSchema(jsonSchema => jsonSchema
                                .FromType<CommonHeaders>(Neuroglia.AsyncApi.JsonSchemaGeneratorConfiguration.Default))));
        }));
builder.Services.AddAsyncApiDocument(document => document
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
builder.Services.AddAsyncApiDocument(document => document
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
        .WithBinding(new HttpChannelBindingDefinition())
        .WithMessage("lightMeasuredEvent", message => message
            .Use("#/components/messages/lightMeasuredEvent")))
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
        .WithMessage("#/channels/events/messages/lightMeasuredEvent"))
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
builder.Services.AddSingleton<IJsonSchemaResolver, JsonSchemaResolver>();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapAsyncApiDocuments();
app.MapRazorPages();

app.Run();