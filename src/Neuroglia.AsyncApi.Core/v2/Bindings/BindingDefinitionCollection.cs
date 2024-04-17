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

using System.Reflection;

namespace Neuroglia.AsyncApi.v2.Bindings;

/// <summary>
/// Represents the base record for all <see cref="IBindingDefinitionCollection{TBinding}"/> implementations
/// </summary>
/// <typeparam name="TBinding">The type of <see cref="IBindingDefinition"/> contained by the <see cref="BindingDefinitionCollection{TBinding}"/></typeparam>
[DataContract]
public abstract record BindingDefinitionCollection<TBinding>
    : ReferenceableComponentDefinition, IBindingDefinitionCollection<TBinding>
    where TBinding : IBindingDefinition
{

    List<PropertyInfo> _bindingProperties = null!;
    List<PropertyInfo> BindingProperties
    {
        get
        {
            _bindingProperties ??= GetType().GetProperties().Where(p => typeof(TBinding).IsAssignableFrom(p.PropertyType)).ToList();
            return _bindingProperties;
        }
    }

    /// <summary>
    /// Adds the specified <see cref="IBindingDefinition"/> to the <see cref="BindingDefinitionCollection{TBinding}"/>
    /// </summary>
    /// <param name="binding">The <see cref="IBindingDefinition"/> to add</param>
    public virtual void Add(TBinding binding)
    {
        ArgumentNullException.ThrowIfNull(binding);

        var property = BindingProperties.FirstOrDefault(p => p.PropertyType.IsAssignableFrom(binding.GetType())) ?? throw new InvalidOperationException($"Failed to find a binding property of the specified type '{typeof(TBinding).Name}'");

        property.SetValue(this, binding);
    }

    /// <summary>
    /// Converts the <see cref="BindingDefinitionCollection{TBinding}"/> into a new <see cref="IEnumerable{T}"/> containing the <see cref="IBindingDefinition"/>s the <see cref="BindingDefinitionCollection{TBinding}"/> is made out of
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the <see cref="IBindingDefinition"/>s the <see cref="BindingDefinitionCollection{TBinding}"/> is made out of</returns>
    public abstract IEnumerable<TBinding> AsEnumerable();

}
