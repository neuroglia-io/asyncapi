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

using Neuroglia.AsyncApi;
using System.ComponentModel;

namespace StreetLightsApi.Server.Messages
{
    [Tag("movement", "A tag for movement-related messages"), Tag("sensor", "A tag for sensor-related messages")]
    [Message(Name = "MovementDetected")]
    public class MovementDetectedEvent
    {

        [Description("The id of the sensor that has detected movement")]
        public int SensorId { get; set; }

        [Description("The date and time at which the event has been created")]
        public DateTime SentAt { get; set; }

    }

}
