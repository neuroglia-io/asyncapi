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
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Neuroglia.AsyncApi
{

    /// <summary>
    /// Defines extensions for <see cref="JSchemaGenerator"/>s
    /// </summary>
    public static class JSchemaGeneratorExtensions
    {

        /// <summary>
        /// Generates a new <see cref="JSchema"/> for the specified <see cref="ParameterInfo"/>
        /// </summary>
        /// <param name="generator">The extended <see cref="JSchemaGenerator"/></param>
        /// <param name="parameter">The <see cref="ParameterInfo"/> to generate a new <see cref="JSchema"/> for</param>
        /// <returns>A new <see cref="JSchema"/></returns>
        public static JSchema Generate(this JSchemaGenerator generator, ParameterInfo parameter)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            JSchema schema = generator.Generate(parameter.ParameterType);
            if (parameter.TryGetCustomAttribute(out MinLengthAttribute minLengthAttribute))
                schema.MinimumLength = minLengthAttribute.Length;
            if (parameter.TryGetCustomAttribute(out MaxLengthAttribute maxLengthAttribute))
                schema.MaximumLength = maxLengthAttribute.Length;
            if (parameter.TryGetCustomAttribute(out RangeAttribute rangeAttribute))
            {
                schema.Minimum = Convert.ToDouble(rangeAttribute.Minimum);
                schema.Maximum = Convert.ToDouble(rangeAttribute.Maximum);
            }
            if (parameter.TryGetCustomAttribute(out RegularExpressionAttribute regexAttribute))
                schema.Pattern = regexAttribute.Pattern;
            if (parameter.TryGetCustomAttribute<EmailAddressAttribute>(out _))
                schema.Format = "email";
            if(parameter.TryGetCustomAttribute(out DataTypeAttribute dataTypeAttribute))
            {
                switch (dataTypeAttribute.DataType)
                {
                    case DataType.Currency:
                        schema.Pattern = "^[a-zA-Z]{3}$";
                        break;
                    case DataType.Date:
                        schema.Format = "date";
                        break;
                    case DataType.Duration:
                        schema.Format = "duration";
                        break;
                    case DataType.EmailAddress:
                        schema.Format = "email";
                        break;
                    case DataType.ImageUrl:
                    case DataType.Url:
                        schema.Format = "uri";
                        break;
                    case DataType.Time:
                        schema.Format = "time";
                        break;
                }
            }
            return schema;
        }

    }

}
