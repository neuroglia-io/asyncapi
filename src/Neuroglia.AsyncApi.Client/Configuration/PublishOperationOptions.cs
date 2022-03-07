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
namespace Neuroglia.AsyncApi.Client.Configuration
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IPublishOperationOptionsBuilder"/> interface
    /// </summary>
    public class PublishOperationOptions
        : IPublishOperationOptions
    {

        /// <inheritdoc/>
        public virtual string Protocol { get; set; }

    }

}
