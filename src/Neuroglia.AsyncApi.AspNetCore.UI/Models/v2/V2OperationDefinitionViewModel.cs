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

namespace Neuroglia.AsyncApi.AspNetCore.UI.Models.v2;

/// <summary>
/// Holds the data used to render a <see cref="V2OperationDefinition"/> view
/// </summary>
public record V2OperationDefinitionViewModel
    : V2AsyncApiDocumentViewModel
{

    /// <inheritdoc/>
    public V2OperationDefinitionViewModel(V2AsyncApiDocument document, string channelKey, V2OperationType operationType, V2OperationDefinition operation) : base(document) { ChannelKey = channelKey; OperationType = operationType; Operation = operation; }

    /// <summary>
    /// Gets the key of the <see cref="V2ChannelDefinition"/> the <see cref="V2OperationDefinition"/> to render belongs to
    /// </summary>
    public string ChannelKey { get; }

    /// <summary>
    /// Gets the type of the <see cref="V2OperationDefinition"/> to render
    /// </summary>
    public V2OperationType OperationType { get; }

    /// <summary>
    /// Gets the <see cref="V2OperationDefinition"/> to render
    /// </summary>
    public V2OperationDefinition Operation { get; }

}
