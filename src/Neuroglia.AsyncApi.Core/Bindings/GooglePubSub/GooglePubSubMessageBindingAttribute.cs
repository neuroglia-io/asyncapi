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

namespace Neuroglia.AsyncApi.Bindings.GooglePubSub;

/// <summary>
/// Represents the attribute used to define an <see cref="GooglePubSubMessageBindingDefinition"/>
/// </summary>
/// <param name="name">The binding's name</param>
/// <param name="version">The binding's version</param>
public class GooglePubSubMessageBindingAttribute(string name, string version = "latest")
    : MessageBindingAttribute<GooglePubSubMessageBindingDefinition>(name, version)
{

    /// <summary>
    /// Gets/sets the attributes for this message (If this field is empty, the message must contain non-empty data. This can be used to filter messages on the subscription.). Values must follow the '{key}:{value}' format
    /// </summary>
    public virtual string[]? Attributes { get; init; }

    /// <summary>
    /// Gets/sets a value that identifies related messages for which publish order should be respected (For more information, see ordering messages.)
    /// </summary>
    public virtual string? OrderingKey { get; init; }

    /// <inheritdoc/>
    public override GooglePubSubMessageBindingDefinition Build() => new()
    {
        BindingVersion = Version,
        Attributes = this.Attributes == null ? null : new(this.Attributes.Select(l =>
        {
            var components = l.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (components.Length != 2) return null;
            return new { Key = components[0], Value = components[1] };
        })
            .Where(v => v != null)
            .ToDictionary(kvp => kvp!.Key, kvp => (object)kvp!.Value)),
        OrderingKey = this.OrderingKey
    };

}
