using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SAPB1.SLFramework.Enums
{
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BoUTBTableType
    {
        bott_Document = 0,
        bott_DocumentLines = 1,
        bott_MasterData = 2,
        bott_MasterDataLines = 3,
        bott_NoObject = 4,
        bott_NoObjectAutoIncrement = 5
    }
}
