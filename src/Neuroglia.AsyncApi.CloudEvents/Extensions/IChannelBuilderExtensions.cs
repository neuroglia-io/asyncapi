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

using Neuroglia.AsyncApi.FluentBuilders.v2;
using Neuroglia.AsyncApi.v2;

namespace Neuroglia.AsyncApi.CloudEvents;

/// <summary>
/// Defines extensions for <see cref="IV2OperationDefinitionBuilder"/>s
/// </summary>
public static class IAsyncApiDocumentBuilderExtensions
{

    /// <summary>
    /// Configures the <see cref="V2OperationDefinition"/> to build to use the specified <see cref="V2MessageDefinition"/>
    /// </summary>
    /// <param name="operation">The <see cref="IV2OperationDefinitionBuilder"/> to configure</param>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="V2MessageDefinition"/> to use</param>
    /// <returns>The configured <see cref="IV2OperationDefinitionBuilder"/></returns>
    public static IV2OperationDefinitionBuilder WithCloudEventMessage(this IV2OperationDefinitionBuilder operation, Action<ICloudEventMessageDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(setup);

        operation.WithMessage(message =>
        {
            var cloudEvent = new CloudEventMessageDefinitionBuilder(message);
            setup(cloudEvent);
            cloudEvent.Build();
        });

        return operation;
    }

}