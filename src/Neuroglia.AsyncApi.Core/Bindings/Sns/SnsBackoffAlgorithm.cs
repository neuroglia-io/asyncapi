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

using Neuroglia.Serialization.Json.Converters;
using System.ComponentModel;

namespace Neuroglia.AsyncApi.Bindings.Sns;

/// <summary>
/// Enumerates retry backoff algorithm supported by SNS
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum SnsBackoffAlgorithm
{
    /// <summary>
    /// Indicates the arithmetic backoff algorithm
    /// </summary>
    [EnumMember(Value = "arithmetic")]
    Arithmetic = 1,
    /// <summary>
    /// Indicates the exponential backoff algorithm
    /// </summary>
    [EnumMember(Value = "exponential")]
    Exponential = 2,
    /// <summary>
    /// Indicates the geometric backoff algorithm
    /// </summary>
    [EnumMember(Value = "geometric")]
    Geometric = 4,
    /// <summary>
    /// Indicates the linear backoff algorithm
    /// </summary>
    [EnumMember(Value = "linear")]
    Linear = 8
}