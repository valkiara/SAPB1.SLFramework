using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ItemClassEnum
    {
        itcService = 1,   // Service class
        itcMaterial = 2   // Material class
    }
}
