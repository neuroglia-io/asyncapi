using Neuroglia.AsyncApi;
using System;
using System.ComponentModel;

namespace StreetLightsApi.Server.Messages
{

    [Message(Name = "LightMeasured")]
    public class LightMeasuredEvent
    {

        [Description("The id of the light to measure")]
        public int Id { get; set; }

        [Description("The specified light's lumens measurement")]
        public int Lumens { get; set; }

        [Description("The date and time at which the event has been created")]
        public DateTime SentAt { get; set; }

    }

}
