using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SAPB1.SLFramework.Enums
{
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BoFieldTypes
    {
        db_Alpha = 0,
        db_Memo = 1,
        db_Numeric = 2,
        db_Date = 3,
        db_Float = 4
    }
}
