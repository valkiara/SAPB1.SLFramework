using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum IssuePrimarilyByEnum
    {
        ipbSerialAndBatchNumbers = 0,   // By serial or batch numbers
        ipbBinLocations = 1             // By bin locations
    }
}
