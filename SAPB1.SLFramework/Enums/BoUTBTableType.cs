using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
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
