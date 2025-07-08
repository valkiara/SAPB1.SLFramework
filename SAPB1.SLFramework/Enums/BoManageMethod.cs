using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoManageMethod
    {
        bomm_OnEveryTransaction = 0,
        bomm_OnReleaseOnly = 1
    }
}