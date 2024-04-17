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

namespace Neuroglia.AsyncApi.v2.Bindings.Pulsar;

/// <summary>
/// Represents the object used to configure a Pulsar channel's retention policy
/// </summary>
[DataContract]
public record PulsarRetentionPolicyDefinition
{

    /// <summary>
    /// Gets/sets the retention time, in minutes.
    /// </summary>
    [DataMember(Order = 1, Name = "time"), JsonPropertyOrder(1), JsonPropertyName("time"), YamlMember(Order = 1, Alias = "time")]
    public virtual int Time { get; set; } = 0;

    /// <summary>
    /// Gets/sets the retention size, in MB.
    /// </summary>
    [DataMember(Order = 2, Name = "size"), JsonPropertyOrder(2), JsonPropertyName("size"), YamlMember(Order = 2, Alias = "size")]
    public virtual int Size { get; set; } = 0;

}