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

namespace Neuroglia.AsyncApi.Bindings.IbmMQ;

/// <summary>
/// Represents the attribute used to define an <see cref="IbmMQMessageBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="version">The binding's version</param>
public class IbmMQMessageBindingAttribute(string name, string version = "latest")
    : MessageBindingAttribute<IbmMQMessageBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets/sets the type of the message.
    /// </summary>
    public virtual IbmMQMessageType Type { get; init; }

    /// <summary>
    /// Gets/sets a string that defines the IBM MQ message headers to include with this message. More than one header can be specified as a comma separated list. Supporting information on IBM MQ message formats can be found on this page in the IBM MQ Knowledge Center.
    /// </summary>
    public virtual string? Headers { get; init; }

    /// <summary>
    /// Gets/sets additional information for application developers: describes the message type or format.
    /// </summary>
    public virtual string? Description { get; init; }

    /// <summary>
    /// Gets/sets the recommended setting the client should use for the TTL (Time-To-Live) of the message. This is a period of time expressed in milliseconds and set by the application that puts the message. expiry values are API dependent e.g., MQI and JMS use different units of time and default values for unlimited. General information on IBM MQ message expiry can be found on this page in the IBM MQ Knowledge Center.
    /// </summary>
    public virtual int? Expiry { get; init; }

    /// <inheritdoc/>
    public override IbmMQMessageBindingDefinition Build() => new()
    {
        BindingVersion = this.Version,
        Type = this.Type,
        Headers = this.Headers,
        Description = this.Description,
        Expiry = this.Expiry
    };

}