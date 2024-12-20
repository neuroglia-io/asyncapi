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

using Neuroglia.AsyncApi.Bindings;

namespace Neuroglia.AsyncApi.Bindings.Nats;

/// <summary>
/// Represents the object used to configure a <see href="https://nats.io/">NATS</see> operation binding
/// </summary>
[DataContract]
public record NatsOperationBindingDefinition
    : NatsBindingDefinition, IOperationBindingDefinition
{

    /// <summary>
    /// Gets/sets the name of the queue to use. It MUST NOT exceed 255 characters.
    /// </summary>
    [DataMember(Order = 1, Name = "queue"), JsonPropertyOrder(1), JsonPropertyName("queue"), YamlMember(Order = 1, Alias = "queue")]
    public virtual string? Queue { get; set; }

}
