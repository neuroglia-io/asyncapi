using Neuroglia.AsyncApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace StreetLightsApi.Server.Messages
{

    [Tag("light", "A tag for light-related messages"), Tag("measurement", "A tag for measurement-related messages")]
    [Message(Name = "LightMeasured")]
    public class LightMeasuredEvent
    {

        [Description("The id of the light to measure")]
        public int Id { get; set; }

        [Description("The specified light's lumens measurement")]
        public int Lumens { get; set; }

        [Description("The date and time at which the event has been created")]
        public DateTime SentAt { get; set; }

        [Description("The event's metadata")]
        public Dictionary<string, string> Metadata { get; set; }

    }

}
