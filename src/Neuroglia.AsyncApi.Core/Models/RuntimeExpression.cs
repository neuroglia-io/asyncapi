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
using System;

namespace Neuroglia.AsyncApi.Models
{
    /// <summary>
    /// Represents a parsed <see href="https://www.asyncapi.com/docs/specifications/v2.1.0#runtimeExpression">Async API runtime expression</see>
    /// </summary>
    public class RuntimeExpression
    {

        /// <summary>
        /// Gets the default expression component for <see cref="RuntimeExpression"/>s
        /// </summary>
        public const string DefaultExpression = "$message";

        /// <summary>
        /// Initializes a new <see cref="RuntimeExpression"/>
        /// </summary>
        protected RuntimeExpression()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="RuntimeExpression"/>
        /// </summary>
        /// <param name="expression">The <see cref="RuntimeExpression"/>'s expression component (ex: 'message')</param>
        /// <param name="source">The <see cref="RuntimeExpression"/>'s source component (ex: 'header')</param>
        /// <param name="fragment">The <see cref="RuntimeExpression"/>'s fragment component (ex: '#/property')</param>
        public RuntimeExpression(string expression, RuntimeExpressionSource source, string fragment)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentNullException(nameof(expression));
            if (string.IsNullOrWhiteSpace(fragment))
                throw new ArgumentNullException(nameof(fragment));
            this.Expression = expression;
            this.Source = source;
            this.Fragment = fragment;
        }

        /// <summary>
        /// Gets the <see cref="RuntimeExpression"/>'s expression component (ex: 'message')
        /// </summary>
        public virtual string Expression { get; }

        /// <summary>
        /// Gets the <see cref="RuntimeExpression"/>'s source component (ex: 'header')
        /// </summary>
        public virtual RuntimeExpressionSource Source { get; }

        /// <summary>
        /// Gets the <see cref="RuntimeExpression"/>'s fragment component (ex: '#/property')
        /// </summary>
        public virtual string Fragment { get; }

        /// <summary>
        /// Parses the specified input
        /// </summary>
        /// <param name="input">The input to parse</param>
        /// <returns>A new <see cref="RuntimeExpression"/></returns>
        public static RuntimeExpression Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));
            if (!input.Trim().StartsWith(DefaultExpression))
                throw new ArgumentException($"A valid Async API runtime expression should start with '{DefaultExpression}'", nameof(input));
            string substring = input.Trim().Substring(DefaultExpression.Length + 1);
            string source = substring.Split('#', StringSplitOptions.RemoveEmptyEntries)[0];
            string fragment = substring.Substring(source.Length + 1);
            return new(DefaultExpression, EnumHelper.Parse<RuntimeExpressionSource>(source) , fragment);
        }

        /// <summary>
        /// Attempts to parse the specifed input
        /// </summary>
        /// <param name="input">The input to parse</param>
        /// <param name="expression">The parsed <see cref="RuntimeExpression"/></param>
        /// <returns>A boolean indicating whether or not the specified input could be parsed</returns>
        public static bool TryParse(string input, out RuntimeExpression expression)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));
            expression = null;
            try
            {
                expression = Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
