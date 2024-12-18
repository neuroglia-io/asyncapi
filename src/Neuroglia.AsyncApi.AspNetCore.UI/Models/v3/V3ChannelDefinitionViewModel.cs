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
/// Holds the data used to render an <see cref="V3ChannelDefinition"/> view
/// </summary>
/// <param name="document">The document the <see cref="V3ChannelDefinition"/> to render belongs to</param>
/// <param name="channelName">The name of the <see cref="V3ChannelDefinition"/> to render</param>
/// <param name="channel">The <see cref="V3ChannelDefinition"/> to render</param>
public record V3ChannelDefinitionViewModel(V3AsyncApiDocument document, string channelName, V3ChannelDefinition channel)
    : V3AsyncApiDocumentViewModel(document)
{

    /// <summary>
    /// Gets the name of the <see cref="V3ChannelDefinition"/> to render
    /// </summary>
    public string ChannelName { get; } = channelName;

    /// <summary>
    /// Gets the <see cref="V3ChannelDefinition"/> to render
    /// </summary>
    public V3ChannelDefinition Channel { get; } = channel;

}
