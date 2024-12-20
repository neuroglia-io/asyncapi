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

namespace Neuroglia.AsyncApi.Bindings.Sns;

/// <summary>
/// Represents the object used to configure a SNS operation binding
/// </summary>
[DataContract]
public record SnsOperationBindingDefinition
    : SnsBindingDefinition, IOperationBindingDefinition
{

    /// <summary>
    /// Gets/sets the SNS Topic, which can often be assumed to be the channel name. The field is provided in case you need to supply the ARN, or the Topic name when it is not the equal to the channel name in the AsyncAPI document.
    /// </summary>
    [DataMember(Order = 1, Name = "topic"), JsonPropertyOrder(1), JsonPropertyName("topic"), YamlMember(Order = 1, Alias = "topic")]
    public virtual SnsIdentifier? Topic { get; set; }

    /// <summary>
    /// Gets/sets the protocols that listen to this topic and their endpoints.
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 2, Name = "consumers"), JsonPropertyOrder(2), JsonPropertyName("consumers"), YamlMember(Order = 2, Alias = "consumers")]
    public virtual EquatableList<SnsConsumer> Consumers { get; set; } = [];

    /// <summary>
    /// Gets/sets the policy for retries to HTTP. The field is the default for HTTP receivers of the SNS Topic which may be overridden by a specific consumer.olicy for retries to HTTP. The field is the default for HTTP receivers of the SNS Topic which may be overridden by a specific consumer.
    /// </summary>
    [DataMember(Order = 3, Name = "deliveryPolicy"), JsonPropertyOrder(3), JsonPropertyName("deliveryPolicy"), YamlMember(Order = 3, Alias = "deliveryPolicy")]
    public virtual SnsConsumerDeliveryPolicy? DeliveryPolicy { get; set; }

    /// <summary>
    /// Gets/sets the version of this binding.
    /// </summary>
    [DataMember(Order = 4, Name = "bindingVersion"), JsonPropertyOrder(4), JsonPropertyName("bindingVersion"), YamlMember(Order = 4, Alias = "bindingVersion")]
    public virtual string? BindingVersion { get; set; } = "latest";

}
