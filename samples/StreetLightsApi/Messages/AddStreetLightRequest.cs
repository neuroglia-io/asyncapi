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

namespace StreetLightsApi.Messages;

/// <summary>
/// Represents a request to add a new street light
/// </summary>
public class AddStreetLightRequest
{

    /// <summary>
    /// Gets/sets the id of the street to add a new light to
    /// </summary>
    public required string StreetId { get; init; }

    /// <summary>
    /// Gets/sets the latitude of the light to add
    /// </summary>
    public required decimal Latitude { get; init; }

    /// <summary>
    /// Gets/sets the longitude of the light to add
    /// </summary>
    public required decimal Longitude { get; init; }

}
