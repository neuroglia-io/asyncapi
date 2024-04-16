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

namespace Neuroglia.AsyncApi.v2.Bindings.GooglePubSub;

/// <summary>
/// Enumerates all supported Google Pub/Sub message encodings
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum GooglePubSubMessageEncoding
{
    /// <summary>
    /// Indicates an unspecified encoding
    /// </summary>
    [EnumMember(Value = "ENCODING_UNSPECIFIED")]
    Unspecified = 0,
    /// <summary>
    /// Indicates a JSON encoding
    /// </summary>
    [EnumMember(Value = "JSON")]
    Json = 1,
    /// <summary>
    /// Indicates a binary encoding
    /// </summary>
    [EnumMember(Value = "BINARY")]
    Binary = 2
}