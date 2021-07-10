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

namespace Neuroglia.AsyncApi
{
    /// <summary>
    /// Enumerates all supported HTTP methods
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.StringEnumConverterFactory))]
    public enum HttpMethod
    {
        /// <summary>
        /// Indicates the GET http method
        /// </summary>
        [EnumMember(Value = "GET")]
        GET,
        /// <summary>
        /// Indicates the POST http method
        /// </summary>
        [EnumMember(Value = "POST")]
        POST,
        /// <summary>
        /// Indicates the PUT http method
        /// </summary>
        [EnumMember(Value = "PUT")]
        PUT,
        /// <summary>
        /// Indicates the PATCH http method
        /// </summary>
        [EnumMember(Value = "PATCH")]
        PATCH,
        /// <summary>
        /// Indicates the DELETE http method
        /// </summary>
        [EnumMember(Value = "DELETE")]
        DELETE,
        /// <summary>
        /// Indicates the HEAD http method
        /// </summary>
        [EnumMember(Value = "HEAD")]
        HEAD,
        /// <summary>
        /// Indicates the OPTIONS http method
        /// </summary>
        [EnumMember(Value = "OPTIONS")]
        OPTIONS,
        /// <summary>
        /// Indicates the CONNECT http method
        /// </summary>
        [EnumMember(Value = "CONNECT")]
        CONNECT,
        /// <summary>
        /// Indicates the TRACE http method
        /// </summary>
        [EnumMember(Value = "TRACE")]
        TRACE
    }

}
