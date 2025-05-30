using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SAPB1.SLFramework.Enums
{
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BoYesNoEnum
    {
        tNO = 0,
        tYES = 1
    }
}
