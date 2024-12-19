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

namespace Neuroglia.AsyncApi.FluentBuilders.v2;

/// <summary>
/// Represents the default implementation of the <see cref="IV2ExternalDocumentationDefinitionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V2ExternalDocumentationDefinition"/>s</param>
public class V2ExternalDocumentationDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V2ExternalDocumentationDefinition>> validators)
    : IV2ExternalDocumentationDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="V2ExternalDocumentationDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V2ExternalDocumentationDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V2ExternalDocumentationDefinition"/> to configure
    /// </summary>
    protected virtual V2ExternalDocumentationDefinition ExternalDocumentation { get; } = new();

    /// <inheritdoc/>
    public virtual IV2ExternalDocumentationDefinitionBuilder WithUrl(Uri url)
    {
        ArgumentNullException.ThrowIfNull(url);
        ExternalDocumentation.Url = url;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV2ExternalDocumentationDefinitionBuilder WithDescription(string? description)
    {
        ExternalDocumentation.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual V2ExternalDocumentationDefinition Build()
    {
        var validationResults = Validators.Select(v => v.Validate(ExternalDocumentation));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors!));
        return ExternalDocumentation;
    }

}