using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoYesNoEnum
    {
        tNO = 0,
        tYES = 1
    }
}
