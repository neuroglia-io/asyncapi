/*
 * Copyright © 2021 Neuroglia SPRL. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using FluentValidation;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neuroglia.AsyncApi
{

    /// <summary>
    /// Defines extensions for <see cref="JsonSchema"/>s
    /// </summary>
    public static class JsonSchemaExtensions
    {

        /// <summary>
        /// Generates examples for the specified schema
        /// </summary>
        /// <param name="schema">The <see cref="JsonSchema"/> to generate a new example for</param>
        /// <returns>A new <see cref="Dictionary{TKey, TValue}"/> containing the generated examples mapped by name</returns>
        public static Dictionary<string, JToken> GenerateExamples(this JsonSchema schema)
        {
            Dictionary<string, JToken> examples = new();
            JToken minimalExample = schema.GenerateExample(requiredPropertiesOnly: true);
            if (schema.Properties.All(p =>
                schema.RequiredProperties.Contains(p.Key)
                && !p.Value.Type.HasFlag(JsonObjectType.Null)))
            {
                examples.Add("Payload", minimalExample);
            }
            else
            {
                JObject extendedExample = (JObject)schema.GenerateExample();
                examples.Add("Minimal payload", minimalExample);
                examples.Add("Extended payload", extendedExample);
            }
            return examples;
        }

        /// <summary>
        /// Generates an example for the specified schema
        /// </summary>
        /// <param name="schema">The <see cref="JsonSchema"/> to generate a new example for</param>
        /// <param name="name">The name of the <see cref="JsonSchema"/> to generate a new example for</param>
        /// <param name="requiredPropertiesOnly">A boolean indicating whether or not to generate an example based only on required properties</param>
        /// <returns>A new example <see cref="JToken"/></returns>
        public static JToken GenerateExample(this JsonSchema schema, string name = null, bool requiredPropertiesOnly = false)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            var schemaType = schema.Type;
            if (schemaType.HasFlag(JsonObjectType.Null))
                if (requiredPropertiesOnly)
                    return null;
                else
                    schemaType &= ~JsonObjectType.Null;
            if (schemaType.HasFlag(JsonObjectType.None))
                schemaType &= ~JsonObjectType.None;
            return schemaType switch
            {
                JsonObjectType.Array => GenerateExampleArrayFor(schema, requiredPropertiesOnly),
                JsonObjectType.String => GenerateExampleStringFor(schema, name),
                JsonObjectType.Boolean => JToken.FromObject(new Random().Next(0, 1) == 1),
                JsonObjectType.Integer => GenerateExampleIntegerFor(schema),
                JsonObjectType.Number => GenerateExampleNumberFor(schema),
                JsonObjectType.Object => GenerateExampleObjectFor(schema, requiredPropertiesOnly),
                _ => null,
            };
        }

        private static JObject GenerateExampleObjectFor(JsonSchema schema, bool requiredPropertiesOnly)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            JObject example = new();
            if (!schema.Properties.Any())
            {
                schema.Properties.Add("property1", new JsonSchemaProperty() { Type = JsonObjectType.String });
                schema.Properties.Add("property2", new JsonSchemaProperty() { Type = JsonObjectType.Integer });
                schema.Properties.Add("property3", new JsonSchemaProperty() { Type = JsonObjectType.Boolean });
            }
            foreach (KeyValuePair<string, JsonSchemaProperty> property in schema.Properties
                .Where(kvp => !requiredPropertiesOnly || schema.RequiredProperties.Contains(kvp.Key)))
            {
                example.Add(property.Key, GenerateExample(property.Value, property.Key, requiredPropertiesOnly));
            }
            return example;
        }

        private static JArray GenerateExampleArrayFor(JsonSchema schema, bool requiredPropertiesOnly)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            int min = 1;
            int max = 3;
            if (schema.MinItems > 0)
                min = (int)schema.MinItems;
            if (schema.MaxItems > min
             && schema.MaxItems < 5)
                max = (int)schema.MaxItems;
            int itemsCount = new Random().Next(min, max);
            JArray array = new JArray();
            for (int i = 0; i < itemsCount; i++)
            {
                array.Add(GenerateExample(schema.Items.First(), string.Empty, requiredPropertiesOnly));
            }
            return array;
        }

        private static JToken GenerateExampleIntegerFor(JsonSchema schema)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            int min = int.MinValue;
            int max = int.MaxValue;
            if (schema.Minimum.HasValue)
                min = (int)schema.Minimum.Value;
            if (schema.Maximum.HasValue)
                max = (int)schema.Maximum.Value;
            return JToken.FromObject(new Random().Next(min, max));
        }

        private static JToken GenerateExampleNumberFor(JsonSchema schema)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            decimal min = decimal.MinValue;
            decimal max = decimal.MaxValue;
            if (schema.Minimum.HasValue)
                min = (decimal)schema.Minimum.Value;
            if (schema.Maximum.HasValue)
                max = (decimal)schema.Maximum.Value;
            return JToken.FromObject(Math.Round(new Random().NextDecimal(min, max), 2));
        }

        private static JToken GenerateExampleStringFor(JsonSchema schema, string name)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            switch (schema.Format)
            {
                case "date":
                case "date-time":
                    return JToken.FromObject(DateTime.Now);
                case "email":
                    return "fake@email.com";
                default:
                    return string.IsNullOrWhiteSpace(name) ? "Lorem ipsum dolor sit amet" : name;
            }
        }

    }

}
