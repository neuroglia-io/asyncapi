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

namespace Neuroglia.AsyncApi.v2;

/// <summary>
/// Enumerates all supported security schemes
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
[TypeConverter(typeof(EnumMemberTypeConverter))]
public enum SecuritySchemeType
{
    /// <summary>
    /// Indicates a security scheme based on a username/password pair
    /// </summary>
    [EnumMember(Value = "userPassword")]
    UserPassword,
    /// <summary>
    /// Indicates a security scheme based on API keys
    /// </summary>
    [EnumMember(Value = "apiKey")]
    ApiKey,
    /// <summary>
    /// Indicates a security scheme based on X509 certificates
    /// </summary>
    [EnumMember(Value = "X509")]
    X509,
    /// <summary>
    /// Indicates a security scheme based on symmetric encryption
    /// </summary>
    [EnumMember(Value = "symmetricEncryption")]
    SymmetricEncryption,
    /// <summary>
    /// Indicates a security scheme based on asymmetric encryption
    /// </summary>
    [EnumMember(Value = "asymmetricEncryption")]
    AsymmetricEncryption,
    /// <summary>
    /// Indicates a security scheme based on HTTP API keys
    /// </summary>
    [EnumMember(Value = "httpApiKey")]
    HttpApiKey,
    /// <summary>
    /// Indicates a security scheme based on HTTP
    /// </summary>
    [EnumMember(Value = "http")]
    Http,
    /// <summary>
    /// Indicates a security scheme based on OAUTH2
    /// </summary>
    [EnumMember(Value = "oauth2")]
    OAuth2,
    /// <summary>
    /// Indicates a security scheme based on OpenID Connect
    /// </summary>
    [EnumMember(Value = "openIdConnect")]
    OpenIDConnect,
    /// <summary>
    /// Indicates a plain security scheme
    /// </summary>
    [EnumMember(Value = "plain")]
    Plain,
    /// <summary>
    /// Indicates a SCRAM SHA256 security scheme
    /// </summary>
    [EnumMember(Value = "scramSha256")]
    ScramSha256,
    /// <summary>
    /// Indicates a SCRAM SHA512 security scheme
    /// </summary>
    [EnumMember(Value = "scramSha512")]
    ScramSha512,
    /// <summary>
    /// Indicates a GSSAPI security scheme
    /// </summary>
    [EnumMember(Value = "gssapi")]
    Gssapi
}
