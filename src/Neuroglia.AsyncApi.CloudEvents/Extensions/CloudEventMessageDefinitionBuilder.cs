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

using Json.Schema;
using Json.Schema.Generation;
using Neuroglia.AsyncApi.FluentBuilders;
using Neuroglia.AsyncApi.v2;
using Neuroglia.Eventing.CloudEvents;
using Neuroglia.Data.Schemas.Json;

namespace Neuroglia.AsyncApi.CloudEvents;

/// <summary>
/// Represents the default implementation of the <see cref="ICloudEventMessageDefinitionBuilder"/> interface
/// </summary>
/// <param name="underlyingBuilder">The underlying <see cref="IMessageDefinitionBuilder"/> wrapped by the <see cref="CloudEventMessageDefinitionBuilder"/></param>
public class CloudEventMessageDefinitionBuilder(IMessageDefinitionBuilder underlyingBuilder)
    : ICloudEventMessageDefinitionBuilder
{

    /// <summary>
    /// Gets the uri at which to retrieve the Cloud Event Specification schema
    /// </summary>
    public const string CloudEventSchemaUri = "https://raw.githubusercontent.com/cloudevents/spec/v1.0.1/spec.json";

    static readonly JsonSchema DefaultSchema = new JsonSchemaBuilder().FromType<object>();

    /// <summary>
    /// Gets the underlying <see cref="IMessageDefinitionBuilder"/> wrapped by the <see cref="CloudEventMessageDefinitionBuilder"/>
    /// </summary>
    protected IMessageDefinitionBuilder UnderlyingBuilder { get; } = underlyingBuilder;

    /// <summary>
    /// Gets a name/<see cref="JsonSchema"/> mapping of all <see cref="CloudEvent"/> context attributes that have been explicitly set
    /// </summary>
    protected Dictionary<string, JsonSchema> ContextAttributes { get; } = [];

    /// <inheritdoc/>
    public virtual ICloudEventMessageDefinitionBuilder WithSpecVersion(string specVersion)
    {
        if(string.IsNullOrWhiteSpace(specVersion)) throw new ArgumentNullException(nameof(specVersion));

        this.ContextAttributes[CloudEventAttributes.SpecVersion] = new JsonSchemaBuilder().FromType<string>().Const(specVersion).Build();

        return this;
    }

    /// <inheritdoc/>
    public virtual ICloudEventMessageDefinitionBuilder WithSource(Uri source)
    {
        ArgumentNullException.ThrowIfNull(source);

        this.ContextAttributes[CloudEventAttributes.Source] = new JsonSchemaBuilder().FromType<Uri>().Const(source.OriginalString).Build();

        return this;
    }

    /// <inheritdoc/>
    public virtual ICloudEventMessageDefinitionBuilder WithType(string type)
    {
        if (string.IsNullOrWhiteSpace(type)) throw new ArgumentNullException(nameof(type));

        this.ContextAttributes[CloudEventAttributes.Type] = new JsonSchemaBuilder().FromType<string>().Const(type).Build();

        return this;
    }

    /// <inheritdoc/>
    public virtual ICloudEventMessageDefinitionBuilder WithSubject(string? subject)
    {
        if (string.IsNullOrWhiteSpace(subject)) { this.ContextAttributes.Remove(CloudEventAttributes.Subject); return this; }

        this.ContextAttributes[CloudEventAttributes.Subject] = new JsonSchemaBuilder().FromType<string>().Const(subject).Build();

        return this;
    }

    /// <inheritdoc/>
    public virtual ICloudEventMessageDefinitionBuilder WithDataContentType(string? contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType)) { this.ContextAttributes.Remove(CloudEventAttributes.DataContentType); return this; }

        this.ContextAttributes[CloudEventAttributes.DataContentType] = new JsonSchemaBuilder().FromType<string>().Const(contentType).Build();

        return this;
    }

    /// <inheritdoc/>
    public virtual ICloudEventMessageDefinitionBuilder WithDataSchemaUri(Uri? schemaUri)
    {
        if (schemaUri == null) 
        {
            this.ContextAttributes.Remove(CloudEventAttributes.DataSchema);
            this.ContextAttributes[CloudEventAttributes.Data] = DefaultSchema;
        }
        else
        {
            this.ContextAttributes[CloudEventAttributes.DataSchema] = new JsonSchemaBuilder().FromType<Uri>().Const(schemaUri.OriginalString).Build();
            this.ContextAttributes[CloudEventAttributes.Data] = new JsonSchemaBuilder().Ref(schemaUri).Build();
        }
        return this;
    }

    /// <inheritdoc/>
    public virtual ICloudEventMessageDefinitionBuilder WithDataSchema(JsonSchema schema, Uri? schemaUri = null)
    {
        ArgumentNullException.ThrowIfNull(schema);

        this.ContextAttributes[CloudEventAttributes.Data] = schema;
        if (schemaUri != null) this.ContextAttributes[CloudEventAttributes.DataSchema] = new JsonSchemaBuilder().FromType<string>().Const(schemaUri.OriginalString).Build();

        return this;
    }

    /// <inheritdoc/>
    public virtual ICloudEventMessageDefinitionBuilder WithDataOfType(Type type) => this.WithDataSchema(new JsonSchemaBuilder().FromType(type, JsonSchemaGeneratorConfiguration.Default).Build());

    /// <inheritdoc/>
    public virtual ICloudEventMessageDefinitionBuilder WithDataOfType<TData>() => this.WithDataOfType(typeof(TData));

    /// <inheritdoc/>
    public virtual ICloudEventMessageDefinitionBuilder WithExtensionAttribute(string name, string? value)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (!name.All(char.IsLetterOrDigit) || name.Length > 20) throw new ArgumentException("The Cloud Event Specification mandates that extension attribute names MUST consist of lower-case letters ('a' to 'z') or digits ('0' to '9') from the ASCII character set", nameof(name));
        if (string.IsNullOrWhiteSpace(value)) { this.ContextAttributes.Remove(name); return this; }

        this.ContextAttributes[name.ToLowerInvariant()] = new JsonSchemaBuilder().FromType<string>().Const(value).Build();

        return this;
    }

    /// <inheritdoc/>
    public virtual V2MessageDefinition Build()
    {
        var cloudEventSchema = new JsonSchemaBuilder().Ref(CloudEventSchemaUri).Build();

        var schemaBuilder = new JsonSchemaBuilder();
        schemaBuilder.AllOf(cloudEventSchema);

        if (this.ContextAttributes.Count > 0) schemaBuilder.Properties(this.ContextAttributes);
        var schema = schemaBuilder.Build();

        this.UnderlyingBuilder.WithPayloadSchema(schema);

        return this.UnderlyingBuilder.Build();
    }

}
