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

}
