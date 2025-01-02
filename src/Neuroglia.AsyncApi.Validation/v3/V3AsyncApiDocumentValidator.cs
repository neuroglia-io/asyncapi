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
/// Represents the service used to validate <see cref="V3AsyncApiDocument"/>s
/// </summary>
public class V3AsyncApiDocumentValidator
    : AbstractValidator<V3AsyncApiDocument>
{

    /// <inheritdoc/>
    public V3AsyncApiDocumentValidator()
    {
        this.RuleFor(d => d.AsyncApi)
            .NotEmpty();
        this.RuleFor(d => d.Info)
            .NotNull()
            .SetValidator(document => new V3ApiInfoValidator(document));
        this.RuleFor(d => d.DefaultContentType)
            .NotEmpty();
        this.RuleForEach(d => d.Servers!.Values)
            .SetValidator(document => new V3ServerValidator(document))
            .When(d => d.Servers != null);
        this.RuleForEach(d => d.Channels.Values)
            .SetValidator(document => new V3ChannelValidator(document))
            .When(d => d.Channels != null);
        this.RuleForEach(d => d.Operations.Values)
            .SetValidator(document => new V3OperationValidator(document))
            .When(d => d.Operations != null);
        this.RuleFor(d => d.Components!)
            .SetValidator(document => new V3ComponentCollectionValidator(document));
    }

}
