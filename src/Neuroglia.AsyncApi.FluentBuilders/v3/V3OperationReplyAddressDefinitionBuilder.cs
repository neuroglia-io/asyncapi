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
/// Represents the default implementation of the <see cref="IV3OperationReplyAddressDefinitionBuilder"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="validators">The services used to validate <see cref="V3ReplyAddressDefinition"/>s</param>
public class V3OperationReplyAddressDefinitionBuilder(IServiceProvider serviceProvider, IEnumerable<IValidator<V3ReplyAddressDefinition>> validators)
    : IV3OperationReplyAddressDefinitionBuilder
{

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected virtual IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the services used to validate <see cref="V3ReplyAddressDefinition"/>s
    /// </summary>
    protected virtual IEnumerable<IValidator<V3ReplyAddressDefinition>> Validators { get; } = validators;

    /// <summary>
    /// Gets the <see cref="V3ReplyAddressDefinition"/> to configure
    /// </summary>
    protected virtual V3ReplyAddressDefinition ReplyAddress { get; } = new();

    /// <inheritdoc/>
    public virtual void Use(string reference)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reference);
        ReplyAddress.Reference = reference;
    }

    /// <inheritdoc/>
    public virtual IV3OperationReplyAddressDefinitionBuilder WithDescription(string? description)
    {
        ReplyAddress.Description = description;
        return this;
    }

    /// <inheritdoc/>
    public virtual IV3OperationReplyAddressDefinitionBuilder WithLocation(string location)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(location);
        ReplyAddress.Location = location;
        return this;
    }

    /// <inheritdoc/>
    public virtual V3ReplyAddressDefinition Build()
    {
        var validationResults = Validators.Select(v => v.Validate(ReplyAddress));
        if (!validationResults.All(r => r.IsValid)) throw new ValidationException(validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors!));
        return ReplyAddress;
    }

}
