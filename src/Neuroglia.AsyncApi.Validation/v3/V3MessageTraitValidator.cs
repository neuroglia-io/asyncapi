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
/// Represents the service used to validate <see cref="V3MessageTraitDefinition"/>s
/// </summary>
public class V3MessageTraitValidator<TMessage>
    : V3ReferenceableComponentValidator<TMessage>
    where TMessage : V3MessageTraitDefinition
{

    /// <inheritdoc/>
    public V3MessageTraitValidator(V3AsyncApiDocument? document = null)
        : base(document)
    {
        this.RuleFor(o => o.Headers!)
            .SetValidator(new V3SchemaValidator(document))
            .When(o => !o.IsReference);
        this.RuleFor(o => o.CorrelationId!)
            .SetValidator(new V3CorrelationIdValidator(document))
            .When(o => !o.IsReference);
        this.RuleForEach(o => o.Tags)
            .SetValidator(new V3TagValidator(document))
            .When(o => !o.IsReference);
        this.RuleFor(o => o.ExternalDocs!)
            .SetValidator(new V3ExternalDocumentationValidator(document))
            .When(o => !o.IsReference);
        this.RuleFor(o => o.Bindings!)
            .SetValidator(new V3MessageBindingCollectionValidator(document))
            .When(o => !o.IsReference);
        this.RuleForEach(o => o.Examples!)
            .SetValidator(new V3MessageExampleValidator(document))
            .When(o => !o.IsReference);
    }

}
