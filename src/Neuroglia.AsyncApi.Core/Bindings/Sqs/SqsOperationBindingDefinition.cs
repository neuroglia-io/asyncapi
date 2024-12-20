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

namespace Neuroglia.AsyncApi.Bindings.Sqs;

/// <summary>
/// Represents the object used to configure a SQS operation binding
/// </summary>
[DataContract]
public record SqsOperationBindingDefinition
    : SqsBindingDefinition, IOperationBindingDefinition
{

    /// <summary>
    /// Gets/sets the queues that are either the endpoint for an SNS Operation Binding Object, or the deadLetterQueue of the SQS Operation Binding Object
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "queues"), JsonPropertyOrder(1), JsonPropertyName("queues"), YamlMember(Order = 1, Alias = "queues")]
    public virtual EquatableList<SqsQueueDefinition> Queues { get; set; } = [];

    /// <summary>
    /// Gets/sets the version of this binding.
    /// </summary>
    [DataMember(Order = 2, Name = "bindingVersion"), JsonPropertyOrder(2), JsonPropertyName("bindingVersion"), YamlMember(Order = 2, Alias = "bindingVersion")]
    public virtual string? BindingVersion { get; set; } = "latest";

}
