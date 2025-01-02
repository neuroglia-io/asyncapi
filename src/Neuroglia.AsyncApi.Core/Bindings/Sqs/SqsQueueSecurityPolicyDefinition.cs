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

namespace Neuroglia.AsyncApi.Bindings.Sqs;

/// <summary>
/// Represents an object used to configure the security policy of an SQS queue
/// </summary>
[DataContract]
public record SqsQueueSecurityPolicyDefinition
{

    /// <summary>
    /// Gets/sets the statements that each controls a permission for this queue.
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "statements"), JsonPropertyOrder(1), JsonPropertyName("statements"), YamlMember(Order = 1, Alias = "statements")]
    public virtual EquatableList<SqsTopicSecurityStatementDefinition> Statements { get; set; } = [];

}
