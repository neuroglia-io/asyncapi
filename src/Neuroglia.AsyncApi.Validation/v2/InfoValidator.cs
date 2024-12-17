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

using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.Validation;

/// <summary>
/// Represents the service used to validate the <see cref="V2ApiInfo"/>
/// </summary>
public class InfoValidator
    : AbstractValidator<V2ApiInfo>
{

    /// <summary>
    /// Initializes a new <see cref="InfoValidator"/>
    /// </summary>
    public InfoValidator()
    {
        this.RuleFor(i => i.Title)
            .NotEmpty();
        this.RuleFor(i => i.Version)
            .NotEmpty();
        this.RuleFor(i => i.License!)
            .SetValidator(new LicenseValidator());
    }

}
