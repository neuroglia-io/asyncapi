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

using Neuroglia.Serialization;
using System.Collections;
using System.Text.Json.Nodes;

namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Represents the default implementation of the <see cref="IJsonSchemaExampleGenerator"/> interface
/// </summary>
/// <remarks>
/// Initializes a new <see cref="JsonSchemaExampleGenerator"/>
/// </remarks>
/// <param name="serializer">The service used to serialize/deserialize objects to/from JSON</param>
public class JsonSchemaExampleGenerator(IJsonSerializer serializer)
    : IJsonSchemaExampleGenerator
{

    JsonSchema _defaultSchema = null!;

    /// <summary>
    /// Gets the service used to serialize/deserialize objects to/from JSON
    /// </summary>
    protected IJsonSerializer Serializer { get; } = serializer;

    /// <summary>
    /// Gets the <see cref="JsonSchema"/> to use when none was specified
    /// </summary>
    protected JsonSchema DefaultSchema
    {
        get
        {
            if(this._defaultSchema == null)
            {
                var schemaBuilder = new JsonSchemaBuilder();
                var schemaProperties = new Dictionary<string, JsonSchema>
                {
                    { "property1", schemaBuilder.FromType<string>() },
                    { "property2", schemaBuilder.FromType<int>() },
                    { "property3", schemaBuilder.FromType<bool>() },
                };
                schemaBuilder.Properties(schemaProperties);
                this._defaultSchema = schemaBuilder.Build();
            }
            return this._defaultSchema;
        }
    }

    /// <inheritdoc/>
    public virtual object? GenerateExample(JsonSchema schema, string? name = null, bool requiredPropertiesOnly = false)
    {
        ArgumentNullException.ThrowIfNull(schema);

        var constValue = schema.GetConst();
        if (constValue != null) return constValue;

        var enumValues = schema.GetEnum();
        if (enumValues != null) return enumValues.FirstOrDefault()?.ToString();

        var examples = schema.GetExamples();
        if (examples != null) return examples.FirstOrDefault()?.ToString();

        var schemaType = schema.GetJsonType() & ~SchemaValueType.Null;
        return schemaType switch
        {
            SchemaValueType.Array => GenerateExampleArray(schema, requiredPropertiesOnly),
            SchemaValueType.Boolean => GenerateExampleBoolean(schema),
            SchemaValueType.Integer => GenerateExampleInteger(schema),
            SchemaValueType.Number => GenerateExampleNumber(schema),
            SchemaValueType.Object => GenerateExampleObject(schema, requiredPropertiesOnly),
            SchemaValueType.String => GenerateExampleString(schema),
            _ => null
        };
    }

    /// <summary>
    /// Generates a new example array that conforms to the specified <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="schema">The <see cref="JsonSchema"/> the array to generate must conform to</param>
    /// <param name="requiredPropertiesOnly">A boolean indicating whether or not only the required the generator should generate values for required properties only</param>
    /// <returns>A new example array</returns>
    protected virtual IEnumerable? GenerateExampleArray(JsonSchema schema, bool requiredPropertiesOnly)
    {
        ArgumentNullException.ThrowIfNull(schema);

        var itemsCount = 3;
        var minItems = schema.GetMinItems();
        var maxItems = schema.GetMaxItems();
        if (minItems.HasValue && minItems > 0) itemsCount = (int)minItems.Value;
        if (maxItems.HasValue && maxItems < 5) itemsCount = (int)maxItems.Value;

        var array = new List<object>(itemsCount);
        var itemSchema = schema.GetItems() ?? this.DefaultSchema;
        for(var i = 0; i < itemsCount; i++) array.Add(GenerateExample(itemSchema, string.Empty, requiredPropertiesOnly)!);

        return array;
    }

    /// <summary>
    /// Generates a new example boolean that conforms to the specified <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="schema">The <see cref="JsonSchema"/> the boolean to generate must conform to</param>
    /// <returns>A new boolean</returns>
    protected virtual bool? GenerateExampleBoolean(JsonSchema schema) => new Random().Next(0, 1) == 1;

    /// <summary>
    /// Generates a new example array that conforms to the specified <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="schema">The <see cref="JsonSchema"/> the integer to generate must conform to</param>
    /// <returns>A new integer</returns>
    protected virtual int? GenerateExampleInteger(JsonSchema schema) => new Random().Next(0, 100);

    /// <summary>
    /// Generates a new example number that conforms to the specified <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="schema">The <see cref="JsonSchema"/> the number to generate must conform to</param>
    /// <returns>A new number</returns>
    protected virtual object? GenerateExampleNumber(JsonSchema schema)
    {
        ArgumentNullException.ThrowIfNull(schema);

        var min = 0m;
        var max = 10m;
        var minItems = schema.GetMinItems();
        var maxItems = schema.GetMaxItems();
        if (minItems.HasValue && minItems > 0) min = minItems.Value;
        if (maxItems.HasValue && maxItems < 5) max = maxItems.Value;

        return decimal.Round(new Random().NextDecimal(min, max), 2);
    }

    /// <summary>
    /// Generates a new example object that conforms to the specified <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="schema">The <see cref="JsonSchema"/> the object to generate must conform to</param>
    /// <param name="requiredPropertiesOnly">A boolean indicating whether or not only the required the generator should generate values for required properties only</param>
    /// <returns>A new object</returns>
    protected virtual object? GenerateExampleObject(JsonSchema schema, bool requiredPropertiesOnly)
    {
        ArgumentNullException.ThrowIfNull(schema);

        var example = new JsonObject();
        var objectSchema = schema;
        if (objectSchema.GetProperties()?.Any() != true) schema = this.DefaultSchema;

        var requiredProperties = objectSchema.GetRequired();
        var properties = objectSchema.GetProperties()?.Where(p => !requiredPropertiesOnly || requiredProperties?.Contains(p.Key) == true);
        if (properties == null)
        {

            if (requiredPropertiesOnly) return example;

            var propertyValue = this.GenerateExample(new JsonSchemaBuilder().FromType<string>());
            example.Add("property1", JsonNode.Parse(this.Serializer.SerializeToText(propertyValue)));

            propertyValue = this.GenerateExample(new JsonSchemaBuilder().FromType<string>());
            example.Add("property2", JsonNode.Parse(this.Serializer.SerializeToText(propertyValue)));

            propertyValue = this.GenerateExample(new JsonSchemaBuilder().FromType<string>());
            example.Add("property3", JsonNode.Parse(this.Serializer.SerializeToText(propertyValue)));

        }
        else
        {
            foreach (var property in properties)
            {
                var propertyValue = this.GenerateExample(property.Value, property.Key, requiredPropertiesOnly);
                example.Add(property.Key, JsonNode.Parse(this.Serializer.SerializeToText(propertyValue)));
            }
        }

        return example;
    }

    /// <summary>
    /// Generates a new example string that conforms to the specified <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="schema">The <see cref="JsonSchema"/> the string to generate must conform to</param>
    /// <returns>A new number</returns>
    protected virtual string? GenerateExampleString(JsonSchema schema)
    {
        var format = schema.GetFormat();
        if (format == null) return "string";
       
        return format.Key switch
        {
            "date-time" => DateTimeOffset.Now.ToString("O"),
            "duration" => "PT1S",
            "email" => "example@email.com",
            "hostname" => "hostname.example.com",
            "ipv4" => "192.168.0.1",
            "ipv6" => "::ffff:192.168.0.1",
            "uri" => "https://example-uri.com",
            "uri-reference" => "/example/uri-reference",
            "uri-template" => "/example/uri-{template}",
            "regex" or "regular-expression" => "^[a-zA-Z0-9]",
            "uuid" => Guid.Empty.ToString(),
            _ => "string",
        };
    }

}
