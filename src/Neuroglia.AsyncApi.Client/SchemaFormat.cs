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

namespace Neuroglia.AsyncApi.Client;

/// <summary>
/// Enumerates all supported schema formats
/// </summary>
public static class SchemaFormat
{

    /// <summary>
    /// Gets the format for AsyncAPI schemas
    /// </summary>
    public const string AsyncApi = "application/vnd.aai.asyncapi+json";
    /// <summary>
    /// Gets the format for Avro Schemas
    /// </summary>
    public const string Avro = "application/avro";
    /// <summary>
    /// Gets the format for JSON Schemas
    /// </summary>
    public const string Json = "application/schema+json";
    /// <summary>
    /// Gets the format for XML Schemas
    /// </summary>
    public const string Xml = "application/xml";

}