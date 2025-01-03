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

namespace Neuroglia.AsyncApi.Validation.v3;

/// <summary>
/// Represents the service used to validate the messages of a <see cref="V3OperationDefinition"/>
/// </summary>
public class V3OperationMessageValidator
    : AbstractValidator<KeyValuePair<V3ReferenceDefinition, V3ReferenceDefinition>>
{

    /// <inheritdoc/>
    public V3OperationMessageValidator(V3AsyncApiDocument? document = null)
    {
        this.Document = document;
        this.RuleFor(kvp => kvp)
            .Must(ReferenceChannelMessage)
            .WithMessage("The operation must reference a message defined by the channel it belongs to");
    }

    /// <summary>
    /// Gets the <see cref="V3AsyncApiDocument"/> to validate
    /// </summary>
    protected V3AsyncApiDocument? Document { get; }

    /// <summary>
    /// Determines whether or not the specified <see cref="V3ChannelDefinition"/> defines the specified <see cref="V3MessageDefinition"/>
    /// </summary>
    /// <param name="channelMessage">The channel/message reference mapping to check</param>
    /// <returns>A boolean indicating whether or not the specified <see cref="V3ChannelDefinition"/> defines the specified <see cref="V3MessageDefinition"/></returns>
    protected virtual bool ReferenceChannelMessage(KeyValuePair<V3ReferenceDefinition, V3ReferenceDefinition> channelMessage)
    {
        if (Document == null) return true;
        try
        {
            var channelName = channelMessage.Key.Reference.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last();
            var channel = Document.DereferenceChannel(channelMessage.Key.Reference);
            return Document.DereferenceChannelMessage(channelName, channel, channelMessage.Value.Reference) != null;
        }
        catch
        {
            return false;
        }
    }

}
