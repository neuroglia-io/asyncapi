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

namespace Neuroglia.AsyncApi.Validation.v2;

/// <summary>
/// Represents the service used to validate <see cref="V2AsyncApiDocument"/>s
/// </summary>
public class V2AsyncApiDocumentValidator
    : AbstractValidator<V2AsyncApiDocument>
{

    /// <summary>
    /// Initializes a new <see cref="V2AsyncApiDocumentValidator"/>
    /// </summary>
    public V2AsyncApiDocumentValidator()
    {
        this.RuleFor(d => d.AsyncApi)
            .NotEmpty();
        this.RuleFor(d => d.Info)
            .NotNull()
            .SetValidator(new V2ApiInfoValidator());
        this.RuleFor(d => d.Channels)
            .NotEmpty();
        this.RuleForEach(d => d.Channels.Values)
            .SetValidator(new V2ChannelValidator())
            .When(d => d.Channels != null);
        this.RuleFor(d => d.Components!)
            .SetValidator(new V2ComponentCollectionValidator());
        this.RuleForEach(d => d.Servers!.Values)
            .SetValidator(new V2ServerValidator())
            .When(d => d.Servers != null);
        this.RuleForEach(d => d.Tags)
            .SetValidator(new V2TagValidator());
    }

}
