﻿// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
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
/// Represents the service used to validate <see cref="V3CorrelationIdDefinition"/>s
/// </summary>
public class V3CorrelationIdValidator
    : V3ReferenceableComponentValidator<V3CorrelationIdDefinition>
{

    /// <inheritdoc/>
    public V3CorrelationIdValidator(V3AsyncApiDocument? document = null) 
        : base(document)
    {
        this.RuleFor(c => c.Location)
            .NotEmpty()
            .When(c => !c.IsReference);
        this.RuleFor(c => c.Location)
            .Must(x => RuntimeExpression.TryParse(x!, out var expression) && expression != null)
            .When(c => !c.IsReference);
    }

}