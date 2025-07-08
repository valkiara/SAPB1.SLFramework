using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoBaseDateRateEnum
    {
        bdr_PostingDate = 0,
        bdr_TaxDate = 1
    }
}
