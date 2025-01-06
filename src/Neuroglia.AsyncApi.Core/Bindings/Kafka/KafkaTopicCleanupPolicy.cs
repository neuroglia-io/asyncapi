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

namespace Neuroglia.AsyncApi.Bindings.Kafka;

/// <summary>
/// Enumerates all supported topic cleanup policies
/// </summary>
public static class KafkaTopicCleanupPolicy
{

    /// <summary>
    /// Gets the name of the policy used to discard old segments when their retention time or size limit has been reached
    /// </summary>
    public const string Delete = "delete";

    /// <summary>
    /// Gets the name of the policy used to enable log compaction, which retains the latest value for each key
    /// </summary>
    public const string Compact = "compact";

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing all supported topic cleanup policies
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all supported topic cleanup policies</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Delete;
        yield return Compact;
    }

}