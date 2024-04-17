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

namespace Neuroglia.AsyncApi.v2.Bindings.Sns;

/// <summary>
/// Represents an object used to configure an SNS topic security statement
/// </summary>
[DataContract]
public record SnsTopicSecurityStatementDefinition
{

    /// <summary>
    /// Gets/sets the type of SNS Topic. Can be either standard or FIFO.
    /// </summary>
    [DataMember(Order = 1, Name = "effect"), JsonPropertyOrder(1), JsonPropertyName("effect"), YamlMember(Order = 1, Alias = "effect")]
    public virtual SnsTopicSecurityStatementEffect Effect { get; set; }

    /// <summary>
    /// Gets/sets the AWS account or resource ARN that this statement applies to. Can be a string or a list of string.
    /// </summary>
    [DataMember(Order = 2, Name = "principal"), JsonPropertyOrder(2), JsonPropertyName("principal"), YamlMember(Order = 2, Alias = "principal")]
    public virtual object? Principal { get; set; }

    /// <summary>
    /// Gets/sets the SNS permission being allowed or denied e.g. sns:Publish. Can be a string or a list of string.
    /// </summary>
    [DataMember(Order = 3, Name = "action"), JsonPropertyOrder(3), JsonPropertyName("action"), YamlMember(Order = 3, Alias = "action")]
    public virtual object? Action { get; set; }

}
