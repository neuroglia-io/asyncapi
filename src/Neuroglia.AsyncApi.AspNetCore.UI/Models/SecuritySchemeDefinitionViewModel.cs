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

namespace Neuroglia.AsyncApi.AspNetCore.UI.Models;

/// <summary>
/// Holds the data used to render a <see cref="SecuritySchemeDefinition"/> view
/// </summary>
public record SecuritySchemeDefinitionViewModel
    : AsyncApiDocumentViewModel
{

    /// <inheritdoc/>
    public SecuritySchemeDefinitionViewModel(AsyncApiDocument document, string parentRef, SecuritySchemeDefinition scheme) : base(document) { this.ParentRef = parentRef; this.Scheme = scheme; }

    /// <summary>
    /// Gets a reference to the parent component of the <see cref="SecuritySchemeDefinition"/> to render
    /// </summary>
    public string ParentRef { get; }

    /// <summary>
    /// Gets the <see cref="SecuritySchemeDefinition"/> to render
    /// </summary>
    public SecuritySchemeDefinition Scheme { get; }

}