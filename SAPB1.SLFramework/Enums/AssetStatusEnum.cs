using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AssetStatusEnum
    {
        New = 0,      // Manually created; not yet capitalized
        Active = 1,   // Capitalized and active
        InActive = 2  // Retired / inactive
    }
}
