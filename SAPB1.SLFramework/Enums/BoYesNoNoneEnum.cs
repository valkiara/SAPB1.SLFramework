using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoYesNoNoneEnum
    {
        boNO = 0,
        boYES = 1,
        boNONE = 2
    }
}
