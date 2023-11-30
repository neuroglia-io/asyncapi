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
