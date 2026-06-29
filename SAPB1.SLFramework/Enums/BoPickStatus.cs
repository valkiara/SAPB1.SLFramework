using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoPickStatus
    {
        ps_Released = 0,
        ps_Picked = 1,
        ps_PartiallyPicked = 2,
        ps_PartiallyDelivered = 3,
        ps_Closed = 4
    }
}
