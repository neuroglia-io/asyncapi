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

namespace Neuroglia.AsyncApi.FluentBuilders.v3;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="V3SchemaDefinition"/>s
/// </summary>
public interface IV3SchemaDefinitionBuilder
    : IReferenceableComponentDefinitionBuilder<V3SchemaDefinition>
{

    /// <summary>
    /// Configures the <see cref="V3SchemaDefinition"/> to use the specified schema
    /// </summary>
    /// <param name="schema">The schema to use</param>
    /// <returns>The configured <see cref="IV3SchemaDefinitionBuilder"/></returns>
    IV3SchemaDefinitionBuilder WithSchema(object schema);

    /// <summary>
    /// Configures the <see cref="V3SchemaDefinition"/> to use the specified <see cref="JsonSchema"/>
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="JsonSchema"/> to use</param>
    void WithJsonSchema(Action<JsonSchemaBuilder> setup);

    /// <summary>
    /// Configures the <see cref="V3SchemaDefinition"/> to use the specified format
    /// </summary>
    /// <param name="format">The format to use</param>
    /// <returns>The configured <see cref="IV3SchemaDefinitionBuilder"/></returns>
    IV3SchemaDefinitionBuilder WithFormat(string format);

    /// <summary>
    /// Builds the configured <see cref="V3SchemaDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="V3SchemaDefinition"/></returns>
    V3SchemaDefinition Build();

}