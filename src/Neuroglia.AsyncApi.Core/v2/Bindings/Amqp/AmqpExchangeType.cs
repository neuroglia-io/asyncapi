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

namespace Neuroglia.AsyncApi.v2.Bindings.Amqp;

/// <summary>
/// Enumerates all supported AMQP exchange types
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum AmqpExchangeType
{
    /// <summary>
    /// Indicates a topic AMQP exchange
    /// </summary>
    [EnumMember(Value = "topic")]
    Topic = 1,
    /// <summary>
    /// Indicates a direct AMQP exchange
    /// </summary>
    [EnumMember(Value = "direct")]
    Direct = 2,
    /// <summary>
    /// Indicates a fanout AMQP exchange
    /// </summary>
    [EnumMember(Value = "fanout")]
    Fanout = 4,
    /// <summary>
    /// Indicates a default AMQP exchange
    /// </summary>
    [EnumMember(Value = "default")]
    Default = 8,
    /// <summary>
    /// Indicates a headers AMQP exchange
    /// </summary>
    [EnumMember(Value = "headers")]
    Headers = 16
}
