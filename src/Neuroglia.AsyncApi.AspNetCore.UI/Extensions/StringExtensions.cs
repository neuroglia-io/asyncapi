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

using System.Text.RegularExpressions;

namespace Neuroglia.AsyncApi;

/// <summary>
/// Defines extensions for strings
/// </summary>
public static class StringExtensions
{

    /// <summary>
    /// Wraps placeholders with code tags
    /// </summary>
    /// <param name="input">The input string</param>
    /// <returns>The resulting string</returns>
    public static string WrapPlaceholdersWithCodeTags(this string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        var pattern = @"\{[^}]+\}";
        var replacement = "<code>$&</code>";
        var result = Regex.Replace(input, pattern, replacement);
        return result;
    }

}