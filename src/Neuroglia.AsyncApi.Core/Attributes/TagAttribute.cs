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
using Neuroglia.AsyncApi.Models;
using System;

namespace Neuroglia.AsyncApi
{
    /// <summary>
    /// Represents an <see cref="Attribute"/> used to tag an <see cref="Operation"/> method
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class TagAttribute
        : Attribute
    {

        /// <summary>
        /// Initializes a new <see cref="TagAttribute"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="Tag"/> to generate</param>
        /// <param name="description">The description of the <see cref="Tag"/> to generate</param>
        public TagAttribute(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Initializes a new <see cref="TagAttribute"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="Tag"/> to generate</param>
        public TagAttribute(string name)
            : this(name, null)
        {

        }

        /// <summary>
        /// Gets the name of the <see cref="Tag"/> to generate
        /// </summary>
        public virtual string Name { get; }

        /// <summary>
        /// Gets the description of the <see cref="Tag"/> to generate
        /// </summary>
        public virtual string Description { get; set; }

    }

}
