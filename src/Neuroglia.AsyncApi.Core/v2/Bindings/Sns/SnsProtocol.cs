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

namespace Neuroglia.AsyncApi.v2.Bindings.Sns;

/// <summary>
/// Enumerates all supported SNS protocols
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum SnsProtocol
{
    /// <summary>
    /// Indicates the http protocol
    /// </summary>
    [EnumMember(Value = "http")]
    Http,
    /// <summary>
    /// Indicates the https protocol
    /// </summary>
    [EnumMember(Value = "https")]
    Https,
    /// <summary>
    /// Indicates the email protocol
    /// </summary>
    [EnumMember(Value = "email")]
    Email,
    /// <summary>
    /// Indicates the email JSON protocol
    /// </summary>
    [EnumMember(Value = "email-json")]
    EmailJson,
    /// <summary>
    /// Indicates the SMS protocol
    /// </summary>
    [EnumMember(Value = "sms")]
    Sms,
    /// <summary>
    /// Indicates the SQS protocol
    /// </summary>
    [EnumMember(Value = "sqs")]
    Sqs,
    /// <summary>
    /// Indicates the application protocol
    /// </summary>
    [EnumMember(Value = "application")]
    Application,
    /// <summary>
    /// Indicates the lambda protocol
    /// </summary>
    [EnumMember(Value = "lambda")]
    Lambda,
    /// <summary>
    /// Indicates the firehose protocol
    /// </summary>
    [EnumMember(Value = "firehose")]
    Firehose
}