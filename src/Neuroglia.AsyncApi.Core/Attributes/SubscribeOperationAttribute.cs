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

namespace Neuroglia.AsyncApi;

/// <summary>
/// Represents an <see cref="Attribute"/> used to mark a method as an <see cref="V2OperationDefinition"/> of type <see cref="V2OperationType.Subscribe"/>
/// </summary>
public class SubscribeOperationAttribute
    : OperationAttribute
{
    
    /// <summary>
    /// Initializes a new <see cref="SubscribeOperationAttribute"/>
    /// </summary>
    /// <param name="messageType">The <see cref="V2OperationDefinition"/>'s message type</param>
    public SubscribeOperationAttribute(Type messageType) : base(V2OperationType.Subscribe, messageType) { }

    /// <summary>
    /// Initializes a new <see cref="OperationAttribute"/>
    /// </summary>
    public SubscribeOperationAttribute() : base(V2OperationType.Subscribe) { }

}
