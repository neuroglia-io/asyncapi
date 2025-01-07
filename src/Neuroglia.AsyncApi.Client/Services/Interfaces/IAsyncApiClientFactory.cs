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

namespace Neuroglia.AsyncApi.Client.Services;

/// <summary>
/// Defines the fundamentals of a service used to create <see cref="IAsyncApiClient"/>s
/// </summary>
public interface IAsyncApiClientFactory
{

    /// <summary>
    /// Creates a new <see cref="IAsyncApiClient"/> used to interact with the application described by the specified <see cref="IAsyncApiDocument"/>
    /// </summary>
    /// <param name="document">The <see cref="IAsyncApiDocument"/> that describes the application to create a new <see cref="IAsyncApiClient"/> for</param>
    /// <returns>A new <see cref="IAsyncApiClient"/></returns>
    IAsyncApiClient CreateFor(IAsyncApiDocument document);

}
