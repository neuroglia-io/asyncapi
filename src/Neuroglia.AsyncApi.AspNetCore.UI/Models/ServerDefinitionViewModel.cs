﻿// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
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
/// Holds the data used to render a <see cref="ServerDefinition"/> view
/// </summary>
public record ServerDefinitionViewModel
    : AsyncApiDocumentViewModel
{

    /// <inheritdoc/>
    public ServerDefinitionViewModel(AsyncApiDocument document, string key, ServerDefinition server) : base(document) { this.Key = key; this.Server = server; }

    /// <summary>
    /// Gets the key of the <see cref="ServerDefinition"/> to render
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Gets the <see cref="ServerDefinition"/> to render
    /// </summary>
    public ServerDefinition Server { get; }

}
