/*
 * Copyright © 2021 Neuroglia SPRL. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using Neuroglia.AsyncApi.Models;
using System;

namespace Neuroglia.AsyncApi
{
    /// <summary>
    /// Represents an <see cref="Attribute"/> used to mark a method as an <see cref="Operation"/> of type <see cref="OperationType.Publish"/>
    /// </summary>
    public class PublishOperationAttribute
        : OperationAttribute
    {

        /// <summary>
        /// Initializes a new <see cref="PublishOperationAttribute"/>
        /// </summary>
        /// <param name="messageType">The <see cref="Operation"/>'s message type</param>
        public PublishOperationAttribute(Type messageType)
            : base(OperationType.Publish, messageType)
        {

        }

        /// <summary>
        /// Initializes a new <see cref="PublishOperationAttribute"/>
        /// </summary>
        public PublishOperationAttribute()
            : base(OperationType.Publish)
        {
        }

    }

}
