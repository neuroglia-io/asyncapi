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
using Neuroglia.AsyncApi.Sdk.Models.Bindings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Neuroglia.AsyncApi.Sdk.Models
{

    /// <summary>
    /// Represents the base class for all <see cref="IBindingDefinitionCollection{TBinding}"/> implementations
    /// </summary>
    /// <typeparam name="TBinding">The type of <see cref="IBindingDefinition"/> contained by the <see cref="BindingDefinitionCollection{TBinding}"/></typeparam>
    public abstract class BindingDefinitionCollection<TBinding>
        : ReferenceableComponent, IBindingDefinitionCollection<TBinding>
        where TBinding : IBindingDefinition
    {

        private List<PropertyInfo> _BindingProperties = new();
        private List<PropertyInfo> BindingProperties
        {
            get
            {
                if (this._BindingProperties == null)
                    this._BindingProperties = this.GetType().GetProperties().Where(p => typeof(TBinding).IsAssignableFrom(p.PropertyType)).ToList();
                return this._BindingProperties;
            }
        }

        /// <summary>
        /// Adds the specified <see cref="IBindingDefinition"/> to the <see cref="BindingDefinitionCollection{TBinding}"/>
        /// </summary>
        /// <param name="binding">The <see cref="IBindingDefinition"/> to add</param>
        public virtual void Add(TBinding binding)
        {
            if (binding == null)
                throw new ArgumentNullException(nameof(binding));
            PropertyInfo property = this.BindingProperties.FirstOrDefault(p => p.PropertyType.IsAssignableFrom(binding.GetType()));
            if (property == null)
                throw new InvalidOperationException($"Failed to find a binding property of the specified type '{typeof(TBinding).Name}'");
            property.SetValue(this, binding);
        }

        /// <inheritdoc/>
        public IEnumerator<TBinding> GetEnumerator()
        {
            foreach(PropertyInfo property in this.BindingProperties)
            {
                TBinding binding = (TBinding)property.GetValue(this, Array.Empty<object>());
                if (binding != null)
                    yield return binding;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }

}
