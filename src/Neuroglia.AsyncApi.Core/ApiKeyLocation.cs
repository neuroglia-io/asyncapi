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

namespace Neuroglia.AsyncApi;

/// <summary>
/// Exposes constants about API key locations
/// </summary>
public static class ApiKeyLocation
{

    /// <summary>
    /// Stores the API key in the 'user' field
    /// </summary>
    public const string User = "user";
    /// <summary>
    /// Stores the API key in the 'password' field
    /// </summary>
    public const string Password = "password";

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> that contains all supported API key locations
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> that contains all supported API key locations</returns>
    public static IEnumerable<string> GetLocations()
    {
        yield return User;
        yield return Password;
    }

}
