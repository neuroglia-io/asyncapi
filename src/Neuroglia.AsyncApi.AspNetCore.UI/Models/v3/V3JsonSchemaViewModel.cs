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
/// Holds the data required to render a <see cref="JsonSchema"/> view
/// </summary>
/// <param name="Document">The <see cref="V3AsyncApiDocument"/> that defines the <see cref="V3SchemaDefinition"/> to render</param>
/// <param name="Definition">The <see cref="V3SchemaDefinition"/> to render</param>
/// <param name="Context">The <see cref="V3SchemaDefinition"/>'s context</param>
/// <param name="ParentReference">A reference to the component that defines the <see cref="V3SchemaDefinition"/></param>
/// <param name="Examples">An <see cref="IEnumerable{T}"/> containing examples of the <see cref="V3SchemaDefinition"/> to render</param>
public record V3SchemaViewModel(V3AsyncApiDocument Document, SchemaContext Context, V3SchemaDefinition Definition, string ParentReference, IEnumerable<V3MessageExampleDefinition>? Examples = null)
    : V3AsyncApiDocument(Document)
{



}
