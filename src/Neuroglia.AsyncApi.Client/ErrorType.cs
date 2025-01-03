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

namespace Neuroglia.AsyncApi.Client;

/// <summary>
/// Enumerates all default error types
/// </summary>
public static class ErrorType
{

    const string BaseUri = "https://asyncapi.neuroglia.io/error/types/";

    /// <summary>
    /// Gets the type for validation errors
    /// </summary>
    public static readonly Uri Validation = new($"{BaseUri}validation");

    /// <summary>
    /// Enumerates all default error types
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all default error types</returns>
    public static IEnumerable<Uri> AsEnumerable()
    {
        yield return Validation;
    }

}
