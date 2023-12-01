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

using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.FluentBuilders;

/// <summary>
/// Represents the base class for all <see cref="IOperationTraitBuilder"/> implementations
/// </summary>
/// <remarks>
/// Initializes a new <see cref="OperationTraitBuilder"/>
/// </remarks>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">An <see cref="IEnumerable{T}"/> containing the services used to validate <see cref="OperationTraitDefinition"/>s</param>
public class OperationTraitBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<OperationTraitDefinition>> validators)
    : OperationTraitDefinitionBuilder<IOperationTraitBuilder, OperationTraitDefinition>(serviceProvider, validators), IOperationTraitBuilder
{
}
