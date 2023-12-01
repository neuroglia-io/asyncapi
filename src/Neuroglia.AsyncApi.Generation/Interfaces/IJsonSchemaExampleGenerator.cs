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

namespace Neuroglia.AsyncApi.Generation;

/// <summary>
/// Defines the fundamentals of a service used to generate example values based on a JSON Schema
/// </summary>
public interface IJsonSchemaExampleGenerator
{

    /// <summary>
    /// Generates an example value based on the provided JSON Schema.
    /// </summary>
    /// <param name="schema">The JSON Schema for which an example value is to be generated.</param>
    /// <param name="name">The name of the JSON Schema to generate an example for</param>
    /// <param name="requiredPropertiesOnly">A boolean indicating whether or not only the required the generator should generate values for required properties only</param>
    /// <returns>An object representing an example value conforming to the provided JSON Schema.</returns>
    object? GenerateExample(JsonSchema schema, string? name = null, bool requiredPropertiesOnly = false);

}
