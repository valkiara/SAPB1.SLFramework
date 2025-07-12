using System.Text.Json.Serialization;

namespace SAPB1.SLFramework.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BoDocumentTypes
    {
        dDocument_Items = 0,
        dDocument_Service = 1
    }
}
