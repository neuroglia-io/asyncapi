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

namespace Neuroglia.AsyncApi.AspNetCore.UI.Models.v3;

/// <summary>
/// Holds the data used to render an <see cref="IBindingDefinition"/> view
/// </summary>
/// <param name="Document">The <see cref="V3AsyncApiDocument"/> that defines the <see cref="IBindingDefinition"/> to render</param>
/// <param name="Binding">The <see cref="IBindingDefinition"/> to render</param>
/// <param name="ParentRef">The component the <see cref="IBindingDefinition"/> is defined by</param>
public record V3BindingDefinitionViewModel(V3AsyncApiDocument Document, IBindingDefinition Binding, string ParentRef)
    : V3AsyncApiDocumentViewModel(Document)
{



}
