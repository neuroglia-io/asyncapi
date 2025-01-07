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

using SolaceSystems.Solclient.Messaging;

namespace Neuroglia.AsyncApi.Client.Bindings.Solace;

public partial class SolaceBindingHandler
{
    /// <summary>
    /// Represents an object used to describe a Solace destination
    /// </summary>
    /// <param name="Definition">The definition of the described destination</param>
    /// <param name="Instance">The destination instance</param>
    public record SolaceDestinationDescriptor(SolaceDestinationDefinition Definition, IDestination Instance);

}

