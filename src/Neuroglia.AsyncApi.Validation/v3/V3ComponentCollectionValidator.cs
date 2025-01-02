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
/// Represents the service used to validate <see cref="V3ComponentDefinitionCollection"/>s
/// </summary>
public class V3ComponentCollectionValidator
    : AbstractValidator<V3ComponentDefinitionCollection>
{

    /// <inheritdoc/>
    public V3ComponentCollectionValidator(V3AsyncApiDocument? document = null)
    {
        this.RuleForEach(c => c.Schemas!.Values)
            .SetValidator(new V3SchemaValidator(document))
            .When(c => c.Schemas != null);
        this.RuleForEach(c => c.Servers!.Values)
            .SetValidator(new V3ServerValidator(document))
            .When(c => c.Servers != null);
        this.RuleForEach(c => c.Channels!.Values)
            .SetValidator(new V3ChannelValidator(document))
            .When(c => c.Channels != null);
        this.RuleForEach(c => c.Operations!.Values)
            .SetValidator(new V3OperationValidator(document))
            .When(c => c.Operations != null);
        this.RuleForEach(c => c.Messages!.Values)
            .SetValidator(new V3MessageValidator(document))
            .When(c => c.Messages != null);
        this.RuleForEach(c => c.SecuritySchemes!.Values)
            .SetValidator(new V3SecuritySchemeValidator(document))
            .When(c => c.SecuritySchemes != null);
        this.RuleForEach(c => c.ServerVariables!.Values)
            .SetValidator(new V3ServerVariableValidator(document))
            .When(c => c.ServerVariables != null);
        this.RuleForEach(c => c.Parameters!.Values)
            .SetValidator(new V3ParameterValidator(document))
            .When(c => c.Parameters != null);
        this.RuleForEach(c => c.CorrelationIds!.Values)
            .SetValidator(new V3CorrelationIdValidator(document))
            .When(c => c.CorrelationIds != null);
        this.RuleForEach(c => c.Replies!.Values)
            .SetValidator(new V3ReplyValidator(document))
            .When(c => c.Replies != null);
        this.RuleForEach(c => c.ReplyAddresses!.Values)
            .SetValidator(new V3ReplyAddressValidator(document))
            .When(c => c.ReplyAddresses != null);
        this.RuleForEach(c => c.ExternalDocs!.Values)
            .SetValidator(new V3ExternalDocumentationValidator(document))
            .When(c => c.ExternalDocs != null);
        this.RuleForEach(c => c.Tags!.Values)
            .SetValidator(new V3TagValidator(document))
            .When(c => c.Tags != null);
        this.RuleForEach(c => c.OperationTraits!.Values)
            .SetValidator(new V3OperationTraitValidator<V3OperationTraitDefinition>(document))
            .When(c => c.OperationTraits != null);
        this.RuleForEach(c => c.MessageTraits!.Values)
            .SetValidator(new V3MessageTraitValidator<V3MessageTraitDefinition>(document))
            .When(c => c.MessageTraits != null);
        this.RuleForEach(c => c.ServerBindings!.Values)
            .SetValidator(new V3ServerBindingCollectionValidator(document))
            .When(c => c.ServerBindings != null);
        this.RuleForEach(c => c.ChannelBindings!.Values)
            .SetValidator(new V3ChannelBindingCollectionValidator(document))
            .When(c => c.ChannelBindings != null);
        this.RuleForEach(c => c.OperationBindings!.Values)
            .SetValidator(new V3OperationBindingCollectionValidator(document))
            .When(c => c.OperationBindings != null);
        this.RuleForEach(c => c.MessageBindings!.Values)
            .SetValidator(new V3MessageBindingCollectionValidator(document))
            .When(c => c.MessageBindings != null);
    }

}