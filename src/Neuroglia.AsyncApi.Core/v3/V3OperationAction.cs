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

namespace Neuroglia.AsyncApi.v3;

/// <summary>
/// Enumerates all Async API operation actions
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum V3OperationAction
{
    /// <summary>
    /// Indicates that the application will send a message to the given channel
    /// </summary>
    [EnumMember(Value = "send")]
    Send = 1,
    /// <summary>
    /// Indicates that the application should expect receiving messages from the given channel
    /// </summary>
    [EnumMember(Value = "receive")]
    Receive = 2
}