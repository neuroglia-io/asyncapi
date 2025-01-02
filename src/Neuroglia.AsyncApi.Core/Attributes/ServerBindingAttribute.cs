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
/// Represents the base class for all attributes used to define server bindings
/// </summary>
/// <typeparam name="TBinding">The type of the defined binding</typeparam>
/// <param name="name">The binding's name</param>
/// <param name="version">The binding's version</param>
public abstract class ServerBindingAttribute<TBinding>(string name, string version)
    : BindingAttribute<TBinding>(name, version)
    where TBinding : class, IServerBindingDefinition
{



}
