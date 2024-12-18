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
/// Holds the data used to render a <see cref="V3OperationDefinition"/> view
/// </summary>
/// <param name="document">The <see cref="V3AsyncApiDocument"/> that defines the <see cref="V3OperationDefinition"/> to render</param>
/// <param name="operationName">The name of the <see cref="V3OperationDefinition"/> to render</param>
/// <param name="operation">The <see cref="V3OperationDefinition"/> to render</param>
public record V3OperationDefinitionViewModel(V3AsyncApiDocument document, string operationName, V3OperationDefinition operation)
    : V3AsyncApiDocumentViewModel(document)
{

    /// <summary>
    /// Gets the name of the <see cref="V3OperationDefinition"/> to render
    /// </summary>
    public string OperationName { get; } = operationName;

    /// <summary>
    /// Gets the <see cref="V3OperationDefinition"/> to render
    /// </summary>
    public V3OperationDefinition Operation { get; } = operation;

}
