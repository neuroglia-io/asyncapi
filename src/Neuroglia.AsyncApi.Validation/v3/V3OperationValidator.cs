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
/// Represents the service used to validate <see cref="V3OperationDefinition"/>s
/// </summary>
public class V3OperationValidator
    : V3OperationTraitValidator<V3OperationDefinition>
{

    /// <inheritdoc/>
    public V3OperationValidator(V3AsyncApiDocument? document = null) 
        : base(document)
    {
        this.RuleFor(o => o.Channel)
            .NotNull()
            .When(o => !o.IsReference);
        this.RuleFor(o => o.Channel)
            .SetValidator(new V3ReferenceValidator<V3ChannelDefinition>(document))
            .When(o => !o.IsReference);
        this.RuleForEach(o => o.Traits!)
            .SetValidator(new V3OperationTraitValidator<V3OperationTraitDefinition>(document))
            .When(o => !o.IsReference);
        this.RuleForEach(o => o.Messages)
            .NotEmpty()
            .When(o => !o.IsReference);
        this.RuleForEach(o => o.Messages.Select(m => new KeyValuePair<V3ReferenceDefinition, V3ReferenceDefinition>(o.Channel, m)))
            .SetValidator(o => new V3OperationMessageValidator(document))
            .OverridePropertyName(nameof(V3OperationDefinition.Messages))
            .When(o => !o.IsReference);
        this.RuleFor(o => o.Reply!)
            .SetValidator(new V3ReplyValidator(document))
            .When(o => !o.IsReference);
    }

}
