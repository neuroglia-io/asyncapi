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

namespace Neuroglia.AsyncApi.Validation.v3;

/// <summary>
/// Represents the service used to validate <see cref="V3ChannelDefinition"/>s
/// </summary>
public class V3ChannelValidator
    : V3ReferenceableComponentValidator<V3ChannelDefinition>
{

    /// <inheritdoc/>
    public V3ChannelValidator(V3AsyncApiDocument? document = null)
        : base(document)
    {
        this.RuleFor(s => s.Address)
            .NotEmpty()
            .When(s => !s.IsReference);
        this.RuleFor(s => s.Servers)
            .NotEmpty()
            .When(s => !s.IsReference);
        this.RuleForEach(s => s.Servers)
            .SetValidator(new V3ReferenceValidator<V3ServerDefinition>(document))
            .When(s => !s.IsReference);
        this.RuleForEach(s => s.Parameters!.Values)
            .SetValidator(new V3ParameterValidator(document))
            .When(s => !s.IsReference && s.Parameters != null);
        this.RuleForEach(s => s.Tags)
            .SetValidator(new V3TagValidator(document))
            .When(s => !s.IsReference);
        this.RuleFor(s => s.ExternalDocs!)
            .SetValidator(new V3ExternalDocumentationValidator(document))
            .When(s => !s.IsReference);
        this.RuleFor(s => s.Bindings!)
           .SetValidator(new V3ChannelBindingCollectionValidator(document))
           .When(s => !s.IsReference);
    }

}
