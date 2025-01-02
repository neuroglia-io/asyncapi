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

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents the base class for all attributes used to define bindings
/// </summary>
/// <typeparam name="TBinding">The type of the defined binding</typeparam>
/// <param name="name">The binding's name</param>
/// <param name="version">The binding's version</param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
public abstract class BindingAttribute<TBinding>(string name, string version)
    : Attribute, IBindingAttribute
    where TBinding : class, IBindingDefinition
{

    /// <summary>
    /// Gets the binding's name
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Gets the binding's name
    /// </summary>
    public string Version { get; } = version;

    /// <summary>
    /// Builds the configured <see cref="IBindingDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="IBindingDefinition"/></returns>
    public abstract TBinding Build();

    IBindingDefinition IBindingAttribute.Build() => this.Build();

}
