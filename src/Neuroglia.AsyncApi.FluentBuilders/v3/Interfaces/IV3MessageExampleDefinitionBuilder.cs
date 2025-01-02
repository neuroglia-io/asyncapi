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
/// Defines the fundamentals of a service used to build <see cref="V3MessageExampleDefinition"/>s
/// </summary>
public interface IV3MessageExampleDefinitionBuilder
{

    /// <summary>
    /// Configures the <see cref="V3MessageExampleDefinition"/> to use the specified header
    /// </summary>
    /// <param name="name">The name of the header to add</param>
    /// <param name="value">The value of the header to add</param>
    /// <returns>The configured <see cref="IV3MessageExampleDefinitionBuilder"/></returns>
    IV3MessageExampleDefinitionBuilder WithHeader(string name, object value);

    /// <summary>
    /// Configures the <see cref="V3MessageExampleDefinition"/> to use the specified headers
    /// </summary>
    /// <param name="headers">A key/value map of the headers to use</param>
    /// <returns>The configured <see cref="IV3MessageExampleDefinitionBuilder"/></returns>
    IV3MessageExampleDefinitionBuilder WithHeaders(IDictionary<string, object>? headers);

    /// <summary>
    /// Configures the <see cref="V3MessageExampleDefinition"/> to use the specified payload
    /// </summary>
    /// <param name="payload">A key/value map of the payload properties to use</param>
    /// <returns>The configured <see cref="IV3MessageExampleDefinitionBuilder"/></returns>
    IV3MessageExampleDefinitionBuilder WithPayload(IDictionary<string, object>? payload);

    /// <summary>
    /// Configures the <see cref="V3MessageExampleDefinition"/> to build to use the specified name
    /// </summary>
    /// <param name="name">The name to use</param>
    /// <returns>The configured <see cref="IV3MessageExampleDefinitionBuilder"/></returns>
    IV3MessageExampleDefinitionBuilder WithName(string? name);

    /// <summary>
    /// Configures the <see cref="V3MessageExampleDefinition"/> to build to use the specified summary
    /// </summary>
    /// <param name="summary">The summary to use</param>
    /// <returns>The configured <see cref="IV3MessageExampleDefinitionBuilder"/></returns>
    IV3MessageExampleDefinitionBuilder WithSummary(string? summary);

    /// <summary>
    /// Builds the configured <see cref="V3MessageExampleDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="V3MessageExampleDefinition"/></returns>
    V3MessageExampleDefinition Build();

}
