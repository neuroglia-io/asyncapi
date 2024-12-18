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

using Neuroglia.AsyncApi.AspNetCore.UI.Models.v2;

namespace Neuroglia.AsyncApi.AspNetCore.UI.Models;

/// <summary>
/// Holds the data used to render a <see cref="OAuthFlowDefinition"/> view
/// </summary>
public record OAuthFlowDefinitionViewModel
    : V2AsyncApiDocumentViewModel
{

    /// <inheritdoc/>
    public OAuthFlowDefinitionViewModel(V2AsyncApiDocument document, string parentRef, string flowType, OAuthFlowDefinition flow) : base(document) { this.ParentRef = parentRef; this.FlowType = flowType; this.Flow = flow; }

    /// <summary>
    /// Gets a reference to the <see cref="OAuthFlowDefinition"/>'s parent component
    /// </summary>
    public string ParentRef { get; }

    /// <summary>
    /// Gets the flow type
    /// </summary>
    public string FlowType { get; }

    /// <summary>
    /// Gets the <see cref="OAuthFlowDefinition"/> to render
    /// </summary>
    public OAuthFlowDefinition Flow { get; }

}