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
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neuroglia.AsyncApi
{

    /// <summary>
    /// Defines extensions for <see cref="JSchema"/>s
    /// </summary>
    public static class JSchemaExtensions
    {

        /// <summary>
        /// Generates examples for the specified schema
        /// </summary>
        /// <param name="schema">The <see cref="JSchema"/> to generate a new example for</param>
        /// <returns>A new <see cref="Dictionary{TKey, TValue}"/> containing the generated examples mapped by name</returns>
        public static Dictionary<string, JToken> GenerateExamples(this JSchema schema)
        {
            Dictionary<string, JToken> examples = new();
            JToken minimalExample = schema.GenerateExample(requiredPropertiesOnly: true);
            if (schema.Properties.All(p =>
                schema.Required.Contains(p.Key)
                && !p.Value.Type.Value.HasFlag(JSchemaType.Null)))
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
        /// <param name="schema">The <see cref="JSchema"/> to generate a new example for</param>
        /// <param name="name">The name of the <see cref="JSchema"/> to generate a new example for</param>
        /// <param name="requiredPropertiesOnly">A boolean indicating whether or not to generate an example based only on required properties</param>
        /// <returns>A new example <see cref="JToken"/></returns>
        public static JToken GenerateExample(this JSchema schema, string name = null, bool requiredPropertiesOnly = false)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            if (!schema.Type.HasValue)
                return null;
            JSchemaType schemaType = schema.Type.Value;
            if (schemaType.HasFlag(JSchemaType.Null))
                if (requiredPropertiesOnly)
                    return null;
                else
                    schemaType &= ~JSchemaType.Null;
            if (schemaType.HasFlag(JSchemaType.None))
                schemaType &= ~JSchemaType.None;
            return schemaType switch
            {
                JSchemaType.Array => GenerateExampleArrayFor(schema, requiredPropertiesOnly),
                JSchemaType.String => GenerateExampleStringFor(schema, name),
                JSchemaType.Boolean => JToken.FromObject(new Random().Next(0, 1) == 1),
                JSchemaType.Integer => GenerateExampleIntegerFor(schema),
                JSchemaType.Number => GenerateExampleNumberFor(schema),
                JSchemaType.Object => GenerateExampleObjectFor(schema, requiredPropertiesOnly),
                _ => null,
            };
        }

        private static JObject GenerateExampleObjectFor(JSchema schema, bool requiredPropertiesOnly)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            JObject example = new();
            if (!schema.Properties.Any())
            {
                schema.Properties.Add("property1", new JSchema() { Type = JSchemaType.String });
                schema.Properties.Add("property2", new JSchema() { Type = JSchemaType.Integer });
                schema.Properties.Add("property3", new JSchema() { Type = JSchemaType.Boolean });
            }
            foreach (KeyValuePair<string, JSchema> property in schema.Properties.Where(kvp => requiredPropertiesOnly ? schema.Required.Contains(kvp.Key) : true))
            {
                example.Add(property.Key, GenerateExample(property.Value, property.Key, requiredPropertiesOnly));
            }
            return example;
        }

        private static JArray GenerateExampleArrayFor(JSchema schema, bool requiredPropertiesOnly)
        {
            if (schema == null)
                throw new ArgumentNullException(nameof(schema));
            int min = 1;
            int max = 3;
            if (schema.MinimumItems.HasValue
                && schema.MinimumItems.Value > 0)
                min = (int)schema.MinimumItems.Value;
            if (schema.MaximumItems.HasValue
             && schema.MaximumItems.Value > min
             && schema.MaximumItems.Value < 5)
                max = (int)schema.MaximumItems.Value;
            int itemsCount = new Random().Next(min, max);
            JArray array = new JArray();
            for (int i = 0; i < itemsCount; i++)
            {
                array.Add(GenerateExample(schema.Items[0], string.Empty, requiredPropertiesOnly));
            }
            return array;
        }

        private static JToken GenerateExampleIntegerFor(JSchema schema)
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

        private static JToken GenerateExampleNumberFor(JSchema schema)
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

        private static JToken GenerateExampleStringFor(JSchema schema, string name)
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
