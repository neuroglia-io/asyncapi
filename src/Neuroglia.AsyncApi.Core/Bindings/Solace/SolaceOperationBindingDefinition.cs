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

namespace Neuroglia.AsyncApi.Bindings.Solace;

/// <summary>
/// Represents the object used to configure a JMS operation binding
/// </summary>
[DataContract]
public record SolaceOperationBindingDefinition
    : SolaceBindingDefinition, IOperationBindingDefinition
{

    /// <summary>
    /// Gets/sets the version of this binding.
    /// </summary>
    [DataMember(Order = 1, Name = "bindingVersion"), JsonPropertyOrder(1), JsonPropertyName("bindingVersion"), YamlMember(Order = 1, Alias = "bindingVersion")]
    public virtual string BindingVersion { get; set; } = "0.4.0";

    /// <summary>
    /// Gets/sets a list of the destinations to use.
    /// </summary>
    [DataMember(Order = 2, Name = "destinations"), JsonPropertyOrder(2), JsonPropertyName("destinations"), YamlMember(Order = 2, Alias = "destinations")]
    public virtual EquatableList<SolaceDestinationDefinition>? Destinations { get; set; }

    /// <summary>
    /// Gets/sets the destination's time to live. Can be a integer representing the interval in milliseconds, a schema or reference.
    /// </summary>
    [DataMember(Order = 3, Name = "timeToLive"), JsonPropertyOrder(3), JsonPropertyName("timeToLive"), YamlMember(Order = 3, Alias = "timeToLive")]
    public virtual object? TimeToLive { get; set; }

    /// <summary>
    /// Gets/sets the destination's priority. Can be a integer between 0-255 with 0 as the lowest priority and 255 as the highest, a schema or reference.
    /// </summary>
    [DataMember(Order = 4, Name = "priority"), JsonPropertyOrder(4), JsonPropertyName("priority"), YamlMember(Order = 4, Alias = "priority")]
    public virtual object? Priority { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the operation messages are eligible to be moved to a Dead Message Queue.
    /// </summary>
    [DataMember(Order = 5, Name = "dmqEligible"), JsonPropertyOrder(5), JsonPropertyName("dmqEligible"), YamlMember(Order = 5, Alias = "dmqEligible")]
    public virtual bool DmqEligible { get; set; }

}
