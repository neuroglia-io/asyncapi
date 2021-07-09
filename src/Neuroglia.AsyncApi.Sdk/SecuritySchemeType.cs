/*
 * Copyright © 2021 Neuroglia SPRL. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using System.Runtime.Serialization;

namespace Neuroglia.AsyncApi.Sdk
{
    /// <summary>
    /// Enumerates all supported security schemes
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.StringEnumConverterFactory))]
    public enum SecuritySchemeType
    {
        [EnumMember(Value = "userPassword")]
        UserPassword,
        [EnumMember(Value = "apiKey")]
        ApiKey,
        [EnumMember(Value = "X509")]
        X509,
        [EnumMember(Value = "symmetricEncryption")]
        SymmetricEncryption,
        [EnumMember(Value = "asymmetricEncryption")]
        AsymmetricEncryption,
        [EnumMember(Value = "httpApiKey")]
        HttpApiKey,
        [EnumMember(Value = "http")]
        Http,
        [EnumMember(Value = "oauth2")]
        OAuth2,
        [EnumMember(Value = "openIdConnect")]
        OpenIDConnect,
        [EnumMember(Value = "plain")]
        Plain,
        [EnumMember(Value = "scramSha256")]
        ScramSha256,
        [EnumMember(Value = "scramSha512")]
        ScramSha512,
        [EnumMember(Value = "gssapi")]
        Gssapi
    }

}
