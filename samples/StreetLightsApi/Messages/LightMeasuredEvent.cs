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

using Json.Schema.Generation;
using Neuroglia.AsyncApi;

namespace StreetLightsApi.Server.Messages;

[Tag("light", "A tag for light-related messages"), Tag("measurement", "A tag for measurement-related messages")]
[Message(Name = "LightMeasured", Description = "A message used to measure light")]
public class LightMeasuredEvent
{

    [Required, Description("The id of the measured light ")]
    public Guid Id { get; set; }

    [Required, Description("The type of the measured light")]
    public StreetLightType Type { get; set; }

    [Required, Description("The specified light's lumens measurement")]
    public int Lumens { get; set; }

    [Required, Description("The date and time at which the event has been created")]
    public DateTime SentAt { get; set; }

    [Description("The event's metadata")]
    public IDictionary<string, string>? Metadata { get; set; }

    public List<Todo> Todos { get; set; }

}

public record Todo(string Name);
