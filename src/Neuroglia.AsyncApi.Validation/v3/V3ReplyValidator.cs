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
/// Represents the service used to validate <see cref="V3ReplyDefinition"/>s
/// </summary>
public class V3ReplyValidator
    : V3ReferenceableComponentValidator<V3ReplyDefinition>
{

    /// <inheritdoc/>
    public V3ReplyValidator(V3AsyncApiDocument? document = null) 
        : base(document)
    {
        this.RuleFor(r => r.Address)
            .NotNull()
            .When(r => !r.IsReference && r.Channel == null);
        this.RuleFor(r => r.Address)
            .Null()
            .When(r => !r.IsReference && r.Channel != null);
        this.RuleFor(r => r.Address!)
            .SetValidator(new V3ReplyAddressValidator(document))
            .When(r => !r.IsReference);
        this.RuleFor(r => r.Channel)
            .NotNull()
            .When(r => !r.IsReference && r.Address == null);
        this.RuleFor(r => r.Channel)
            .Null()
            .When(r => !r.IsReference && r.Address != null);
        this.RuleFor(r => r.Channel!)
            .SetValidator(new V3ReferenceValidator<V3ChannelDefinition>(document))
            .When(r => !r.IsReference);
        this.RuleForEach(r => r.Messages!)
            .SetValidator(new V3ReferenceValidator<V3MessageDefinition>(document))
            .When(r => !r.IsReference);
    }

}
