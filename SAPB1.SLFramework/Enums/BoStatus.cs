using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoStatus
    {
        bost_Open = 0,
        bost_Close = 1,
        bost_Paid = 2,
        bost_Delivered = 3
    }
}