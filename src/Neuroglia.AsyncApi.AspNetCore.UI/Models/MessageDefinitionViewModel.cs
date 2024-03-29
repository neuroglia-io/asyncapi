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
/// Holds the data used to render a <see cref="MessageDefinition"/> view
/// </summary>
public record MessageDefinitionViewModel
    : AsyncApiDocumentViewModel
{

    /// <inheritdoc/>
    public MessageDefinitionViewModel(AsyncApiDocument document, int index, string operationRef, MessageDefinition definition)
        : base(document)
    {
        this.Index = index;
        this.OperationRef = operationRef;
        this.Definition = definition;
    }

    /// <summary>
    /// Gets the message's index
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// Gets a reference to the message's parent component
    /// </summary>
    public string OperationRef { get; }

    /// <summary>
    /// Gets the <see cref="MessageDefinition"/> to render
    /// </summary>
    public MessageDefinition Definition { get; }

}
